using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceLearning
{
    public partial class frm_DoiTac_Details : Form
    {
        Int32 id;
        Context db = new Context();
        DOI_TAC DOI_TAC = new DOI_TAC();
        public frm_DoiTac_Details()
        {
            InitializeComponent();
            Load();
            AddBinding();
        }

        private void AddBinding()
        {
            txtName.DataBindings.Add(   new Binding("Text", guna2DataGridView1.DataSource, "a"));
            txtRep.DataBindings.Add(    new Binding("Text", guna2DataGridView1.DataSource, "b"));
            txtPhone.DataBindings.Add(  new Binding("Text", guna2DataGridView1.DataSource, "d"));
            txtMail.DataBindings.Add(   new Binding("Text", guna2DataGridView1.DataSource, "c"));
            
        }
        private new void Load()
        {
            var result = from dt in db.DOI_TAC
                             //join
                             where dt.Hide == false
                         select new { a = dt.TenDoiTac, b = dt.DaiDien, c = dt.Email, d = dt.SDT, e = dt.ID_DoiTac };

            guna2DataGridView1.DataSource = result.ToList();

            // Sửa lại tiêu đề của các cột
            guna2DataGridView1.Columns["a"].HeaderText = "Tên Đối Tác";
            guna2DataGridView1.Columns["b"].HeaderText = "Người Đại Diện";
            guna2DataGridView1.Columns["c"].HeaderText = "Địa chỉ Email";
            guna2DataGridView1.Columns["d"].HeaderText = "Số điện thoại";
            guna2DataGridView1.Columns["e"].Visible = false;
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DOI_TAC dt = new DOI_TAC();//txtName.Text.Trim(),txtRep.Text.Trim(),txtPhone.Text.Trim(),txtMail.Text.Trim());
            db.DOI_TAC.Add(dt);
            db.SaveChanges();
            Load();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["e"].Value);
            DOI_TAC = db.DOI_TAC.Find(id);
            DOI_TAC.TenDoiTac = txtName.Text.Trim();
            DOI_TAC.DaiDien = txtRep.Text.Trim();
            DOI_TAC.SDT = txtPhone.Text.Trim();
            DOI_TAC.Email = txtMail.Text.Trim();
            db.Entry(DOI_TAC).State = EntityState.Modified;
            db.SaveChanges();
            Load();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa Đối tác {guna2DataGridView1.SelectedRows[0].Cells["a"].Value.ToString()} không?", "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["e"].Value);
                    DOI_TAC = db.DOI_TAC.Find(id);
                    DOI_TAC.Hide = true;
                    db.Entry(DOI_TAC).State = EntityState.Modified;
                    db.SaveChanges();
                    Load();
                }
                else
                    return;
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Find(object sender, EventArgs e)
        {
            FindDoiTac();
        }

        private void FindDoiTac()
        {
            // Lấy từ khóa tìm kiếm từ TextBox
            string searchKeyword = txtName.Text.Trim();

            // Nếu từ khóa tìm kiếm không rỗng
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                // Thực hiện tìm kiếm theo Tên Đối Tác hoặc Người Đại Diện
                var result = from dt in db.DOI_TAC
                             where dt.TenDoiTac.Contains(searchKeyword) || dt.DaiDien.Contains(searchKeyword)
                             select new { a = dt.TenDoiTac, b = dt.DaiDien, c = dt.Email, d = dt.SDT, e = dt.ID_DoiTac };

                // Cập nhật DataGridView với kết quả tìm kiếm
                guna2DataGridView1.DataSource = result.ToList();
            }
            else
            {
                // Nếu từ khóa tìm kiếm rỗng, load lại toàn bộ dữ liệu
                Load();
            }
        }

        private void btn_Import(object sender, EventArgs e)
        {
            // Gọi hàm import
            ImportDoiTac();
        }

      

        private void btn_Export(object sender, EventArgs e)
        {
            // Gọi hàm export
            ExportDoiTac();
        }

        private void ExportDoiTac()
        {
            try
            {
                // Tạo một đối tượng ExcelPackage
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    // Tạo một Sheet có tên là "DoiTac"
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("DoiTac");

                    // Ghi dữ liệu từ DataGridView vào Excel
                    for (int i = 1; i <= guna2DataGridView1.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i].Value = guna2DataGridView1.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Style.Font.Bold = true;
                    }

                    for (int i = 1; i <= guna2DataGridView1.Rows.Count; i++)
                    {
                        for (int j = 1; j <= guna2DataGridView1.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 1, j].Value = guna2DataGridView1.Rows[i - 1].Cells[j - 1].Value;
                        }
                    }
                    worksheet.Cells.AutoFitColumns();
                    // Lưu file Excel
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        saveFileDialog.FilterIndex = 2;
                        saveFileDialog.RestoreDirectory = true;

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
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

        private void ImportDoiTac()
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(openFileDialog.FileName);

                        using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                            if (worksheet != null)
                            {
                                // Xóa dữ liệu hiện tại trong DataGridView
                                guna2DataGridView1.DataSource = null;

                                // Đọc dữ liệu từ Excel và gán cho DataGridView
                                var data = worksheet.Cells["A1:" + worksheet.Dimension.End.Address].Value;
                                guna2DataGridView1.DataSource = data;

                                MessageBox.Show("Import thành công!");
                            }
                            else
                            {
                                MessageBox.Show("File Excel không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình import. Chi tiết lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

                // Lấy giá trị từ cột tương ứng trong dòng được chọn
                string TenDT = selectedRow.Cells["a"].Value.ToString();
                string Daidien = selectedRow.Cells["b"].Value.ToString();
                string email = selectedRow.Cells["c"].Value.ToString();
                string sdt = selectedRow.Cells["d"].Value.ToString();

                // Hiển thị thông tin lên các TextBox
                txtName.Text = TenDT;
                txtRep.Text = Daidien;
                txtMail.Text = email;
                txtPhone.Text = sdt;
            }
        }
    }
}
