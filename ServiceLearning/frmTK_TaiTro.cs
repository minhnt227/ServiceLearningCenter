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
    public partial class frmTK_TaiTro : Form
    {
        Context db = new Context();

        public frmTK_TaiTro()
        {
            InitializeComponent();
        }

        private void frmTK_TaiTro_Load(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            ThongKeTaiTro();
            Xoa();
            btnLoc.Enabled = false;

        }
        private void ThongKeTaiTro()
        {
            try
            {
                dgvTT.Rows.Clear();
                dgvTT.Refresh();
                this.dgvTT.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstTenTaiTro = new List<string>();
                List<int> IDTaiTro = new List<int>();
                lstTenTaiTro = db.TAI_TRO.Where(x=>x.Hide == false).Select(x => x.TenTaiTro).Take(200).ToList();
                IDTaiTro = db.TAI_TRO.Where(x => x.Hide == false).Select(x => x.ID_TaiTro).Take(200).ToList();
                for (int j = 0; j < IDTaiTro.Count; j++)
                {
                    string TenDT = lstTenTaiTro[j];
                    int id = IDTaiTro[j];
                    dgvTT.Rows.Add();
                    dgvTT.Rows[j].Cells[0].Value = j + 1;
                    dgvTT.Rows[j].Cells[1].Value = TenDT;
                    List<string> nguoidaidien = (from s in db.TAI_TRO
                                                 where s.ID_TaiTro == id
                                                 select (s.DaiDien)).ToList();
                    dgvTT.Rows[j].Cells[2].Value = nguoidaidien[0];
                    List<string> SDT = (from s in db.TAI_TRO
                                        where s.ID_TaiTro == id
                                        select (s.SDT)).ToList();
                    dgvTT.Rows[j].Cells[3].Value = SDT[0];
                    List<string> email = (from s in db.TAI_TRO
                                          where s.ID_TaiTro == id
                                          select (s.Email)).ToList();
                    dgvTT.Rows[j].Cells[4].Value = email[0];
                    List<int> lstMaHD = new List<int>();
                    lstMaHD = (from s in db.TAI_TRO
                               join b in db.HD_TAITRO on s.ID_TaiTro equals b.ID_TaiTro
                               where b.ID_TaiTro == id
                               select (b.MaHD)).ToList();
                    if (lstMaHD.Count == 0) dgvTT.Rows[j].Cells[5].Value = "";
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
                        dgvTT.Rows[j].Cells[5].Value = TenHD;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void Xoa()
        {
            int n = dgvTT.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                if (dgvTT.Rows[i].Cells[5].Value == " ")
                {
                    //Object stt = dgvSV.Rows[i].Cells[0].Value;
                    dgvTT.Rows.RemoveAt(dgvTT.Rows[i].Index);
                    i--;
                    n--;
                    // dgvSV.Rows[i+1].Cells[0].Value = stt;
                }
            }
            for (int i = 0; i < n - 1; i++)
            {
                dgvTT.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void btn_Export(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvTT, sfd.FileName);
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

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            dgvTT.Rows.Clear();
            dgvTT.Refresh();
            ThongKeTaiTro();
            Xoa();
            btnLoc.Enabled = false;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocTaiTro();
        }
        private void LocTaiTro()
        {
            try
            {
                dgvTT.Rows.Clear();
                dgvTT.Refresh();
                this.dgvTT.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstTenTaiTro = new List<string>();
                List<int> IDTaiTro = new List<int>();
                lstTenTaiTro = db.TAI_TRO.Where(x => x.Hide == false).Select(x => x.TenTaiTro).ToList();
                IDTaiTro = db.TAI_TRO.Where(x => x.Hide == false).Select(x => x.ID_TaiTro).ToList();
                for (int j = 0; j < IDTaiTro.Count; j++)
                {
                    string TenDT = lstTenTaiTro[j];
                    int id = IDTaiTro[j];
                    dgvTT.Rows.Add();
                    dgvTT.Rows[j].Cells[0].Value = j + 1;
                    dgvTT.Rows[j].Cells[1].Value = TenDT;
                    List<string> nguoidaidien = (from s in db.TAI_TRO
                                                 where s.ID_TaiTro == id
                                                 select (s.DaiDien)).ToList();
                    dgvTT.Rows[j].Cells[2].Value = nguoidaidien[0];
                    List<string> SDT = (from s in db.TAI_TRO
                                        where s.ID_TaiTro == id
                                        select (s.SDT)).ToList();
                    dgvTT.Rows[j].Cells[3].Value = SDT[0];
                    List<string> email = (from s in db.TAI_TRO
                                          where s.ID_TaiTro == id
                                          select (s.Email)).ToList();
                    dgvTT.Rows[j].Cells[4].Value = email[0];
                    List<int> lstMaHD = new List<int>();
                    if (dtpBD.Text == " " && dtpKT.Text == " ")
                    {
                        lstMaHD = (from s in db.TAI_TRO
                                   join b in db.HD_TAITRO on s.ID_TaiTro equals b.ID_TaiTro
                                   where b.ID_TaiTro == id
                                   select (b.MaHD)).ToList();
                    }
                    else if (dtpBD.Text != " " && dtpKT.Text == " ")
                    {
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        lstMaHD = (from s in db.TAI_TRO
                                   join b in db.HD_TAITRO on s.ID_TaiTro equals b.ID_TaiTro
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where b.ID_TaiTro == id && c.NgayBatDau >= BD
                                   select (b.MaHD)).ToList();
                    }
                    else if (dtpBD.Text == " " && dtpKT.Text != " ")
                    {
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        lstMaHD = (from s in db.TAI_TRO
                                   join b in db.HD_TAITRO on s.ID_TaiTro equals b.ID_TaiTro
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where b.ID_TaiTro == id && c.NgayKetThuc <= KT
                                   select (b.MaHD)).ToList();
                    }
                    else if (dtpBD.Text != " " && dtpKT.Text != " ")
                    {
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        lstMaHD = (from s in db.TAI_TRO
                                   join b in db.HD_TAITRO on s.ID_TaiTro equals b.ID_TaiTro
                                   join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                   where b.ID_TaiTro == id && c.NgayBatDau >= BD && c.NgayKetThuc <= KT
                                   select (b.MaHD)).ToList();
                    }

                    if (lstMaHD.Count == 0) dgvTT.Rows[j].Cells[5].Value = "";
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
                        dgvTT.Rows[j].Cells[5].Value = TenHD;
                    }
                }
                Xoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
