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

        private void frmTK_Khoa_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> lstTenKhoa = new List<string>();
                lstTenKhoa = db.KHOAs.Select(x => x.TenKhoa).ToList();

                for (int i = 0; i < lstTenKhoa.Count; i++)
                {
                    dgvTK_1.Columns.Add(lstTenKhoa[i], lstTenKhoa[i]);
                }
                dgvTK_1.Columns.Add("Total", "Tổng");
                List<int> lstMaHD = new List<int>();
                List<string> lstTenHD = new List<string>();
                lstMaHD = db.HOAT_DONG.Select(x => x.MaHD).ToList();
                lstTenHD = db.HOAT_DONG.Select(x => x.TenHoatDong).ToList();
                for (int j = 0; j < lstMaHD.Count; j++)
                {

                    int MaHD = lstMaHD[j];
                    string TenHD = lstTenHD[j];
                    dgvTK_1.Rows.Add();
                    dgvTK_1.Rows[j].Cells[0].Value = j + 1;
                    dgvTK_1.Rows[j].Cells[1].Value = TenHD;
                    List<string> lstKhoa = new List<string>();
                    lstKhoa = db.KHOAs.Select(x => x.MaKhoa).ToList();
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
                        dgvTK_1.Rows[j].Cells[i + 2].Value = tong;
                        total = total + tong;
                    }
                    dgvTK_1.Rows[j].Cells[lstKhoa.Count + 2].Value = total;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void btnExport_Click(object sender, EventArgs e)
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
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("KhoaData");

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvTK_1.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgvTK_1.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvTK_1.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvTK_1.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvTK_1.Rows[i - 1].Cells[j - 1].Value;
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
