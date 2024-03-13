using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceLearning
{
    public partial class frmHome : Form
    {

        public frmHome()
        {
            InitializeComponent();
            WebClient webClient = new WebClient();
            var client = new WebClient();
            if (!(webClient.DownloadString("https://drive.google.com/file/d/1dlsAdBBJNmSkXciUkK8JZnQ6VYq-w3Gy/view?usp=drive_link").Contains("2.0.0")))
            {
               // if (MessageBox.Show("A new update is available! Do you want to download it?", "Demo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (File.Exists(@".\ServiceLearningSetup.msi")) { File.Delete(@".\ServiceLearningSetup.msi"); }
                        client.DownloadFile("https://drive.google.com/file/d/14Ci57INKIG8hoEUhQ8bx0jXbFVPOBOOX/view?usp=drive_link", @"ServiceLearningSetup.zip");
                        string zipPath = @".\ServiceLearningSetup.zip";
                        string extractPath = @".\";
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        Process process = new Process();
                        process.StartInfo.FileName = "msiexec.exe";
                        process.StartInfo.Arguments = string.Format("/i ServiceLearningSetup.msi");
                        this.Close();
                        process.Start();
                    }
                    catch
                    {
                    }
                }
            }
        }
        private void container(object _form)
        {
            if (panel_Feedback.Controls.Count > 0)
            {
                panel_Feedback.Controls.Clear();
            }
            Form frm = _form as Form;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panel_Feedback.Controls.Add(frm);
            panel_Feedback.Tag = frm;
            frm.Show();
        }
        private void btn_HoatDong(object sender, EventArgs e)
        {
            container(new frm_Theme());
        }

        private void btn_DoiTac(object sender, EventArgs e)
        {
            container(new frm_DoiTac_Details());
        }

        private void panel_Menu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_SinhVien(object sender, EventArgs e)
        {
            container(new frmSinhVien());
        }

        private void btn_HangMuc(object sender, EventArgs e)
        {
            container(new frmAddHangMuc());

        }

        private void btn_KhoaVien(object sender, EventArgs e)
        {
            container(new frmKhoaDetails());
        }

        private void btn_GiangVien(object sender, EventArgs e)
        {
            container(new frm_GiangVien());
        }

        private void panel_Feedback_Paint(object sender, PaintEventArgs e)
        {

        }


        private void btn_TTGV(object sender, EventArgs e)
        {
            container(new frmTK_GiangVien());
        }

        private void btn_TTDT(object sender, EventArgs e)
        {
            container(new frmTK_DoiTac());
        }

        private void btn_TTTT(object sender, EventArgs e)
        {
            container(new frmTK_TaiTro());
        }

        private void btn_TKTC(object sender, EventArgs e)
        {
            container(new frmTK_TaiChinh());
        }

        private void btn_TKKhoa(object sender, EventArgs e)
        {
            container(new frmTK_SinhVien());
        }

        private void btn_TaiTro(object sender, EventArgs e)
        {
            container(new frmTTDetails());
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            container(new frmTK_Khoa());
        }

        private void Home_Load(object sender, EventArgs e)
        {
            container(new frm_Theme());
        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {
            container(new frm_Theme());

        }
    }
}
