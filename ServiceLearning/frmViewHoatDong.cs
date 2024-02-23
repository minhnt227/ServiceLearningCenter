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
using ComponentFactory.Krypton.Toolkit;

namespace ServiceLearning
{
    public partial class frmViewHoatDong : Form
    {
        public frmViewHoatDong()
        {
            InitializeComponent();
            this.Text = "Tạo Mới Hoạt Động" ;
        }
        public void LoadFormView(int idHD)
        {
            try
            {
                using (Context db = new Context())
                {
                    HOAT_DONG hD = db.HOAT_DONG.Find(idHD);
                    //txtMaHD.Text = hD.MaHD.Trim();
                    txtTenHD.Text = hD.TenHoatDong;
                    txtLoai.Text = hD.Loai == null ? "" : hD.Loai;
                    txtDateBegin.Text = hD.NgayBatDau == null ? DateTime.Now.ToString() : hD.NgayBatDau.ToString();
                    txtDateEnd.Text = hD.NgayKetThuc == null ? DateTime.Now.ToString() : hD.NgayKetThuc.ToString();
                    LoadHD_SinhVien(hD);
                    LoadHD_GV(hD);
                    LoadHD_DT(hD);
                    LoadHD_TT(hD);
                    LoadHD_TC(hD);
                    foreach (Control g in panel1.Controls)
                    {
                        if (g is GroupBox)
                        {
                            foreach (Control c in g.Controls)
                            {
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
            numTaiTro.Value = Latest.TaiTro == null ? 0 : (decimal)Latest.UEF;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
