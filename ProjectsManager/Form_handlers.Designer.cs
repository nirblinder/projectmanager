namespace ProjectsManager
{
    partial class Form_handlers
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.dataGridViewHandlers = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHandlers)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(12, 14);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(115, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(133, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(41, 23);
            this.buttonAdd.TabIndex = 10;
            this.buttonAdd.Text = "הוסף";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(259, 12);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(41, 23);
            this.buttonRemove.TabIndex = 9;
            this.buttonRemove.Text = "הסר";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // dataGridViewHandlers
            // 
            this.dataGridViewHandlers.AllowUserToAddRows = false;
            this.dataGridViewHandlers.AllowUserToDeleteRows = false;
            this.dataGridViewHandlers.AllowUserToResizeColumns = false;
            this.dataGridViewHandlers.AllowUserToResizeRows = false;
            this.dataGridViewHandlers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewHandlers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewHandlers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHandlers.ColumnHeadersVisible = false;
            this.dataGridViewHandlers.Location = new System.Drawing.Point(12, 38);
            this.dataGridViewHandlers.MultiSelect = false;
            this.dataGridViewHandlers.Name = "dataGridViewHandlers";
            this.dataGridViewHandlers.ReadOnly = true;
            this.dataGridViewHandlers.RowHeadersVisible = false;
            this.dataGridViewHandlers.Size = new System.Drawing.Size(288, 346);
            this.dataGridViewHandlers.TabIndex = 8;
            // 
            // Form_handlers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(312, 396);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.dataGridViewHandlers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_handlers";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "מטפלים(חשבונות)";
            this.Load += new System.EventHandler(this.Form_handlers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHandlers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.DataGridView dataGridViewHandlers;
    }
}