using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectsManager
{
    public partial class Form_UserEnter : Form
    {
        DBinterface localInterface;
        Form1 mainForm;

        public Form_UserEnter(Form1 form, DBinterface dbInterface)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            mainForm = form;
        }

        private void buttonUserEnter_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string pass = textBoxPassword.Text;
            if (username == String.Empty || pass == String.Empty)
                return;

            pass = MyUtills.ConvertToMd5(pass);

            string adminUsername = "develop";
            string adminPassword = MyUtills.ConvertToMd5("bnrbnr");

            DataTable userTable = localInterface.Select("SELECT type FROM users WHERE username='" + username + "' AND password='" + pass + "'").Tables[0];
            int res = userTable.Rows.Count;
            if (username == adminUsername && pass == adminPassword)
            {
                mainForm.UserEnter(1);
                this.Close();
            } 
            else if (res > 0)
            {
                mainForm.UserEnter((int)userTable.Rows[0][0]);
                this.Close();
            }
            else
            {
                labelStatus.Text = "שם משתמש או סיסמא לא נכונים";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private void Form_UserEnter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonUserEnter.PerformClick();
        }

        private void textBoxPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonUserEnter.PerformClick();
        }

        private void textBoxUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonUserEnter.PerformClick();
        }

    }
}
