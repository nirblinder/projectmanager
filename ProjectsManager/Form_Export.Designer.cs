namespace ProjectsManager
{
    partial class Form_Export
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
            this.radioButtonBillNormal = new System.Windows.Forms.RadioButton();
            this.radioButtonBillGov = new System.Windows.Forms.RadioButton();
            this.radioButtonBillOne = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBrowseExportLibrary = new System.Windows.Forms.Button();
            this.textBoxExportLibrary = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.groupBoxOneBillDetails = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBillHourCost = new System.Windows.Forms.TextBox();
            this.textBoxBillHours = new System.Windows.Forms.TextBox();
            this.labelBillHours = new System.Windows.Forms.Label();
            this.labelBillHourCost = new System.Windows.Forms.Label();
            this.radioButtonBillNormalDesc = new System.Windows.Forms.RadioButton();
            this.groupBoxOneBillDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonBillNormal
            // 
            this.radioButtonBillNormal.AutoSize = true;
            this.radioButtonBillNormal.Checked = true;
            this.radioButtonBillNormal.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBillNormal.Location = new System.Drawing.Point(102, 62);
            this.radioButtonBillNormal.Name = "radioButtonBillNormal";
            this.radioButtonBillNormal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButtonBillNormal.Size = new System.Drawing.Size(44, 18);
            this.radioButtonBillNormal.TabIndex = 0;
            this.radioButtonBillNormal.TabStop = true;
            this.radioButtonBillNormal.Text = "רגיל";
            this.radioButtonBillNormal.UseVisualStyleBackColor = true;
            // 
            // radioButtonBillGov
            // 
            this.radioButtonBillGov.AutoSize = true;
            this.radioButtonBillGov.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBillGov.Location = new System.Drawing.Point(102, 85);
            this.radioButtonBillGov.Name = "radioButtonBillGov";
            this.radioButtonBillGov.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButtonBillGov.Size = new System.Drawing.Size(63, 18);
            this.radioButtonBillGov.TabIndex = 1;
            this.radioButtonBillGov.Text = "ממשלתי";
            this.radioButtonBillGov.UseVisualStyleBackColor = true;
            // 
            // radioButtonBillOne
            // 
            this.radioButtonBillOne.AutoSize = true;
            this.radioButtonBillOne.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBillOne.Location = new System.Drawing.Point(101, 108);
            this.radioButtonBillOne.Name = "radioButtonBillOne";
            this.radioButtonBillOne.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButtonBillOne.Size = new System.Drawing.Size(64, 18);
            this.radioButtonBillOne.TabIndex = 2;
            this.radioButtonBillOne.Text = "חד-פעמי";
            this.radioButtonBillOne.UseVisualStyleBackColor = true;
            this.radioButtonBillOne.CheckedChanged += new System.EventHandler(this.radioButtonBillOne_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(73, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "סוג חשבון:";
            // 
            // buttonBrowseExportLibrary
            // 
            this.buttonBrowseExportLibrary.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonBrowseExportLibrary.Location = new System.Drawing.Point(369, 178);
            this.buttonBrowseExportLibrary.Name = "buttonBrowseExportLibrary";
            this.buttonBrowseExportLibrary.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonBrowseExportLibrary.Size = new System.Drawing.Size(49, 23);
            this.buttonBrowseExportLibrary.TabIndex = 8;
            this.buttonBrowseExportLibrary.Text = "עיון...";
            this.buttonBrowseExportLibrary.UseVisualStyleBackColor = true;
            this.buttonBrowseExportLibrary.Click += new System.EventHandler(this.buttonBrowseExportLibrary_Click);
            // 
            // textBoxExportLibrary
            // 
            this.textBoxExportLibrary.Location = new System.Drawing.Point(102, 179);
            this.textBoxExportLibrary.Name = "textBoxExportLibrary";
            this.textBoxExportLibrary.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxExportLibrary.Size = new System.Drawing.Size(247, 20);
            this.textBoxExportLibrary.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(73, 145);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(97, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "ספריית ייצוא קבצים:";
            // 
            // buttonExport
            // 
            this.buttonExport.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonExport.Location = new System.Drawing.Point(258, 218);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonExport.Size = new System.Drawing.Size(91, 23);
            this.buttonExport.TabIndex = 9;
            this.buttonExport.Text = "הכן מסמך";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // groupBoxOneBillDetails
            // 
            this.groupBoxOneBillDetails.Controls.Add(this.label4);
            this.groupBoxOneBillDetails.Controls.Add(this.textBoxBillHourCost);
            this.groupBoxOneBillDetails.Controls.Add(this.textBoxBillHours);
            this.groupBoxOneBillDetails.Controls.Add(this.labelBillHours);
            this.groupBoxOneBillDetails.Controls.Add(this.labelBillHourCost);
            this.groupBoxOneBillDetails.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBoxOneBillDetails.Location = new System.Drawing.Point(266, 50);
            this.groupBoxOneBillDetails.Name = "groupBoxOneBillDetails";
            this.groupBoxOneBillDetails.Size = new System.Drawing.Size(218, 100);
            this.groupBoxOneBillDetails.TabIndex = 10;
            this.groupBoxOneBillDetails.TabStop = false;
            this.groupBoxOneBillDetails.Text = "הכנס נתוני חשבון ";
            this.groupBoxOneBillDetails.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "ש\"ח";
            // 
            // textBoxBillHourCost
            // 
            this.textBoxBillHourCost.Location = new System.Drawing.Point(48, 55);
            this.textBoxBillHourCost.Name = "textBoxBillHourCost";
            this.textBoxBillHourCost.Size = new System.Drawing.Size(56, 20);
            this.textBoxBillHourCost.TabIndex = 3;
            // 
            // textBoxBillHours
            // 
            this.textBoxBillHours.Location = new System.Drawing.Point(48, 31);
            this.textBoxBillHours.Name = "textBoxBillHours";
            this.textBoxBillHours.Size = new System.Drawing.Size(56, 20);
            this.textBoxBillHours.TabIndex = 2;
            // 
            // labelBillHours
            // 
            this.labelBillHours.AutoSize = true;
            this.labelBillHours.Location = new System.Drawing.Point(112, 34);
            this.labelBillHours.Name = "labelBillHours";
            this.labelBillHours.Size = new System.Drawing.Size(85, 14);
            this.labelBillHours.TabIndex = 1;
            this.labelBillHours.Text = "מס\' שעות עבודה:";
            // 
            // labelBillHourCost
            // 
            this.labelBillHourCost.AutoSize = true;
            this.labelBillHourCost.Location = new System.Drawing.Point(110, 58);
            this.labelBillHourCost.Name = "labelBillHourCost";
            this.labelBillHourCost.Size = new System.Drawing.Size(87, 14);
            this.labelBillHourCost.TabIndex = 0;
            this.labelBillHourCost.Text = "עלות שעת עבודה:";
            // 
            // radioButtonBillNormalDesc
            // 
            this.radioButtonBillNormalDesc.AutoSize = true;
            this.radioButtonBillNormalDesc.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBillNormalDesc.Location = new System.Drawing.Point(167, 62);
            this.radioButtonBillNormalDesc.Name = "radioButtonBillNormalDesc";
            this.radioButtonBillNormalDesc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.radioButtonBillNormalDesc.Size = new System.Drawing.Size(81, 18);
            this.radioButtonBillNormalDesc.TabIndex = 11;
            this.radioButtonBillNormalDesc.Text = "רגיל + פירוט";
            this.radioButtonBillNormalDesc.UseVisualStyleBackColor = true;
            // 
            // Form_Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(496, 262);
            this.Controls.Add(this.radioButtonBillNormalDesc);
            this.Controls.Add(this.groupBoxOneBillDetails);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonBrowseExportLibrary);
            this.Controls.Add(this.textBoxExportLibrary);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonBillOne);
            this.Controls.Add(this.radioButtonBillGov);
            this.Controls.Add(this.radioButtonBillNormal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form_Export";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "ייצוא חשבון";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Export_FormClosed);
            this.Load += new System.EventHandler(this.Form_Export_Load);
            this.groupBoxOneBillDetails.ResumeLayout(false);
            this.groupBoxOneBillDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonBillNormal;
        private System.Windows.Forms.RadioButton radioButtonBillGov;
        private System.Windows.Forms.RadioButton radioButtonBillOne;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBrowseExportLibrary;
        private System.Windows.Forms.TextBox textBoxExportLibrary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.GroupBox groupBoxOneBillDetails;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxBillHourCost;
        private System.Windows.Forms.TextBox textBoxBillHours;
        private System.Windows.Forms.Label labelBillHours;
        private System.Windows.Forms.Label labelBillHourCost;
        private System.Windows.Forms.RadioButton radioButtonBillNormalDesc;
    }
}