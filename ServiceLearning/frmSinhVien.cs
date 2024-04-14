using System;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using Guna.UI2.WinForms;

namespace ServiceLearning
{
    public partial class frmSinhVien : Form
    {
        private Context dbContext;

        public frmSinhVien()
        {
            InitializeComponent();
            dbContext = new Context();
        }

        private void gbSinhVien_Enter(object sender, EventArgs e)
        {
            // Nếu có mã nguồn cho sự kiện này, bạn có thể thêm mã vào đây
        }

        public bool FindDuplicateMSSV()
        {
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MSSV"].Value != null && row.Cells["MSSV"].Value.ToString() == txtMSSV.Text)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ValidateSV()
        {
            if (txtMSSV.Text.Length == 0)
            {
                MessageBox.Show("MSSV Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (FindDuplicateMSSV())
            {
                MessageBox.Show("MSSV Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        public void clearSVFields()
        {
            txtMSSV.Clear();
            txtName.Clear();
            cbKhoa.SelectedIndex = -1;
            txtMSSV.ReadOnly = false;
            btnAddSV.Enabled = true;
        }

        private void btnAddSV_Click(object sender, EventArgs e)
        {
            if (ValidateSV())
            {     
                // Lưu dữ liệu vào cơ sở dữ liệu
                AddDataToDatabase(txtMSSV.Text, txtName.Text, cbKhoa.SelectedValue.ToString().Trim());
                //Load db
                LoadDataToDGV();

                // Xóa các trường dữ liệu
                clearSVFields();
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
            }
        }

        private void AddDataToDatabase(string mssv, string hoTen, string khoa)
        {
            mssv = mssv.Trim(); hoTen = hoTen.Trim();
            try
            {
                using (Context dbContext = new Context())
                {
                    // Tạo một đối tượng SINH_VIEN mới
                    SINH_VIEN newSinhVien = new SINH_VIEN
                    {
                        MSSV = mssv,
                        HoTen = hoTen,
                        Khoa = khoa,
                        Hide = false,

                        // Nếu có thêm các trường khác, hãy thêm vào đây
                    };

                    // Thêm đối tượng mới vào DbSet và lưu vào cơ sở dữ liệu
                    dbContext.SINH_VIEN.Add(newSinhVien);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed when AddDataToDatabase.\n**Detail**\n\n" + ex.Message,"Error, Contact Min đẹp trai");
            }
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            // Load dữ liệu vào DataGridView
            LoadDataToDGV();
            LoadMaKhoaToComboBox();
            LoadAutoComplete();
            btnTimSV.Enabled = false;
        }
        private void LoadMaKhoaToComboBox()
        {
            /*// Lấy danh sách mã khoa từ bảng KHOA
            var maKhoaList = dbContext.KHOAs.Select(k => k.MaKhoa).ToList();

            // Gán danh sách mã khoa vào ComboBox
            cbKhoa.DataSource = maKhoaList;*/
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
        private void LoadDataToDGV()
        {
            try
            {
                using (Context dbContext = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng SINH_VIEN
                    var sinhVienData = from sv in dbContext.SINH_VIEN
                                       where (sv.Hide == false)
                                       select new
                                       {
                                           MSSV = sv.MSSV,
                                           HoTen = sv.HoTen,
                                           Khoa = sv.Khoa,
                                           TenKhoa = sv.KHOA1.TenKhoa,
                                       };

                    // Gán dữ liệu cho DataGridView dgvSinhVien
                    dgvSinhVien.DataSource = sinhVienData.Take(500).ToList();

                    //// Đổi tên tiêu đề của các cột
                    //dgvSinhVien.Columns["MSSV"].HeaderText = "Mã Sinh Viên";
                    //dgvSinhVien.Columns["HoTen"].HeaderText = "Họ Tên";
                    //dgvSinhVien.Columns["Khoa"].HeaderText = "Mã Khoa";
                    //dgvSinhVien.Columns["TenKhoa"].HeaderText = "Tên Khoa";
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu Sinh Viên.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reset()
        {
            txtMSSV.Text = txtName.Text = cbKhoa.Text = "";
        }
  


        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSVEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (Context dbContext = new Context())
                {
                    // Lấy thông tin từ các TextBox và ComboBox
                    string mssv = txtMSSV.Text;
                    string hoTen = txtName.Text;
                    string khoa = cbKhoa.SelectedValue.ToString().Trim();
                    string tenkhoa = cbKhoa.Text;

                    // Kiểm tra xem MSSV có tồn tại trong cơ sở dữ liệu không
                    SINH_VIEN sinhVienToUpdate = dbContext.SINH_VIEN.FirstOrDefault(sv => sv.MSSV == mssv);

                    if (sinhVienToUpdate != null)
                    {
                        // Cập nhật thông tin sinh viên
                        sinhVienToUpdate.HoTen = hoTen;
                        sinhVienToUpdate.Khoa = khoa;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        dbContext.SaveChanges();

                        MessageBox.Show("Cập nhật thông tin sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataToDGV();

                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed when btnSVEdit_Click.\n**Detail**\n\n" + ex.Message, "Error, Contact Min đẹp trai");
            }
        }

        private void import_excel(object sender, EventArgs e)
        {
            // Sử dụng OpenFileDialog để chọn tệp Excel
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Đường dẫn đến tệp Excel đã chọn
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        // Sử dụng thư viện EPPlus để đọc dữ liệu từ tệp Excel
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                        {
                            // Lấy sheet đầu tiên (index 0)
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                            int rowCount = worksheet.Dimension.Rows;

                            // Bắt đầu từ dòng thứ 2 để bỏ qua dòng tiêu đề
                            for (int row = 2; row <= rowCount; row++)
                            {
                                // Lấy dữ liệu từ từng ô trong dòng
                                string mssv = worksheet.Cells[row, 1].Value?.ToString();
                                string hoTen = worksheet.Cells[row, 2].Value?.ToString();
                                string khoa = worksheet.Cells[row, 3].Value?.ToString();

                                // Kiểm tra và thêm sinh viên mới vào cơ sở dữ liệu
                                AddOrUpdateSinhVien(mssv, hoTen, khoa);
                            }

                            // Cập nhật lại dgvSinhVien sau khi thêm sinh viên mới
                            LoadDataToDGV();
                        }

                        MessageBox.Show("Import dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi đọc dữ liệu từ tệp Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AddOrUpdateSinhVien(string mssv, string hoTen, string khoa)
        {
            // Kiểm tra xem sinh viên đã tồn tại trong cơ sở dữ liệu chưa
            SINH_VIEN existingSinhVien = dbContext.SINH_VIEN.FirstOrDefault(sv => sv.MSSV == mssv);

            if (existingSinhVien == null)
            {
                // Nếu chưa tồn tại, thêm sinh viên mới vào cơ sở dữ liệu
                SINH_VIEN newSinhVien = new SINH_VIEN
                {
                    MSSV = mssv,
                    HoTen = hoTen,
                    Khoa = khoa
                };

                dbContext.SINH_VIEN.Add(newSinhVien);
            }
            else
            {
                // Nếu đã tồn tại, cập nhật thông tin của sinh viên
                existingSinhVien.HoTen = hoTen;
                existingSinhVien.Khoa = khoa;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            dbContext.SaveChanges();
        }


        private void dgvSinhVien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có dòng được chọn không và có phải là dòng dữ liệu hay không
            if (e.RowIndex >= 0 && e.RowIndex < dgvSinhVien.Rows.Count - 1)
            {
                DataGridViewRow selectedRow = dgvSinhVien.Rows[e.RowIndex];

                // Lấy thông tin từ dòng được chọn
                string mssv = selectedRow.Cells["MSSV"].Value.ToString();
                string hoTen = selectedRow.Cells["HoTen"].Value.ToString();
                string khoa = selectedRow.Cells["Khoa"].Value.ToString();

                // Hiển thị thông tin trong các TextBox và ComboBox
                txtMSSV.Text = mssv;
                txtName.Text = hoTen;
                cbKhoa.SelectedValue = khoa;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên {dgvSinhVien.SelectedRows[0].Cells["MSSV"].Value.ToString()} không?", "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // Kiểm tra xem có dòng được chọn trong dgvSinhVien không
                    if (dgvSinhVien.SelectedRows.Count > 0)
                    {
                        // Lặp qua các dòng được chọn và xóa sinh viên khỏi cơ sở dữ liệu
                        foreach (DataGridViewRow selectedRow in dgvSinhVien.SelectedRows)
                        {
                            // Lấy thông tin từ dòng được chọn
                            string mssv = selectedRow.Cells["MSSV"].Value.ToString();

                            // Xóa sinh viên từ cơ sở dữ liệu
                            DeleteSinhVien(mssv);
                        }

                        // Cập nhật lại dgvSinhVien sau khi xóa sinh viên
                        LoadDataToDGV();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                return;
        }

        private void DeleteSinhVien(string mssv)
        {
            using (Context dbContext = new Context())
            {

                    // Lấy danh sách sinh viên cần xóa
                    SINH_VIEN sinhVienToDelete = dbContext.SINH_VIEN.Where(sv => sv.MSSV == mssv).FirstOrDefault();

                        sinhVienToDelete.Hide = true;
                dbContext.Entry(sinhVienToDelete).State = System.Data.Entity.EntityState.Modified;
                // Lưu thay đổi vào cơ sở dữ liệu
                dbContext.SaveChanges();

                    MessageBox.Show("Xóa sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToDGV();
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
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("SinhVienData");

                            // Write headers to the worksheet
                            for (int col = 1; col <= dgvSinhVien.Columns.Count; col++)
                            {
                                worksheet.Cells[1, col].Value = dgvSinhVien.Columns[col - 1].HeaderText;
                            }

                            // Write data from DataGridView to the worksheet
                            for (int row = 0; row < dgvSinhVien.Rows.Count; row++)
                            {
                                for (int col = 0; col < dgvSinhVien.Columns.Count; col++)
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = dgvSinhVien.Rows[row].Cells[col].Value;
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
            // Get the search keyword from txtboxes

            string Mssv = txtMSSV.Text.ToString().Trim();
            string hoTen = txtName.Text.Trim();
            string MaKhoa = cbKhoa.SelectedValue.ToString().Trim();
            // Check if the search keyword is empty
            if (string.IsNullOrEmpty(Mssv) && string.IsNullOrEmpty(hoTen) && string.IsNullOrEmpty(MaKhoa))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu để tìm", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSinhVien_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadAutoComplete()
        {
            AutoCompleteStringCollection autoName = new AutoCompleteStringCollection();
            AutoCompleteStringCollection autoMSSV = new AutoCompleteStringCollection();
            try
            {
                using (Context dbContext = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng SINH_VIEN
                    var lstSV = from sv in dbContext.SINH_VIEN
                                       where (sv.Hide == false)
                                       select new
                                       {
                                           HoTen = sv.HoTen,
                                           MSSV = sv.MSSV,
                                           Khoa = sv.Khoa,
                                           TenKhoa = sv.KHOA1.TenKhoa,
                                       };
                    foreach (var SV in lstSV)
                    {
                        autoName.Add(SV.HoTen);
                        autoMSSV.Add(SV.MSSV);
                    }    
                }
                txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtName.AutoCompleteCustomSource = autoName;
                
                txtMSSV.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtMSSV.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtMSSV.AutoCompleteCustomSource = autoMSSV;

            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu trong LoadAutoComplete().\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMSSV_TextChanged(object sender, EventArgs e)
        {
            btnTimSV.Enabled = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnTimSV.Enabled = true;
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTimSV.Enabled = true;
        }

        private void btnRefreshSV_Click(object sender, EventArgs e)
        {
            cbKhoa.SelectedItem = null;
            //cbKhoa.SelectedIndex = -1;
            txtMSSV.Text = txtName.Text = "";
            btnTimSV.Enabled = false;
        }
    }
}
