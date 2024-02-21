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

        private void frmTK_GiangVien_Load(object sender, EventArgs e)
        {
            try
            {
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
                    dgvTK_GVv.Rows.Add();
                    dgvTK_GVv.Rows[j].Cells[0].Value = j + 1;
                    dgvTK_GVv.Rows[j].Cells[1].Value = MaGV;
                    dgvTK_GVv.Rows[j].Cells[2].Value = HoTenLotGV;
                    dgvTK_GVv.Rows[j].Cells[3].Value = tenGV;
                    List<string> donvi = (from s in db.GIANG_VIEN
                                          where s.MaGV == MaGV
                                          select (s.KHOA1.TenKhoa)).ToList();
                    dgvTK_GVv.Rows[j].Cells[4].Value = donvi[0];
                    List<int> lstMaHD = new List<int>();
                    lstMaHD = (from s in db.GIANG_VIEN
                               join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                               where s.MaGV == MaGV
                               select (b.MaHD)).ToList();
                    if (lstMaHD.Count == 0) dgvTK_GVv.Rows[j].Cells[5].Value = "";
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
                        dgvTK_GVv.Rows[j].Cells[5].Value = TenHD;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
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
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("GiangVienData");

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvTK_GVv.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgvTK_GVv.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvTK_GVv.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvTK_GVv.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvTK_GVv.Rows[i - 1].Cells[j - 1].Value;
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
