using ComponentFactory.Krypton.Ribbon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceLearning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            switch(kryptonRibbon1.SelectedTab.Text)
            {
                case "Giảng viên": 
                    LoadGridGV();
                    break;
                case "Hoạt động": 
                    LoadGridHD();
                    break;
               case "Sinh Viên": 
                    LoadGridSV();
                    break;
               case "Đối Tác": 
                    LoadGridDT();
                    break;
                case "Tài Trợ": 
                    LoadGridTT();
                    break;
               case "Hạng Mục ĐG": 
                    LoadGridHM();
                    break;
               case "Khoa/Viện": 
                    LoadGridKhoa();
                    break;
                default:
                    kryptonRibbon1.SelectedTab = tabHD;
                    LoadGridHD();
                    break;

            }
        }
        public void LoadGridHD()
        {
            using (Context db = new Context())
            {
                var _event = from ev in db.HOAT_DONG
                             where ev.Hide == false
                             select new
                             {
                                 a = ev.MaHD,
                                 b = ev.TenHoatDong,
                                 c = ev.Loai,
                                 d = ev.NgayBatDau,
                                 e = ev.NgayKetThuc,
                                 f = ev.CreatedDate,
                             };
                dgvMain.DataSource = _event.ToList();
                FormatGridViewHD();
            }
        }
        public void FormatGridViewHD()
        {
            dgvMain.Columns["d"].DefaultCellStyle.Format = "dd-MM-yyyy";
            dgvMain.Columns["e"].DefaultCellStyle.Format = "dd-MM-yyyy";
            dgvMain.Columns["f"].DefaultCellStyle.Format = "dd-MM-yyyy";
            dgvMain.Columns["a"].HeaderText = "Mã HĐ";
            dgvMain.Columns["b"].HeaderText = "Tên Hoạt Động";
            dgvMain.Columns["c"].HeaderText = "Loại";
            dgvMain.Columns["d"].HeaderText = "Ngày BĐ";
            dgvMain.Columns["e"].HeaderText = "Ngày KT";
            dgvMain.Columns["f"].HeaderText = "Created Date";
            
        }
        
        public void FormatGridViewKhoa()
        {
            dgvMain.Columns[4].DefaultCellStyle.Format = "dd-MM-yyyy";
            dgvMain.Columns[0].HeaderText = "Mã Đơn Vị";
            dgvMain.Columns[1].HeaderText = "Tên Đơn Vị";
            dgvMain.Columns[2].HeaderText = "Số Điện Thoại";
            dgvMain.Columns[3].HeaderText = "Email";
            dgvMain.Columns[4].HeaderText = "Ngày Thành Lập";
        }
        public void FormatGridViewTaiTro()
        {
            dgvMain.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvMain.Columns[0].HeaderText = "ID";
            dgvMain.Columns[1].HeaderText = "Nhà Tài Trợ";
            dgvMain.Columns[2].HeaderText = "Đại Diện";
            dgvMain.Columns[3].HeaderText = "Số điện thoại";
            dgvMain.Columns[4].HeaderText = "Email";
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvMain.Columns["c"].Index
            && e.Value != null)
            {
                int i = (int)e.Value;
                e.Value = GetLoai(i);
            }
            else return;
        }
        public static string ConvertDMY(DateTime? dt)
        {
            return dt == null ? "N/A" : ((DateTime)dt).ToString("dd-MM-yyyy");
        }
        public string GetLoai(int i)
        {
            switch (i)
            {
                case 0: return "Sự kiện";
                case 1: return "Dự án";
                case 2: return "Môn học";
                default: return "N/A";
            }
        }

        private void kryptonRibbonGroupButton1_Click(object sender, EventArgs e)
        {
            frmAddHoatDong New = new frmAddHoatDong();
            New.ShowDialog();
        }

        private void btnHD_Edit_Click(object sender, EventArgs e)
        {
            string EventID = dgvMain.CurrentRow.Cells["a"].Value.ToString();
            frmAddHoatDong Update = new frmAddHoatDong();
            Update.LoadFormUpdate(EventID);
            Update.ShowDialog();
        }

        private void btnHDDetail_Click(object sender, EventArgs e)
        {
            string EventID = dgvMain.CurrentRow.Cells["a"].Value.ToString();
            frmViewHoatDong View = new frmViewHoatDong();
            View.LoadFormView(EventID);
            View.Show();
        }

        private void btnHDDel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn có chắc muốn xóa hoạt động này không?", "Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    using (Context db = new Context())
                    {
                        string EventID = dgvMain.CurrentRow.Cells["a"].Value.ToString();
                        HOAT_DONG DelHD = db.HOAT_DONG.Find(EventID);
                        if (DelHD == null) return;
                        else
                        {
                            DelHD.Hide = true;
                            db.Entry(DelHD).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show("Xóa Hoạt Động thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch  (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
                return;
        }

        //private void kryptonRibbonTab1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    dataGridView1.Rows.Clear();
        //    dataGridView1.Columns.Clear();
        //    dataGridView1.Refresh();
        //    using (Context db = new Context())
        //    {
        //        var _gv = (from gv in db.GIANG_VIEN
        //                     where gv.Hide == false
        //                     select new
        //                     {
        //                         a = gv.MaGV,
        //                         b = gv.HoTenLot,
        //                         c = gv.Ten,
        //                         d = gv.DonVi,
        //                         e = gv.KHOA1.TenKhoa,
        //                     }).ToList();
        //        dataGridView1.DataSource = _gv;
        //    }
        //}

        private void kryptonRibbon1_SelectedTabChanged(object sender, EventArgs e)
        {
            try
            {
                KryptonRibbon Rib = sender as KryptonRibbon;
                if (Rib.SelectedTab == null) return;
                string Tname = Rib.SelectedTab.Text;
                if (e.ToString() == null || Tname == null) return;

                dgvMain.DataSource = null;
                dgvMain.Refresh();
                if (Tname == "Hoạt động")
                {
                    LoadGridHD();
                }
                else
                    if (Tname == "Giảng viên")
                {
                    LoadGridGV();
                }
                else
                    if (Tname == "Sinh Viên")
                {
                    LoadGridSV();
                }
                else
                    if (Tname == "Đối Tác")
                {
                    LoadGridDT();
                }
                else
                    if (Tname == "Hạng Mục ĐG")
                {
                    LoadGridHM();
                }
                else
                    if (Tname == "Khoa/Viện")
                {
                    LoadGridKhoa();
                }
                else
                    if (Tname == "Tài Trợ")
                {
                    LoadGridTT();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while changing tab", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadGridTT()
        {
            using (Context db = new Context())
            {
                var _tt = (from tt in db.TAI_TRO
                          where tt.Hide == false
                          select new
                          {
                              ID = tt.ID_TaiTro,
                              Ten = tt.TenTaiTro,
                              DaiDien = tt.DaiDien,
                              SDT = tt.SDT,
                              Email = tt.Email,

                          }).ToList();
                if (_tt == null) return;
                dgvMain.DataSource = _tt;
                FormatGridViewTaiTro();
            }
        }
        private void LoadGridKhoa()
        {
            using (Context db = new Context())
            {
                var _k = (from k in db.KHOAs
                           where k.Hide == false
                           select new
                           {
                               MaDonVi = k.MaKhoa,
                               Ten = k.TenKhoa,
                               sdt = k.SDT,
                               email = k.Email,
                               NgayThanhLap = k.NgayThanhLap,

                           }).ToList();
                if (_k == null) return;
                dgvMain.DataSource = _k;
                FormatGridViewKhoa();
            }
        }
            private void LoadGridHM()
        {
            using (Context db = new Context())
            {
                var _hm = (from hm in db.HANG_MUC
                           where hm.Hide == false
                           select new
                           {
                               STT = hm.ID,
                               Ten = hm.TenHangMuc,
                           }).ToList();
                if (_hm == null) return;
                dgvMain.DataSource = _hm;
            }
        }
        private void LoadGridGV()
        {
            using (Context db = new Context())
            {
                var _gv = (from gv in db.GIANG_VIEN
                           where gv.Hide == false
                           select new
                           {
                               a = gv.MaGV,
                               b = gv.HoTenLot,
                               c = gv.Ten,
                               d = gv.KHOA1.TenKhoa,
                           }).ToList();
                if (_gv == null) return;
                dgvMain.DataSource = _gv;
            }
        }
        private void LoadGridSV()
        {
            using (Context db = new Context())
            {
                var _sv = (from gv in db.SINH_VIEN
                           where gv.Hide == false
                           select new
                           {
                               MSSV = gv.MSSV,
                               HoTen = gv.HoTen,
                               Khoa = gv.KHOA1.TenKhoa,
                           }).ToList();
                if (_sv == null) return;
                dgvMain.DataSource = _sv;
            }
        }
        private void LoadGridDT()
        {
            using (Context db = new Context())
            {
                var _gv = (from gv in db.DOI_TAC
                           where gv.Hide == false
                           select new
                           {
                               ID = gv.ID_DoiTac,
                               Ten = gv.TenDoiTac,
                               DaiDien = gv.DaiDien,
                               SDT = gv.SDT,
                               Email = gv.Email,
                           }).ToList();
                if (_gv == null) return;
                dgvMain.DataSource = _gv;
            }
        }

        private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string Tname = this.kryptonRibbon1.SelectedTab.Text;
            if (Tname == "Hoạt động")
            {
                btnHDEdit.PerformClick();
            }
            else
                return;
        }

        private void btnHM_New_Click(object sender, EventArgs e)
        {
            frmAddHangMuc frmNewHM = new frmAddHangMuc();
            frmNewHM.ShowDialog();
        }

        private void btnHM_Edit_Click(object sender, EventArgs e)
        {
            frmAddHangMuc frmEditHM = new frmAddHangMuc();
            if (dgvMain.CurrentRow == null)
                return;
            frmEditHM.ID = (int)dgvMain.CurrentRow.Cells["STT"].Value;
            frmEditHM.loadUpdateForm(dgvMain.CurrentRow.Cells["Ten"].Value.ToString());
            frmEditHM.ShowDialog();
        }

        private void btnHM_Del_Click(object sender, EventArgs e)
        {
            int ID = (int) dgvMain.CurrentRow.Cells["STT"].Value;
            if (DialogResult.Yes == MessageBox.Show("Bạn có chắc muốn xóa hạng mục này không?", "Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    using (Context db = new Context())
                    {
                        HANG_MUC DelHM = db.HANG_MUC.Find(ID);
                        if (DelHM == null) return;
                        else
                        {
                            DelHM.Hide = true;
                            db.Entry(DelHM).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show("Xóa Hạng Mục thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            kryptonRibbon1.SelectedTab = TabDG;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                return;
        }

        private void btnTK_Khoa_Click(object sender, EventArgs e)
        {
            frmTK_Khoa TK_Khoa = new frmTK_Khoa();
            TK_Khoa.Show();
        }

        private void btnTK_GV_Click(object sender, EventArgs e)
        {
            frmTK_GiangVien TK = new frmTK_GiangVien();
            TK.Show();
        }

        private void btnTK_DT_Click(object sender, EventArgs e)
        {
            frmTK_DoiTac tK_DoiTac = new frmTK_DoiTac();
            tK_DoiTac.Show();
        }

        private void btnTK_TT_Click(object sender, EventArgs e)
        {
            frmTK_TaiTro TK = new frmTK_TaiTro();
            TK.Show();
        }

        private void btnTK_TC_Click(object sender, EventArgs e)
        {
            frmTK_TaiChinh TK = new frmTK_TaiChinh();
            TK.Show();
        }

        private void btnK_new_Click(object sender, EventArgs e)
        {
            frmKhoaDetails AddKhoa = new frmKhoaDetails();
            AddKhoa.ShowDialog();
        }

        private void btnK_edit_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null)
                return;
            frmKhoaDetails EditKhoa = new frmKhoaDetails();
            EditKhoa.isCreate = false;
            EditKhoa.LoadFrmEditKhoa(dgvMain.CurrentRow.Cells[0].Value.ToString());
            EditKhoa.ShowDialog(this);
        }

        private void btnK_delete_Click(object sender, EventArgs e)
        {
            string MaKhoa = dgvMain.CurrentRow.Cells[0].Value.ToString();
            if (DialogResult.Yes == MessageBox.Show("Bạn có chắc muốn xóa Đơn vị này không?", "Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    using (Context db = new Context())
                    {
                        KHOA DelKH = db.KHOAs.Find(MaKhoa);
                        if (DelKH == null) return;
                        else
                        {
                            DelKH.Hide = true;
                            db.Entry(DelKH).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show("Xóa Đơn Vị thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                return;
        }

        private void btnTT_New_Click(object sender, EventArgs e)
        {
            frmTTDetails frmTTDetails = new frmTTDetails();
            frmTTDetails.ShowDialog();
        }

        private void btnTT_Edit_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null)
                return;
            frmTTDetails TTEdits = new frmTTDetails();
            TTEdits.isCreate = false;
            TTEdits.IDtt = (int)dgvMain.CurrentRow.Cells[0].Value;
            TTEdits.LoadFrmEditTT();
            TTEdits.ShowDialog(this);
        }

        private void btnTT_Delete_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null)
                return;
            int IDtt = (int)dgvMain.CurrentRow.Cells[0].Value;
            if (DialogResult.Yes == MessageBox.Show("Bạn có chắc muốn xóa Nhà Tài trợ này không?", "Cảnh báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    using (Context db = new Context())
                    {
                        TAI_TRO DelTT = db.TAI_TRO.Find(IDtt);
                        if (DelTT == null) return;
                        else
                        {
                            DelTT.Hide = true;
                            db.Entry(DelTT).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            MessageBox.Show("Xóa Nhà Tài Trợ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                return;
        }
    }
}
