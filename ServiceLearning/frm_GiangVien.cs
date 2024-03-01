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
    public partial class frm_GiangVien : Form
    {
        public frm_GiangVien()
        {
            InitializeComponent();
        }

        private void gbSinhVien_Enter(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frm_GiangVien_Load(object sender, EventArgs e)
        {      
            // Lấy dữ liệu từ bảng GIANG_VIEN và hiển thị trong dgv_GiangVien
            LoadDataToDGV_GiangVien();
            LoadDataToCbKhoa();
        }
        private void LoadDataToCbKhoa()
        {
            try
            {
                using (Context db = new Context())
                {
                    var khoa = from k in db.KHOAs
                               where (k.Hide == false)
                               select new
                               {
                                   MaKH = k.MaKhoa,
                                   Ten = k.TenKhoa,
                               };
                    cbKhoa.DataSource = khoa.ToList();
                    cbKhoa.DisplayMember = "Ten";
                    cbKhoa.ValueMember = "MaKH";
                    cbKhoa.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Khoa:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddDataToGiangVien(string maGV, string hoTenLot, string ten, string khoa)
        {
            using (Context db = new Context())
            {
                // Kiểm tra xem giảng viên đã tồn tại chưa
                GIANG_VIEN existingGV = db.GIANG_VIEN.FirstOrDefault(gv => gv.MaGV == maGV);

                if (existingGV == null)
                {
                    // Nếu chưa tồn tại, thêm giảng viên mới vào cơ sở dữ liệu
                    GIANG_VIEN newGV = new GIANG_VIEN
                    {
                        MaGV = maGV,
                        HoTenLot = hoTenLot,
                        Ten = ten,
                        Khoa = khoa
                    };

                    db.GIANG_VIEN.Add(newGV);
                    db.SaveChanges();
                }
            }
        }

        private void LoadDataToDGV_GiangVien()
        {
            try
            {
                using (Context db = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng GIANG_VIEN
                    var giangVienData = from gv in db.GIANG_VIEN
                                        where gv.Hide == false
                                        select new
                                        {
                                            MaGV = gv.MaGV,
                                            HoTenLot = gv.HoTenLot,
                                            Ten = gv.Ten,
                                            Khoa = gv.Khoa,
                                            TenKhoa = gv.KHOA1.TenKhoa,
                                        };

                    // Gán dữ liệu cho DataGridView dgv_GiangVien
                    dgv_GiangVien.DataSource = giangVienData.ToList();

                    // Đổi tên tiêu đề của các cột
                    dgv_GiangVien.Columns["MaGV"].HeaderText = "Mã Giảng Viên";
                    dgv_GiangVien.Columns["HoTenLot"].HeaderText = "Họ Tên Lót";
                    dgv_GiangVien.Columns["Ten"].HeaderText = "Tên";
                    dgv_GiangVien.Columns["Khoa"].HeaderText = "Mã Khoa";
                    dgv_GiangVien.Columns["TenKhoa"].HeaderText = "Tên Khoa";
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu Giảng Viên.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgv_GiangVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void reset()
        {
            txtHoTenLot.Text = txtMaGv.Text = txtName.Text = "";
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các điều khiển
            string maGV = txtMaGv.Text.Trim();
            string hoTenLot = txtHoTenLot.Text.Trim();
            string ten = txtName.Text.Trim();
            string khoa = cbKhoa.SelectedValue.ToString().Trim();

            // Kiểm tra xem MaGV có tồn tại không
            using (Context dbContext = new Context())
            {
                GIANG_VIEN existingGV = dbContext.GIANG_VIEN.Find(maGV);

                if (existingGV == null)
                {
                    // Nếu MaGV chưa tồn tại, thêm mới
                    GIANG_VIEN newGV = new GIANG_VIEN
                    {
                        MaGV = maGV,
                        HoTenLot = hoTenLot,
                        Ten = ten,
                        Khoa = khoa
                    };

                    dbContext.GIANG_VIEN.Add(newGV);
                    dbContext.SaveChanges();

                    MessageBox.Show("Thêm giảng viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reset();
                    LoadDataToDGV_GiangVien();
                }
                else
                    MessageBox.Show("Mã giảng viên đã tồn tại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_GiangVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgv_GiangVien.Rows[e.RowIndex];

                // Lấy giá trị từ cột tương ứng trong dòng được chọn
                string maGV = selectedRow.Cells["MaGV"].Value.ToString();
                string hoTenLot = selectedRow.Cells["HoTenLot"].Value.ToString();
                string ten = selectedRow.Cells["Ten"].Value.ToString();
                string khoa = selectedRow.Cells["Khoa"].Value.ToString();

                // Hiển thị thông tin lên các TextBox
                txtMaGv.Text = maGV;
                txtHoTenLot.Text = hoTenLot;
                txtName.Text = ten;
                cbKhoa.SelectedValue = khoa;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các TextBox
            string maGV = txtMaGv.Text.Trim();
            string hoTenLot = txtHoTenLot.Text.Trim();
            string ten = txtName.Text.Trim();
            string khoa = cbKhoa.SelectedValue.ToString().Trim();

            using (Context dbContext = new Context())
            {
                // Kiểm tra xem giáo viên có tồn tại không
                GIANG_VIEN existingGV = dbContext.GIANG_VIEN.Find(maGV);

                if (existingGV != null)
                {
                    // Cập nhật thông tin giáo viên
                    existingGV.HoTenLot = hoTenLot;
                    existingGV.Ten = ten;
                    existingGV.Khoa = khoa;

                    dbContext.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin giáo viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại DataGridView sau khi cập nhật thông tin giáo viên
                    reset();
                    LoadDataToDGV_GiangVien();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy giáo viên có MaGV là " + maGV, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng được chọn trong dgv_GiangVien không
            if (dgv_GiangVien.SelectedRows.Count > 0)
            {
                // Lấy MaGV của giảng viên được chọn
                string maGV = dgv_GiangVien.SelectedRows[0].Cells["MaGV"].Value.ToString();

                // Thực hiện xóa giảng viên trong cơ sở dữ liệu
                using (Context dbContext = new Context())
                {
                    // Lấy giảng viên cần xóa
                    GIANG_VIEN existingGV = dbContext.GIANG_VIEN.Find(maGV);

                    if (existingGV != null)
                    {
                        // Xóa giảng viên từ DbSet
                        existingGV.Hide = true;
                        dbContext.Entry(existingGV).State = System.Data.Entity.EntityState.Modified;
                        // Lưu thay đổi vào cơ sở dữ liệu
                        dbContext.SaveChanges();

                        MessageBox.Show("Xóa giảng viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại DataGridView sau khi xóa giảng viên
                        LoadDataToDGV_GiangVien();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy giảng viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn giảng viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_importGV(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Title = "Select an Excel File"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);

                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            // Bắt đầu từ dòng thứ 2 (dòng đầu tiên là tiêu đề)
                            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                            {
                                // Lấy dữ liệu từ cột tương ứng
                                string maGV = worksheet.Cells[row, 1].Value?.ToString().Trim();
                                string hoTenLot = worksheet.Cells[row, 2].Value?.ToString().Trim();
                                string ten = worksheet.Cells[row, 3].Value?.ToString().Trim();
                                string tenKhoa = worksheet.Cells[row, 4].Value?.ToString().Trim();

                                // Kiểm tra dữ liệu không rỗng
                                if (!string.IsNullOrEmpty(maGV) && !string.IsNullOrEmpty(hoTenLot) && !string.IsNullOrEmpty(ten))
                                {
                                    // Thêm giảng viên vào cơ sở dữ liệu
                                    AddGiangVienToDatabase(maGV, hoTenLot, ten, tenKhoa);
                                }
                            }

                            MessageBox.Show("Thêm giảng viên từ Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật lại DataGridView sau khi thêm giảng viên
                            LoadDataToDGV_GiangVien();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu trong tệp Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddGiangVienToDatabase(string maGV, string hoTenLot, string ten, string tenKhoa)
        {
            using (Context dbContext = new Context())
            {
                // Kiểm tra xem khoa đã tồn tại hay không
                KHOA existingKhoa = dbContext.KHOAs.FirstOrDefault(k => k.MaKhoa == tenKhoa);

                if (existingKhoa == null)
                {
                    // Nếu khoa không tồn tại, có thể thông báo lỗi hoặc thêm mới khoa
                    MessageBox.Show($"Khoa '{tenKhoa}' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hoặc thêm mã lỗi/xử lý tương ứng tùy vào yêu cầu của bạn
                }

                // Kiểm tra xem mã giảng viên đã tồn tại hay chưa
                bool maGVExists = dbContext.GIANG_VIEN.Any(gv => gv.MaGV == maGV);

                if (maGVExists)
                {
                    // Nếu mã giảng viên đã tồn tại, có thể thông báo lỗi hoặc thực hiện hành động tương ứng
                    MessageBox.Show($"Mã giảng viên '{maGV}' đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hoặc thêm mã lỗi/xử lý tương ứng tùy vào yêu cầu của bạn
                }

                // Tạo một đối tượng GIANG_VIEN mới
                GIANG_VIEN newGiangVien = new GIANG_VIEN
                {
                    MaGV = maGV,
                    HoTenLot = hoTenLot,
                    Ten = ten,
                    Khoa = existingKhoa.MaKhoa
                };

                // Thêm giảng viên mới vào cơ sở dữ liệu
                dbContext.GIANG_VIEN.Add(newGiangVien);

                // Lưu thay đổi vào cơ sở dữ liệu
                dbContext.SaveChanges();
            }
        }

        private void btn_Export(object sender, EventArgs e)
        {
            // Create SaveFileDialog to choose the location to save the Excel file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx|All Files|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Create a new Excel package
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            // Add a new worksheet to the Excel package
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("GiangVienData");

                            // Write headers to the worksheet
                            for (int col = 1; col <= dgv_GiangVien.Columns.Count; col++)
                            {
                                worksheet.Cells[1, col].Value = dgv_GiangVien.Columns[col - 1].HeaderText;
                            }

                            // Write data from DataGridView to the worksheet
                            for (int row = 0; row < dgv_GiangVien.Rows.Count; row++)
                            {
                                for (int col = 0; col < dgv_GiangVien.Columns.Count; col++)
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = dgv_GiangVien.Rows[row].Cells[col].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns();
                            // Save the Excel package to the selected file
                            package.SaveAs(new FileInfo(filePath));

                            MessageBox.Show("Export successful.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting data to Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_Find(object sender, EventArgs e)
        {
            // Get the search keyword from txtMaGV
            string searchKeyword = txtMaGv.Text.Trim();

            // Check if the search keyword is empty
            if (string.IsNullOrEmpty(searchKeyword))
            {
                MessageBox.Show("Please enter a search keyword.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool matchFound = false;

            // Iterate through each row in the DataGridView and filter based on the search keyword
            foreach (DataGridViewRow row in dgv_GiangVien.Rows)
            {
                // Assuming the MaGV is in the first column (index 0)
                object cellValue = row.Cells["MaGV"].Value;

                // Null check for cell value
                if (cellValue != null)
                {
                    string maGVCellValue = cellValue.ToString();

                    // Case-insensitive search using String.IndexOf
                    if (maGVCellValue.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Highlight the matching row (optional)
                        row.Selected = true;

                        // Scroll to the matching row (optional)
                        dgv_GiangVien.FirstDisplayedScrollingRowIndex = row.Index;

                        matchFound = true;

                        // Stop searching after the first match (remove this line if you want to highlight multiple matches)
                        break;
                    }
                }
            }

            // Display a message if no matches are found
            if (!matchFound)
            {
                MessageBox.Show("No matches found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
