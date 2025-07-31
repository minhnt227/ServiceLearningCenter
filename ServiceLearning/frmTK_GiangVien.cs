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
    public partial class frmTK_GiangVien : Form
    {
        Context db = new Context();
        public frmTK_GiangVien()
        {
            InitializeComponent();
        }
        public void DisplayCMBKhoa(ComboBox a)
        {
            var kh = db.KHOAs.Where(s=>s.Hide == false).Select(s => s);
            a.DataSource = kh.ToList();
            a.ValueMember = "MaKhoa";
            a.DisplayMember = "TenKhoa";
            a.SelectedIndex = -1;

        }
        private void frmTK_GiangVien_Load(object sender, EventArgs e)
        {   
            cmbKhoa.SelectedIndex = -1;
            DisplayCMBKhoa(cmbKhoa);    
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            ThongKeGiangVien();
            Xoa();
            btnLoc.Enabled = false;
        }
        private void ThongKeGiangVien()
        {
            try
            {
                dgvGV.Rows.Clear();
                dgvGV.Refresh();
                this.dgvGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstMaGV = new List<string>();
                List<string> lstTenGV = new List<string>();
                List<string> lstHoTenLotGV = new List<string>();
                lstMaGV = db.GIANG_VIEN.Where(x=>x.Hide == false).Select(x => x.MaGV).Take(100).ToList();
                lstTenGV = db.GIANG_VIEN.Where(x => x.Hide == false).Select(x => x.Ten).Take(100).ToList();
                lstHoTenLotGV = db.GIANG_VIEN.Where(x => x.Hide == false).Select(x => x.HoTenLot).Take(100).ToList();
                for (int j = 0; j < lstMaGV.Count; j++)
                {
                    string MaGV = lstMaGV[j];
                    string tenGV = lstTenGV[j];
                    string HoTenLotGV = lstHoTenLotGV[j];
                    dgvGV.Rows.Add();
                    dgvGV.Rows[j].Cells[0].Value = j + 1;
                    dgvGV.Rows[j].Cells[1].Value = MaGV;
                    dgvGV.Rows[j].Cells[2].Value = HoTenLotGV;
                    dgvGV.Rows[j].Cells[3].Value = tenGV;
                    List<string> khoa = (from s in db.GIANG_VIEN
                                         where s.MaGV == MaGV && s.Hide == false
                                         select (s.KHOA1.TenKhoa)).ToList();
                    dgvGV.Rows[j].Cells[4].Value = khoa[0];
                    List<int> lstMaHD = new List<int>();
                    lstMaHD = (from s in db.GIANG_VIEN
                               join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                               where s.MaGV == MaGV && b.HOAT_DONG.Hide == false
                               select (b.MaHD)).ToList();
                    if (lstMaHD.Count == 0) dgvGV.Rows[j].Cells[5].Value = " ";
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
                        dgvGV.Rows[j].Cells[5].Value = TenHD;
                    }
                }
                Xoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void Xoa()
        {
            int n = dgvGV.Rows.Count;
            //MessageBox.Show(n.ToString());
            for (int i = 0; i < n; i++)
            {
                if (dgvGV.Rows[i].Cells[5].Value.ToString() == " " || dgvGV.Rows[i].Cells[4].Value.ToString() == " ")
                {
                    //Object stt = dgvSV.Rows[i].Cells[0].Value;
                    dgvGV.Rows.RemoveAt(dgvGV.Rows[i].Index);
                    i--;
                    n--;
                    // dgvSV.Rows[i+1].Cells[0].Value = stt;
                }
            }
            for (int i = 0; i < n - 1; i++)
            {
                dgvGV.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvGV, sfd.FileName);
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

        private void cmbKhoa_SelectedValueChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            cmbKhoa.SelectedIndex = -1;
            dgvGV.Rows.Clear();
            dgvGV.Refresh();
            ThongKeGiangVien();
            Xoa();
            btnLoc.Enabled = false;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocGiangVien();
        }
        private void LocGiangVien()
        {
            try
            {
                // 1. Clear existing data source and refresh
                // Setting DataSource to null first helps prevent issues if the DGV was previously bound.
                dgvGV.DataSource = null;
                dgvGV.Rows.Clear(); // Ensure rows are cleared if DataSource was previously set
                dgvGV.Refresh();

                // Set WrapMode once, ideally in form load or DGV init.
                // If this is called frequently, consider setting it only once during initialization.
                this.dgvGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // 2. Retrieve filter values from UI controls
                string Ten      = txtName.Text.Trim();
                string MaKhoa   = cmbKhoa.SelectedIndex != -1 ? cmbKhoa.SelectedValue?.ToString().Trim() : null;
                DateTime? dateBD;
                if (string.IsNullOrEmpty(dtpBD.Text.Trim()))
                    dateBD = null;
                else
                    dateBD = Convert.ToDateTime(dtpBD.Text.Trim());

                DateTime? dateKT;
                if (string.IsNullOrEmpty(dtpKT.Text.Trim()))
                    dateKT = null;
                else
                    dateKT = Convert.ToDateTime(dtpKT.Text.Trim());

                // 3. Construct the LINQ query to fetch filtered data
                // Start with all non-hidden lecturers.
                var dataGV = db.GIANG_VIEN.Where(gv => gv.Hide == false);

                // Apply faculty filter if a faculty is selected
                if (!string.IsNullOrEmpty(MaKhoa))
                {
                    dataGV = dataGV.Where(gv => gv.Khoa.Trim() == MaKhoa);
                }
                if (!string.IsNullOrEmpty(Ten))
                {
                    // Use .Contains() for partial matching and .ToLower() for case-insensitive search.
                    // Add null check for gv.Ten to prevent NullReferenceException.
                    dataGV = dataGV.Where(gv => gv.Ten != null && gv.Ten.ToLower().Contains(Ten.ToLower()));
                }

                // Project the filtered lecturers into the GIANGVIEN.
                // This is where all necessary data is fetched in a single database query.
                var Queryresults = dataGV.Select(gv => new 
                {
                    MaGV = gv.MaGV.Trim(),
                    HoTenLot = gv.HoTenLot,
                    Ten = gv.Ten,
                    // Access the faculty name via the navigation property (KHOA1)
                    TenKhoa = gv.KHOA1.TenKhoa,
                    DSHoatDongThamGia = 
                    gv.HD_GIANGVIEN
                      .Where(hdgv => hdgv.HOAT_DONG.Hide == false && // Filter out hidden activities
                                     (dateBD == null || hdgv.HOAT_DONG.NgayBatDau  >= dateBD) &&
                                     (dateKT == null || hdgv.HOAT_DONG.NgayKetThuc <= dateKT ))
                      .Select(hdgv => hdgv.HOAT_DONG)
                      .ToList() // Materialize the list of activity names for string.Join
                
                }).ToList(); // Execute the main query and bring all results into memory

                var results = Queryresults.Select(item => new TK_GiangVienDisplayDto
                {
                    MaGV = item.MaGV,
                    HoTenLot = item.HoTenLot,
                    Ten = item.Ten,
                    TenKhoa = item.TenKhoa,
                    // Perform string.Join on the already materialized 'RelevantActivities' collection
                    TenHoatDongThamGia = "- " + string.Join("\n- ", item.DSHoatDongThamGia.Select(a => a.TenHoatDong))
                }).ToList();
                // 4. Add sequential numbering (STT) to the results in memory
                for (int i = 0; i < results.Count; i++)
                {
                    results[i].STT = i + 1;
                }

                // 5. Bind the processed data to the DataGridView
                dgvGV.DataSource = results;

                //cleanup
                Xoa();



                /* dgvGV.Rows.Clear();
                 dgvGV.Refresh();
                 this.dgvGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                 List<string> lstMaGV = new List<string>();
                 List<string> lstTenGV = new List<string>();
                 List<string> lstHoTenLotGV = new List<string>();
                 lstMaGV = db.GIANG_VIEN.Where(x=>x.Hide == false).Select(x => x.MaGV).ToList();
                 lstTenGV = db.GIANG_VIEN.Where(x => x.Hide == false).Select(x => x.Ten).ToList();
                 lstHoTenLotGV = db.GIANG_VIEN.Where(x => x.Hide == false).Select(x => x.HoTenLot).ToList();
                 for (int j = 0; j < lstMaGV.Count; j++)
                 {
                     string MaGV = lstMaGV[j];
                     string tenGV = lstTenGV[j];
                     string HoTenLotGV = lstHoTenLotGV[j];
                     dgvGV.Rows.Add();
                     dgvGV.Rows[j].Cells[0].Value = j + 1;
                     dgvGV.Rows[j].Cells[1].Value = MaGV;
                     dgvGV.Rows[j].Cells[2].Value = HoTenLotGV;
                     dgvGV.Rows[j].Cells[3].Value = tenGV;
                     if (cmbKhoa.SelectedIndex == -1)
                     { 
                         List<string> khoa = (from s in db.GIANG_VIEN
                                              where s.MaGV == MaGV && s.Hide == false
                                              select (s.KHOA1.TenKhoa)).ToList();
                         dgvGV.Rows[j].Cells[4].Value = khoa[0];
                     }
                     else
                     {   
                         string makhoa = cmbKhoa.SelectedValue.ToString();
                         //MessageBox.Show(makhoa);
                         List<string> khoa = (from s in db.GIANG_VIEN
                                              where s.MaGV == MaGV && s.Khoa == makhoa && s.Hide == false
                                              select (s.KHOA1.TenKhoa)).ToList();
                         if (khoa.Count == 0) { dgvGV.Rows[j].Cells[4].Value = " "; }
                         else dgvGV.Rows[j].Cells[4].Value = khoa[0];
                     }
                     List<int> lstMaHD = new List<int>();
                     if (dtpBD.Text == " " && dtpKT.Text == " ")
                     {
                         lstMaHD = (from s in db.GIANG_VIEN
                                    join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV 
                                    where s.MaGV == MaGV && b.HOAT_DONG.Hide == false
                                    select (b.MaHD)).ToList();
                     }
                     else if (dtpBD.Text != " " && dtpKT.Text == " ")
                     {
                         DateTime BD = Convert.ToDateTime(dtpBD.Text);
                         lstMaHD = (from s in db.GIANG_VIEN
                                    join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                                    join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                    where s.MaGV == MaGV && d.NgayBatDau >= BD && b.HOAT_DONG.Hide == false
                                    select (b.MaHD)).ToList();
                     }
                     else if (dtpBD.Text == " " && dtpKT.Text != " ")
                     {
                         DateTime KT = Convert.ToDateTime(dtpKT.Text);
                         lstMaHD = (from s in db.GIANG_VIEN
                                    join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                                    join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                    where s.MaGV == MaGV && d.NgayKetThuc <= KT && b.HOAT_DONG.Hide == false
                                    select (b.MaHD)).ToList();

                     }
                     else if (dtpBD.Text != " " && dtpKT.Text != " ")
                     {
                         DateTime BD = Convert.ToDateTime(dtpBD.Text);
                         DateTime KT = Convert.ToDateTime(dtpKT.Text);
                         lstMaHD = (from s in db.GIANG_VIEN
                                    join b in db.HD_GIANGVIEN on s.MaGV equals b.MaGV
                                    join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                    where s.MaGV == MaGV && d.NgayBatDau >= BD && d.NgayKetThuc <= KT && b.HOAT_DONG.Hide == false
                                    select (b.MaHD)).ToList();
                     }
                     if (lstMaHD.Count == 0) dgvGV.Rows[j].Cells[5].Value = " ";
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
                         dgvGV.Rows[j].Cells[5].Value = TenHD;
                     }
                 }
                Xoa();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length >0)
                btnLoc.Enabled = true;
            else btnLoc.Enabled = false;
        }
    }
    public class TK_GiangVienDisplayDto
    {
        public int STT { get; set; } // Sequential Number for display in the first column
        public string MaGV { get; set; } // Lecturer ID
        public string HoTenLot { get; set; } // Lecturer's last and middle name
        public string Ten { get; set; } // Lecturer's first name
        public string TenKhoa { get; set; } // Name of the faculty/department
        public string TenHoatDongThamGia { get; set; } // Concatenated list of activities participated in
    }
}
