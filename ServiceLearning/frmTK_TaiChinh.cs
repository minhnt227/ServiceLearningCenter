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
    public partial class frmTK_TaiChinh : Form
    {
        Context db = new Context();

        public frmTK_TaiChinh()
        {
            InitializeComponent();
        }
        public void Display()
        {
            var lst = from s in db.TAI_CHINH
                      join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                      where s.Hide == false && b.Hide == false
                      group s by s.MaHD into g
                      let maxID = g.Max(x => x.ID_TaiChinh)
                      from s in g
                      where s.ID_TaiChinh == maxID
                      join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                      select new
                      {
                          TenHoatDong = b.TenHoatDong,
                          Loai = b.Loai,
                          NgayBatDau = b.NgayBatDau,
                          TongChiPhi = s.UEF + s.TaiTro,
                          UEF = s.UEF,
                          Taitro = s.TaiTro,
                          Khac = s.Khac
                      };
            dgvTC.DataSource = lst.ToList();
            FormatGridView();
        }
        public void FormatGridView()
        {
            dgvTC.Columns["TenHoatDong"].HeaderText = "Tên Hoạt Động";
            dgvTC.Columns["Loai"].HeaderText = "Loại";
            dgvTC.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd-MM-yyyy";
            dgvTC.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
            dgvTC.Columns["TongChiPhi"].HeaderText = "Tổng chi phí";
            dgvTC.Columns["UEF"].HeaderText = "UEF";
            dgvTC.Columns["Taitro"].HeaderText = "Tài trợ";
            dgvTC.Columns["Khac"].HeaderText = "Khác";
        }
        public void LoadLoai()
        {
            cmbLoai.Items.Add("Dự án");
            cmbLoai.Items.Add("Sự kiện");
            cmbLoai.Items.Add("Môn học");
        }
        private void frmTK_TaiChinh_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLoai();
                Display();
                btnLoc.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_Export(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvTC, sfd.FileName);
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

       

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            cmbLoai.SelectedIndex = -1;
            dtpBD.CustomFormat = " ";
            Display();
            btnLoc.Enabled = false;
        }

        private void cmbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cmbLoai.SelectedIndex != -1)   
            {
                string loai = cmbLoai.SelectedItem.ToString();
                if (dtpBD.Text == " " && dtpKT.Text == " ")
                {
                    var lst = from s in db.TAI_CHINH
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.Hide == false && b.Hide == false
                              group s by s.MaHD into g
                              let maxID = g.Max(x => x.ID_TaiChinh)
                              from s in g
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.ID_TaiChinh == maxID && b.Loai == loai
                              select new
                              {
                                  TenHoatDong = b.TenHoatDong,
                                  Loai = b.Loai,
                                  NgayBatDau = b.NgayBatDau,
                                  TongChiPhi = s.UEF + s.TaiTro,
                                  UEF = s.UEF,
                                  Taitro = s.TaiTro,
                                  Khac = s.Khac
                              };
                    dgvTC.DataSource = lst.ToList();
                }
                else if (dtpBD.Text != " " && dtpKT.Text == " ")
                {
                    DateTime BD = Convert.ToDateTime(dtpBD.Text);
                    var lst = from s in db.TAI_CHINH
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.Hide == false && b.Hide == false
                              group s by s.MaHD into g
                              let maxID = g.Max(x => x.ID_TaiChinh)
                              from s in g
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.ID_TaiChinh == maxID && b.NgayBatDau >= BD && b.Loai == loai
                              select new
                              {
                                  TenHoatDong = b.TenHoatDong,
                                  Loai = b.Loai,
                                  NgayBatDau = b.NgayBatDau,
                                  TongChiPhi = s.UEF + s.TaiTro,
                                  UEF = s.UEF,
                                  Taitro = s.TaiTro,
                                  Khac = s.Khac
                              };
                    dgvTC.DataSource = lst.ToList();
                }
                else if (dtpBD.Text == " " && dtpKT.Text != " ")
                {
                    DateTime KT = Convert.ToDateTime(dtpKT.Text);
                    var lst = from s in db.TAI_CHINH
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.Hide == false && b.Hide == false
                              group s by s.MaHD into g
                              let maxID = g.Max(x => x.ID_TaiChinh)
                              from s in g
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.ID_TaiChinh == maxID && b.NgayKetThuc <= KT && b.Loai == loai
                              select new
                              {
                                  TenHoatDong = b.TenHoatDong,
                                  Loai = b.Loai,
                                  NgayBatDau = b.NgayBatDau,
                                  TongChiPhi = s.UEF + s.TaiTro,
                                  UEF = s.UEF,
                                  Taitro = s.TaiTro,
                                  Khac = s.Khac
                              };
                    dgvTC.DataSource = lst.ToList();
                }
                else if (dtpBD.Text != " " && dtpKT.Text != " ")
                {
                    DateTime BD = Convert.ToDateTime(dtpBD.Text);
                    DateTime KT = Convert.ToDateTime(dtpKT.Text);
                    var lst = from s in db.TAI_CHINH
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.Hide == false && b.Hide == false
                              group s by s.MaHD into g
                              let maxID = g.Max(x => x.ID_TaiChinh)
                              from s in g
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.ID_TaiChinh == maxID && b.NgayBatDau >= BD && b.NgayKetThuc <= KT && b.Loai == loai
                              select new
                              {
                                  TenHoatDong = b.TenHoatDong,
                                  Loai = b.Loai,
                                  NgayBatDau = b.NgayBatDau,
                                  TongChiPhi = s.UEF + s.TaiTro,
                                  UEF = s.UEF,
                                  Taitro = s.TaiTro,
                                  Khac = s.Khac
                              };
                    dgvTC.DataSource = lst.ToList();
                }
            }
            else if (cmbLoai.SelectedIndex == -1 )
            {
                if (dtpBD.Text == " " && dtpKT.Text == " ")
                {
                    var lst = from s in db.TAI_CHINH
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.Hide == false && b.Hide == false
                              group s by s.MaHD into g
                              let maxID = g.Max(x => x.ID_TaiChinh)
                              from s in g
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.ID_TaiChinh == maxID 
                              select new
                              {
                                  TenHoatDong = b.TenHoatDong,
                                  Loai = b.Loai,
                                  NgayBatDau = b.NgayBatDau,
                                  TongChiPhi = s.UEF + s.TaiTro,
                                  UEF = s.UEF,
                                  Taitro = s.TaiTro,
                                  Khac = s.Khac
                              };
                    dgvTC.DataSource = lst.ToList();
                }
                else if (dtpBD.Text != " " && dtpKT.Text == " ")
                {
                    DateTime BD = Convert.ToDateTime(dtpBD.Text);
                    var lst = from s in db.TAI_CHINH
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.Hide == false && b.Hide == false
                              group s by s.MaHD into g
                              let maxID = g.Max(x => x.ID_TaiChinh)
                              from s in g
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.ID_TaiChinh == maxID && b.NgayBatDau >= BD 
                              select new
                              {
                                  TenHoatDong = b.TenHoatDong,
                                  Loai = b.Loai,
                                  NgayBatDau = b.NgayBatDau,
                                  TongChiPhi = s.UEF + s.TaiTro,
                                  UEF = s.UEF,
                                  Taitro = s.TaiTro,
                                  Khac = s.Khac
                              };
                    dgvTC.DataSource = lst.ToList();
                }
                else if (dtpBD.Text == " " && dtpKT.Text != " ")
                {
                    DateTime KT = Convert.ToDateTime(dtpKT.Text);
                    var lst = from s in db.TAI_CHINH
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.Hide == false && b.Hide == false
                              group s by s.MaHD into g
                              let maxID = g.Max(x => x.ID_TaiChinh)
                              from s in g
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.ID_TaiChinh == maxID && b.NgayKetThuc <= KT 
                              select new
                              {
                                  TenHoatDong = b.TenHoatDong,
                                  Loai = b.Loai,
                                  NgayBatDau = b.NgayBatDau,
                                  TongChiPhi = s.UEF + s.TaiTro,
                                  UEF = s.UEF,
                                  Taitro = s.TaiTro,
                                  Khac = s.Khac
                              };
                    dgvTC.DataSource = lst.ToList();
                }
                else if (dtpBD.Text != " " && dtpKT.Text != " ")
                {
                    DateTime BD = Convert.ToDateTime(dtpBD.Text);
                    DateTime KT = Convert.ToDateTime(dtpKT.Text);
                    var lst = from s in db.TAI_CHINH
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.Hide == false && b.Hide == false
                              group s by s.MaHD into g
                              let maxID = g.Max(x => x.ID_TaiChinh)
                              from s in g
                              join b in db.HOAT_DONG on s.MaHD equals b.MaHD
                              where s.ID_TaiChinh == maxID && b.NgayBatDau >= BD && b.NgayKetThuc <= KT 
                              select new
                              {
                                  TenHoatDong = b.TenHoatDong,
                                  Loai = b.Loai,
                                  NgayBatDau = b.NgayBatDau,
                                  TongChiPhi = s.UEF + s.TaiTro,
                                  UEF = s.UEF,
                                  Taitro = s.TaiTro,
                                  Khac = s.Khac
                              };
                    dgvTC.DataSource = lst.ToList();
                }
            }   
            
            FormatGridView();
        }

        private void dtpNgayKT_ValueChanged(object sender, EventArgs e)
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
    }
}
