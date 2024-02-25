namespace ServiceLearning
{
    partial class frmTK_TaiChinh
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTK_TaiChinh));
            this.dgvTK_TC = new Guna.UI2.WinForms.Guna2DataGridView();
            this.stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateBegin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TK_TC_uef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaiTro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Khac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExport = new Guna.UI2.WinForms.Guna2Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpKT = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpBD = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTK_TC)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTK_TC
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvTK_TC.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTK_TC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTK_TC.ColumnHeadersHeight = 45;
            this.dgvTK_TC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stt,
            this.LHD,
            this.Ten,
            this.DateBegin,
            this.Tong,
            this.TK_TC_uef,
            this.TaiTro,
            this.Khac});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTK_TC.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTK_TC.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTK_TC.Location = new System.Drawing.Point(3, 120);
            this.dgvTK_TC.Name = "dgvTK_TC";
            this.dgvTK_TC.ReadOnly = true;
            this.dgvTK_TC.RowHeadersVisible = false;
            this.dgvTK_TC.Size = new System.Drawing.Size(833, 376);
            this.dgvTK_TC.TabIndex = 6;
            this.dgvTK_TC.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTK_TC.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvTK_TC.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvTK_TC.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvTK_TC.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvTK_TC.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvTK_TC.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTK_TC.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvTK_TC.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTK_TC.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvTK_TC.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvTK_TC.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTK_TC.ThemeStyle.HeaderStyle.Height = 45;
            this.dgvTK_TC.ThemeStyle.ReadOnly = true;
            this.dgvTK_TC.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTK_TC.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTK_TC.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvTK_TC.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvTK_TC.ThemeStyle.RowsStyle.Height = 22;
            this.dgvTK_TC.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTK_TC.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // stt
            // 
            this.stt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.stt.FillWeight = 30.45685F;
            this.stt.HeaderText = "STT";
            this.stt.Name = "stt";
            this.stt.ReadOnly = true;
            this.stt.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.stt.Width = 61;
            // 
            // LHD
            // 
            this.LHD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LHD.FillWeight = 113.9086F;
            this.LHD.HeaderText = "Loại hoạt động";
            this.LHD.MinimumWidth = 4;
            this.LHD.Name = "LHD";
            this.LHD.ReadOnly = true;
            this.LHD.Width = 126;
            // 
            // Ten
            // 
            this.Ten.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Ten.FillWeight = 113.9086F;
            this.Ten.HeaderText = "Tên hoạt động";
            this.Ten.MinimumWidth = 4;
            this.Ten.Name = "Ten";
            this.Ten.ReadOnly = true;
            this.Ten.Width = 123;
            // 
            // DateBegin
            // 
            this.DateBegin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DateBegin.FillWeight = 113.9086F;
            this.DateBegin.HeaderText = "Ngày bắt đầu";
            this.DateBegin.Name = "DateBegin";
            this.DateBegin.ReadOnly = true;
            this.DateBegin.Width = 91;
            // 
            // Tong
            // 
            this.Tong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Tong.FillWeight = 113.9086F;
            this.Tong.HeaderText = "Tổng chi phí";
            this.Tong.Name = "Tong";
            this.Tong.ReadOnly = true;
            this.Tong.Width = 88;
            // 
            // TK_TC_uef
            // 
            this.TK_TC_uef.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TK_TC_uef.HeaderText = "UEF";
            this.TK_TC_uef.Name = "TK_TC_uef";
            this.TK_TC_uef.ReadOnly = true;
            this.TK_TC_uef.Width = 65;
            // 
            // TaiTro
            // 
            this.TaiTro.HeaderText = "Tài trợ";
            this.TaiTro.Name = "TaiTro";
            this.TaiTro.ReadOnly = true;
            // 
            // Khac
            // 
            this.Khac.HeaderText = "Khác";
            this.Khac.Name = "Khac";
            this.Khac.ReadOnly = true;
            // 
            // btnExport
            // 
            this.btnExport.BorderRadius = 5;
            this.btnExport.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExport.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExport.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExport.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExport.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(691, 502);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(145, 27);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Xuất file excel";
            this.btnExport.Click += new System.EventHandler(this.btn_Export);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoc);
            this.groupBox1.Controls.Add(this.guna2PictureBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpKT);
            this.groupBox1.Controls.Add(this.dtpBD);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(223, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 83);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(331, 52);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(53, 23);
            this.btnLoc.TabIndex = 20;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox2.Image")));
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(331, 19);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(45, 27);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox2.TabIndex = 19;
            this.guna2PictureBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(163, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "Ngày kết thúc";
            // 
            // dtpKT
            // 
            this.dtpKT.Checked = true;
            this.dtpKT.CustomFormat = " ";
            this.dtpKT.FillColor = System.Drawing.Color.White;
            this.dtpKT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpKT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpKT.Location = new System.Drawing.Point(166, 41);
            this.dtpKT.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpKT.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpKT.Name = "dtpKT";
            this.dtpKT.Size = new System.Drawing.Size(139, 36);
            this.dtpKT.TabIndex = 17;
            this.dtpKT.Value = new System.DateTime(2023, 10, 19, 20, 27, 45, 846);
            // 
            // dtpBD
            // 
            this.dtpBD.Checked = true;
            this.dtpBD.CustomFormat = " ";
            this.dtpBD.FillColor = System.Drawing.Color.White;
            this.dtpBD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBD.Location = new System.Drawing.Point(15, 39);
            this.dtpBD.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpBD.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpBD.Name = "dtpBD";
            this.dtpBD.Size = new System.Drawing.Size(131, 36);
            this.dtpBD.TabIndex = 16;
            this.dtpBD.Value = new System.DateTime(2023, 10, 19, 20, 44, 29, 208);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Ngày bắt đầu";
            // 
            // frmTK_TaiChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 541);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvTK_TC);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmTK_TaiChinh";
            this.Text = "Thống kê Tài Chính";
            this.Load += new System.EventHandler(this.frmTK_TaiChinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTK_TC)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2DataGridView dgvTK_TC;
        private System.Windows.Forms.DataGridViewTextBoxColumn stt;
        private System.Windows.Forms.DataGridViewTextBoxColumn LHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ten;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateBegin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tong;
        private System.Windows.Forms.DataGridViewTextBoxColumn TK_TC_uef;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaiTro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Khac;
        private Guna.UI2.WinForms.Guna2Button btnExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoc;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpKT;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpBD;
        private System.Windows.Forms.Label label3;
    }
}