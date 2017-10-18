namespace ProjectsManager
{
    partial class Form_SelectCustomer
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
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
            this.buttonNewCustomer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(94, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(151, 20);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "חיפוש";
            // 
            // dataGridViewCustomers
            // 
            this.dataGridViewCustomers.AllowUserToAddRows = false;
            this.dataGridViewCustomers.AllowUserToDeleteRows = false;
            this.dataGridViewCustomers.AllowUserToResizeColumns = false;
            this.dataGridViewCustomers.AllowUserToResizeRows = false;
            this.dataGridViewCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCustomers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomers.ColumnHeadersVisible = false;
            this.dataGridViewCustomers.Location = new System.Drawing.Point(12, 38);
            this.dataGridViewCustomers.MultiSelect = false;
            this.dataGridViewCustomers.Name = "dataGridViewCustomers";
            this.dataGridViewCustomers.ReadOnly = true;
            this.dataGridViewCustomers.RowHeadersVisible = false;
            this.dataGridViewCustomers.Size = new System.Drawing.Size(288, 346);
            this.dataGridViewCustomers.TabIndex = 3;
            this.dataGridViewCustomers.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomers_CellContentDoubleClick);
            // 
            // buttonNewCustomer
            // 
            this.buttonNewCustomer.Location = new System.Drawing.Point(251, 12);
            this.buttonNewCustomer.Name = "buttonNewCustomer";
            this.buttonNewCustomer.Size = new System.Drawing.Size(49, 20);
            this.buttonNewCustomer.TabIndex = 4;
            this.buttonNewCustomer.Text = "חדש";
            this.buttonNewCustomer.UseVisualStyleBackColor = true;
            this.buttonNewCustomer.Click += new System.EventHandler(this.buttonNewCustomer_Click);
            // 
            // Form_SelectCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(312, 396);
            this.Controls.Add(this.buttonNewCustomer);
            this.Controls.Add(this.dataGridViewCustomers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_SelectCustomer";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "בחירת לקוח";
            this.Load += new System.EventHandler(this.Form_SelectCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewCustomers;
        private System.Windows.Forms.Button buttonNewCustomer;
    }
}