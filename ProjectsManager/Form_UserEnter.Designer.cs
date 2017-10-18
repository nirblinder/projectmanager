namespace ProjectsManager
{
    partial class Form_UserEnter
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
            this.buttonUserEnter = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonUserEnter
            // 
            this.buttonUserEnter.Location = new System.Drawing.Point(340, 82);
            this.buttonUserEnter.Name = "buttonUserEnter";
            this.buttonUserEnter.Size = new System.Drawing.Size(99, 25);
            this.buttonUserEnter.TabIndex = 2;
            this.buttonUserEnter.Text = "כניסה";
            this.buttonUserEnter.UseVisualStyleBackColor = true;
            this.buttonUserEnter.Click += new System.EventHandler(this.buttonUserEnter_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(181, 85);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(98, 21);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyUp);
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(181, 41);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(98, 21);
            this.textBoxUsername.TabIndex = 0;
            this.textBoxUsername.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxUsername_KeyUp);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(56, 88);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "סיסמא";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(56, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "שם משתמש";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(143, 128);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 15);
            this.labelStatus.TabIndex = 16;
            // 
            // Form_UserEnter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(471, 162);
            this.Controls.Add(this.buttonUserEnter);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.labelStatus);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form_UserEnter";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "כניסת משתמש";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_UserEnter_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUserEnter;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelStatus;
    }
}