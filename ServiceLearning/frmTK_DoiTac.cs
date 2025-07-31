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
using System.Xml.Linq;

namespace ServiceLearning
{
    public partial class frmTK_DoiTac : Form
    {
        public frmTK_DoiTac()
        {
            InitializeComponent();
        }

        Context db = new Context();
        private void frmTK_DoiTac_Load(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            ThongKeDoiTac();
            Xoa();
            btnLoc.Enabled = false;
        }
        private void ThongKeDoiTac()
        {
            try
            {
                dgvDT.Rows.Clear();
                dgvDT.Refresh();
                this.dgvDT.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                this.dgvDT.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                List<string> lstTenDoiTac = new List<string>();
                List<int> IDDoiTac = new List<int>();
                lstTenDoiTac = db.DOI_TAC.Where(x => x.Hide == false).Select(x => x.TenDoiTac).Take(200).ToList();
                IDDoiTac = db.DOI_TAC.Where(x => x.Hide == false).Select(x => x.ID_DoiTac).Take(200).ToList();
                for (int j = 0; j < IDDoiTac.Count; j++)
                {
                    string TenDT = lstTenDoiTac[j];
                    int id = IDDoiTac[j];
                    dgvDT.Rows.Add();
                    dgvDT.Rows[j].Cells[0].Value = j + 1;
                    dgvDT.Rows[j].Cells[1].Value = TenDT;
                    List<string> nguoidaidien = (from s in db.DOI_TAC
                                                 where s.ID_DoiTac == id && s.Hide == false
                                                 select (s.DaiDien)).ToList();
                    dgvDT.Rows[j].Cells[2].Value = nguoidaidien[0];
                    List<string> SDT = (from s in db.DOI_TAC
                                        where s.ID_DoiTac == id && s.Hide == false
                                        select (s.SDT)).ToList();
                    dgvDT.Rows[j].Cells[3].Value = SDT[0];
                    List<string> email = (from s in db.DOI_TAC
                                          where s.ID_DoiTac == id && s.Hide == false
                                          select (s.Email)).ToList();
                    dgvDT.Rows[j].Cells[4].Value = email[0];
                    List<int> lstMaHD = new List<int>();
                    lstMaHD = (from s in db.DOI_TAC
                               join b in db.HD_DOITAC on s.ID_DoiTac equals b.ID_DoiTac
                               where b.ID_DoiTac == id && b.HOAT_DONG.Hide == false
                               select (b.MaHD)).ToList();
                    if (lstMaHD.Count == 0) dgvDT.Rows[j].Cells[5].Value = " ";
                    else
                    {
                        string TenHD = "- ";
                        int MaHD = lstMaHD[0];
                        List<string> NameHD = (from s in db.HOAT_DONG
                                               where s.MaHD == MaHD && s.Hide == false
                                               select (s.TenHoatDong)).ToList();
                        TenHD = TenHD + NameHD[0];
                        for (int i = 1; i < lstMaHD.Count; i++)
                        {
                            MaHD = lstMaHD[i];
                            List<string> TenHoat = (from s in db.HOAT_DONG
                                                    where s.MaHD == MaHD && s.Hide == false
                                                    select (s.TenHoatDong)).ToList();
                            TenHD = TenHD + "\n- " + TenHoat[0];
                        }
                        dgvDT.Rows[j].Cells[5].Value = TenHD;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void Xoa()
        {
            int n = dgvDT.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                if (dgvDT.Rows[i].Cells[5].Value == " ")
                {
                    //Object stt = dgvSV.Rows[i].Cells[0].Value;
                    dgvDT.Rows.RemoveAt(dgvDT.Rows[i].Index);
                    i--;
                    n--;
                    // dgvSV.Rows[i+1].Cells[0].Value = stt;
                }
            }
            for (int i = 0; i < n - 1; i++)
            {
                dgvDT.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void btn_Export(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvDT, sfd.FileName);
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
            dgvDT.Rows.Clear();
            dgvDT.Refresh();
            ThongKeDoiTac();
            Xoa();
            btnLoc.Enabled = false;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocDoiTac();
        }
        private void LocDoiTac()
        {
            try
            {
                dgvDT.Rows.Clear();
                dgvDT.Refresh();
                this.dgvDT.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                this.dgvDT.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                //List<string> lstTenDoiTac = new List<string>();
                //List<int> IDDoiTac = new List<int>();

                // 1. Tải tất cả dữ liệu đối tác cần thiết trong một lần truy vấn
                // Giả sử db.DOI_TAC là một DbSet trong DbContext của bạn và nó có các thuộc tính
                // ID_DoiTac, TenDoiTac, DaiDien, SDT, Email, Hide.
                var partners = db.DOI_TAC
                                 .Where(x => x.Hide != true)
                                 .Select(x => new
                                 {
                                     x.ID_DoiTac,
                                     x.TenDoiTac,
                                     x.DaiDien,
                                     x.SDT,
                                     x.Email
                                 });

                string DTName = txtName.Text.Trim();
                if (!string.IsNullOrEmpty(DTName))
                {
                    partners = partners.Where(d => d.TenDoiTac.Contains(DTName));
                }
                // Lấy giá trị ngày tháng từ DateTimePicker
                // Giả định dtpBD và dtpKT là các điều khiển DateTimePicker.
                // Kiểm tra nếu DateTimePicker có giá trị (ví dụ: thông qua thuộc tính Checked hoặc kiểm tra giá trị mặc định)
                // Nếu bạn đang sử dụng ShowCheckBox, hãy kiểm tra dtpBD.Checked và dtpKT.Checked.
                // Nếu không, bạn có thể cần kiểm tra dtpBD.Value != default(DateTime) hoặc một giá trị mặc định cụ thể.
                

                DateTime? startDate = null;
                if (dtpBD.Text != " ") // Thay thế bằng cách kiểm tra phù hợp cho DateTimePicker của bạn
                {
                    if (DateTime.TryParse(dtpBD.Text, out DateTime tempDate))
                    {
                        startDate = tempDate;
                    }
                }

                DateTime? endDate = null;
                if (dtpKT.Text != " ") // Thay thế bằng cách kiểm tra phù hợp cho DateTimePicker của bạn
                {
                    if (DateTime.TryParse(dtpKT.Text, out DateTime tempDate))
                    {
                        endDate = tempDate;
                    }
                }

                int rowIdx = 0; // Chỉ số hàng cho DataGridView
                foreach (var partner in partners)
                {
                    dgvDT.Rows.Add();
                    dgvDT.Rows[rowIdx].Cells[0].Value = rowIdx + 1; // Số thứ tự
                    dgvDT.Rows[rowIdx].Cells[1].Value = partner.TenDoiTac;
                    dgvDT.Rows[rowIdx].Cells[2].Value = partner.DaiDien;
                    dgvDT.Rows[rowIdx].Cells[3].Value = partner.SDT;
                    dgvDT.Rows[rowIdx].Cells[4].Value = partner.Email;
                    // 2. Lọc các hoạt động liên quan đến đối tác này dựa trên ngày tháng
                    // Bắt đầu với tất cả các hoạt động của đối tác không ẩn
                    var queryHoatDong = db.HD_DOITAC
                                          .Where(b => b.ID_DoiTac == partner.ID_DoiTac && b.HOAT_DONG.Hide == false)
                                          .Select(b => b.HOAT_DONG); // Chọn đối tượng HOAT_DONG

                    // Áp dụng bộ lọc ngày tháng động và tên Đối Tác
                    if (startDate.HasValue)
                    {
                        queryHoatDong = queryHoatDong.Where(c => c.NgayBatDau >= startDate.Value);
                    }
                    if (endDate.HasValue)
                    {
                        queryHoatDong = queryHoatDong.Where(c => c.NgayKetThuc <= endDate.Value);
                    }
                    

                    var lstTenDoiTacs = queryHoatDong.Select(c => c.TenHoatDong).ToList();

                    if (lstTenDoiTacs.Count == 0)
                    {
                        dgvDT.Rows[rowIdx].Cells[5].Value = " ";
                    }
                    else
                    {
                        // Sử dụng StringBuilder để nối chuỗi hiệu quả hơn
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        for (int i = 0; i < lstTenDoiTacs.Count; i++)
                        {
                            sb.Append("- ").Append(lstTenDoiTacs[i]);
                            if (i < lstTenDoiTacs.Count - 1)
                            {
                                sb.Append("\n");
                            }
                        }
                        dgvDT.Rows[rowIdx].Cells[5].Value = sb.ToString();
                    }
                    rowIdx++;
                }
                Xoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length > 0)
                btnLoc.Enabled = true;
            else btnLoc.Enabled = false;
        }
    }
}
