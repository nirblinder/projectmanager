namespace ProjectsManager
{
    partial class Form_Summery
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewSummery = new System.Windows.Forms.DataGridView();
            this.dateTimePickerYear = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSummery)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "שנה";
            // 
            // dataGridViewSummery
            // 
            this.dataGridViewSummery.AllowUserToAddRows = false;
            this.dataGridViewSummery.AllowUserToDeleteRows = false;
            this.dataGridViewSummery.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewSummery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSummery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSummery.Location = new System.Drawing.Point(12, 52);
            this.dataGridViewSummery.Name = "dataGridViewSummery";
            this.dataGridViewSummery.ReadOnly = true;
            this.dataGridViewSummery.RowHeadersVisible = false;
            this.dataGridViewSummery.Size = new System.Drawing.Size(505, 329);
            this.dataGridViewSummery.TabIndex = 2;
            this.dataGridViewSummery.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewSummery_CellFormatting);
            // 
            // dateTimePickerYear
            // 
            this.dateTimePickerYear.Location = new System.Drawing.Point(65, 12);
            this.dateTimePickerYear.Name = "dateTimePickerYear";
            this.dateTimePickerYear.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dateTimePickerYear.RightToLeftLayout = true;
            this.dateTimePickerYear.ShowUpDown = true;
            this.dateTimePickerYear.Size = new System.Drawing.Size(128, 20);
            this.dateTimePickerYear.TabIndex = 3;
            this.dateTimePickerYear.ValueChanged += new System.EventHandler(this.dateTimePickerYear_ValueChanged);
            // 
            // Form_Summery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(529, 410);
            this.Controls.Add(this.dateTimePickerYear);
            this.Controls.Add(this.dataGridViewSummery);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Summery";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "סיכומים";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSummery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewSummery;
        private System.Windows.Forms.DateTimePicker dateTimePickerYear;
    }
}