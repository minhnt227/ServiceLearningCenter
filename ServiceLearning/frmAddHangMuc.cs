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
    public partial class frmAddHangMuc : Form
    {
        public int ID = -1;
        public frmAddHangMuc()
        {
            InitializeComponent();
        }

        private void btnAdd_HM_Click(object sender, EventArgs e)
        {
            if (btnAdd_HM.Text.Trim() == "Tạo")
            {
                AddNewwHM();
            }  
            else 
            if (btnAdd_HM.Text.Trim() == "Sửa")
            {
                EditHM();
            }    
            else
                this.Close();
        }

        private void AddNewwHM()
        {
            try
            {
                using (Context db = new Context())
                {
                    HANG_MUC HM = new HANG_MUC();
                    HM.TenHangMuc = txtHM_Ten.Text;
                    HM.Hide = false;
                    db.HANG_MUC.Add(HM);
                    db.SaveChanges();
                    MessageBox.Show("Thêm thành công");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n***************************************** \n\n" + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void EditHM()
        {
            try
            {
                using (Context db = new Context())
                {
                    HANG_MUC HM = db.HANG_MUC.Find(ID);
                    if (HM == null)
                        return;
                    HM.TenHangMuc = txtHM_Ten.Text;
                    HM.Hide = false;
                    db.Entry(HM).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Sửa thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n*****************************************\n\n " + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadUpdateForm(string name)
        {
            txtHM_Ten.Text = name;
            this.Text = "Sửa đổi Hạng Mục";
            btnAdd_HM.Text = "Sửa";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (btnAdd_HM.Text.Trim() == "Tạo")
            {
                AddNewwHM();
            }
            else
            if (btnAdd_HM.Text.Trim() == "Sửa")
            {
                EditHM();
            }
            else
                this.Close();
        }

        private void frmAddHangMuc_Load(object sender, EventArgs e)
        {

        }
    }
}
