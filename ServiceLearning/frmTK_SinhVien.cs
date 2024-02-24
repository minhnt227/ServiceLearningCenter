using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceLearning
{
    public partial class frmTK_SinhVien : Form
    {
        Context db = new Context();
        public frmTK_SinhVien()
        {
            InitializeComponent();
        }
        private void frmTK_SinhVien_Load(object sender, EventArgs e)
        {
            LoadLoai();
            cmbKhoa.SelectedIndex = -1;
            DisplayCMBKhoa(cmbKhoa);
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            ThongkeSinhVien();
            Xoa();
            btnLoc.Enabled = false;
        }
        public void DisplayCMBKhoa(ComboBox a)
        {
            var kh = db.KHOAs.Select(s => s);
            a.DataSource = kh.ToList();
            a.ValueMember = "MaKhoa";
            a.DisplayMember = "TenKhoa";
            a.SelectedIndex = -1;

        }
        public void LoadLoai()
        {
            cmbLoai.Items.Add("Dự án");
            cmbLoai.Items.Add("Sự kiện");
            cmbLoai.Items.Add("Môn học");
        }
        private void ThongkeSinhVien()
        {
            dgvSV.Rows.Clear();
            dgvSV.Refresh();
            this.dgvSV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            List<string> lstMaSV = new List<string>();
            List<string> lstHoTenSV = new List<string>();
            lstMaSV = db.SINH_VIEN.Where(x => x.Hide == false).Select(x => x.MSSV).ToList();
            lstHoTenSV = db.SINH_VIEN.Where(x => x.Hide == false).Select(x => x.HoTen).ToList();
            for (int j = 0; j < lstMaSV.Count; j++)
            {
                string MaSV = lstMaSV[j];
                string HoTenSV = lstHoTenSV[j];
                dgvSV.Rows.Add();
                dgvSV.Rows[j].Cells[0].Value = j + 1;
                dgvSV.Rows[j].Cells[1].Value = MaSV;
                dgvSV.Rows[j].Cells[2].Value = HoTenSV;
                List<string> Khoa = (from s in db.SINH_VIEN
                                     join b in db.KHOAs on s.Khoa equals b.MaKhoa
                                     where s.MSSV == MaSV && b.Hide == false
                                     select (b.TenKhoa)).ToList();
                dgvSV.Rows[j].Cells[3].Value = Khoa[0];
                List<int> lstMaHD = new List<int>();
                lstMaHD = (from s in db.SINH_VIEN
                           join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                           join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                           where s.MSSV == MaSV && c.Hide == false
                           select (c.MaHD)).ToList();
                if (lstMaHD.Count == 0)
                {
                    dgvSV.Rows[j].Cells[4].Value = " ";
                    dgvSV.Rows[j].Cells[5].Value = " ";
                }
                else
                {
                    string vaitro = "- ";
                    string TenHD = "- ";
                    int MaHD = lstMaHD[0];
                    List<string> lstvaitro = (from s in db.HOAT_DONG
                                              join b in db.HD_SINHVIEN on s.MaHD equals b.MaHD
                                              where s.MaHD == MaHD && b.MSSV == MaSV && s.Hide == false
                                              select (b.VaiTro)).ToList();
                    vaitro = vaitro + lstvaitro[0];
                    List<string> NameHD = (from s in db.HOAT_DONG
                                           where s.MaHD == MaHD && s.Hide == false
                                           select (s.TenHoatDong)).ToList();
                    TenHD = TenHD + NameHD[0];
                    for (int i = 1; i < lstMaHD.Count; i++)
                    {
                        MaHD = lstMaHD[i];
                        List<string> TenVai = (from s in db.HOAT_DONG
                                               join b in db.HD_SINHVIEN on s.MaHD equals b.MaHD
                                               where s.MaHD == MaHD && b.MSSV == MaSV && s.Hide == false
                                               select (b.VaiTro)).ToList();
                        vaitro = vaitro + "\n- " + TenVai[0];
                        List<string> TenHoat = (from s in db.HOAT_DONG
                                                where s.MaHD == MaHD && s.Hide == false
                                                select (s.TenHoatDong)).ToList();
                        TenHD = TenHD + "\n- " + TenHoat[0];
                    }
                    dgvSV.Rows[j].Cells[4].Value = vaitro;
                    dgvSV.Rows[j].Cells[5].Value = TenHD;
                }
            }
            
        }
        private void LocThongkeSinhVien()
        {
            try
            {
                dgvSV.Rows.Clear();
                dgvSV.Refresh();
                this.dgvSV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstMaSV = new List<string>();
                List<string> lstHoTenSV = new List<string>();
                lstMaSV = db.SINH_VIEN.Where(x => x.Hide == false).Select(x => x.MSSV).ToList();
                lstHoTenSV = db.SINH_VIEN.Where(x => x.Hide == false).Select(x => x.HoTen).ToList();
                for (int j = 0; j < lstMaSV.Count; j++)
                {
                    string MaSV = lstMaSV[j];
                    string HoTenSV = lstHoTenSV[j];
                    dgvSV.Rows.Add();
                    dgvSV.Rows[j].Cells[0].Value = j + 1;
                    dgvSV.Rows[j].Cells[1].Value = MaSV;
                    dgvSV.Rows[j].Cells[2].Value = HoTenSV;
                    if (cmbKhoa.SelectedIndex != -1)
                    {
                        string khoa = cmbKhoa.SelectedValue.ToString();
                        // MessageBox.Show(khoa);
                        List<string> Khoa = (from s in db.SINH_VIEN
                                             join b in db.KHOAs on s.Khoa equals b.MaKhoa
                                             where s.MSSV == MaSV && b.Hide == false && s.Khoa == khoa
                                             select (b.TenKhoa)).ToList();
                        if (Khoa.Count == 0) { dgvSV.Rows[j].Cells[3].Value = " "; }
                        else dgvSV.Rows[j].Cells[3].Value = Khoa[0];
                    }
                    else
                    {
                        List<string> Khoa = (from s in db.SINH_VIEN
                                             join b in db.KHOAs on s.Khoa equals b.MaKhoa
                                             where s.MSSV == MaSV && b.Hide == false
                                             select (b.TenKhoa)).ToList();
                        dgvSV.Rows[j].Cells[3].Value = Khoa[0];
                    }
                    List<int> lstMaHD = new List<int>();
                    if (cmbLoai.SelectedIndex == -1 && dtpBD.Text == " " && dtpKT.Text == " ")
                    {
                        lstMaHD = (from s in db.SINH_VIEN
                                   join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where s.MSSV == MaSV && c.Hide == false
                                   select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex != -1 && dtpBD.Text == " " && dtpKT.Text == " ")
                    {
                        string loai = cmbLoai.SelectedItem.ToString();
                        lstMaHD = (from s in db.SINH_VIEN
                                   join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where s.MSSV == MaSV && c.Hide == false && c.Loai == loai
                                   select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex != -1 && dtpBD.Text != " " && dtpKT.Text == " ")
                    {
                        string loai = cmbLoai.SelectedItem.ToString();
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        lstMaHD = (from s in db.SINH_VIEN
                                   join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where s.MSSV == MaSV && c.Hide == false && c.NgayBatDau >= BD && c.Loai == loai
                                   select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex != -1 && dtpBD.Text == " " && dtpKT.Text != " ")
                    {
                        string loai = cmbLoai.SelectedItem.ToString();
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        lstMaHD = (from s in db.SINH_VIEN
                                   join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where s.MSSV == MaSV && c.Hide == false && c.NgayKetThuc <= KT && c.Loai == loai
                                   select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex == -1 && dtpBD.Text != " " && dtpKT.Text == " ")
                    {
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        lstMaHD = (from s in db.SINH_VIEN
                                   join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where s.MSSV == MaSV && c.Hide == false && c.NgayBatDau >= BD
                                   select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex == -1 && dtpBD.Text == " " && dtpKT.Text != " ")
                    {
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        lstMaHD = (from s in db.SINH_VIEN
                                   join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where s.MSSV == MaSV && c.Hide == false && c.NgayKetThuc <= KT
                                   select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex == -1 && dtpBD.Text != " " && dtpKT.Text != " ")
                    {
                        string loai = cmbLoai.SelectedItem.ToString();
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        // MessageBox.Show(BD.ToString());
                        lstMaHD = (from s in db.SINH_VIEN
                                   join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where s.MSSV == MaSV && c.Hide == false && c.NgayBatDau >= BD && c.NgayKetThuc <= KT && c.Loai == loai
                                   select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex != -1 && dtpBD.Text != " " && dtpKT.Text != " ")
                    {
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        // MessageBox.Show(BD.ToString());
                        lstMaHD = (from s in db.SINH_VIEN
                                   join b in db.HD_SINHVIEN on s.MSSV equals b.MSSV
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where s.MSSV == MaSV && c.Hide == false && c.NgayBatDau >= BD && c.NgayKetThuc <= KT
                                   select (c.MaHD)).ToList();
                    }


                    if (lstMaHD.Count == 0)
                    {
                        dgvSV.Rows[j].Cells[4].Value = " ";
                        dgvSV.Rows[j].Cells[5].Value = " ";
                    }
                    else
                    {
                        string vaitro = "- ";
                        string TenHD = "- ";
                        int MaHD = lstMaHD[0];
                        List<string> lstvaitro = (from s in db.HOAT_DONG
                                                  join b in db.HD_SINHVIEN on s.MaHD equals b.MaHD
                                                  where s.MaHD == MaHD && b.MSSV == MaSV && s.Hide == false
                                                  select (b.VaiTro)).ToList();
                        vaitro = vaitro + lstvaitro[0];
                        List<string> NameHD = (from s in db.HOAT_DONG
                                               where s.MaHD == MaHD && s.Hide == false
                                               select (s.TenHoatDong)).ToList();
                        TenHD = TenHD + NameHD[0];
                        for (int i = 1; i < lstMaHD.Count; i++)
                        {
                            MaHD = lstMaHD[i];
                            List<string> TenVai = (from s in db.HOAT_DONG
                                                   join b in db.HD_SINHVIEN on s.MaHD equals b.MaHD
                                                   where s.MaHD == MaHD && b.MSSV == MaSV && s.Hide == false
                                                   select (b.VaiTro)).ToList();
                            vaitro = vaitro + "\n- " + TenVai[0];
                            List<string> TenHoat = (from s in db.HOAT_DONG
                                                    where s.MaHD == MaHD && s.Hide == false
                                                    select (s.TenHoatDong)).ToList();
                            TenHD = TenHD + "\n- " + TenHoat[0];
                        }
                        dgvSV.Rows[j].Cells[4].Value = vaitro;
                        dgvSV.Rows[j].Cells[5].Value = TenHD;
                    }
                }
                Xoa();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Xoa()
        {
            int n = dgvSV.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                if (dgvSV.Rows[i].Cells[5].Value == " " || dgvSV.Rows[i].Cells[3].Value == " ")
                {
                    //Object stt = dgvSV.Rows[i].Cells[0].Value;
                    dgvSV.Rows.RemoveAt(dgvSV.Rows[i].Index);
                    i--;
                    n--;
                   // dgvSV.Rows[i+1].Cells[0].Value = stt;
                }
            }
            for (int i=0;i<n-1;i++)
            {
                dgvSV.Rows[i].Cells[0].Value = i + 1;
            }    
        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocThongkeSinhVien();
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

        private void cmbKhoa_TextChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void cmbLoai_TextChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            cmbLoai.SelectedIndex = -1;
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            cmbKhoa.SelectedIndex = -1;
            dgvSV.Rows.Clear();
            dgvSV.Refresh();
            ThongkeSinhVien();
            Xoa();
            btnLoc.Enabled = false;
        }

        private void cmbKhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void cmbLoai_SelectedValueChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvSV, sfd.FileName);
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
    }
}
