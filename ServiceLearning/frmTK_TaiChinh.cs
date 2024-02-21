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
                    dgvTK_TC.Rows.Add();
                    dgvTK_TC.Rows[j].Cells[0].Value = j + 1;
                    int ID = lstID[j];
                    List<int?> MaHD = (from s in db.TAI_CHINH
                                         where s.ID_TaiChinh == ID
                                         select (s.MaHD)).ToList();
                    int MHD = (int)MaHD[0];
                    List<string> loai = (from s in db.HOAT_DONG
                                         where s.MaHD == MHD
                                         select (s.Loai)).ToList();
                    dgvTK_TC.Rows[j].Cells[1].Value = loai[0];

                    List<string> TenHD = (from s in db.HOAT_DONG
                                          where s.MaHD == MHD
                                          select (s.TenHoatDong)).ToList();
                    dgvTK_TC.Rows[j].Cells[2].Value = TenHD[0];


                    List<DateTime?> thoigian = (from s in db.HOAT_DONG
                                                where s.MaHD == MHD
                                                select (s.NgayBatDau)).ToList();
                    dgvTK_TC.Rows[j].Cells[3].Value = thoigian[0];

                    List<decimal?> UEF = (from s in db.TAI_CHINH
                                          where s.ID_TaiChinh == ID
                                          select (s.UEF)).ToList();
                    dgvTK_TC.Rows[j].Cells[5].Value = UEF[0];

                    List<decimal?> taitro = (from s in db.TAI_CHINH
                                             where s.ID_TaiChinh == ID
                                             select (s.TaiTro)).ToList();
                    dgvTK_TC.Rows[j].Cells[6].Value = taitro[0];
                    int tong = (int)UEF[0] + (int)taitro[0];
                    dgvTK_TC.Rows[j].Cells[4].Value = tong;
                    List<string> khac = (from s in db.TAI_CHINH
                                         where s.ID_TaiChinh == ID
                                         select (s.Khac)).ToList();
                    dgvTK_TC.Rows[j].Cells[7].Value = khac[0];


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Export(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);

                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("TaiChinhData");

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvTK_TC.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgvTK_TC.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvTK_TC.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvTK_TC.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvTK_TC.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }

                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
