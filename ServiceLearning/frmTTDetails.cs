using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceLearning
{
    public partial class frmTTDetails : Form
    {
        public int IDtt = -1;
        public bool isCreate = true;
        public frmTTDetails()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            if (isCreate)
            {
                AddNewTT();
            }
            else
            {
                EditTT();
            }
        }
        private bool isEmpty(string a)
        {
            return a.Length == 0;
        }
        private bool isValidated()
        {
            if (isEmpty(txtTT_name.Text.Trim()))
                return false;
            return true;
        }
        private void AddNewTT()
        {
            if (!isValidated()) { 
                return; 
            }
            try
            {
                using (Context db = new Context())
                {
                    TAI_TRO TT= new TAI_TRO();
                    TT.TenTaiTro = txtTT_name.Text.Trim();
                    TT.DaiDien = txtTT_rep.Text.Trim();
                    TT.SDT = txtTT_sdt.Text.Trim();
                    TT.Email = txtTT_email.Text.Trim();
                    TT.Hide = false;
                    db.TAI_TRO.Add(TT);
                    db.SaveChanges();
                    MessageBox.Show("Thêm thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n***************************************** \n\n" + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditTT()
        {
            if (!isValidated())
            {
                return;
            }
            try
            {
                using (Context db = new Context())
                {
                    TAI_TRO TT = db.TAI_TRO.Find(IDtt);
                    if (TT == null)
                        return;
                    TT.TenTaiTro = txtTT_name.Text.Trim();
                    TT.DaiDien = txtTT_rep.Text.Trim();
                    TT.SDT = txtTT_sdt.Text.Trim();
                    TT.Email = txtTT_email.Text.Trim();
                    TT.Hide = false;
                    db.Entry(TT).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Sửa thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n***************************************** \n\n" + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadFrmEditTT()
        {
            try
            {
                using (Context db = new Context())
                {
                    TAI_TRO TT = db.TAI_TRO.Find(IDtt);
                    if (TT == null)
                        Close();
                    txtTT_name.Text = TT.TenTaiTro.Trim();
                    txtTT_rep.Text = TT.DaiDien.Trim();
                    txtTT_sdt.Text = TT.SDT.Trim();
                    txtTT_email.Text = TT.Email.Trim();
                    btnTTManage.Text = "Sửa";
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
                AddNewTT();
                LoadDataToDGV_TaiTro();
            }
            else
            {
                EditTT();
            }
        }
        private void LoadDataToDGV_TaiTro()
        {
            try
            {
                using (Context db = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng TAI_TRO
                    var taiTroData = from taiTro in db.TAI_TRO
                                     select new
                                     {
                                         //ID_TaiTro = taiTro.ID_TaiTro,
                                         TenTaiTro = taiTro.TenTaiTro,
                                         DaiDien = taiTro.DaiDien,
                                         SDT = taiTro.SDT,
                                         Email = taiTro.Email
                                     };

                    // Gán dữ liệu cho DataGridView dgv_TaiTro
                    dgv_TaiTro.DataSource = taiTroData.ToList();

                    // Đổi tên tiêu đề của các cột
                    //dgv_TaiTro.Columns["ID_TaiTro"].HeaderText = "ID Tài Trợ";
                    dgv_TaiTro.Columns["TenTaiTro"].HeaderText = "Tên Tài Trợ";
                    dgv_TaiTro.Columns["DaiDien"].HeaderText = "Người Đại Diện";
                    dgv_TaiTro.Columns["SDT"].HeaderText = "Số Điện Thoại";
                    dgv_TaiTro.Columns["Email"].HeaderText = "Email";
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu Tài Trợ.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void frmTTDetails_Load(object sender, EventArgs e)
        {
            LoadDataToDGV_TaiTro();
        }

        private void btn_Find(object sender, EventArgs e)
        {
            string searchKeyword = txtTT_name.Text.Trim();

            if (string.IsNullOrEmpty(searchKeyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (Context db = new Context())
            {
                var taiTroData = from taiTro in db.TAI_TRO
                                 where taiTro.TenTaiTro.Contains(searchKeyword) ||
                                       taiTro.DaiDien.Contains(searchKeyword) ||
                                       taiTro.SDT.Contains(searchKeyword) ||
                                       taiTro.Email.Contains(searchKeyword)
                                 select new
                                 {
                                     ID_TaiTro = taiTro.ID_TaiTro,
                                     TenTaiTro = taiTro.TenTaiTro,
                                     DaiDien = taiTro.DaiDien,
                                     SDT = taiTro.SDT,
                                     Email = taiTro.Email
                                 };

                dgv_TaiTro.DataSource = taiTroData.ToList();
            }
        }
        private void AddTaiTroToDatabase(string tenTaiTro, string daiDien, string sdt, string email)
        {
            try
            {
                using (Context db = new Context())
                {
                    TAI_TRO taiTro = new TAI_TRO
                    {
                        TenTaiTro = tenTaiTro,
                        DaiDien = daiDien,
                        SDT = sdt,
                        Email = email,
                        Hide = false
                    };

                    db.TAI_TRO.Add(taiTro);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu vào cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_Import(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Title = "Chọn tệp Excel"
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
                            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                            {
                                string tenTaiTro = worksheet.Cells[row, 1].Value?.ToString();
                                string daiDien = worksheet.Cells[row, 2].Value?.ToString();
                                string sdt = worksheet.Cells[row, 3].Value?.ToString();
                                string email = worksheet.Cells[row, 4].Value?.ToString();

                                if (!string.IsNullOrEmpty(tenTaiTro) && !string.IsNullOrEmpty(daiDien) && !string.IsNullOrEmpty(sdt) && !string.IsNullOrEmpty(email))
                                {
                                    AddTaiTroToDatabase(tenTaiTro, daiDien, sdt, email);
                                }
                            }

                            MessageBox.Show("Import dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadDataToDGV_TaiTro();
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

        private void btn_Export(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Title = "Lưu tệp Excel"
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(saveFileDialog1.FileName);

                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TaiTro");

                        worksheet.Cells["A1"].Value = "Tên Tài Trợ";
                        worksheet.Cells["B1"].Value = "Đại Diện";
                        worksheet.Cells["C1"].Value = "SĐT";
                        worksheet.Cells["D1"].Value = "Email";

                        for (int row = 0; row < dgv_TaiTro.Rows.Count; row++)
                        {
                            for (int col = 0; col < dgv_TaiTro.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dgv_TaiTro.Rows[row].Cells[col].Value?.ToString();
                            }
                        }

                        package.Save();
                    }

                    MessageBox.Show("Export dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
