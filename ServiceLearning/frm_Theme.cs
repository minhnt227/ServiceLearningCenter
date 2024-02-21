using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceLearning
{
    public partial class frm_Theme : Form
    {
        public frm_Theme()
        {
            InitializeComponent();
        }

        private void frm_Theme_Load(object sender, EventArgs e)
        {
            // Gọi hàm để load dữ liệu hoạt động vào DataGridView
            LoadDataToDGV_HoatDong();
        }

        private void LoadDataToDGV_HoatDong()
        {
            using (Context dbContext = new Context())
            {
                // Truy vấn LINQ để lấy dữ liệu từ bảng HOAT_DONG
                var hoatDongData = from hoatDong in dbContext.HOAT_DONG
                                   select new
                                   {
                                       MaHD = hoatDong.MaHD,
                                       TenHoatDong = hoatDong.TenHoatDong,
                                       Loai = hoatDong.Loai,
                                       NgayBatDau = hoatDong.NgayBatDau,
                                       NgayKetThuc = hoatDong.NgayKetThuc,
                                       CreatedDate = hoatDong.CreatedDate
                                   };

                // Gán dữ liệu cho DataGridView dgv_HoatDong
                dgv_HoatDong.DataSource = hoatDongData.ToList();

                // Đổi tên tiêu đề của các cột
                dgv_HoatDong.Columns["MaHD"].HeaderText = "Mã Hoạt Động";
                dgv_HoatDong.Columns["TenHoatDong"].HeaderText = "Tên Hoạt Động";
                dgv_HoatDong.Columns["Loai"].HeaderText = "Loại";
                dgv_HoatDong.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
                dgv_HoatDong.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
                dgv_HoatDong.Columns["CreatedDate"].HeaderText = "Ngày Tạo";
            }
        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
      
        }

        private void view_HoatDong(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng được chọn không
            if (dgv_HoatDong.SelectedRows.Count > 0)
            {
                // Lấy giá trị MaHD từ dòng được chọn
                int maHD = (int)dgv_HoatDong.SelectedRows[0].Cells["MaHD"].Value;

                // Tạo instance của frmViewHoatDong và truyền giá trị MaHD
                frmViewHoatDong frmView = new frmViewHoatDong();
                frmView.LoadFormView(maHD);

                // Mở form frmViewHoatDong
                frmView.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hoạt động để xem.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_XoaHD(object sender, EventArgs e)
        {
            // Thêm try-catch để xử lý lỗi
            try
            {
                // Kiểm tra xem có dòng được chọn không
                if (dgv_HoatDong.SelectedRows.Count > 0)
                {
                    // Lấy giá trị MaHD từ dòng được chọn
                    int maHD = (int)dgv_HoatDong.SelectedRows[0].Cells["MaHD"].Value;

                    // Hiển thị hộp thoại xác nhận
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hoạt động có mã {maHD} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Xác nhận xóa nếu người dùng nhấn Yes
                    if (result == DialogResult.Yes)
                    {
                        // Thực hiện xóa hoạt động ở đây
                        DeleteHoatDong(maHD);
                        // Cập nhật lại dgv_HoatDong sau khi xóa
                        LoadDataToDGV_HoatDong();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một hoạt động để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // In ra thông điệp lỗi chi tiết ra console hoặc ghi vào log
                Console.WriteLine(ex.Message);
                MessageBox.Show("Đã xảy ra lỗi khi xóa hoạt động.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DeleteHoatDong(int maHD)
        {
            using (Context dbContext = new Context()) // Sử dụng một đối tượng Context mới
            {
                // Tìm hoạt động cần xóa dựa trên MaHD
                HOAT_DONG hoatDongToDelete = dbContext.HOAT_DONG.FirstOrDefault(hd => hd.MaHD == maHD);

                if (hoatDongToDelete != null)
                {
                    // Xóa hoạt động từ DbSet và lưu vào cơ sở dữ liệu
                    dbContext.HOAT_DONG.Remove(hoatDongToDelete);
                    dbContext.SaveChanges();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int EventID = (int)dgv_HoatDong.CurrentRow.Cells["MaHD"].Value;
            frmAddHoatDong Update = new frmAddHoatDong();
            Update.LoadFormUpdate(EventID);
            Update.ShowDialog();
        }

        private void btn_AddHD(object sender, EventArgs e)
        {
            frmAddHoatDong fm = new frmAddHoatDong();
            fm.ShowDialog();
        }

        private void dgv_HoatDong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
