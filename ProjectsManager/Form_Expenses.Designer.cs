namespace ProjectsManager
{
    partial class Form_Expenses
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comboBoxSuppliers = new System.Windows.Forms.ComboBox();
            this.dataGridViewExpenses = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonSingle = new System.Windows.Forms.RadioButton();
            this.radioButtonDebit = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxAmountSingle = new System.Windows.Forms.TextBox();
            this.labelAmountSingle = new System.Windows.Forms.Label();
            this.labelDateSingle = new System.Windows.Forms.Label();
            this.dateTimePickerSingle = new System.Windows.Forms.DateTimePicker();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxDebitDuration = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxDebitInterval = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerDebit = new System.Windows.Forms.DateTimePicker();
            this.labelDateDebit = new System.Windows.Forms.Label();
            this.textBoxAmountDebit = new System.Windows.Forms.TextBox();
            this.labelAmountDebit = new System.Windows.Forms.Label();
            this.radioButtonConst = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNotesSingle = new System.Windows.Forms.TextBox();
            this.linkLabelDelete = new System.Windows.Forms.LinkLabel();
            this.linkLabelDeleteExpenses = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpenses)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxSuppliers
            // 
            this.comboBoxSuppliers.AllowDrop = true;
            this.comboBoxSuppliers.DropDownHeight = 500;
            this.comboBoxSuppliers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSuppliers.FormattingEnabled = true;
            this.comboBoxSuppliers.IntegralHeight = false;
            this.comboBoxSuppliers.Location = new System.Drawing.Point(90, 16);
            this.comboBoxSuppliers.Name = "comboBoxSuppliers";
            this.comboBoxSuppliers.Size = new System.Drawing.Size(148, 22);
            this.comboBoxSuppliers.TabIndex = 0;
            this.comboBoxSuppliers.SelectedIndexChanged += new System.EventHandler(this.comboBoxSuppliers_SelectedIndexChanged);
            // 
            // dataGridViewExpenses
            // 
            this.dataGridViewExpenses.AllowUserToAddRows = false;
            this.dataGridViewExpenses.AllowUserToDeleteRows = false;
            this.dataGridViewExpenses.AllowUserToResizeColumns = false;
            this.dataGridViewExpenses.AllowUserToResizeRows = false;
            this.dataGridViewExpenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewExpenses.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewExpenses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewExpenses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewExpenses.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewExpenses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewExpenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewExpenses.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewExpenses.Location = new System.Drawing.Point(381, 20);
            this.dataGridViewExpenses.Name = "dataGridViewExpenses";
            this.dataGridViewExpenses.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridViewExpenses.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewExpenses.RowHeadersVisible = false;
            this.dataGridViewExpenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewExpenses.Size = new System.Drawing.Size(385, 325);
            this.dataGridViewExpenses.TabIndex = 1;
            this.dataGridViewExpenses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExpenses_CellContentClick);
            this.dataGridViewExpenses.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExpenses_CellEndEdit);
            this.dataGridViewExpenses.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewExpenses_CellFormatting);
            this.dataGridViewExpenses.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExpenses_CellValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "עבור ספק:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "הוסף הוצאה";
            // 
            // radioButtonSingle
            // 
            this.radioButtonSingle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioButtonSingle.AutoSize = true;
            this.radioButtonSingle.Checked = true;
            this.radioButtonSingle.Location = new System.Drawing.Point(193, 6);
            this.radioButtonSingle.Name = "radioButtonSingle";
            this.radioButtonSingle.Size = new System.Drawing.Size(70, 18);
            this.radioButtonSingle.TabIndex = 4;
            this.radioButtonSingle.TabStop = true;
            this.radioButtonSingle.Text = "חד פעמית";
            this.radioButtonSingle.UseVisualStyleBackColor = true;
            this.radioButtonSingle.CheckedChanged += new System.EventHandler(this.radioButtonSingle_CheckedChanged);
            // 
            // radioButtonDebit
            // 
            this.radioButtonDebit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioButtonDebit.AutoSize = true;
            this.radioButtonDebit.Location = new System.Drawing.Point(188, 96);
            this.radioButtonDebit.Name = "radioButtonDebit";
            this.radioButtonDebit.Size = new System.Drawing.Size(75, 18);
            this.radioButtonDebit.TabIndex = 5;
            this.radioButtonDebit.Text = "הוראת קבע";
            this.radioButtonDebit.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.49681F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.50319F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxAmountSingle, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonSingle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelAmountSingle, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelDateSingle, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePickerSingle, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 3, 9);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDebitDuration, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDebitInterval, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePickerDebit, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelDateDebit, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAmountDebit, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelAmountDebit, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonConst, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.radioButtonDebit, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxNotesSingle, 3, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 45);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(343, 300);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // textBoxAmountSingle
            // 
            this.textBoxAmountSingle.Location = new System.Drawing.Point(23, 3);
            this.textBoxAmountSingle.Name = "textBoxAmountSingle";
            this.textBoxAmountSingle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxAmountSingle.Size = new System.Drawing.Size(100, 20);
            this.textBoxAmountSingle.TabIndex = 7;
            this.textBoxAmountSingle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelAmountSingle
            // 
            this.labelAmountSingle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAmountSingle.AutoSize = true;
            this.labelAmountSingle.Location = new System.Drawing.Point(146, 8);
            this.labelAmountSingle.Name = "labelAmountSingle";
            this.labelAmountSingle.Size = new System.Drawing.Size(29, 14);
            this.labelAmountSingle.TabIndex = 6;
            this.labelAmountSingle.Text = "סכום";
            // 
            // labelDateSingle
            // 
            this.labelDateSingle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDateSingle.AutoSize = true;
            this.labelDateSingle.Location = new System.Drawing.Point(140, 38);
            this.labelDateSingle.Name = "labelDateSingle";
            this.labelDateSingle.Size = new System.Drawing.Size(35, 14);
            this.labelDateSingle.TabIndex = 7;
            this.labelDateSingle.Text = "תאריך";
            // 
            // dateTimePickerSingle
            // 
            this.dateTimePickerSingle.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerSingle.Location = new System.Drawing.Point(23, 33);
            this.dateTimePickerSingle.Name = "dateTimePickerSingle";
            this.dateTimePickerSingle.RightToLeftLayout = true;
            this.dateTimePickerSingle.Size = new System.Drawing.Size(100, 20);
            this.dateTimePickerSingle.TabIndex = 7;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.Location = new System.Drawing.Point(26, 273);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "צור חדש";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxDebitDuration
            // 
            this.comboBoxDebitDuration.AllowDrop = true;
            this.comboBoxDebitDuration.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxDebitDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDebitDuration.Enabled = false;
            this.comboBoxDebitDuration.FormattingEnabled = true;
            this.comboBoxDebitDuration.Items.AddRange(new object[] {
            "3 חדשים",
            "חצי שנה",
            "שנה ",
            "שנתיים",
            "שלוש שנים",
            "5 שנים"});
            this.comboBoxDebitDuration.Location = new System.Drawing.Point(23, 214);
            this.comboBoxDebitDuration.Name = "comboBoxDebitDuration";
            this.comboBoxDebitDuration.Size = new System.Drawing.Size(100, 22);
            this.comboBoxDebitDuration.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 14);
            this.label4.TabIndex = 15;
            this.label4.Text = "למשך";
            // 
            // comboBoxDebitInterval
            // 
            this.comboBoxDebitInterval.AllowDrop = true;
            this.comboBoxDebitInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxDebitInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDebitInterval.Enabled = false;
            this.comboBoxDebitInterval.FormattingEnabled = true;
            this.comboBoxDebitInterval.Items.AddRange(new object[] {
            "חודשי",
            "דו-חודשי",
            "רבעון",
            "חצי-שנתי",
            "שנתי"});
            this.comboBoxDebitInterval.Location = new System.Drawing.Point(23, 184);
            this.comboBoxDebitInterval.Name = "comboBoxDebitInterval";
            this.comboBoxDebitInterval.Size = new System.Drawing.Size(100, 22);
            this.comboBoxDebitInterval.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "חזרה";
            // 
            // dateTimePickerDebit
            // 
            this.dateTimePickerDebit.Enabled = false;
            this.dateTimePickerDebit.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDebit.Location = new System.Drawing.Point(23, 153);
            this.dateTimePickerDebit.Name = "dateTimePickerDebit";
            this.dateTimePickerDebit.RightToLeftLayout = true;
            this.dateTimePickerDebit.Size = new System.Drawing.Size(100, 20);
            this.dateTimePickerDebit.TabIndex = 8;
            // 
            // labelDateDebit
            // 
            this.labelDateDebit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDateDebit.AutoSize = true;
            this.labelDateDebit.Location = new System.Drawing.Point(133, 158);
            this.labelDateDebit.Name = "labelDateDebit";
            this.labelDateDebit.Size = new System.Drawing.Size(42, 14);
            this.labelDateDebit.TabIndex = 12;
            this.labelDateDebit.Text = "מתאריך";
            // 
            // textBoxAmountDebit
            // 
            this.textBoxAmountDebit.Enabled = false;
            this.textBoxAmountDebit.Location = new System.Drawing.Point(23, 123);
            this.textBoxAmountDebit.Name = "textBoxAmountDebit";
            this.textBoxAmountDebit.Size = new System.Drawing.Size(100, 20);
            this.textBoxAmountDebit.TabIndex = 11;
            // 
            // labelAmountDebit
            // 
            this.labelAmountDebit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAmountDebit.AutoSize = true;
            this.labelAmountDebit.Location = new System.Drawing.Point(146, 128);
            this.labelAmountDebit.Name = "labelAmountDebit";
            this.labelAmountDebit.Size = new System.Drawing.Size(29, 14);
            this.labelAmountDebit.TabIndex = 13;
            this.labelAmountDebit.Text = "סכום";
            // 
            // radioButtonConst
            // 
            this.radioButtonConst.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioButtonConst.AutoSize = true;
            this.radioButtonConst.Location = new System.Drawing.Point(181, 126);
            this.radioButtonConst.Name = "radioButtonConst";
            this.radioButtonConst.Size = new System.Drawing.Size(82, 18);
            this.radioButtonConst.TabIndex = 18;
            this.radioButtonConst.Text = "הוצאה קבועה";
            this.radioButtonConst.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "הערות";
            // 
            // textBoxNotesSingle
            // 
            this.textBoxNotesSingle.Location = new System.Drawing.Point(23, 63);
            this.textBoxNotesSingle.Name = "textBoxNotesSingle";
            this.textBoxNotesSingle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxNotesSingle.Size = new System.Drawing.Size(100, 20);
            this.textBoxNotesSingle.TabIndex = 20;
            this.textBoxNotesSingle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // linkLabelDelete
            // 
            this.linkLabelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelDelete.AutoSize = true;
            this.linkLabelDelete.LinkColor = System.Drawing.Color.Red;
            this.linkLabelDelete.Location = new System.Drawing.Point(707, -33);
            this.linkLabelDelete.Name = "linkLabelDelete";
            this.linkLabelDelete.Size = new System.Drawing.Size(59, 14);
            this.linkLabelDelete.TabIndex = 18;
            this.linkLabelDelete.TabStop = true;
            this.linkLabelDelete.Text = "מחק בחירה";
            this.linkLabelDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDelete_LinkClicked);
            // 
            // linkLabelDeleteExpenses
            // 
            this.linkLabelDeleteExpenses.AutoSize = true;
            this.linkLabelDeleteExpenses.LinkColor = System.Drawing.Color.Red;
            this.linkLabelDeleteExpenses.Location = new System.Drawing.Point(707, 3);
            this.linkLabelDeleteExpenses.Name = "linkLabelDeleteExpenses";
            this.linkLabelDeleteExpenses.Size = new System.Drawing.Size(59, 14);
            this.linkLabelDeleteExpenses.TabIndex = 19;
            this.linkLabelDeleteExpenses.TabStop = true;
            this.linkLabelDeleteExpenses.Text = "מחק בחירה";
            this.linkLabelDeleteExpenses.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDelete_LinkClicked);
            // 
            // Form_Expenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(778, 353);
            this.Controls.Add(this.linkLabelDeleteExpenses);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataGridViewExpenses);
            this.Controls.Add(this.linkLabelDelete);
            this.Controls.Add(this.comboBoxSuppliers);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form_Expenses";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "הוצאות";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Expenses_FormClosed);
            this.Load += new System.EventHandler(this.Form_Expenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExpenses)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSuppliers;
        private System.Windows.Forms.DataGridView dataGridViewExpenses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonSingle;
        private System.Windows.Forms.RadioButton radioButtonDebit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxAmountSingle;
        private System.Windows.Forms.DateTimePicker dateTimePickerSingle;
        private System.Windows.Forms.Label labelAmountSingle;
        private System.Windows.Forms.Label labelDateSingle;
        private System.Windows.Forms.DateTimePicker dateTimePickerDebit;
        private System.Windows.Forms.TextBox textBoxAmountDebit;
        private System.Windows.Forms.Label labelDateDebit;
        private System.Windows.Forms.Label labelAmountDebit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxDebitInterval;
        private System.Windows.Forms.ComboBox comboBoxDebitDuration;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.LinkLabel linkLabelDelete;
        private System.Windows.Forms.LinkLabel linkLabelDeleteExpenses;
        private System.Windows.Forms.RadioButton radioButtonConst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxNotesSingle;
    }
}