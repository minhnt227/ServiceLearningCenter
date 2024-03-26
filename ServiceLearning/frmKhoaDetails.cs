using Microsoft.Office.Interop.Excel;
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
    public partial class frmKhoaDetails : Form
    {
        public bool isCreate = true;
        public frmKhoaDetails()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if(isCreate)
            {
                AddNewKh();
            }
            else
            {
                EditKh();
            }
        }
        private void AddNewKh()
        {
            if (txtMaKh.Text.Length <= 0) { return; }
            try
            {
                using (Context db = new Context())
                {
                    KHOA KH = new KHOA();
                    KH.MaKhoa = txtMaKh.Text.Trim();
                    KH.Hide = false;
                    KH.TenKhoa = txtTenK.Text.Trim();
                    KH.SDT = txtSdtK.Text.Trim();
                    KH.Email = txtEmailK.Text.Trim();
                    KH.NgayThanhLap = dtpKhBegin.Value;
                    db.KHOAs.Add(KH);
                    db.SaveChanges();
                    MessageBox.Show("Thêm thành công");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n***************************************** \n\n" + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditKh()
        {
            if (txtMaKh.Text.Length <= 0) { return; }
            try
            {
                using (Context db = new Context())
                {
                    KHOA KH = db.KHOAs.Find(txtMaKh.Text);
                    if (KH == null)
                        return;
                    KH.Hide = false;
                    KH.TenKhoa = txtTenK.Text.Trim();
                    KH.SDT = txtSdtK.Text.Trim();
                    KH.Email = txtEmailK.Text.Trim();
                    KH.NgayThanhLap = dtpKhBegin.Value;
                    db.Entry(KH).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Sửa thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n*****************************************\n\n " + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadFrmEditKhoa(string MaKhoa)
        {
            try
            {
                using (Context db = new Context())
                {
                    KHOA KH = db.KHOAs.Find(MaKhoa);
                    if (KH == null)
                        Close();
                    txtMaKh.Text = MaKhoa; txtMaKh.ReadOnly = true;
                    txtTenK.Text = KH.TenKhoa;
                    txtSdtK.Text = KH.SDT;
                    txtEmailK.Text = KH.Email;
                    dtpKhBegin.Value = (KH.NgayThanhLap == null)? DateTime.Today: (DateTime)KH.NgayThanhLap;
                    btnManage.Text = "Sửa";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n*****************************************\n\n " + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
                if (isCreate)
                {
                    AddNewKh();
                    LoadDataToDGV_Khoa();


                }
                else
                {
                    EditKh();
                }
        }


        private void LoadDataToDGV_Khoa()
        {
            try
            {
                using (Context dbContext = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng KHOA
                    var khoaData = from khoa in dbContext.KHOAs
                                   where khoa.Hide == false
                                   select new
                                   {
                                       MaKhoa = khoa.MaKhoa,
                                       TenKhoa = khoa.TenKhoa,
                                       SDT = khoa.SDT,
                                       Email = khoa.Email,
                                       NgayThanhLap = khoa.NgayThanhLap
                                   };

                    // Gán dữ liệu cho DataGridView dgvKhoa
                    dgvKhoa.DataSource = khoaData.ToList();

                    // Đổi tên tiêu đề của các cột
                    dgvKhoa.Columns["MaKhoa"].HeaderText = "Mã Khoa";
                    dgvKhoa.Columns["TenKhoa"].HeaderText = "Tên Khoa";
                    dgvKhoa.Columns["SDT"].HeaderText = "Số Điện Thoại";
                    dgvKhoa.Columns["Email"].HeaderText = "Email";
                    dgvKhoa.Columns["NgayThanhLap"].HeaderText = "Ngày Thành Lập";
                    dgvKhoa.Columns["NgayThanhLap"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu Khoa.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void frmKhoaDetails_Load(object sender, EventArgs e)
        {
            LoadDataToDGV_Khoa();
        }

        private void btn_Find(object sender, EventArgs e)
        {

        }

        private void btn_Import(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Title = "Select an Excel File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            List<KHOA> khoaList = new List<KHOA>();

                            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                            {
                                string maKhoa   = worksheet.Cells[row, 1].Value?.ToString();
                                string tenKhoa  = worksheet.Cells[row, 2].Value?.ToString();
                                string sdt      = worksheet.Cells[row, 3].Value?.ToString();
                                string email    = worksheet.Cells[row, 4].Value?.ToString();
                                var dayTemp     = worksheet.Cells[row, 5].Value.ToString();
                                DateTime ngayThanhLap = new DateTime();
                                if (dayTemp != null)
                                {
                                    ngayThanhLap = DateTime.ParseExact(dayTemp,"dd/MM/yyyy",null);
                                }
                                else ngayThanhLap = DateTime.Now;

                                if (!string.IsNullOrEmpty(maKhoa) && !string.IsNullOrEmpty(tenKhoa))
                                {
                                    khoaList.Add(new KHOA
                                    {
                                        MaKhoa = maKhoa,
                                        TenKhoa = tenKhoa,
                                        SDT = sdt,
                                        Email = email,
                                        NgayThanhLap = ngayThanhLap
                                    });
                                }
                            }

                            // Check for existing records
                            List<string> existingMaKhoaList = CheckExistingMaKhoa(khoaList.Select(k => k.MaKhoa).ToList());

                            if (existingMaKhoaList.Any())
                            {
                                MessageBox.Show($"The following MaKhoa already exists: {string.Join(", ", existingMaKhoaList)}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Save the list to the database
                            SaveKhoaListToDatabase(khoaList);

                            // Refresh the DataGridView
                            LoadDataToDGV_Khoa();

                            MessageBox.Show("Import successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No data found in the Excel file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private List<string> CheckExistingMaKhoa(List<string> maKhoaList)
        {
            using (Context db = new Context())
            {
                // Filter the existing MaKhoa from the database
                List<string> existingMaKhoaList = db.KHOAs.Where(k => maKhoaList.Contains(k.MaKhoa)).Select(k => k.MaKhoa).ToList();
                return existingMaKhoaList;
            }
        }


        private void SaveKhoaListToDatabase(List<KHOA> khoaList)
        {
            try
            {
                using (Context db = new Context())
                {
                    foreach (var khoa in khoaList)
                    {
                        KHOA existingKhoa = db.KHOAs.Find(khoa.MaKhoa);

                        if (existingKhoa == null)
                        {
                            // Add new KHOA
                            khoa.Hide = false;
                            db.KHOAs.Add(khoa);
                        }
                        else
                        {
                            // Update existing KHOA
                            existingKhoa.TenKhoa = khoa.TenKhoa;
                            existingKhoa.SDT = khoa.SDT;
                            existingKhoa.Email = khoa.Email;
                            existingKhoa.NgayThanhLap = khoa.NgayThanhLap;
                            existingKhoa.Hide = false;
                        }
                    }

                    // Save changes to the database
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving data to the database: {ex.Message}");
            }
        }

        private void btn_Export(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Export to Excel"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);

                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KhoaData");

                        // Headers
                        worksheet.Cells[1, 1].Value = "MaKhoa";
                        worksheet.Cells[1, 2].Value = "TenKhoa";
                        worksheet.Cells[1, 3].Value = "SDT";
                        worksheet.Cells[1, 4].Value = "Email";
                        worksheet.Cells[1, 5].Value = "NgayThanhLap";

                        // Data
                        int row = 2;
                        foreach (DataGridViewRow dgvRow in dgvKhoa.Rows)
                        {
                            DateTime NTL = (DateTime)dgvRow.Cells["NgayThanhLap"].Value;
                            worksheet.Cells[row, 1].Value = dgvRow.Cells["MaKhoa"].Value?.ToString();
                            worksheet.Cells[row, 2].Value = dgvRow.Cells["TenKhoa"].Value?.ToString();
                            worksheet.Cells[row, 3].Value = dgvRow.Cells["SDT"].Value?.ToString();
                            worksheet.Cells[row, 4].Value = dgvRow.Cells["Email"].Value?.ToString();
                            worksheet.Cells[row, 5].Value = NTL.ToString("dd/MM/yyyy");

                            row++;
                        }

                        // Auto-fit columns
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        // Save the Excel file
                        package.Save();

                        MessageBox.Show("Export successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedKhoa = dgvKhoa.SelectedRows[0];
            if (selectedKhoa != null)
            {
                string Ma = selectedKhoa.Cells["MaKhoa"].Value?.ToString();
                string Ten = selectedKhoa.Cells["TenKhoa"].Value?.ToString();
                string sdt = selectedKhoa.Cells["SDT"].Value?.ToString();
                string email = selectedKhoa.Cells["Email"].Value?.ToString();
                dtpKhBegin.Text = selectedKhoa.Cells["NgayThanhLap"].Value.ToString();

                txtMaKh.Text = Ma;
                txtTenK.Text = Ten;
                txtSdtK.Text = sdt;
                txtEmailK.Text = email;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EditKh();
            LoadDataToDGV_Khoa();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhoa.SelectedRows.Count > 0)
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khoa {dgvKhoa.SelectedRows[0].Cells["TenKhoa"].Value.ToString()} không?", "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    if (txtMaKh.Text.Length <= 0) { return; }
                    try
                    {
                        using (Context db = new Context())
                        {
                            KHOA KH = db.KHOAs.Find(txtMaKh.Text);
                            if (KH == null)
                                return;
                            KH.Hide = true;
                            db.Entry(KH).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            LoadDataToDGV_Khoa();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n*****************************************\n\n " + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    return;
            }
        }
    }
}
