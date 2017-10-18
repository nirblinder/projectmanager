namespace ProjectsManager
{
    partial class Form_Engineers
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
            this.dataGridViewEngineers = new System.Windows.Forms.DataGridView();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEngineers)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEngineers
            // 
            this.dataGridViewEngineers.AllowUserToAddRows = false;
            this.dataGridViewEngineers.AllowUserToDeleteRows = false;
            this.dataGridViewEngineers.AllowUserToResizeColumns = false;
            this.dataGridViewEngineers.AllowUserToResizeRows = false;
            this.dataGridViewEngineers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEngineers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewEngineers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEngineers.ColumnHeadersVisible = false;
            this.dataGridViewEngineers.Location = new System.Drawing.Point(12, 38);
            this.dataGridViewEngineers.MultiSelect = false;
            this.dataGridViewEngineers.Name = "dataGridViewEngineers";
            this.dataGridViewEngineers.ReadOnly = true;
            this.dataGridViewEngineers.RowHeadersVisible = false;
            this.dataGridViewEngineers.Size = new System.Drawing.Size(288, 346);
            this.dataGridViewEngineers.TabIndex = 4;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(259, 12);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(41, 23);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.Text = "הסר";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(133, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(41, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "הוסף";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(12, 14);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(115, 20);
            this.textBoxName.TabIndex = 7;
            // 
            // Form_Engineers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(312, 396);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.dataGridViewEngineers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Engineers";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "רשימת מהנדסים";
            this.Load += new System.EventHandler(this.Form_Engineers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEngineers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEngineers;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxName;
    }
}