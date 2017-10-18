namespace ProjectsManager
{
    partial class Form_ApproveExpense
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
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxDate = new System.Windows.Forms.CheckBox();
            this.checkBoxPaiment = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonYes
            // 
            this.buttonYes.Location = new System.Drawing.Point(53, 153);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(75, 23);
            this.buttonYes.TabIndex = 0;
            this.buttonYes.Text = "אישור";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.Location = new System.Drawing.Point(167, 153);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(75, 23);
            this.buttonNo.TabIndex = 1;
            this.buttonNo.Text = "ביטול";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(121, 76);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.RightToLeftLayout = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "לא מאושר",
            "מאושר"});
            this.comboBox1.Location = new System.Drawing.Point(121, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 22);
            this.comboBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "סכום";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(121, 111);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 6;
            // 
            // checkBoxDate
            // 
            this.checkBoxDate.AutoSize = true;
            this.checkBoxDate.Checked = true;
            this.checkBoxDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDate.Location = new System.Drawing.Point(53, 78);
            this.checkBoxDate.Name = "checkBoxDate";
            this.checkBoxDate.Size = new System.Drawing.Size(60, 18);
            this.checkBoxDate.TabIndex = 7;
            this.checkBoxDate.Text = "בתאריך";
            this.checkBoxDate.UseVisualStyleBackColor = true;
            this.checkBoxDate.CheckedChanged += new System.EventHandler(this.checkBoxDate_CheckedChanged);
            // 
            // checkBoxPaiment
            // 
            this.checkBoxPaiment.AutoSize = true;
            this.checkBoxPaiment.Checked = true;
            this.checkBoxPaiment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPaiment.Location = new System.Drawing.Point(53, 40);
            this.checkBoxPaiment.Name = "checkBoxPaiment";
            this.checkBoxPaiment.Size = new System.Drawing.Size(57, 18);
            this.checkBoxPaiment.TabIndex = 7;
            this.checkBoxPaiment.Text = "תשלום";
            this.checkBoxPaiment.UseVisualStyleBackColor = true;
            this.checkBoxPaiment.CheckedChanged += new System.EventHandler(this.checkBoxPaiment_CheckedChanged);
            // 
            // Form_ApproveExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(296, 201);
            this.Controls.Add(this.checkBoxPaiment);
            this.Controls.Add(this.checkBoxDate);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form_ApproveExpense";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "שינוי סטטוס תשלום";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBoxDate;
        private System.Windows.Forms.CheckBox checkBoxPaiment;
    }
}