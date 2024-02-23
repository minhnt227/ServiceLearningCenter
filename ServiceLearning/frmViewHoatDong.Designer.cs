namespace ServiceLearning
{
    partial class frmViewHoatDong
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.gbGeneralInfo = new System.Windows.Forms.GroupBox();
            this.txtDateEnd = new System.Windows.Forms.TextBox();
            this.txtDateBegin = new System.Windows.Forms.TextBox();
            this.txtLoai = new System.Windows.Forms.TextBox();
            this.txtTenHD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbSVList = new System.Windows.Forms.GroupBox();
            this.dgvSinhVien = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbTaiChinh = new System.Windows.Forms.GroupBox();
            this.numUEF = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.numTaiTro = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.txtTC_Khac = new System.Windows.Forms.TextBox();
            this.txtTC_TieuDe = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.gbTTList = new System.Windows.Forms.GroupBox();
            this.dgvTaiTro = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.gbDT_List = new System.Windows.Forms.GroupBox();
            this.dgvDoiTac = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.gbGVList = new System.Windows.Forms.GroupBox();
            this.dgv_GV = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btnExit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSVExport = new System.Windows.Forms.Button();
            this.MSSV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoTenSV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Khoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes_SV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DB_Khoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoTenLot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVKhoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GV_Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVKHoa_DB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_Ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_DaiDien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_NoiDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_DB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_Rep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_IDDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGVExport = new System.Windows.Forms.Button();
            this.btnDTExport = new System.Windows.Forms.Button();
            this.btnTTExport = new System.Windows.Forms.Button();
            this.gbGeneralInfo.SuspendLayout();
            this.gbSVList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbTaiChinh.SuspendLayout();
            this.gbTTList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiTro)).BeginInit();
            this.gbDT_List.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoiTac)).BeginInit();
            this.gbGVList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GV)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Tomato;
            this.lblHeader.Location = new System.Drawing.Point(483, 19);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(395, 42);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Thông Tin Hoạt Động";
            // 
            // gbGeneralInfo
            // 
            this.gbGeneralInfo.Controls.Add(this.txtDateEnd);
            this.gbGeneralInfo.Controls.Add(this.txtDateBegin);
            this.gbGeneralInfo.Controls.Add(this.txtLoai);
            this.gbGeneralInfo.Controls.Add(this.txtTenHD);
            this.gbGeneralInfo.Controls.Add(this.label3);
            this.gbGeneralInfo.Controls.Add(this.label5);
            this.gbGeneralInfo.Controls.Add(this.label4);
            this.gbGeneralInfo.Controls.Add(this.label2);
            this.gbGeneralInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gbGeneralInfo.Location = new System.Drawing.Point(17, 15);
            this.gbGeneralInfo.Name = "gbGeneralInfo";
            this.gbGeneralInfo.Size = new System.Drawing.Size(1162, 246);
            this.gbGeneralInfo.TabIndex = 1;
            this.gbGeneralInfo.TabStop = false;
            this.gbGeneralInfo.Text = "Thông tin cơ bản";
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.Location = new System.Drawing.Point(688, 185);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.ReadOnly = true;
            this.txtDateEnd.Size = new System.Drawing.Size(172, 29);
            this.txtDateEnd.TabIndex = 4;
            // 
            // txtDateBegin
            // 
            this.txtDateBegin.Location = new System.Drawing.Point(191, 186);
            this.txtDateBegin.Name = "txtDateBegin";
            this.txtDateBegin.ReadOnly = true;
            this.txtDateBegin.Size = new System.Drawing.Size(172, 29);
            this.txtDateBegin.TabIndex = 4;
            // 
            // txtLoai
            // 
            this.txtLoai.Location = new System.Drawing.Point(191, 128);
            this.txtLoai.Name = "txtLoai";
            this.txtLoai.ReadOnly = true;
            this.txtLoai.Size = new System.Drawing.Size(282, 29);
            this.txtLoai.TabIndex = 4;
            // 
            // txtTenHD
            // 
            this.txtTenHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenHD.Location = new System.Drawing.Point(191, 45);
            this.txtTenHD.Name = "txtTenHD";
            this.txtTenHD.ReadOnly = true;
            this.txtTenHD.Size = new System.Drawing.Size(965, 37);
            this.txtTenHD.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Loại";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(548, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ngày kết thúc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ngày bắt đầu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên Hoạt Động";
            // 
            // gbSVList
            // 
            this.gbSVList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSVList.Controls.Add(this.dgvSinhVien);
            this.gbSVList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSVList.Location = new System.Drawing.Point(17, 331);
            this.gbSVList.Name = "gbSVList";
            this.gbSVList.Size = new System.Drawing.Size(1195, 322);
            this.gbSVList.TabIndex = 2;
            this.gbSVList.TabStop = false;
            this.gbSVList.Text = "Danh sách SV";
            // 
            // dgvSinhVien
            // 
            this.dgvSinhVien.AllowUserToAddRows = false;
            this.dgvSinhVien.AllowUserToDeleteRows = false;
            this.dgvSinhVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSinhVien.ColumnHeadersHeight = 40;
            this.dgvSinhVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MSSV,
            this.HoTenSV,
            this.Khoa,
            this.Role,
            this.Notes_SV,
            this.DB_Khoa});
            this.dgvSinhVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSinhVien.Location = new System.Drawing.Point(3, 25);
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.dgvSinhVien.ReadOnly = true;
            this.dgvSinhVien.RowHeadersVisible = false;
            this.dgvSinhVien.RowHeadersWidth = 72;
            this.dgvSinhVien.RowTemplate.Height = 31;
            this.dgvSinhVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSinhVien.Size = new System.Drawing.Size(1189, 294);
            this.dgvSinhVien.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.btnTTExport);
            this.panel1.Controls.Add(this.btnDTExport);
            this.panel1.Controls.Add(this.btnGVExport);
            this.panel1.Controls.Add(this.btnSVExport);
            this.panel1.Controls.Add(this.gbTaiChinh);
            this.panel1.Controls.Add(this.gbTTList);
            this.panel1.Controls.Add(this.gbGeneralInfo);
            this.panel1.Controls.Add(this.gbDT_List);
            this.panel1.Controls.Add(this.gbGVList);
            this.panel1.Controls.Add(this.gbSVList);
            this.panel1.Location = new System.Drawing.Point(12, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1261, 677);
            this.panel1.TabIndex = 3;
            // 
            // gbTaiChinh
            // 
            this.gbTaiChinh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTaiChinh.Controls.Add(this.numUEF);
            this.gbTaiChinh.Controls.Add(this.numTaiTro);
            this.gbTaiChinh.Controls.Add(this.txtTC_Khac);
            this.gbTaiChinh.Controls.Add(this.txtTC_TieuDe);
            this.gbTaiChinh.Controls.Add(this.label29);
            this.gbTaiChinh.Controls.Add(this.label28);
            this.gbTaiChinh.Controls.Add(this.label30);
            this.gbTaiChinh.Controls.Add(this.label32);
            this.gbTaiChinh.Controls.Add(this.label31);
            this.gbTaiChinh.Controls.Add(this.label27);
            this.gbTaiChinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTaiChinh.Location = new System.Drawing.Point(20, 1991);
            this.gbTaiChinh.Name = "gbTaiChinh";
            this.gbTaiChinh.Size = new System.Drawing.Size(1194, 349);
            this.gbTaiChinh.TabIndex = 2;
            this.gbTaiChinh.TabStop = false;
            this.gbTaiChinh.Text = "Thông tin Tài Chính";
            // 
            // numUEF
            // 
            this.numUEF.AlwaysActive = false;
            this.numUEF.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numUEF.Location = new System.Drawing.Point(283, 91);
            this.numUEF.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numUEF.Name = "numUEF";
            this.numUEF.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.numUEF.ReadOnly = true;
            this.numUEF.Size = new System.Drawing.Size(297, 34);
            this.numUEF.TabIndex = 3;
            this.numUEF.ThousandsSeparator = true;
            this.numUEF.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numUEF.UpDownButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Form;
            // 
            // numTaiTro
            // 
            this.numTaiTro.AlwaysActive = false;
            this.numTaiTro.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTaiTro.Location = new System.Drawing.Point(284, 138);
            this.numTaiTro.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numTaiTro.Name = "numTaiTro";
            this.numTaiTro.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.numTaiTro.ReadOnly = true;
            this.numTaiTro.Size = new System.Drawing.Size(297, 34);
            this.numTaiTro.TabIndex = 3;
            this.numTaiTro.ThousandsSeparator = true;
            this.numTaiTro.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numTaiTro.UpDownButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Form;
            // 
            // txtTC_Khac
            // 
            this.txtTC_Khac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTC_Khac.Location = new System.Drawing.Point(284, 186);
            this.txtTC_Khac.Multiline = true;
            this.txtTC_Khac.Name = "txtTC_Khac";
            this.txtTC_Khac.ReadOnly = true;
            this.txtTC_Khac.Size = new System.Drawing.Size(615, 117);
            this.txtTC_Khac.TabIndex = 2;
            // 
            // txtTC_TieuDe
            // 
            this.txtTC_TieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTC_TieuDe.Location = new System.Drawing.Point(284, 49);
            this.txtTC_TieuDe.Name = "txtTC_TieuDe";
            this.txtTC_TieuDe.ReadOnly = true;
            this.txtTC_TieuDe.Size = new System.Drawing.Size(657, 31);
            this.txtTC_TieuDe.TabIndex = 2;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(27, 53);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(89, 25);
            this.label29.TabIndex = 0;
            this.label29.Text = "Tiêu đề:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(27, 96);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(216, 25);
            this.label28.TabIndex = 0;
            this.label28.Text = "Tổng tiền UEF đã chi:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(27, 186);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(57, 25);
            this.label30.TabIndex = 0;
            this.label30.Text = "Khác";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(582, 96);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(53, 25);
            this.label32.TabIndex = 0;
            this.label32.Text = "VND";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(582, 142);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 25);
            this.label31.TabIndex = 0;
            this.label31.Text = "VND";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(27, 143);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(172, 25);
            this.label27.TabIndex = 0;
            this.label27.Text = "Tổng tiền tài trợ:";
            // 
            // gbTTList
            // 
            this.gbTTList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTTList.Controls.Add(this.dgvTaiTro);
            this.gbTTList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTTList.Location = new System.Drawing.Point(20, 1593);
            this.gbTTList.Name = "gbTTList";
            this.gbTTList.Size = new System.Drawing.Size(1195, 361);
            this.gbTTList.TabIndex = 2;
            this.gbTTList.TabStop = false;
            this.gbTTList.Text = "Danh sách Tài Trợ";
            // 
            // dgvTaiTro
            // 
            this.dgvTaiTro.AllowUserToAddRows = false;
            this.dgvTaiTro.AllowUserToDeleteRows = false;
            this.dgvTaiTro.AllowUserToOrderColumns = true;
            this.dgvTaiTro.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaiTro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaiTro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TT_Name,
            this.TT_Rep,
            this.TT_SDT,
            this.TT_Email,
            this.TT_Notes,
            this.TT_IDDB});
            this.dgvTaiTro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaiTro.Location = new System.Drawing.Point(3, 25);
            this.dgvTaiTro.Name = "dgvTaiTro";
            this.dgvTaiTro.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.dgvTaiTro.ReadOnly = true;
            this.dgvTaiTro.RowHeadersVisible = false;
            this.dgvTaiTro.RowHeadersWidth = 72;
            this.dgvTaiTro.RowTemplate.Height = 31;
            this.dgvTaiTro.Size = new System.Drawing.Size(1189, 333);
            this.dgvTaiTro.TabIndex = 0;
            // 
            // gbDT_List
            // 
            this.gbDT_List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDT_List.Controls.Add(this.dgvDoiTac);
            this.gbDT_List.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDT_List.Location = new System.Drawing.Point(20, 1160);
            this.gbDT_List.Name = "gbDT_List";
            this.gbDT_List.Size = new System.Drawing.Size(1195, 361);
            this.gbDT_List.TabIndex = 2;
            this.gbDT_List.TabStop = false;
            this.gbDT_List.Text = "Danh sách Đối Tác";
            // 
            // dgvDoiTac
            // 
            this.dgvDoiTac.AllowUserToAddRows = false;
            this.dgvDoiTac.AllowUserToDeleteRows = false;
            this.dgvDoiTac.AllowUserToOrderColumns = true;
            this.dgvDoiTac.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoiTac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoiTac.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DT_Ten,
            this.DT_DaiDien,
            this.DT_SDT,
            this.DT_Email,
            this.DT_NoiDung,
            this.ID_DB});
            this.dgvDoiTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoiTac.Location = new System.Drawing.Point(3, 25);
            this.dgvDoiTac.Name = "dgvDoiTac";
            this.dgvDoiTac.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.dgvDoiTac.ReadOnly = true;
            this.dgvDoiTac.RowHeadersVisible = false;
            this.dgvDoiTac.RowHeadersWidth = 72;
            this.dgvDoiTac.RowTemplate.Height = 31;
            this.dgvDoiTac.Size = new System.Drawing.Size(1189, 333);
            this.dgvDoiTac.TabIndex = 0;
            // 
            // gbGVList
            // 
            this.gbGVList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGVList.Controls.Add(this.dgv_GV);
            this.gbGVList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGVList.Location = new System.Drawing.Point(20, 718);
            this.gbGVList.Name = "gbGVList";
            this.gbGVList.Size = new System.Drawing.Size(1195, 361);
            this.gbGVList.TabIndex = 2;
            this.gbGVList.TabStop = false;
            this.gbGVList.Text = "Danh sách GV";
            // 
            // dgv_GV
            // 
            this.dgv_GV.AllowUserToAddRows = false;
            this.dgv_GV.AllowUserToDeleteRows = false;
            this.dgv_GV.AllowUserToOrderColumns = true;
            this.dgv_GV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_GV.ColumnHeadersHeight = 40;
            this.dgv_GV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaGV,
            this.HoTenLot,
            this.Ten,
            this.GVKhoa,
            this.GV_Role,
            this.GVKHoa_DB});
            this.dgv_GV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_GV.Location = new System.Drawing.Point(3, 25);
            this.dgv_GV.Name = "dgv_GV";
            this.dgv_GV.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.dgv_GV.ReadOnly = true;
            this.dgv_GV.RowHeadersVisible = false;
            this.dgv_GV.RowHeadersWidth = 72;
            this.dgv_GV.RowTemplate.Height = 31;
            this.dgv_GV.Size = new System.Drawing.Size(1189, 333);
            this.dgv_GV.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1104, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(144, 54);
            this.btnExit.TabIndex = 4;
            this.btnExit.Values.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 844);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1285, 80);
            this.panel2.TabIndex = 5;
            // 
            // btnSVExport
            // 
            this.btnSVExport.BackColor = System.Drawing.Color.Transparent;
            this.btnSVExport.Location = new System.Drawing.Point(52, 278);
            this.btnSVExport.Name = "btnSVExport";
            this.btnSVExport.Size = new System.Drawing.Size(133, 38);
            this.btnSVExport.TabIndex = 3;
            this.btnSVExport.Text = "Xuất Excel";
            this.btnSVExport.UseVisualStyleBackColor = false;
            this.btnSVExport.Click += new System.EventHandler(this.btnSVExport_Click);
            // 
            // MSSV
            // 
            this.MSSV.HeaderText = "MSSV";
            this.MSSV.MinimumWidth = 9;
            this.MSSV.Name = "MSSV";
            this.MSSV.ReadOnly = true;
            // 
            // HoTenSV
            // 
            this.HoTenSV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.HoTenSV.HeaderText = "Họ và Tên";
            this.HoTenSV.MinimumWidth = 9;
            this.HoTenSV.Name = "HoTenSV";
            this.HoTenSV.ReadOnly = true;
            this.HoTenSV.Width = 150;
            // 
            // Khoa
            // 
            this.Khoa.HeaderText = "Khoa";
            this.Khoa.MinimumWidth = 9;
            this.Khoa.Name = "Khoa";
            this.Khoa.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.HeaderText = "Vai trò";
            this.Role.MinimumWidth = 9;
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // Notes_SV
            // 
            this.Notes_SV.HeaderText = "Ghi Chú";
            this.Notes_SV.MinimumWidth = 9;
            this.Notes_SV.Name = "Notes_SV";
            this.Notes_SV.ReadOnly = true;
            // 
            // DB_Khoa
            // 
            this.DB_Khoa.HeaderText = "Mã Khoa";
            this.DB_Khoa.MinimumWidth = 9;
            this.DB_Khoa.Name = "DB_Khoa";
            this.DB_Khoa.ReadOnly = true;
            // 
            // MaGV
            // 
            this.MaGV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaGV.Frozen = true;
            this.MaGV.HeaderText = "Mã Giảng Viên";
            this.MaGV.MinimumWidth = 9;
            this.MaGV.Name = "MaGV";
            this.MaGV.ReadOnly = true;
            this.MaGV.Width = 195;
            // 
            // HoTenLot
            // 
            this.HoTenLot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.HoTenLot.HeaderText = "Họ và Tên Lót";
            this.HoTenLot.MinimumWidth = 9;
            this.HoTenLot.Name = "HoTenLot";
            this.HoTenLot.ReadOnly = true;
            this.HoTenLot.Width = 185;
            // 
            // Ten
            // 
            this.Ten.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Ten.HeaderText = "Tên";
            this.Ten.MinimumWidth = 9;
            this.Ten.Name = "Ten";
            this.Ten.ReadOnly = true;
            this.Ten.Width = 90;
            // 
            // GVKhoa
            // 
            this.GVKhoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.GVKhoa.HeaderText = "Đơn vị";
            this.GVKhoa.MinimumWidth = 9;
            this.GVKhoa.Name = "GVKhoa";
            this.GVKhoa.ReadOnly = true;
            this.GVKhoa.Width = 119;
            // 
            // GV_Role
            // 
            this.GV_Role.HeaderText = "Vai trò";
            this.GV_Role.MinimumWidth = 9;
            this.GV_Role.Name = "GV_Role";
            this.GV_Role.ReadOnly = true;
            // 
            // GVKHoa_DB
            // 
            this.GVKHoa_DB.HeaderText = "Mã Đơn Vị";
            this.GVKHoa_DB.MinimumWidth = 9;
            this.GVKHoa_DB.Name = "GVKHoa_DB";
            this.GVKHoa_DB.ReadOnly = true;
            // 
            // DT_Ten
            // 
            this.DT_Ten.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DT_Ten.Frozen = true;
            this.DT_Ten.HeaderText = "Tên Đối Tác";
            this.DT_Ten.MinimumWidth = 9;
            this.DT_Ten.Name = "DT_Ten";
            this.DT_Ten.ReadOnly = true;
            this.DT_Ten.Width = 164;
            // 
            // DT_DaiDien
            // 
            this.DT_DaiDien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DT_DaiDien.HeaderText = "Đại Diện";
            this.DT_DaiDien.MinimumWidth = 9;
            this.DT_DaiDien.Name = "DT_DaiDien";
            this.DT_DaiDien.ReadOnly = true;
            this.DT_DaiDien.Width = 138;
            // 
            // DT_SDT
            // 
            this.DT_SDT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DT_SDT.HeaderText = "Số Điện Thoại";
            this.DT_SDT.MinimumWidth = 9;
            this.DT_SDT.Name = "DT_SDT";
            this.DT_SDT.ReadOnly = true;
            this.DT_SDT.Width = 187;
            // 
            // DT_Email
            // 
            this.DT_Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DT_Email.HeaderText = "Email";
            this.DT_Email.MinimumWidth = 9;
            this.DT_Email.Name = "DT_Email";
            this.DT_Email.ReadOnly = true;
            this.DT_Email.Width = 108;
            // 
            // DT_NoiDung
            // 
            this.DT_NoiDung.HeaderText = "Nội Dung";
            this.DT_NoiDung.MinimumWidth = 9;
            this.DT_NoiDung.Name = "DT_NoiDung";
            this.DT_NoiDung.ReadOnly = true;
            // 
            // ID_DB
            // 
            this.ID_DB.HeaderText = "ID_DB";
            this.ID_DB.MinimumWidth = 9;
            this.ID_DB.Name = "ID_DB";
            this.ID_DB.ReadOnly = true;
            // 
            // TT_Name
            // 
            this.TT_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TT_Name.Frozen = true;
            this.TT_Name.HeaderText = "Tên Nhà Tài Trợ";
            this.TT_Name.MinimumWidth = 9;
            this.TT_Name.Name = "TT_Name";
            this.TT_Name.ReadOnly = true;
            this.TT_Name.Width = 201;
            // 
            // TT_Rep
            // 
            this.TT_Rep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TT_Rep.HeaderText = "Đại Diện";
            this.TT_Rep.MinimumWidth = 9;
            this.TT_Rep.Name = "TT_Rep";
            this.TT_Rep.ReadOnly = true;
            this.TT_Rep.Width = 138;
            // 
            // TT_SDT
            // 
            this.TT_SDT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TT_SDT.HeaderText = "Số Điện Thoại";
            this.TT_SDT.MinimumWidth = 9;
            this.TT_SDT.Name = "TT_SDT";
            this.TT_SDT.ReadOnly = true;
            this.TT_SDT.Width = 187;
            // 
            // TT_Email
            // 
            this.TT_Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TT_Email.HeaderText = "Email";
            this.TT_Email.MinimumWidth = 9;
            this.TT_Email.Name = "TT_Email";
            this.TT_Email.ReadOnly = true;
            this.TT_Email.Width = 108;
            // 
            // TT_Notes
            // 
            this.TT_Notes.HeaderText = "Nội Dung";
            this.TT_Notes.MinimumWidth = 9;
            this.TT_Notes.Name = "TT_Notes";
            this.TT_Notes.ReadOnly = true;
            // 
            // TT_IDDB
            // 
            this.TT_IDDB.HeaderText = "ID_DB";
            this.TT_IDDB.MinimumWidth = 9;
            this.TT_IDDB.Name = "TT_IDDB";
            this.TT_IDDB.ReadOnly = true;
            // 
            // btnGVExport
            // 
            this.btnGVExport.BackColor = System.Drawing.Color.Transparent;
            this.btnGVExport.Location = new System.Drawing.Point(52, 667);
            this.btnGVExport.Name = "btnGVExport";
            this.btnGVExport.Size = new System.Drawing.Size(133, 38);
            this.btnGVExport.TabIndex = 4;
            this.btnGVExport.Text = "Xuất Excel";
            this.btnGVExport.UseVisualStyleBackColor = false;
            this.btnGVExport.Click += new System.EventHandler(this.btnGVExport_Click);
            // 
            // btnDTExport
            // 
            this.btnDTExport.BackColor = System.Drawing.Color.Transparent;
            this.btnDTExport.Location = new System.Drawing.Point(52, 1111);
            this.btnDTExport.Name = "btnDTExport";
            this.btnDTExport.Size = new System.Drawing.Size(133, 38);
            this.btnDTExport.TabIndex = 5;
            this.btnDTExport.Text = "Xuất Excel";
            this.btnDTExport.UseVisualStyleBackColor = false;
            this.btnDTExport.Click += new System.EventHandler(this.btnDTExport_Click);
            // 
            // btnTTExport
            // 
            this.btnTTExport.BackColor = System.Drawing.Color.Transparent;
            this.btnTTExport.Location = new System.Drawing.Point(52, 1543);
            this.btnTTExport.Name = "btnTTExport";
            this.btnTTExport.Size = new System.Drawing.Size(133, 38);
            this.btnTTExport.TabIndex = 6;
            this.btnTTExport.Text = "Xuất Excel";
            this.btnTTExport.UseVisualStyleBackColor = false;
            this.btnTTExport.Click += new System.EventHandler(this.btnTTExport_Click);
            // 
            // frmViewHoatDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 924);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHeader);
            this.Name = "frmViewHoatDong";
            this.Text = "Thông Tin Hoạt Động";
            this.gbGeneralInfo.ResumeLayout(false);
            this.gbGeneralInfo.PerformLayout();
            this.gbSVList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbTaiChinh.ResumeLayout(false);
            this.gbTaiChinh.PerformLayout();
            this.gbTTList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiTro)).EndInit();
            this.gbDT_List.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoiTac)).EndInit();
            this.gbGVList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox gbGeneralInfo;
        private System.Windows.Forms.TextBox txtTenHD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbSVList;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvSinhVien;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbGVList;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgv_GV;
        private System.Windows.Forms.GroupBox gbDT_List;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvDoiTac;
        private System.Windows.Forms.GroupBox gbTTList;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvTaiTro;
        private System.Windows.Forms.GroupBox gbTaiChinh;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtTC_TieuDe;
        private System.Windows.Forms.TextBox txtTC_Khac;
        private System.Windows.Forms.Label label30;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numTaiTro;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numUEF;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtDateEnd;
        private System.Windows.Forms.TextBox txtDateBegin;
        private System.Windows.Forms.TextBox txtLoai;
        private System.Windows.Forms.Button btnSVExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoTenSV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Khoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes_SV;
        private System.Windows.Forms.DataGridViewTextBoxColumn DB_Khoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_Rep;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_Notes;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_IDDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_Ten;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_DaiDien;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_NoiDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_DB;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoTenLot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ten;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVKhoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn GV_Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVKHoa_DB;
        private System.Windows.Forms.Button btnGVExport;
        private System.Windows.Forms.Button btnTTExport;
        private System.Windows.Forms.Button btnDTExport;
    }
}