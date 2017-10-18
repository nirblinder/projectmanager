namespace ProjectsManager
{
    partial class Form_EditIncome
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.buttonNo = new System.Windows.Forms.Button();
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonGoToBill = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(146, 54);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.RightToLeftLayout = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(81, 57);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(35, 14);
            this.labelDate.TabIndex = 7;
            this.labelDate.Text = "תאריך";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonNo
            // 
            this.buttonNo.Location = new System.Drawing.Point(253, 131);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(75, 23);
            this.buttonNo.TabIndex = 6;
            this.buttonNo.Text = "ביטול";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // buttonYes
            // 
            this.buttonYes.Location = new System.Drawing.Point(160, 131);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(75, 23);
            this.buttonYes.TabIndex = 5;
            this.buttonYes.Text = "אישור";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // buttonGoToBill
            // 
            this.buttonGoToBill.Location = new System.Drawing.Point(13, 130);
            this.buttonGoToBill.Name = "buttonGoToBill";
            this.buttonGoToBill.Size = new System.Drawing.Size(85, 23);
            this.buttonGoToBill.TabIndex = 9;
            this.buttonGoToBill.Text = "מעבר לחשבון";
            this.buttonGoToBill.UseVisualStyleBackColor = true;
            this.buttonGoToBill.Click += new System.EventHandler(this.buttonGoToBill_Click);
            // 
            // Form_EditIncome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(344, 183);
            this.Controls.Add(this.buttonGoToBill);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_EditIncome";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "שינוי הכנסה";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonGoToBill;
    }
}