using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceLearning
{
    public partial class frmTK_Khoa : Form
    {
        Context db = new Context();
        public frmTK_Khoa()
        {
            InitializeComponent();
        }
        public void LoadLoai()
        {
            cmbLoai.Items.Add("Dự án");
            cmbLoai.Items.Add("Sự kiện");
            cmbLoai.Items.Add("Môn học");
        }
        private void frmTK_Khoa_Load(object sender, EventArgs e)
        {
            LoadLoai();
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            ThongKeKhoa();
            btnLoc.Enabled = false;
        }
        private void ThongKeKhoa()
        {
            this.dgvHD.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            try
            {
                List<string> lstTenKhoa = new List<string>();
                lstTenKhoa = db.KHOAs.Where(x=>x.Hide == false).Select(x => x.TenKhoa).ToList();
                this.dgvHD.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvHD.Columns.Add("STT", "STT");
                dgvHD.Columns.Add("Hoạt động", "Hoạt động");
                for (int i = 0; i < lstTenKhoa.Count; i++)
                {
                    dgvHD.Columns.Add(lstTenKhoa[i], lstTenKhoa[i]);
                }
                dgvHD.Columns.Add("Total", "Tổng");
                List<int> lstMaHD = new List<int>();
                List<string> lstTenHD = new List<string>();
                lstMaHD = (from s in db.HOAT_DONG
                           where s.Hide == false 
                           select (s.MaHD)).ToList();
                lstTenHD = (from s in db.HOAT_DONG
                            where s.Hide == false
                            select (s.TenHoatDong)).ToList();              
                for (int j = 0; j < lstMaHD.Count; j++)
                {

                    int MaHD = lstMaHD[j];
                    string TenHD = lstTenHD[j];
                    dgvHD.Rows.Add();
                    dgvHD.Rows[j].Cells[0].Value = j + 1;
                    dgvHD.Rows[j].Cells[1].Value = TenHD;
                    List<string> lstKhoa = new List<string>();
                    lstKhoa = db.KHOAs.Where(x => x.Hide == false).Select(x => x.MaKhoa).ToList();
                    int total = 0;
                    for (int i = 0; i < lstKhoa.Count; i++)
                    {
                        List<string> list = new List<string>();
                        string maKhoa = lstKhoa[i];
                        list = (from s in db.SINH_VIEN
                                join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                where (s.Khoa == maKhoa && b.MaHD == MaHD)
                                select (s.MSSV)).ToList();
                        int tong = (from gv in db.SINH_VIEN
                                    where list.Contains(gv.MSSV)
                                    select gv.MSSV).ToList().Count;
                        dgvHD.Rows[j].Cells[i + 2].Value = tong;
                        total = total + tong;
                    }
                    dgvHD.Rows[j].Cells[lstKhoa.Count + 2].Value = total;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvHD, sfd.FileName);
                }
        }
        private void ToExcel(DataGridView dtg, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                //đặt tên cho sheet
                worksheet.Name = "Thống kê sinh viên";

                // export header trong DataGridView
                for (int i = 0; i < dtg.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dtg.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dtg.RowCount; i++)
                {
                    for (int j = 0; j < dtg.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dtg.Rows[i].Cells[j].Value;
                    }
                }
                excel.Columns.AutoFit();
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
        private void cmbLoai_SelectedValueChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void dtpBD_ValueChanged(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = "yyyy-MM-dd";
            btnLoc.Enabled = true;
        }

        private void dtpBD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dtpBD.CustomFormat = " ";
            }
        }

        private void dtpKT_ValueChanged(object sender, EventArgs e)
        {
            dtpKT.CustomFormat = "yyyy-MM-dd";
            btnLoc.Enabled = true;
        }

        private void dtpKT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dtpKT.CustomFormat = " ";
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocHoatDong();
        }
        private void LocHoatDong()
        {
            dgvHD.Columns.Clear(); 
            dgvHD.Refresh();
            this.dgvHD.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            List<string> lstTenKhoa = new List<string>();
            lstTenKhoa = db.KHOAs.Where(x=>x.Hide == false).Select(x => x.TenKhoa).ToList();

            dgvHD.Columns.Add("STT", "STT");
            dgvHD.Columns.Add("Hoạt động", "Hoạt động");
            for (int i = 0; i < lstTenKhoa.Count; i++)
            {
                dgvHD.Columns.Add(lstTenKhoa[i], lstTenKhoa[i]);
            }
            dgvHD.Columns.Add("Total", "Tổng");
            List<int> lstMaHD = new List<int>();
            List<string> lstTenHD = new List<string>();
            if (cmbLoai.SelectedIndex != -1)
            {
                string loai = cmbLoai.SelectedItem.ToString();
                if (dtpBD.Text == " " && dtpKT.Text == " ")
                {
                    lstMaHD = (from s in db.HOAT_DONG
                               where s.Hide == false && s.Loai == loai
                               select (s.MaHD)).ToList();
                    lstTenHD = (from s in db.HOAT_DONG
                                where s.Hide == false && s.Loai == loai
                                select (s.TenHoatDong)).ToList();
                }
                else if (dtpBD.Text != " " && dtpKT.Text == " ")
                {
                    DateTime BD = Convert.ToDateTime(dtpBD.Text);
                    lstMaHD = (from s in db.HOAT_DONG
                               where s.Hide == false && s.Loai == loai && s.NgayBatDau >= BD
                               select (s.MaHD)).ToList();
                    lstTenHD = (from s in db.HOAT_DONG
                                where s.Hide == false && s.Loai == loai & s.NgayBatDau >= BD
                                select (s.TenHoatDong)).ToList();
                }
                else if (dtpBD.Text == " " && dtpKT.Text != " ")
                {
                    DateTime KT = Convert.ToDateTime(dtpKT.Text);
                    lstMaHD = (from s in db.HOAT_DONG
                               where s.Hide == false && s.Loai == loai && s.NgayKetThuc <=KT
                               select (s.MaHD)).ToList();
                    lstTenHD = (from s in db.HOAT_DONG
                                where s.Hide == false && s.Loai == loai && s.NgayKetThuc <= KT
                                select (s.TenHoatDong)).ToList();
                }
                else if(dtpBD.Text != " " && dtpKT.Text != " ")
                {
                    DateTime BD = Convert.ToDateTime(dtpBD.Text);
                    DateTime KT = Convert.ToDateTime(dtpKT.Text);
                    lstMaHD = (from s in db.HOAT_DONG
                               where s.Hide == false && s.Loai == loai && s.NgayBatDau >= BD && s.NgayKetThuc <= KT
                               select (s.MaHD)).ToList();
                    lstTenHD = (from s in db.HOAT_DONG
                                where s.Hide == false && s.Loai == loai && s.NgayBatDau >= BD && s.NgayKetThuc <= KT
                                select (s.TenHoatDong)).ToList();
                }
            }
            else
            {

                if (dtpBD.Text == " " && dtpKT.Text == " ")
                {
                    lstMaHD = (from s in db.HOAT_DONG
                               where s.Hide == false 
                               select (s.MaHD)).ToList();
                    lstTenHD = (from s in db.HOAT_DONG
                                where s.Hide == false 
                                select (s.TenHoatDong)).ToList();
                }
                else if (dtpBD.Text != " " && dtpKT.Text == " ")
                {
                    DateTime BD = Convert.ToDateTime(dtpBD.Text);
                    lstMaHD = (from s in db.HOAT_DONG
                               where s.Hide == false  && s.NgayBatDau >= BD
                               select (s.MaHD)).ToList();
                    lstTenHD = (from s in db.HOAT_DONG
                                where s.Hide == false  && s.NgayBatDau >= BD
                                select (s.TenHoatDong)).ToList();
                }
                else if (dtpBD.Text == " " && dtpKT.Text != " ")
                {
                    DateTime KT = Convert.ToDateTime(dtpKT.Text);
                    lstMaHD = (from s in db.HOAT_DONG
                               where s.Hide == false  && s.NgayKetThuc <= KT
                               select (s.MaHD)).ToList();
                    lstTenHD = (from s in db.HOAT_DONG
                                where s.Hide == false && s.NgayKetThuc <= KT
                                select (s.TenHoatDong)).ToList();
                }
                else if (dtpBD.Text != " " && dtpKT.Text != " ")
                {
                    DateTime BD = Convert.ToDateTime(dtpBD.Text);
                    DateTime KT = Convert.ToDateTime(dtpKT.Text);
                    lstMaHD = (from s in db.HOAT_DONG
                               where s.Hide == false  && s.NgayBatDau >= BD && s.NgayKetThuc <= KT
                               select (s.MaHD)).ToList();
                    lstTenHD = (from s in db.HOAT_DONG
                                where s.Hide == false && s.NgayBatDau >= BD && s.NgayKetThuc <= KT
                                select (s.TenHoatDong)).ToList();
                }
            }
            for (int j = 0; j < lstMaHD.Count; j++)
            {

                int MaHD = lstMaHD[j];
                string TenHD = lstTenHD[j];
                dgvHD.Rows.Add();
                dgvHD.Rows[j].Cells[0].Value = j + 1;
                dgvHD.Rows[j].Cells[1].Value = TenHD;
                List<string> lstKhoa = new List<string>();
                lstKhoa = db.KHOAs.Where(x => x.Hide == false).Select(x => x.MaKhoa).ToList();
                int total = 0;
                for (int i = 0; i < lstKhoa.Count; i++)
                {
                    List<string> list = new List<string>();
                    string maKhoa = lstKhoa[i];
                    list = (from s in db.SINH_VIEN
                            join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                            join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                            where (s.Khoa == maKhoa && b.MaHD == MaHD)
                            select (s.MSSV)).ToList();
                    int tong = (from gv in db.SINH_VIEN
                                where list.Contains(gv.MSSV)
                                select gv.MSSV).ToList().Count;
                    dgvHD.Rows[j].Cells[i + 2].Value = tong;
                    total = total + tong;
                }
                dgvHD.Rows[j].Cells[lstKhoa.Count + 2].Value = total;
            }
        }
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            cmbLoai.SelectedIndex = -1;
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            dgvHD.Columns.Clear();
            dgvHD.Refresh();
            ThongKeKhoa();
            btnLoc.Enabled = false;
        }
    }
}
