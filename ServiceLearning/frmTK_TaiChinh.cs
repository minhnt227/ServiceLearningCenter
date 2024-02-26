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

        private void frmTK_TaiChinh_Load(object sender, EventArgs e)
        {
            try
            {
                List<int> lstID = new List<int>();
                lstID = db.TAI_CHINH.Select(x => x.ID_TaiChinh).ToList();
                for (int j = 0; j < lstID.Count; j++)
                {
                    dgvTC.Rows.Add();
                    dgvTC.Rows[j].Cells[0].Value = j + 1;
                    int ID = lstID[j];
                    List<int?> MaHD = (from s in db.TAI_CHINH
                                         where s.ID_TaiChinh == ID
                                         select (s.MaHD)).ToList();
                    int MHD = (int)MaHD[0];
                    List<string> loai = (from s in db.HOAT_DONG
                                         where s.MaHD == MHD
                                         select (s.Loai)).ToList();
                    dgvTC.Rows[j].Cells[1].Value = loai[0];

                    List<string> TenHD = (from s in db.HOAT_DONG
                                          where s.MaHD == MHD
                                          select (s.TenHoatDong)).ToList();
                    dgvTC.Rows[j].Cells[2].Value = TenHD[0];


                    List<DateTime?> thoigian = (from s in db.HOAT_DONG
                                                where s.MaHD == MHD
                                                select (s.NgayBatDau)).ToList();
                    dgvTC.Rows[j].Cells[3].Value = thoigian[0];

                    List<decimal?> UEF = (from s in db.TAI_CHINH
                                          where s.ID_TaiChinh == ID
                                          select (s.UEF)).ToList();
                    dgvTC.Rows[j].Cells[5].Value = UEF[0];

                    List<decimal?> taitro = (from s in db.TAI_CHINH
                                             where s.ID_TaiChinh == ID
                                             select (s.TaiTro)).ToList();
                    dgvTC.Rows[j].Cells[6].Value = taitro[0];
                    int tong = (int)UEF[0] + (int)taitro[0];
                    dgvTC.Rows[j].Cells[4].Value = tong;
                    List<string> khac = (from s in db.TAI_CHINH
                                         where s.ID_TaiChinh == ID
                                         select (s.Khac)).ToList();
                    dgvTC.Rows[j].Cells[7].Value = khac[0];


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ThongKeTaiChinh()
        {
            List<int> MaHD = new List<int>();
            MaHD =  (from s in db.TAI_CHINH
                     where s.ID_TaiChinh == ID
                     select (s.Khac)).ToList();
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

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            dgvTC.Rows.Clear();
            dgvTC.Refresh();
            //ThongKeTaiTro();
            //Xoa();
            btnLoc.Enabled = false;
        }
    }
}
