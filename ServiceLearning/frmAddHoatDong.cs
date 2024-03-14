using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Collections;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Xml.Serialization;
using TextBox = System.Windows.Forms.TextBox;
using ComponentFactory.Krypton.Toolkit;
using OfficeOpenXml;
using System.IO;

namespace ServiceLearning
{
    public partial class frmAddHoatDong : Form
    {
        public bool isCreate = true;
        public int DT_ID = -1;
        public int TT_ID = -1;
        public string iniName = ""; //luu ten hd truoc khi sua
        public frmAddHoatDong()
        {
            InitializeComponent();
            this.Text = "Tạo Mới Hoạt Động";
        }
        public void LoadFormUpdate(int idHD)
        {
            try
            {
                LoadKhoaCB();
                using (Context db = new Context())
                {
                    this.Text = "Sửa Hoạt Động";
                    HOAT_DONG hD = db.HOAT_DONG.Find(idHD);
                    lblHeader.Text = "Sửa Hoạt Động";
                    //txtMaHD.Text = hD.MaHD.Trim(); txtMaHD.ReadOnly = true;
                    iniName = hD.TenHoatDong;
                    txtTenHD.Text = hD.TenHoatDong;
                    cbLoai.Text = hD.Loai == null ? "" : hD.Loai;
                    dtpNgayBD.Value = hD.NgayBatDau == null ? DateTime.Now : (DateTime)hD.NgayBatDau;
                    dtpNgayKT.Value = hD.NgayKetThuc == null ? DateTime.Now : (DateTime)hD.NgayKetThuc;
                    LoadHD_SinhVien(hD);
                    LoadHD_GV(hD);
                    LoadHD_DT(hD);
                    LoadHD_TT(hD);
                    LoadHD_TC(hD);
                    btnAddHD.Text = "Sửa Hoạt Động";
                    isCreate = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading form:\n"+ex.Message,"Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void LoadFormView(string idHD)
        {
            try
            {
                this.Text = "Thông Tin Hoạt Động";
                lblHeader.Text = "Thông Tin Hoạt Động";
                btnAddHD.Visible = false;
                isCreate = false;
                using (Context db = new Context())
                {
                    HOAT_DONG hD = db.HOAT_DONG.Find(idHD);
                    //txtMaHD.Text = hD.MaHD.Trim();
                    txtTenHD.Text = hD.TenHoatDong;
                    cbLoai.Text = hD.Loai == null ? "" : hD.Loai;
                    dtpNgayBD.Value = hD.NgayBatDau == null ? DateTime.Now : (DateTime)hD.NgayBatDau;
                    dtpNgayKT.Value = hD.NgayKetThuc == null ? DateTime.Now : (DateTime)hD.NgayKetThuc;
                    LoadHD_SinhVien(hD);
                    LoadHD_GV(hD);
                    LoadHD_DT(hD);
                    LoadHD_TT(hD);
                    LoadHD_TC(hD);
                    lblSV_TotalNumber.Text = dgvSinhVien.Rows.Count.ToString();
                    lblGV_TotalNumber.Text = dgv_GV.Rows.Count.ToString();

                    foreach (Control g in panel1.Controls)
                    {
                        if (g is GroupBox)
                        {
                            foreach (Control c in g.Controls)
                            {
                                if(c is TextBox)
                                {
                                    TextBox a = c as TextBox;
                                    a.ReadOnly = true;
                                }
                                if (c is Button | c is KryptonButton |c is ComboBox | c is DateTimePicker)
                                {
                                    c.Enabled = false;  
                                }
                                if (c is KryptonNumericUpDown)
                                {
                                    KryptonNumericUpDown k = (KryptonNumericUpDown)c;
                                    k.ReadOnly = true;
                                    k.Increment = 0;
                                }    
                                else
                                    continue;
                            }
                        }
                        else
                            continue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading form:\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadHD_SinhVien(HOAT_DONG hD)
        {
            List<HD_SINHVIEN> SVList = hD.HD_SINHVIEN.ToList();
            foreach (HD_SINHVIEN SV in SVList)
            {
                DataGridViewRow row = new DataGridViewRow();
                dgvSinhVien.Rows.Add(SV.MSSV, SV.SINH_VIEN.HoTen, SV.SINH_VIEN.KHOA1.TenKhoa, SV.VaiTro, SV.GhiChu, SV.SINH_VIEN.Khoa, SV.VaiTro);
            }
        }
        
        private void LoadHD_GV(HOAT_DONG hD)
        {    //MaGV,HoTenLot,Ten,GVKhoa,GV_DonVi,GVKHoa_DB
            List<HD_GIANGVIEN> List = hD.HD_GIANGVIEN.ToList();
            foreach (HD_GIANGVIEN GV in List)
            {
                DataGridViewRow row = new DataGridViewRow();
                dgv_GV.Rows.Add(GV.MaGV, GV.GIANG_VIEN.HoTenLot, GV.GIANG_VIEN.Ten, GV.GIANG_VIEN.KHOA1.TenKhoa, GV.VaiTro, GV.GIANG_VIEN.Khoa);
            }
        }
        private void LoadHD_DT(HOAT_DONG hD)
        {    //DT_Ten,DT_DaiDien,DT_SDT,DT_Email,DT_NoiDung,ID_DB
            List<HD_DOITAC> List = hD.HD_DOITAC.ToList();
            foreach (HD_DOITAC DT in List)
            {
                dgvDoiTac.Rows.Add(DT.DOI_TAC.TenDoiTac, DT.DOI_TAC.DaiDien, DT.DOI_TAC.SDT, DT.DOI_TAC.Email, DT.NoiDung, DT.DOI_TAC.ID_DoiTac);
            }
        }

        private void LoadHD_TT(HOAT_DONG hD)
        {    //TT_Name, TT_Rep, TT_SDT, TT_Email, TT_Notes, TT_IDDB
            List<HD_TAITRO> List = hD.HD_TAITRO.ToList();
            foreach (HD_TAITRO TT in List)
            {
                dgvTaiTro.Rows.Add(TT.TAI_TRO.TenTaiTro, TT.TAI_TRO.DaiDien, TT.TAI_TRO.SDT, TT.TAI_TRO.Email, TT.NoiDung, TT.TAI_TRO.ID_TaiTro);
            }
        }

        private void LoadHD_TC(HOAT_DONG hD) //Lấy thông tin tài chính mới nhất
        {
            TAI_CHINH Latest = hD.TAI_CHINH.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (Latest == null)
                return;
            txtTC_TieuDe.Text = Latest.TieuDe;
            txtTC_Khac.Text = Latest.Khac;
            numUEF.Value = Latest.UEF == null? 0 : (decimal)Latest.UEF;
            numTaiTro.Value = Latest.TaiTro == null ? 0 : (decimal)Latest.TaiTro;
        }

        private void frmAddHoatDong_Load(object sender, EventArgs e)
        {
            if (!isCreate) return;
            LoadKhoaCB();
           // btnShowSV.PerformClick();
        }
        //private void LoadLoaiCB()
        //{
        //    cbLoai.DataSource = new Dictionary<int, string>()
        //    {
        //        {0, "Sự kiện"},
        //        {1, "Dự Án"},
        //        {2, "Môn học"},
        //    }.ToList();
        //    cbLoai.DisplayMember = "Value";
        //    cbLoai.ValueMember = "Key";
        //    cbLoai.SelectedIndex = -1;
        //}private void LoadVaiTroCB()
        //{
        //    cbRole.DataSource = new Dictionary<int, string>()
        //    {
        //        {0, "Tham gia"},
        //        {1, "Tổ chức"},
        //    }.ToList();
        //    cbRole.DisplayMember = "Value";
        //    cbRole.ValueMember = "Key";
        //    cbRole.SelectedIndex = -1;
        //}

        private void LoadKhoaCB()
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

                    cbGV_Khoa.DataSource = khoa.ToList();
                    cbGV_Khoa.DisplayMember = "Ten";
                    cbGV_Khoa.ValueMember = "MaKH";
                    cbGV_Khoa.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Khoa:\n"+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

       /* private void button1_Click(object sender, EventArgs e)
        {
            if (gbSinhVien.Visible)
            {
                gbSinhVien.Visible = false;
            }
            else
            {
                gbSinhVien.Visible = true;
            }
        }*/

      /*  private void gbSinhVien_VisibleChanged(object sender, EventArgs e)
        {
            if (gbSinhVien.Visible){
                btnShowSV.Text = "Ẩn";
                gbSVList.Location = new Point(gbSinhVien.Location.X, gbSinhVien.Location.Y*2);
            }
            else { 
                btnShowSV.Text = "Hiện";
                gbSVList.Location = new Point(gbSinhVien.Location.X, gbSinhVien.Location.Y);
            }
        }*/

        private void btnAddSV_Click(object sender, EventArgs e)
        {
            if (ValidateSV())
            { //MSSV,HoTenSV,Khoa,Role,Notes_SV,DB_Khoa, DB_Role
                dgvSinhVien.Rows.Add(txtMSSV.Text, txtSVHoTen.Text, cbKhoa.Text, cbRole.Text, txtSVNotes.Text, cbKhoa.SelectedValue, cbRole.SelectedValue);
                clearSVFields();
                lblSV_TotalNumber.Text = dgvSinhVien.RowCount.ToString();
            }
            else return;
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvSinhVien.CurrentRow;
            if (row != null)
            {
                txtMSSV.Text = row.Cells["MSSV"].Value.ToString();
                txtMSSV.ReadOnly = true;
                txtSVHoTen.Text = row.Cells["HoTenSV"].Value.ToString();
                cbKhoa.SelectedValue = row.Cells["DB_Khoa"].Value != null? row.Cells["DB_Khoa"].Value : -1;
                cbRole.Text = row.Cells["Role"].Value.ToString() != null ? row.Cells["Role"].Value.ToString() : "";
                txtSVNotes.Text = row.Cells["Notes_SV"].Value.ToString();
                btnAddSV.Enabled = false;
            }
            else
                return;
            
        }

        private void kryptonBtnEdit_Click(object sender, EventArgs e)
        {
            if(txtMSSV.ReadOnly == false|| dgvSinhVien.CurrentRow == null) return;
            dgvSinhVien.CurrentRow.Cells["HoTenSV"].Value = txtSVHoTen.Text;
            dgvSinhVien.CurrentRow.Cells["Khoa"].Value = cbKhoa.Text;
            dgvSinhVien.CurrentRow.Cells["Role"].Value = cbRole.Text;
            dgvSinhVien.CurrentRow.Cells["Notes_SV"].Value = txtSVNotes.Text;
            dgvSinhVien.CurrentRow.Cells["DB_Khoa"].Value = cbKhoa.SelectedValue;
            clearSVFields();
        }
        public void clearSVFields()
        {
            txtMSSV.Clear();
            txtSVHoTen.Clear();
            cbKhoa.SelectedIndex = -1;
            cbRole.SelectedIndex = -1;
            txtSVNotes.Clear();
            txtMSSV.ReadOnly = false;
            btnAddSV.Enabled = true;
        }
        public bool ValidateSV()
        {
            if(txtMSSV.Text.Length == 0) 
            {
                MessageBox.Show("MSSV Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else 
            if (FindDuplicateMSSV(txtMSSV.Text.Trim()))
            {
                MessageBox.Show("MSSV Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; 
            }else
            return true;
        }
        public bool FindDuplicateMSSV(string MSSV)
        {
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MSSV"].Value.ToString().Trim() == MSSV)
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }
        //Cập nhật dgv_GV và trả về kết quả
        //Nếu không cập nhật được/không tìm thấy hàng nào với mã GV đã cho, trả về false
        //Nếu cập nhật thành công, return true
        //MaGV,HoTenLot,Ten,GVKhoa,GV_Role,GVKHoa_DB
        public bool Updatedgv_GV(DataRow GV)
        {
            foreach (DataGridViewRow row in dgv_GV.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MaGV"].Value.ToString().Trim() == GV[0].ToString())
                    {
                        row.Cells["HoTenLot"].Value = GV[1].ToString();
                        row.Cells["Ten"].Value = GV[2].ToString();
                        row.Cells["GVKhoa"].Value = GV[3].ToString();
                        row.Cells["GV_Role"].Value = GV[4].ToString();
                        row.Cells["GVKHoa_DB"].Value = GV[5].ToString();
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }
        //MSSV,HoTenSV,Khoa,Role,Notes_SV,DB_Khoa
        public bool UpdateDgv_SV(DataRow SV)
        {
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MSSV"].Value.ToString().Trim() == SV[0].ToString().Trim())
                    {
                        row.Cells["HoTenSV"].Value = SV[1].ToString().Trim();
                        row.Cells["Khoa"].Value = SV[2].ToString().Trim();
                        row.Cells["Role"].Value = SV[3].ToString().Trim();
                        row.Cells["Notes_SV"].Value = SV[4].ToString().Trim();
                        row.Cells["DB_Khoa"].Value = SV[5].ToString().Trim();
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }

        public bool FindDuplicateDT(DataRow DT)
        {
            foreach (DataGridViewRow row in dgvDoiTac.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells[0].Value.ToString().Trim() == DT[0].ToString().Trim() && row.Cells[1].Value.ToString().Trim() == DT[1].ToString().Trim() && row.Cells[2].Value.ToString().Trim() == DT[2].ToString().Trim() && row.Cells[3].Value.ToString().Trim() == DT[3].ToString().Trim())
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }
        public bool FindDuplicateTT(DataRow DT)
        {
            foreach (DataGridViewRow row in dgvTaiTro.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells[0].Value.ToString().Trim() == DT[0].ToString().Trim() && row.Cells[1].Value.ToString().Trim() == DT[1].ToString().Trim() && row.Cells[2].Value.ToString().Trim() == DT[2].ToString().Trim() && row.Cells[3].Value.ToString().Trim() == DT[3].ToString().Trim())
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }

        private void btnSVClear_Click(object sender, EventArgs e)
        {
            clearSVFields();
        }

        private void btnSVDel_Click(object sender, EventArgs e)
        {
            clearSVFields();
            if (dgvSinhVien.Rows.Count == 0 || dgvSinhVien.CurrentRow.Index < 0) return;  
            dgvSinhVien.Rows.RemoveAt(dgvSinhVien.CurrentRow.Index);
            lblSV_TotalNumber.Text = dgvSinhVien.RowCount.ToString();
        }

        private bool frmValidate()
        {
            if (isEmpty())
                return false;
            else
                return true;
        }
        private bool isEmpty()
        {
            if (txtTenHD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Tên HD đang trống","Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                } 
            else return false;
        }
        private void btnAddHD_Click(object sender, EventArgs e)
        {
            //Validate Here()
            if (!frmValidate())
                return;
            else
                SaveHDToDB();
           // else return;
        }
        private void SaveHDToDB()
        {
            try
            {
                using (Context db = new Context())
                {   //tim hoat dong theo ten va chua bi xoa
                    HOAT_DONG hoatDong = isCreate? new HOAT_DONG() : db.HOAT_DONG.Where<HOAT_DONG>(hd=>hd.Hide.Value != true && hd.TenHoatDong.Contains(iniName)).FirstOrDefault();
                    hoatDong.TenHoatDong = txtTenHD.Text.Trim();
                    hoatDong.Loai = cbLoai.Text.Trim();
                    hoatDong.NgayBatDau = dtpNgayBD.Value;
                    hoatDong.NgayKetThuc = dtpNgayKT.Value;
                    hoatDong.CreatedDate = DateTime.Now;
                    hoatDong.Hide = false;
                    if (isCreate) //lưu hoạt động vào DB để tạo ID trước
                    {
                        db.HOAT_DONG.Add(hoatDong);
                        db.SaveChanges();
                    }    
                    AddOrUpdateHD_SinhVien(hoatDong,db);
                    AddOrUpdateHD_GV(hoatDong,db);
                    AddOrUpdateHD_DoiTac(hoatDong, db);
                    AddOrUpdateHD_TaiTro(hoatDong, db);
                    AddHD_TaiChinh(hoatDong, db);

                    if (db.HOAT_DONG.Find(hoatDong.MaHD) != null)
                    {
                        db.Entry(hoatDong).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Thành công", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                       // hoatDong.MaHD = txtMaHD.Text.Trim();
                        db.HOAT_DONG.Add(hoatDong);
                        db.SaveChanges();
                        MessageBox.Show("Thành công","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    Close();
                }    
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddOrUpdateHD_SinhVien(HOAT_DONG hd, Context db)
        {
                foreach (DataGridViewRow dr in dgvSinhVien.Rows)
                {
                    HD_SINHVIEN SVList = db.HD_SINHVIEN.Find(hd.MaHD, dr.Cells["MSSV"].Value.ToString());
                    if (SVList == null)
                    {
                        SVList = new HD_SINHVIEN();
                        SVList.MaHD = hd.MaHD;
                        SVList.MSSV = dr.Cells["MSSV"].Value.ToString().Trim();
                        SVList.VaiTro = dr.Cells["Role"].Value.ToString().Trim();
                        SVList.GhiChu = dr.Cells["Notes_SV"].Value.ToString().Trim();
                        AddOrUpdateSV(SVList, db, dr);
                        hd.HD_SINHVIEN.Add(SVList);
                    }
                    else
                    {
                        SVList.VaiTro = dr.Cells["Role"].Value.ToString().Trim();
                        SVList.GhiChu = dr.Cells["Notes_SV"].Value.ToString().Trim();
                        AddOrUpdateSV(SVList, db, dr);
                        db.Entry(SVList).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }  
        }
        private void AddOrUpdateSV(HD_SINHVIEN lst, Context db, DataGridViewRow dr)
        {
            if (db.SINH_VIEN.Find(dr.Cells["MSSV"].Value.ToString()) != null)
            {
                SINH_VIEN sv = db.SINH_VIEN.Find(lst.MSSV);
                sv.HoTen = dr.Cells["HoTenSV"].Value.ToString().Trim();
                sv.Khoa = dr.Cells["DB_Khoa"].Value.ToString().Trim();
                sv.Hide = false;
                db.Entry(sv).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                lst.SINH_VIEN = new SINH_VIEN();
                lst.SINH_VIEN.MSSV = dr.Cells["MSSV"].Value.ToString().Trim();
                lst.SINH_VIEN.HoTen = dr.Cells["HoTenSV"].Value.ToString().Trim();
                lst.SINH_VIEN.Khoa = dr.Cells["DB_Khoa"].Value.ToString().Trim();
                lst.SINH_VIEN.Hide = false;

            }
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////
        /// </summary>

        private void AddOrUpdateHD_DoiTac(HOAT_DONG hd, Context db)
        {
            foreach (DataGridViewRow dr in dgvDoiTac.Rows)
            {
                DOI_TAC dtTemp = FindDTByName(dr.Cells["DT_Ten"].Value.ToString().Trim());
                if (dtTemp != null) dr.Cells["ID_DB"].Value = dtTemp.ID_DoiTac;
                else dr.Cells["ID_DB"].Value = -1;
                //cho trường hợp nhập vào bằng import excel, ko cần ID, nếu ĐT đã có sẵn trong CSDL, tự điền lại ID của đối tác đó vào
                //Nếu đối tác đó không có sẵn, add vào như bth
                HD_DOITAC hD_DT = db.HD_DOITAC.Find(dr.Cells["ID_DB"].Value, hd.MaHD);
                if (hD_DT == null)
                {
                    hD_DT = new HD_DOITAC();
                    hD_DT.MaHD = hd.MaHD;
                    AddOrUpdateDT(hD_DT, db, dr);
                    hD_DT.ID_DoiTac = hD_DT.DOI_TAC.ID_DoiTac;
                    hD_DT.NoiDung = dr.Cells["DT_NoiDung"].Value.ToString().Trim();
                    hd.HD_DOITAC.Add(hD_DT);
                    db.SaveChanges();
                }
                else
                {
                    hD_DT.NoiDung = dr.Cells["DT_NoiDung"].Value.ToString().Trim();
                    AddOrUpdateDT(hD_DT, db, dr);
                    db.Entry(hD_DT).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }        //DT_Ten,DT_DaiDien,DT_SDT,DT_Email,DT_NoiDung,ID_DB
        private void AddOrUpdateDT(HD_DOITAC lst, Context db, DataGridViewRow dr)
        {
            if (db.DOI_TAC.Find(dr.Cells["ID_DB"].Value) != null)
            {
                DOI_TAC dt = db.DOI_TAC.Find(dr.Cells["ID_DB"].Value);
                dt.TenDoiTac = dr.Cells["DT_Ten"].Value.ToString().Trim();
                dt.DaiDien = dr.Cells["DT_DaiDien"].Value.ToString().Trim();
                dt.SDT = dr.Cells["DT_SDT"].Value.ToString().Trim();
                dt.Email = dr.Cells["DT_Email"].Value.ToString().Trim();
                dt.Hide = false;
                db.Entry(dt).State = EntityState.Modified;
                lst.DOI_TAC = dt;
                db.SaveChanges();
            }
            else
            {
                lst.DOI_TAC = new DOI_TAC();
                lst.DOI_TAC.TenDoiTac = dr.Cells["DT_Ten"].Value.ToString().Trim();
                lst.DOI_TAC.DaiDien = dr.Cells["DT_DaiDien"].Value.ToString().Trim();
                lst.DOI_TAC.SDT = dr.Cells["DT_SDT"].Value.ToString().Trim();
                lst.DOI_TAC.Email = dr.Cells["DT_Email"].Value.ToString().Trim();
                lst.DOI_TAC.Hide = false;
                db.DOI_TAC.Add(lst.DOI_TAC);
                db.SaveChanges();
            }
        }

        //TT_Name, TT_Rep, TT_SDT, TT_Email, TT_Notes, TT_IDDB
        private void AddOrUpdateHD_TaiTro(HOAT_DONG hd, Context db)
        {
            foreach (DataGridViewRow dr in dgvTaiTro.Rows)
            {
                TAI_TRO dtTemp = FindTTByName(dr.Cells["TT_Name"].Value.ToString().Trim());
                if (dtTemp != null) dr.Cells["TT_IDDB"].Value = dtTemp.ID_TaiTro;
                HD_TAITRO hD_TT = db.HD_TAITRO.Find(dr.Cells["TT_IDDB"].Value, hd.MaHD);
                if (hD_TT == null)
                {
                    hD_TT = new HD_TAITRO();
                    hD_TT.MaHD = hd.MaHD;
                    AddOrUpdateTT(hD_TT, db, dr);
                    hD_TT.ID_TaiTro = hD_TT.TAI_TRO.ID_TaiTro;
                    hD_TT.NoiDung = dr.Cells["TT_Notes"].Value.ToString().Trim();
                    hd.HD_TAITRO.Add(hD_TT);
                    db.SaveChanges();
                }
                else
                {
                    hD_TT.NoiDung = dr.Cells["TT_Notes"].Value.ToString().Trim();
                    AddOrUpdateTT(hD_TT, db, dr);
                    db.Entry(hD_TT).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }     
        private void AddOrUpdateTT(HD_TAITRO lst, Context db, DataGridViewRow dr)
        {
            if (db.TAI_TRO.Find(dr.Cells["TT_IDDB"].Value) != null)
            {
                TAI_TRO tt = db.TAI_TRO.Find(dr.Cells["TT_IDDB"].Value);
                tt.TenTaiTro = dr.Cells["TT_Name"].Value.ToString().Trim();
                tt.DaiDien = dr.Cells["TT_Rep"].Value.ToString().Trim();
                tt.SDT = dr.Cells["TT_SDT"].Value.ToString().Trim();
                tt.Email = dr.Cells["TT_Email"].Value.ToString().Trim();
                tt.Hide = false;
                lst.TAI_TRO = tt;
                db.Entry(tt).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                lst.TAI_TRO = new TAI_TRO();
                lst.TAI_TRO.TenTaiTro = dr.Cells["TT_Name"].Value.ToString().Trim();
                lst.TAI_TRO.DaiDien = dr.Cells["TT_Rep"].Value.ToString().Trim();
                lst.TAI_TRO.SDT = dr.Cells["TT_SDT"].Value.ToString().Trim();
                lst.TAI_TRO.Email = dr.Cells["TT_Email"].Value.ToString().Trim();
                lst.TAI_TRO.Hide = false;
                db.TAI_TRO.Add(lst.TAI_TRO);
                db.SaveChanges();
            }
        }

        private void AddHD_TaiChinh (HOAT_DONG hd, Context db)
        {
            TAI_CHINH tc = new TAI_CHINH();
            tc.MaHD = hd.MaHD;
            tc.UEF = numUEF.Value;
            tc.TaiTro = numTaiTro.Value;
            tc.TieuDe = txtTC_TieuDe.Text;
            tc.Khac = txtTC_Khac.Text;
            tc.CreatedDate = DateTime.Now;
            tc.Hide = false;
            db.TAI_CHINH.Add(tc);
            db.SaveChanges();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSVFind_Click(object sender, EventArgs e)
        {
            using (Context db = new Context())
            {
                SINH_VIEN sv = db.SINH_VIEN.Find(txtMSSV.Text);
                if (sv != null)
                {
                    txtSVHoTen.Text = sv.HoTen.ToString();
                    cbKhoa.SelectedValue = sv.Khoa;
                }
                else 
                    return;
            }
        }

        private void dtpNgayKT_ValueChanged(object sender, EventArgs e)
        {
            if(dtpNgayBD.Value>dtpNgayKT.Value)
            {
                MessageBox.Show("Lỗi chọn ngày!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                dtpNgayKT.Value = dtpNgayBD.Value = DateTime.Now ;
            }
        }

        private void btnAddGV_Click(object sender, EventArgs e)
        {
            //MaGV,HoTenLot,Ten,GVKhoa,GV_Role,GVKHoa_DB
            if (ValidateGV())
            { 
                dgv_GV.Rows.Add(txtMaGV.Text, txtGVHoTenLot.Text, txtTenGV.Text, cbGV_Khoa.Text,cbGV_Role.Text , cbGV_Khoa.SelectedValue);
                ClearGVFields();
                lblGV_TotalNumber.Text = dgv_GV.RowCount.ToString();
            }
            else return;
        }
        private bool ValidateGV()
        {
            if (txtMaGV.Text.Length == 0)
            {
                MessageBox.Show("Mã Giảng Viên Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            if (FindDupMaGV())
            {
                MessageBox.Show("Mã Giảng Viên Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }
        private bool FindDupMaGV()
        {
            foreach (DataGridViewRow row in dgv_GV.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MaGV"].Value.ToString() == txtMaGV.Text)
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }
        private void ClearGVFields()
        {
            txtMaGV.Clear();
            txtGVHoTenLot.Clear();
            cbGV_Khoa.SelectedIndex = -1;
            txtTenGV.Clear();
            cbGV_Role.SelectedIndex = -1;
            txtMaGV.ReadOnly = false;
        }

        private void btnGVShow_Click(object sender, EventArgs e)
        {
            if (gbHD_GV.Visible)
            {
                gbHD_GV.Visible = false;
                btnGVExport.Text = "Hiện";
            }
            else
            {
                gbHD_GV.Visible = true;
                btnGVExport.Text = "Ẩn";
            }
        }

        private void dgv_GV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgv_GV.CurrentRow;
            if (row != null) //MaGV,HoTenLot,Ten,GVKhoa,GV_Role,GVKHoa_DB
            {
                txtMaGV.Text        = row.Cells["MaGV"].Value.ToString();
                txtMaGV.ReadOnly    = true;
                txtGVHoTenLot.Text  = row.Cells["HoTenLot"].Value.ToString();
                txtTenGV.Text       = row.Cells["Ten"].Value.ToString();
                cbGV_Khoa.SelectedValue = row.Cells["GVKHoa_DB"].Value != null ? row.Cells["GVKHoa_DB"].Value : -1;
                cbGV_Role.Text       = row.Cells["GV_Role"].Value == null ? "" : row.Cells["GV_Role"].Value.ToString();
            }
            else
                return;
        }

        private void btnClearGV_Click(object sender, EventArgs e)
        {
            ClearGVFields();
        }

        private void btnEditGV_Click(object sender, EventArgs e)
        {
            if (txtMaGV.ReadOnly == false || dgv_GV.CurrentRow == null) return;
            dgv_GV.CurrentRow.Cells["HoTenLot"].Value = txtGVHoTenLot.Text;
            dgv_GV.CurrentRow.Cells["Ten"].Value = txtTenGV.Text;
            dgv_GV.CurrentRow.Cells["GVKhoa"].Value = cbGV_Khoa.Text;
            dgv_GV.CurrentRow.Cells["GV_Role"].Value = cbGV_Role.Text;
            dgv_GV.CurrentRow.Cells["GVKHoa_DB"].Value = cbGV_Khoa.SelectedValue;
            ClearGVFields();
        }

        private void btnDelGV_Click(object sender, EventArgs e)
        {
            ClearGVFields();
            if (dgv_GV.Rows.Count == 0 || dgv_GV.CurrentRow.Index < 0) return;
            dgv_GV.Rows.RemoveAt(dgv_GV.CurrentRow.Index);
            lblGV_TotalNumber.Text = dgv_GV.RowCount.ToString();
        }

        private void AddOrUpdateHD_GV(HOAT_DONG hd, Context db)
        {
            foreach (DataGridViewRow dr in dgv_GV.Rows)
            {
                HD_GIANGVIEN HD_GV = db.HD_GIANGVIEN.Find(hd.MaHD, dr.Cells["MaGV"].Value.ToString());
                if (HD_GV == null)
                {
                    HD_GV = new HD_GIANGVIEN();
                    HD_GV.MaHD = hd.MaHD;
                    HD_GV.MaGV = dr.Cells["MaGV"].Value.ToString().Trim();
                    HD_GV.VaiTro = dr.Cells["GV_Role"].Value.ToString().Trim();
                    AddOrUpdateGV(HD_GV, db, dr);
                    hd.HD_GIANGVIEN.Add(HD_GV);
                }
                else
                {
                    HD_GV.VaiTro = dr.Cells["GV_Role"].Value.ToString().Trim();
                    AddOrUpdateGV(HD_GV, db, dr);
                    db.Entry(HD_GV).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        private void AddOrUpdateGV(HD_GIANGVIEN HD_GV, Context db, DataGridViewRow dr)
        {
            if (db.GIANG_VIEN.Find(HD_GV.MaGV) != null)
            {
                GIANG_VIEN gv = db.GIANG_VIEN.Find(HD_GV.MaGV);
                gv.HoTenLot = dr.Cells["HoTenLot"].Value.ToString().Trim();
                gv.Ten = dr.Cells["Ten"].Value.ToString().Trim();
                gv.Khoa = dr.Cells["GVKHoa_DB"].Value.ToString().Trim();
                //gv.DonVi = dr.Cells["GV_Role"].Value.ToString().Trim();
                gv.Hide = false;
                db.Entry(gv).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                HD_GV.GIANG_VIEN = new GIANG_VIEN();
                HD_GV.GIANG_VIEN.MaGV = HD_GV.MaGV;
                HD_GV.GIANG_VIEN.HoTenLot = dr.Cells["HoTenLot"].Value.ToString().Trim();
                HD_GV.GIANG_VIEN.Ten = dr.Cells["Ten"].Value.ToString().Trim();
                HD_GV.GIANG_VIEN.Khoa = dr.Cells["GVKHoa_DB"].Value.ToString().Trim();
                //HD_GV.GIANG_VIEN.DonVi = dr.Cells["GV_Role"].Value.ToString().Trim();
                HD_GV.GIANG_VIEN.Hide = false;
            }
        }

        private void btnFindGV_Click(object sender, EventArgs e)
        {
            using (Context db = new Context())
            {
                GIANG_VIEN gv = db.GIANG_VIEN.Find(txtMaGV.Text);
                if (gv != null)
                {
                    txtMaGV.Text = gv.MaGV;
                    txtMaGV.ReadOnly = true;
                    txtGVHoTenLot.Text = gv.HoTenLot;
                    txtTenGV.Text = gv.Ten;
                    cbGV_Khoa.SelectedValue = gv.Khoa;
                    //cbGV_Role.Text = gv.DonVi;
                }
                else
                    return;
            }
        }
        private DOI_TAC FindDTByName(string name)
        {
            try
            {
                DOI_TAC result = new DOI_TAC();
                using (Context db = new Context())
                {
                    DOI_TAC dt = (from k in db.DOI_TAC
                                  where (k.Hide == false && k.TenDoiTac.Contains(name))
                                  select k).FirstOrDefault();
                    result = dt;
                }
                return result;
            }
            catch { return null; }
        }

        private void btnDT_Find_Click(object sender, EventArgs e)
        {
            DOI_TAC dt = FindDTByName(txtDT_Ten.Text);
            if (dt != null)
            {
                DT_ID = dt.ID_DoiTac; //Lưu biến tạm để xử lý
                txtDT_Ten.Text = dt.TenDoiTac;
                txtDT_Ten.ReadOnly = true;
                txtDT_Rep.Text = dt.DaiDien;
                txtDT_SDT.Text = dt.SDT;
                txtDT_Email.Text = dt.Email;
            }
            else 
             return; 
        }

        private void btnDT_Clear_Click(object sender, EventArgs e)
        {
            ClearFieldsDT();
        }
        public void ClearFieldsDT()
        {
            txtDT_Ten.ReadOnly = false;
            txtDT_Ten.Clear();
            txtDT_Rep.Clear();
            txtDT_SDT.Clear();
            txtDT_Email.Clear();
            txtDT_NoiDung.Clear();
        }
        //DT_Ten,DT_DaiDien,DT_SDT,DT_Email,DT_NoiDung,ID_DB
        private void btnDT_Add_Click(object sender, EventArgs e)
        {
            if (ValidateDT())
            {
                dgvDoiTac.Rows.Add(txtDT_Ten.Text, txtDT_Rep.Text, txtDT_SDT.Text, txtDT_Email.Text, txtDT_NoiDung.Text,DT_ID);
                DT_ID = -1; //reset DT_ID sau khi sd
                ClearFieldsDT();
            }
            else return;
        }

        private bool ValidateDT()
        {
            if (txtDT_Ten.Text.Length == 0)
            {
                MessageBox.Show("Tên Đối Tác Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            if (FindDupDT())
            {
                MessageBox.Show("Tên Đối Tác Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }
        private bool FindDupDT()
        {
            foreach (DataGridViewRow row in dgvDoiTac.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["DT_Ten"].Value.ToString() == txtDT_Ten.Text)
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }

        private void btnDT_Edit_Click(object sender, EventArgs e)
        {
            if (dgvDoiTac.CurrentRow == null) return;
            dgvDoiTac.CurrentRow.Cells["DT_Ten"].Value = txtDT_Ten.Text;
            dgvDoiTac.CurrentRow.Cells["DT_DaiDien"].Value = txtDT_Rep.Text;
            dgvDoiTac.CurrentRow.Cells["DT_SDT"].Value = txtDT_SDT.Text;
            dgvDoiTac.CurrentRow.Cells["DT_Email"].Value = txtDT_Email.Text;
            dgvDoiTac.CurrentRow.Cells["DT_NoiDung"].Value = txtDT_NoiDung.Text;
            ClearFieldsDT();
        }

        private void btnDT_Del_Click(object sender, EventArgs e)
        {
            ClearFieldsDT();
            if (dgvDoiTac.Rows.Count == 0 || dgvDoiTac.CurrentRow.Index < 0) return;
            dgvDoiTac.Rows.RemoveAt(dgvDoiTac.CurrentRow.Index);
        }

        private void dgvDoiTac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDoiTac.CurrentRow;
            if (row != null) //DT_Ten,DT_DaiDien,DT_SDT,DT_Email,DT_NoiDung,ID_DB
            {
                txtDT_Ten.Text      = row.Cells["DT_Ten"].Value.ToString();
                //txtDT_Ten.ReadOnly  = true;
                txtDT_Rep.Text      = row.Cells["DT_DaiDien"].Value.ToString();
                txtDT_SDT.Text      = row.Cells["DT_SDT"].Value.ToString();
                txtDT_Email.Text    = row.Cells["DT_Email"].Value.ToString();
                txtDT_NoiDung.Text  = row.Cells["DT_NoiDung"].Value.ToString();
            }
            else
                return;
        }

        private TAI_TRO FindTTByName(string name)
        {
            try
            {
                TAI_TRO result = new TAI_TRO();
                using (Context db = new Context())
                {
                    TAI_TRO tt = (from s in db.TAI_TRO
                                  where s.TenTaiTro.Contains(name) && s.Hide == false
                                  select s).FirstOrDefault();
                    result = tt;
                }
                return result;
            }
            catch { return null; }
        }

        private KHOA FindKhoaByName(string name)
        {
            try
            {
                KHOA result = new KHOA();
                using (Context db = new Context())
                {
                    KHOA tt = (from s in db.KHOAs
                                  where s.TenKhoa.Contains(name) && s.Hide == false
                                  select s).FirstOrDefault();
                    result = tt;
                }
                return result;
            }
            catch { return null; }
        }

        private void btnTT_Find_Click(object sender, EventArgs e)
        {
            TAI_TRO tt = FindTTByName(txtTT_Name.Text);
            if (tt != null)
            {
                TT_ID = tt.ID_TaiTro; //Lưu biến tạm để xử lý
                txtTT_Name.Text = tt.TenTaiTro;
                txtTT_Name.ReadOnly = true;
                txtTT_Rep.Text = tt.DaiDien;
                txtTT_SDT.Text = tt.SDT;
                txtTT_Email.Text = tt.Email;
            }
            else
              return; 
        }

        public void ClearFieldsTT()
        {
            btnTT_Add.Enabled = true;
            txtTT_Name.ReadOnly = false;
            txtTT_Name.Clear();
            txtTT_Rep.Clear();
            txtTT_SDT.Clear();
            txtTT_Email.Clear();
            txtTT_NoiDung.Clear();
        }

        private void btnTT_Clear_Click(object sender, EventArgs e)
        {
            ClearFieldsTT();
        }

        private void btnTT_Add_Click(object sender, EventArgs e)
        {
            if (ValidateTT())
            {
                dgvTaiTro.Rows.Add(txtTT_Name.Text, txtTT_Rep.Text, txtTT_SDT.Text, txtTT_Email.Text, txtTT_NoiDung.Text, TT_ID);
                TT_ID = -1; //reset DT_ID sau khi sd
                ClearFieldsTT();
            }
            else return;
        }
        //TT_Name, TT_Rep, TT_SDT, TT_Email, TT_Notes, TT_IDDB
        private bool ValidateTT()
        {
            if (txtTT_Name.Text.Length == 0)
            {
                MessageBox.Show("Tên Nhà Tài Trợ Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            if (FindDupTT())
            {
                MessageBox.Show("Tên Nhà Tài Trợ Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }
        private bool FindDupTT()
        {
            foreach (DataGridViewRow row in dgvTaiTro.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["TT_Name"].Value.ToString() == txtTT_Name.Text)
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }

        private void btnTT_Edit_Click(object sender, EventArgs e)
        {
            if (dgvTaiTro.CurrentRow == null) return;
            dgvTaiTro.CurrentRow.Cells["TT_Name"].Value = txtTT_Name.Text;
            dgvTaiTro.CurrentRow.Cells["TT_Rep"].Value = txtTT_Rep.Text;
            dgvTaiTro.CurrentRow.Cells["TT_SDT"].Value = txtTT_SDT.Text;
            dgvTaiTro.CurrentRow.Cells["TT_Email"].Value = txtTT_Email.Text;
            dgvTaiTro.CurrentRow.Cells["TT_Notes"].Value = txtTT_NoiDung.Text;
            ClearFieldsTT();
        }

        private void btnTT_Del_Click(object sender, EventArgs e)
        {
            ClearFieldsTT();
            if (dgvTaiTro.Rows.Count == 0 || dgvTaiTro.CurrentRow.Index < 0) return;
            dgvTaiTro.Rows.RemoveAt(dgvTaiTro.CurrentRow.Index);
        }

        private void dgvTaiTro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnTT_Add.Enabled = false;
            DataGridViewRow row = dgvTaiTro.CurrentRow;
            if (row != null) //TT_Name, TT_Rep, TT_SDT, TT_Email, TT_Notes, TT_IDDB
            {
                txtTT_Name.Text = row.Cells["TT_Name"].Value.ToString();
                //txtDT_Ten.ReadOnly  = true;
                txtTT_Rep.Text = row.Cells["TT_Rep"].Value.ToString();
                txtTT_SDT.Text = row.Cells["TT_SDT"].Value.ToString();
                txtTT_Email.Text = row.Cells["TT_Email"].Value.ToString();
                txtTT_NoiDung.Text = row.Cells["TT_Notes"].Value.ToString();
            }
            else
                return;
        }

        private void btnExportSV_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);

                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvSinhVien.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgvSinhVien.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvSinhVien.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvSinhVien.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvSinhVien.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns(0);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi:\n\n " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn File nhập";
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportSV(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import không thành công!\n\n" + ex.Message);
                }
            }
            lblSV_TotalNumber.Text = dgvSinhVien.RowCount.ToString();
        }
        private void ImportSV(string path)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                for (int i = excelWorksheet.Dimension.Start.Column; i <=  excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString().Trim());
                }
                for (int i = excelWorksheet.Dimension.Start.Row + 1; i <=  excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRow = new List<string>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        if (excelWorksheet.Cells[i, j].Value == null)
                        {
                            listRow.Add("");
                            continue;
                        }
                        listRow.Add(excelWorksheet.Cells[i,j].Value.ToString().Trim());
                    }
                    dt.Rows.Add(listRow.ToArray());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    string nameTemp = dr[2].ToString().Trim();
                    //fill mã khoa
                    KHOA temp = FindKhoaByName(nameTemp);
                    if(temp == null)
                    {
                        DialogResult d = MessageBox.Show("Đơn vị "+nameTemp + " chưa được tạo, bạn có muốn tạo mới không?\n\n\n Chú ý: Đơn vị tạo tự động sẽ có ngày thành lập là ngày được tạo", "Chưa tồn tại đơn vị", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                        if (d == DialogResult.Yes)
                        {
                            //Tao makhoa tu dong
                            string MaKhoa = string.Join(string.Empty, nameTemp.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0])).ToUpper() ;
                            temp = new KHOA();
                            temp.MaKhoa = MaKhoa;
                            temp.TenKhoa = nameTemp;
                            temp.NgayThanhLap = DateTime.Now;
                            temp.Hide = false;
                            try
                            {
                                using (Context db = new Context())
                                {
                                    db.KHOAs.Add(temp);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi tạo khoa mới, vui lòng liên hệ admin\n\n"+ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else continue;
                    }    
                    dr[5] = temp.MaKhoa;
                    //so từng hàng trong file excel với bảng dgv hiện tại, nếu trùng, cập nhật, không trùng, thêm vào dgv
                    if (UpdateDgv_SV(dr)) continue;
                    dgvSinhVien.Rows.Add(dr.ItemArray);

                }
            }
        }


        private void btnGVExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);

                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgv_GV.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgv_GV.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgv_GV.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgv_GV.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgv_GV.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns(0);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi:\n\n " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGVImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn File nhập";
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportGV(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import không thành công!\n\n" + ex.Message);
                }
            }
            lblGV_TotalNumber.Text = dgv_GV.RowCount.ToString();
        }

        private void ImportGV(string path)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString().Trim());
                }
                for (int i = excelWorksheet.Dimension.Start.Row + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRow = new List<string>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        if (excelWorksheet.Cells[i, j].Value == null)
                        {
                            listRow.Add("");
                            continue;
                        }
                        listRow.Add(excelWorksheet.Cells[i, j].Value.ToString().Trim());
                    }
                    dt.Rows.Add(listRow.ToArray());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    //fill mã khoa
                    string nameTemp = dr[3].ToString().Trim();
                    KHOA temp = FindKhoaByName(nameTemp);
                    if (temp == null)
                    {
                        DialogResult d = MessageBox.Show("Đơn vị " + nameTemp + " chưa được tạo, bạn có muốn tạo mới không?\n Chú ý: Đơn vị tạo tự động sẽ có ngày thành lập là ngày được tạo", "Chưa tồn tại đơn vị", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                        if (d == DialogResult.Yes)
                        {
                            //Tao makhoa tu dong
                            string MaKhoa = string.Join(string.Empty, nameTemp.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0])).ToUpper();
                            temp = new KHOA();
                            temp.MaKhoa = MaKhoa;
                            temp.TenKhoa = nameTemp;
                            temp.NgayThanhLap = DateTime.Now;
                            temp.Hide = false;
                            try
                            {
                                using (Context db = new Context())
                                {
                                    db.KHOAs.Add(temp);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi tạo khoa mới, vui lòng liên hệ admin\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else continue;
                    }
                    dr[5] = temp.MaKhoa;
                    //so từng hàng trong file excel với bảng dgv hiện tại, nếu trùng, cập nhật, không trùng, thêm vào dgv
                    if (Updatedgv_GV(dr)) continue;
                    dgv_GV.Rows.Add(dr.ItemArray);

                }
            }
        }

        private void btnDTExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);

                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvDoiTac.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgvDoiTac.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvDoiTac.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvDoiTac.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvDoiTac.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns(0);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi:\n\n " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDTImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn File nhập";
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportDT(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import không thành công!\n\n" + ex.Message);
                }
            }
        }

        private void ImportDT(string path)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString().Trim());
                }
                for (int i = excelWorksheet.Dimension.Start.Row + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRow = new List<string>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        if (excelWorksheet.Cells[i, j].Value == null)
                        {
                            listRow.Add("");
                            continue;
                        }
                        listRow.Add(excelWorksheet.Cells[i, j].Value.ToString().Trim());
                    }
                    dt.Rows.Add(listRow.ToArray());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (FindDuplicateDT(dr)) continue;
                    dgvDoiTac.Rows.Add(dr.ItemArray);

                }
            }
        }

        private void btnTTExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);

                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvTaiTro.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgvTaiTro.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvTaiTro.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvTaiTro.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvTaiTro.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns(0);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi:\n\n " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTTImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn File nhập";
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportTT(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import không thành công!\n\n" + ex.Message);
                }
            }
        }

        private void ImportTT(string path)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString().Trim());
                }
                for (int i = excelWorksheet.Dimension.Start.Row + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRow = new List<string>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        if (excelWorksheet.Cells[i, j].Value == null)
                        {
                            listRow.Add("");
                            continue;
                        }
                        listRow.Add(excelWorksheet.Cells[i, j].Value.ToString().Trim());
                    }
                    dt.Rows.Add(listRow.ToArray());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (FindDuplicateTT(dr)) continue;
                    dgvTaiTro.Rows.Add(dr.ItemArray);

                }
            }
        }

        private void label34_Click(object sender, EventArgs e)
        {
            lblSV_TotalNumber.Text = dgvSinhVien.RowCount.ToString();
        }

        private void label35_Click(object sender, EventArgs e)
        {
            lblGV_TotalNumber.Text = dgv_GV.RowCount.ToString();
        }
    }
}
