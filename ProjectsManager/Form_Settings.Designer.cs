namespace ProjectsManager
{
    partial class Form_Settings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Settings));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCheckConnectivity = new System.Windows.Forms.Button();
            this.pictureBoxConnection = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageVariables = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelUpdateVat = new System.Windows.Forms.Label();
            this.textBoxNewVat = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dateTimePickerNewVat = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCurrentVat = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewVAT = new System.Windows.Forms.DataGridView();
            this.tabPageExport = new System.Windows.Forms.TabPage();
            this.buttonSaveImportLibrary = new System.Windows.Forms.Button();
            this.buttonBrowseImportLibrary = new System.Windows.Forms.Button();
            this.textBoxImportLibrary = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonSaveExportLibrary = new System.Windows.Forms.Button();
            this.buttonBrowseExportLibrary = new System.Windows.Forms.Button();
            this.textBoxIExportLibrary = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPageServer = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageUsers = new System.Windows.Forms.TabPage();
            this.panelUsers = new System.Windows.Forms.Panel();
            this.comboBoxUserTypes = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.textBoxNewUsername = new System.Windows.Forms.TextBox();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonManageUsers = new System.Windows.Forms.Button();
            this.textBoxAdminPassword = new System.Windows.Forms.TextBox();
            this.textBoxAdminUsername = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.contextMenuStripDeleteUser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnection)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageVariables.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVAT)).BeginInit();
            this.tabPageExport.SuspendLayout();
            this.tabPageServer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPageUsers.SuspendLayout();
            this.panelUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.contextMenuStripDeleteUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(40, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(53, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 75);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 107);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(57, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxServer.Location = new System.Drawing.Point(116, 6);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxServer.Size = new System.Drawing.Size(101, 20);
            this.textBoxServer.TabIndex = 4;
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxDatabase.Location = new System.Drawing.Point(116, 38);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxDatabase.Size = new System.Drawing.Size(101, 20);
            this.textBoxDatabase.TabIndex = 5;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxUsername.Location = new System.Drawing.Point(116, 72);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxUsername.Size = new System.Drawing.Size(101, 20);
            this.textBoxUsername.TabIndex = 6;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxPassword.Location = new System.Drawing.Point(116, 104);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxPassword.Size = new System.Drawing.Size(101, 20);
            this.textBoxPassword.TabIndex = 7;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonSave.Location = new System.Drawing.Point(223, 159);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(69, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "שמירה";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCheckConnectivity
            // 
            this.buttonCheckConnectivity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCheckConnectivity.Location = new System.Drawing.Point(116, 132);
            this.buttonCheckConnectivity.Name = "buttonCheckConnectivity";
            this.buttonCheckConnectivity.Size = new System.Drawing.Size(101, 21);
            this.buttonCheckConnectivity.TabIndex = 9;
            this.buttonCheckConnectivity.Text = "בדיקת חיבור לשרת";
            this.buttonCheckConnectivity.UseVisualStyleBackColor = true;
            this.buttonCheckConnectivity.Click += new System.EventHandler(this.buttonCheckConnectivity_Click);
            // 
            // pictureBoxConnection
            // 
            this.pictureBoxConnection.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBoxConnection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxConnection.BackgroundImage")));
            this.pictureBoxConnection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxConnection.Location = new System.Drawing.Point(223, 132);
            this.pictureBoxConnection.Name = "pictureBoxConnection";
            this.pictureBoxConnection.Size = new System.Drawing.Size(44, 21);
            this.pictureBoxConnection.TabIndex = 10;
            this.pictureBoxConnection.TabStop = false;
            this.pictureBoxConnection.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageVariables);
            this.tabControl1.Controls.Add(this.tabPageExport);
            this.tabControl1.Controls.Add(this.tabPageServer);
            this.tabControl1.Controls.Add(this.tabPageUsers);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabControl1.Location = new System.Drawing.Point(12, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(488, 238);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPageVariables
            // 
            this.tabPageVariables.Controls.Add(this.groupBox1);
            this.tabPageVariables.Controls.Add(this.textBoxCurrentVat);
            this.tabPageVariables.Controls.Add(this.label5);
            this.tabPageVariables.Controls.Add(this.dataGridViewVAT);
            this.tabPageVariables.Location = new System.Drawing.Point(4, 23);
            this.tabPageVariables.Name = "tabPageVariables";
            this.tabPageVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVariables.Size = new System.Drawing.Size(480, 211);
            this.tabPageVariables.TabIndex = 0;
            this.tabPageVariables.Text = "משתנים";
            this.tabPageVariables.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelUpdateVat);
            this.groupBox1.Controls.Add(this.textBoxNewVat);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dateTimePickerNewVat);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(267, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 94);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "עדכון מע\"מ";
            // 
            // labelUpdateVat
            // 
            this.labelUpdateVat.AutoSize = true;
            this.labelUpdateVat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelUpdateVat.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelUpdateVat.ForeColor = System.Drawing.Color.Blue;
            this.labelUpdateVat.Location = new System.Drawing.Point(8, 61);
            this.labelUpdateVat.Name = "labelUpdateVat";
            this.labelUpdateVat.Size = new System.Drawing.Size(30, 14);
            this.labelUpdateVat.TabIndex = 4;
            this.labelUpdateVat.Text = "עדכון";
            this.labelUpdateVat.Click += new System.EventHandler(this.labelUpdateVat_Click);
            // 
            // textBoxNewVat
            // 
            this.textBoxNewVat.Location = new System.Drawing.Point(48, 58);
            this.textBoxNewVat.Name = "textBoxNewVat";
            this.textBoxNewVat.Size = new System.Drawing.Size(60, 20);
            this.textBoxNewVat.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(123, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 14);
            this.label8.TabIndex = 2;
            this.label8.Text = "ערך";
            // 
            // dateTimePickerNewVat
            // 
            this.dateTimePickerNewVat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerNewVat.Location = new System.Drawing.Point(10, 26);
            this.dateTimePickerNewVat.Name = "dateTimePickerNewVat";
            this.dateTimePickerNewVat.RightToLeftLayout = true;
            this.dateTimePickerNewVat.Size = new System.Drawing.Size(98, 20);
            this.dateTimePickerNewVat.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(114, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "תאריך";
            // 
            // textBoxCurrentVat
            // 
            this.textBoxCurrentVat.BackColor = System.Drawing.Color.White;
            this.textBoxCurrentVat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCurrentVat.Location = new System.Drawing.Point(315, 25);
            this.textBoxCurrentVat.Name = "textBoxCurrentVat";
            this.textBoxCurrentVat.ReadOnly = true;
            this.textBoxCurrentVat.Size = new System.Drawing.Size(40, 13);
            this.textBoxCurrentVat.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(354, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "מע\"מ נוכחי: %";
            // 
            // dataGridViewVAT
            // 
            this.dataGridViewVAT.AllowUserToAddRows = false;
            this.dataGridViewVAT.AllowUserToDeleteRows = false;
            this.dataGridViewVAT.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewVAT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewVAT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVAT.ColumnHeadersVisible = false;
            this.dataGridViewVAT.Location = new System.Drawing.Point(79, 56);
            this.dataGridViewVAT.Name = "dataGridViewVAT";
            this.dataGridViewVAT.ReadOnly = true;
            this.dataGridViewVAT.RowHeadersVisible = false;
            this.dataGridViewVAT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewVAT.Size = new System.Drawing.Size(160, 149);
            this.dataGridViewVAT.TabIndex = 0;
            // 
            // tabPageExport
            // 
            this.tabPageExport.Controls.Add(this.buttonSaveImportLibrary);
            this.tabPageExport.Controls.Add(this.buttonBrowseImportLibrary);
            this.tabPageExport.Controls.Add(this.textBoxImportLibrary);
            this.tabPageExport.Controls.Add(this.label9);
            this.tabPageExport.Controls.Add(this.buttonSaveExportLibrary);
            this.tabPageExport.Controls.Add(this.buttonBrowseExportLibrary);
            this.tabPageExport.Controls.Add(this.textBoxIExportLibrary);
            this.tabPageExport.Controls.Add(this.label6);
            this.tabPageExport.Location = new System.Drawing.Point(4, 23);
            this.tabPageExport.Name = "tabPageExport";
            this.tabPageExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExport.Size = new System.Drawing.Size(480, 211);
            this.tabPageExport.TabIndex = 2;
            this.tabPageExport.Text = "ייצוא חשבונות";
            this.tabPageExport.UseVisualStyleBackColor = true;
            // 
            // buttonSaveImportLibrary
            // 
            this.buttonSaveImportLibrary.Location = new System.Drawing.Point(75, 172);
            this.buttonSaveImportLibrary.Name = "buttonSaveImportLibrary";
            this.buttonSaveImportLibrary.Size = new System.Drawing.Size(49, 23);
            this.buttonSaveImportLibrary.TabIndex = 10;
            this.buttonSaveImportLibrary.Text = "שמור";
            this.buttonSaveImportLibrary.UseVisualStyleBackColor = true;
            this.buttonSaveImportLibrary.Click += new System.EventHandler(this.buttonSaveImportLibrary_Click);
            // 
            // buttonBrowseImportLibrary
            // 
            this.buttonBrowseImportLibrary.Location = new System.Drawing.Point(75, 143);
            this.buttonBrowseImportLibrary.Name = "buttonBrowseImportLibrary";
            this.buttonBrowseImportLibrary.Size = new System.Drawing.Size(49, 23);
            this.buttonBrowseImportLibrary.TabIndex = 9;
            this.buttonBrowseImportLibrary.Text = "עיון...";
            this.buttonBrowseImportLibrary.UseVisualStyleBackColor = true;
            this.buttonBrowseImportLibrary.Click += new System.EventHandler(this.buttonBrowseImportLibrary_Click);
            // 
            // textBoxImportLibrary
            // 
            this.textBoxImportLibrary.Location = new System.Drawing.Point(142, 144);
            this.textBoxImportLibrary.Name = "textBoxImportLibrary";
            this.textBoxImportLibrary.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxImportLibrary.Size = new System.Drawing.Size(247, 20);
            this.textBoxImportLibrary.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label9.Location = new System.Drawing.Point(288, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 14);
            this.label9.TabIndex = 7;
            this.label9.Text = "ספריית קבצי התבנית:";
            // 
            // buttonSaveExportLibrary
            // 
            this.buttonSaveExportLibrary.Location = new System.Drawing.Point(75, 82);
            this.buttonSaveExportLibrary.Name = "buttonSaveExportLibrary";
            this.buttonSaveExportLibrary.Size = new System.Drawing.Size(49, 23);
            this.buttonSaveExportLibrary.TabIndex = 6;
            this.buttonSaveExportLibrary.Text = "שמור";
            this.buttonSaveExportLibrary.UseVisualStyleBackColor = true;
            this.buttonSaveExportLibrary.Click += new System.EventHandler(this.buttonSaveExportLibrary_Click);
            // 
            // buttonBrowseExportLibrary
            // 
            this.buttonBrowseExportLibrary.Location = new System.Drawing.Point(75, 53);
            this.buttonBrowseExportLibrary.Name = "buttonBrowseExportLibrary";
            this.buttonBrowseExportLibrary.Size = new System.Drawing.Size(49, 23);
            this.buttonBrowseExportLibrary.TabIndex = 5;
            this.buttonBrowseExportLibrary.Text = "עיון...";
            this.buttonBrowseExportLibrary.UseVisualStyleBackColor = true;
            this.buttonBrowseExportLibrary.Click += new System.EventHandler(this.buttonBrowseExportLibrary_Click);
            // 
            // textBoxIExportLibrary
            // 
            this.textBoxIExportLibrary.Location = new System.Drawing.Point(142, 54);
            this.textBoxIExportLibrary.Name = "textBoxIExportLibrary";
            this.textBoxIExportLibrary.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxIExportLibrary.Size = new System.Drawing.Size(247, 20);
            this.textBoxIExportLibrary.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(295, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 14);
            this.label6.TabIndex = 3;
            this.label6.Text = "ספריית ייצוא קבצים:";
            // 
            // tabPageServer
            // 
            this.tabPageServer.Controls.Add(this.tableLayoutPanel1);
            this.tabPageServer.Location = new System.Drawing.Point(4, 23);
            this.tabPageServer.Name = "tabPageServer";
            this.tabPageServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServer.Size = new System.Drawing.Size(480, 211);
            this.tabPageServer.TabIndex = 1;
            this.tabPageServer.Text = "שרת";
            this.tabPageServer.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.2844F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.7156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxConnection, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonCheckConnectivity, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPassword, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxServer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxUsername, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxDatabase, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(46, 14);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(295, 186);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabPageUsers
            // 
            this.tabPageUsers.Controls.Add(this.panelUsers);
            this.tabPageUsers.Controls.Add(this.buttonManageUsers);
            this.tabPageUsers.Controls.Add(this.textBoxAdminPassword);
            this.tabPageUsers.Controls.Add(this.textBoxAdminUsername);
            this.tabPageUsers.Controls.Add(this.label12);
            this.tabPageUsers.Controls.Add(this.label11);
            this.tabPageUsers.Controls.Add(this.label10);
            this.tabPageUsers.Location = new System.Drawing.Point(4, 23);
            this.tabPageUsers.Name = "tabPageUsers";
            this.tabPageUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsers.Size = new System.Drawing.Size(480, 211);
            this.tabPageUsers.TabIndex = 3;
            this.tabPageUsers.Text = "משתמשים";
            this.tabPageUsers.UseVisualStyleBackColor = true;
            this.tabPageUsers.Enter += new System.EventHandler(this.tabPageUsers_Enter);
            // 
            // panelUsers
            // 
            this.panelUsers.Controls.Add(this.comboBoxUserTypes);
            this.panelUsers.Controls.Add(this.label13);
            this.panelUsers.Controls.Add(this.buttonAddUser);
            this.panelUsers.Controls.Add(this.textBoxNewUsername);
            this.panelUsers.Controls.Add(this.textBoxNewPassword);
            this.panelUsers.Controls.Add(this.label15);
            this.panelUsers.Controls.Add(this.dataGridViewUsers);
            this.panelUsers.Controls.Add(this.label14);
            this.panelUsers.Location = new System.Drawing.Point(6, 55);
            this.panelUsers.Name = "panelUsers";
            this.panelUsers.Size = new System.Drawing.Size(468, 150);
            this.panelUsers.TabIndex = 6;
            this.panelUsers.Visible = false;
            // 
            // comboBoxUserTypes
            // 
            this.comboBoxUserTypes.FormattingEnabled = true;
            this.comboBoxUserTypes.Location = new System.Drawing.Point(90, 123);
            this.comboBoxUserTypes.Name = "comboBoxUserTypes";
            this.comboBoxUserTypes.Size = new System.Drawing.Size(78, 22);
            this.comboBoxUserTypes.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(174, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 14);
            this.label13.TabIndex = 11;
            this.label13.Text = "סוג";
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Location = new System.Drawing.Point(17, 121);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(56, 23);
            this.buttonAddUser.TabIndex = 6;
            this.buttonAddUser.Text = "חדש";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // textBoxNewUsername
            // 
            this.textBoxNewUsername.Location = new System.Drawing.Point(328, 124);
            this.textBoxNewUsername.Name = "textBoxNewUsername";
            this.textBoxNewUsername.Size = new System.Drawing.Size(80, 20);
            this.textBoxNewUsername.TabIndex = 3;
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.Location = new System.Drawing.Point(202, 124);
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.Size = new System.Drawing.Size(80, 20);
            this.textBoxNewPassword.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(414, 127);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 14);
            this.label15.TabIndex = 7;
            this.label15.Text = "משתמש";
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.AllowUserToAddRows = false;
            this.dataGridViewUsers.AllowUserToDeleteRows = false;
            this.dataGridViewUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUsers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewUsers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewUsers.MultiSelect = false;
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.ReadOnly = true;
            this.dataGridViewUsers.RowHeadersVisible = false;
            this.dataGridViewUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUsers.Size = new System.Drawing.Size(462, 112);
            this.dataGridViewUsers.TabIndex = 0;
            this.dataGridViewUsers.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewUsers_CellMouseDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(288, 127);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 14);
            this.label14.TabIndex = 8;
            this.label14.Text = "סיסמא";
            // 
            // buttonManageUsers
            // 
            this.buttonManageUsers.Location = new System.Drawing.Point(50, 27);
            this.buttonManageUsers.Name = "buttonManageUsers";
            this.buttonManageUsers.Size = new System.Drawing.Size(85, 22);
            this.buttonManageUsers.TabIndex = 2;
            this.buttonManageUsers.Text = "ניהול";
            this.buttonManageUsers.UseVisualStyleBackColor = true;
            this.buttonManageUsers.Click += new System.EventHandler(this.buttonManageUsers_Click);
            // 
            // textBoxAdminPassword
            // 
            this.textBoxAdminPassword.Location = new System.Drawing.Point(169, 29);
            this.textBoxAdminPassword.Name = "textBoxAdminPassword";
            this.textBoxAdminPassword.PasswordChar = '*';
            this.textBoxAdminPassword.Size = new System.Drawing.Size(85, 20);
            this.textBoxAdminPassword.TabIndex = 1;
            // 
            // textBoxAdminUsername
            // 
            this.textBoxAdminUsername.Location = new System.Drawing.Point(288, 29);
            this.textBoxAdminUsername.Name = "textBoxAdminUsername";
            this.textBoxAdminUsername.Size = new System.Drawing.Size(85, 20);
            this.textBoxAdminUsername.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(220, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 14);
            this.label12.TabIndex = 12;
            this.label12.Text = "סיסמא";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(311, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 14);
            this.label11.TabIndex = 11;
            this.label11.Text = "שם משתמש";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(391, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 14);
            this.label10.TabIndex = 10;
            this.label10.Text = "מנהל:";
            // 
            // contextMenuStripDeleteUser
            // 
            this.contextMenuStripDeleteUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDelete});
            this.contextMenuStripDeleteUser.Name = "contextMenuStripDeleteUser";
            this.contextMenuStripDeleteUser.Size = new System.Drawing.Size(110, 26);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemDelete.Text = "מחיקה";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(512, 265);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form_Settings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "הגדרות";
            this.Load += new System.EventHandler(this.Form_Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnection)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageVariables.ResumeLayout(false);
            this.tabPageVariables.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVAT)).EndInit();
            this.tabPageExport.ResumeLayout(false);
            this.tabPageExport.PerformLayout();
            this.tabPageServer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPageUsers.ResumeLayout(false);
            this.tabPageUsers.PerformLayout();
            this.panelUsers.ResumeLayout(false);
            this.panelUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.contextMenuStripDeleteUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCheckConnectivity;
        private System.Windows.Forms.PictureBox pictureBoxConnection;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageVariables;
        private System.Windows.Forms.TabPage tabPageServer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewVAT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelUpdateVat;
        private System.Windows.Forms.TextBox textBoxNewVat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dateTimePickerNewVat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCurrentVat;
        private System.Windows.Forms.TabPage tabPageExport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonBrowseExportLibrary;
        private System.Windows.Forms.TextBox textBoxIExportLibrary;
        private System.Windows.Forms.Button buttonSaveExportLibrary;
        private System.Windows.Forms.Button buttonSaveImportLibrary;
        private System.Windows.Forms.Button buttonBrowseImportLibrary;
        private System.Windows.Forms.TextBox textBoxImportLibrary;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPageUsers;
        private System.Windows.Forms.Panel panelUsers;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.TextBox textBoxNewUsername;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonManageUsers;
        private System.Windows.Forms.TextBox textBoxAdminPassword;
        private System.Windows.Forms.TextBox textBoxAdminUsername;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxUserTypes;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDeleteUser;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
    }
}