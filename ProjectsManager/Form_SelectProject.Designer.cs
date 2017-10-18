namespace ProjectsManager
{
    partial class Form_SelectProject
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
            this.dataGridViewProjects = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.statusStripProjectsSelected = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelProjectsSelected = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjects)).BeginInit();
            this.statusStripProjectsSelected.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewProjects
            // 
            this.dataGridViewProjects.AllowUserToAddRows = false;
            this.dataGridViewProjects.AllowUserToDeleteRows = false;
            this.dataGridViewProjects.AllowUserToResizeColumns = false;
            this.dataGridViewProjects.AllowUserToResizeRows = false;
            this.dataGridViewProjects.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewProjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProjects.ColumnHeadersVisible = false;
            this.dataGridViewProjects.Location = new System.Drawing.Point(14, 38);
            this.dataGridViewProjects.MultiSelect = false;
            this.dataGridViewProjects.Name = "dataGridViewProjects";
            this.dataGridViewProjects.RowHeadersVisible = false;
            this.dataGridViewProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProjects.Size = new System.Drawing.Size(383, 346);
            this.dataGridViewProjects.TabIndex = 6;
            this.dataGridViewProjects.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProjects_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "חיפוש";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(52, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(206, 20);
            this.textBoxSearch.TabIndex = 4;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonSelect.Location = new System.Drawing.Point(322, 10);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonSelect.TabIndex = 7;
            this.buttonSelect.Text = "אישור";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // statusStripProjectsSelected
            // 
            this.statusStripProjectsSelected.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelProjectsSelected});
            this.statusStripProjectsSelected.Location = new System.Drawing.Point(0, 407);
            this.statusStripProjectsSelected.Name = "statusStripProjectsSelected";
            this.statusStripProjectsSelected.Size = new System.Drawing.Size(409, 22);
            this.statusStripProjectsSelected.TabIndex = 8;
            this.statusStripProjectsSelected.Text = "statusStrip1";
            // 
            // toolStripStatusLabelProjectsSelected
            // 
            this.toolStripStatusLabelProjectsSelected.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabelProjectsSelected.Name = "toolStripStatusLabelProjectsSelected";
            this.toolStripStatusLabelProjectsSelected.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelProjectsSelected.Text = "toolStripStatusLabel1";
            // 
            // Form_SelectProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(409, 429);
            this.Controls.Add(this.statusStripProjectsSelected);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.dataGridViewProjects);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_SelectProject";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "בחירת פרוייקט";
            this.Load += new System.EventHandler(this.Form_SelectProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjects)).EndInit();
            this.statusStripProjectsSelected.ResumeLayout(false);
            this.statusStripProjectsSelected.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewProjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.StatusStrip statusStripProjectsSelected;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProjectsSelected;
    }
}