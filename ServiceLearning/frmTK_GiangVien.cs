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
    public partial class frmTK_GiangVien : Form
    {
        Context db = new Context();
        public frmTK_GiangVien()
        {
            InitializeComponent();
        }
        public void DisplayCMBKhoa(ComboBox a)
        {
            var kh = db.KHOAs.Where(s=>s.Hide == false).Select(s => s);
            a.DataSource = kh.ToList();
            a.ValueMember = "MaKhoa";
            a.DisplayMember = "TenKhoa";
            a.SelectedIndex = -1;

        }
        private void frmTK_GiangVien_Load(object sender, EventArgs e)
        {   
            cmbKhoa.SelectedIndex = -1;
            DisplayCMBKhoa(cmbKhoa);    
            DisplayCMBKhoa(cmbKhoa);
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            ThongKeGiangVien();
            Xoa();
            btnLoc.Enabled = false;
        }
        private void ThongKeGiangVien()
        {
            try
            {
                dgvGV.Rows.Clear();
                dgvGV.Refresh();
                this.dgvGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstMaGV = new List<string>();
                List<string> lstTenGV = new List<string>();
                List<string> lstHoTenLotGV = new List<string>();
                lstMaGV = db.GIANG_VIEN.Where(x=>x.Hide == false).Select(x => x.MaGV).ToList();
                lstTenGV = db.GIANG_VIEN.Where(x => x.Hide == false).Select(x => x.Ten).ToList();
                lstHoTenLotGV = db.GIANG_VIEN.Where(x => x.Hide == false).Select(x => x.HoTenLot).ToList();
                for (int j = 0; j < lstMaGV.Count; j++)
                {
                    string MaGV = lstMaGV[j];
                    string tenGV = lstTenGV[j];
                    string HoTenLotGV = lstHoTenLotGV[j];
                    dgvGV.Rows.Add();
                    dgvGV.Rows[j].Cells[0].Value = j + 1;
                    dgvGV.Rows[j].Cells[1].Value = MaGV;
                    dgvGV.Rows[j].Cells[2].Value = HoTenLotGV;
                    dgvGV.Rows[j].Cells[3].Value = tenGV;
                    List<string> khoa = (from s in db.GIANG_VIEN
                                         where s.MaGV == MaGV && s.Hide == false
                                         select (s.KHOA1.TenKhoa)).ToList();
                    dgvGV.Rows[j].Cells[4].Value = khoa[0];
                    List<int> lstMaHD = new List<int>();
                    lstMaHD = (from s in db.GIANG_VIEN
                               join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                               where s.MaGV == MaGV
                               select (b.MaHD)).ToList();
                    if (lstMaHD.Count == 0) dgvGV.Rows[j].Cells[5].Value = " ";
                    else
                    {
                        string TenHD = "- ";
                        int MaHD = lstMaHD[0];
                        List<string> NameHD = (from s in db.HOAT_DONG
                                               where s.MaHD == MaHD
                                               select (s.TenHoatDong)).ToList();
                        TenHD = TenHD + NameHD[0];
                        for (int i = 1; i < lstMaHD.Count; i++)
                        {
                            MaHD = lstMaHD[i];
                            List<string> TenHoat = (from s in db.HOAT_DONG
                                                    where s.MaHD == MaHD
                                                    select (s.TenHoatDong)).ToList();
                            TenHD = TenHD + "\n- " + TenHoat[0];
                        }
                        dgvGV.Rows[j].Cells[5].Value = TenHD;
                    }
                }
                Xoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void Xoa()
        {
            int n = dgvGV.Rows.Count;
            //MessageBox.Show(n.ToString());
            for (int i = 0; i < n; i++)
            {
                if (dgvGV.Rows[i].Cells[5].Value == " " || dgvGV.Rows[i].Cells[4].Value == " ")
                {
                    //Object stt = dgvSV.Rows[i].Cells[0].Value;
                    dgvGV.Rows.RemoveAt(dgvGV.Rows[i].Index);
                    i--;
                    n--;
                    // dgvSV.Rows[i+1].Cells[0].Value = stt;
                }
            }
            for (int i = 0; i < n - 1; i++)
            {
                dgvGV.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvGV, sfd.FileName);
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

        private void cmbKhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            cmbKhoa.SelectedIndex = -1;
            dgvGV.Rows.Clear();
            dgvGV.Refresh();
            ThongKeGiangVien();
            Xoa();
            btnLoc.Enabled = false;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocGiangVien();
        }
        private void LocGiangVien()
        {
            try
            {
                dgvGV.Rows.Clear();
                dgvGV.Refresh();
                this.dgvGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstMaGV = new List<string>();
                List<string> lstTenGV = new List<string>();
                List<string> lstHoTenLotGV = new List<string>();
                lstMaGV = db.GIANG_VIEN.Select(x => x.MaGV).ToList();
                lstTenGV = db.GIANG_VIEN.Select(x => x.Ten).ToList();
                lstHoTenLotGV = db.GIANG_VIEN.Select(x => x.HoTenLot).ToList();
                for (int j = 0; j < lstMaGV.Count; j++)
                {
                    string MaGV = lstMaGV[j];
                    string tenGV = lstTenGV[j];
                    string HoTenLotGV = lstHoTenLotGV[j];
                    dgvGV.Rows.Add();
                    dgvGV.Rows[j].Cells[0].Value = j + 1;
                    dgvGV.Rows[j].Cells[1].Value = MaGV;
                    dgvGV.Rows[j].Cells[2].Value = HoTenLotGV;
                    dgvGV.Rows[j].Cells[3].Value = tenGV;
                    if (cmbKhoa.SelectedIndex == -1)
                    { 
                        List<string> khoa = (from s in db.GIANG_VIEN
                                             where s.MaGV == MaGV
                                             select (s.KHOA1.TenKhoa)).ToList();
                        dgvGV.Rows[j].Cells[4].Value = khoa[0];
                    }
                    else
                    {   
                        string makhoa = cmbKhoa.SelectedValue.ToString();
                        //MessageBox.Show(makhoa);
                        List<string> khoa = (from s in db.GIANG_VIEN
                                             where s.MaGV == MaGV && s.Khoa == makhoa
                                             select (s.KHOA1.TenKhoa)).ToList();
                        if (khoa.Count == 0) { dgvGV.Rows[j].Cells[4].Value = " "; }
                        else dgvGV.Rows[j].Cells[4].Value = khoa[0];
                    }
                    List<int> lstMaHD = new List<int>();
                    if (dtpBD.Text == " " && dtpKT.Text == " ")
                    {
                        lstMaHD = (from s in db.GIANG_VIEN
                                   join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV 
                                   where s.MaGV == MaGV
                                   select (b.MaHD)).ToList();
                    }
                    else if (dtpBD.Text != " " && dtpKT.Text == " ")
                    {
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        lstMaHD = (from s in db.GIANG_VIEN
                                   join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                                   join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                   where s.MaGV == MaGV && d.NgayBatDau >= BD
                                   select (b.MaHD)).ToList();
                    }
                    else if (dtpBD.Text == " " && dtpKT.Text != " ")
                    {
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        lstMaHD = (from s in db.GIANG_VIEN
                                   join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                                   join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                   where s.MaGV == MaGV && d.NgayKetThuc <=KT
                                   select (b.MaHD)).ToList();

                    }
                    else if (dtpBD.Text != " " && dtpKT.Text != " ")
                    {
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        lstMaHD = (from s in db.GIANG_VIEN
                                   join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                                   join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                   where s.MaGV == MaGV && d.NgayBatDau >=BD && d.NgayKetThuc<= KT
                                   select (b.MaHD)).ToList();
                    }
                    if (lstMaHD.Count == 0) dgvGV.Rows[j].Cells[5].Value = " ";
                    else
                    {
                        string TenHD = "- ";
                        int MaHD = lstMaHD[0];
                        List<string> NameHD = (from s in db.HOAT_DONG
                                               where s.MaHD == MaHD
                                               select (s.TenHoatDong)).ToList();
                        TenHD = TenHD + NameHD[0];
                        for (int i = 1; i < lstMaHD.Count; i++)
                        {
                            MaHD = lstMaHD[i];
                            List<string> TenHoat = (from s in db.HOAT_DONG
                                                    where s.MaHD == MaHD
                                                    select (s.TenHoatDong)).ToList();
                            TenHD = TenHD + "\n- " + TenHoat[0];
                        }
                        dgvGV.Rows[j].Cells[5].Value = TenHD;
                    }
                }
               Xoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
