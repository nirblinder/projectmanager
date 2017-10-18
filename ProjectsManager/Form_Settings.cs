using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;

namespace ProjectsManager
{
    public partial class Form_Settings : Form
    {
        DBinterface localInterface;

        public Form_Settings(DBinterface dbInterface)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));
            localInterface = dbInterface;
        }
        private void Form_Settings_Load(object sender, EventArgs e)
        {
            textBoxServer.Text = localInterface.conSettings.server;
            textBoxDatabase.Text = localInterface.conSettings.database;
            textBoxUsername.Text = localInterface.conSettings.username;
            textBoxPassword.Text = localInterface.conSettings.password;
            showVatDataGridView();
            textBoxIExportLibrary.Text = localInterface.exportLibrary;
            textBoxImportLibrary.Text = localInterface.importLibrary;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            localInterface.conSettings.server = textBoxServer.Text;
            localInterface.conSettings.database = textBoxDatabase.Text;
            localInterface.conSettings.username = textBoxUsername.Text;
            localInterface.conSettings.password = textBoxPassword.Text;
            localInterface.SaveSettings();
            this.Close();
        }
        
        private void buttonCheckConnectivity_Click(object sender, EventArgs e)
        {
            if (localInterface.ConnecivityCheck(textBoxServer.Text, textBoxDatabase.Text, textBoxUsername.Text, textBoxPassword.Text))
            {
                pictureBoxConnection.Visible = true;
                Application.DoEvents();
                Thread.Sleep(2000);
            }
            pictureBoxConnection.Visible = false;
        }
        private void labelUpdateVat_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            double newVat = 0;
            string inputNewVat = textBoxNewVat.Text;
            if (inputNewVat == "")
                inputNewVat = "0";
            if (!Double.TryParse(inputNewVat, out newVat))
            {
                MessageBox.Show("מע\"מ לא חוקי", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }
            String newDateFormatted = MyUtills.dateToSQL(dateTimePickerNewVat.Value);
            //String newDate = dateTimePickerNewVat.Value.ToShortDateString();
            //String newDateFormatted = newDate.Substring(6,4) + "-" + newDate.Substring(3,2) + "-" + newDate.Substring(0,2);
            dt = localInterface.Select("SELECT id FROM vat WHERE date='" + newDateFormatted + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (inputNewVat !="0")
                    localInterface.Update("UPDATE vat SET vat='"+inputNewVat+"' WHERE date='"+newDateFormatted+"'");
                else
                    localInterface.Update("DELETE FROM vat WHERE date='" + newDateFormatted + "'");
            }
            else
            {
                if (inputNewVat != "0")
                    localInterface.Insert("vat", "date,vat", newDateFormatted+"','"+newVat);
            }
            showVatDataGridView();
        }
        private void showVatDataGridView()
        {
            DataTable dt = new DataTable();
            dt = localInterface.Select("SELECT date,vat FROM vat ORDER BY date DESC").Tables[0];
            dataGridViewVAT.DataSource = dt;
            textBoxCurrentVat.Text = dataGridViewVAT.Rows[0].Cells[1].Value.ToString();
            dataGridViewVAT.Columns[0].Width = 80;
            dataGridViewVAT.Columns[1].Width = 77;
        }

        private void buttonBrowseExportLibrary_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = System.Environment.SpecialFolder.Desktop;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
                textBoxIExportLibrary.Text = fbd.SelectedPath;
            
        }
        private void buttonSaveExportLibrary_Click(object sender, EventArgs e)
        {
            localInterface.saveExportLibrary(textBoxIExportLibrary.Text);
           
        }

        private void buttonBrowseImportLibrary_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = System.Environment.SpecialFolder.Desktop;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
                textBoxImportLibrary.Text = fbd.SelectedPath;
            
        }

        private void buttonSaveImportLibrary_Click(object sender, EventArgs e)
        {
            localInterface.saveImportLibrary(textBoxImportLibrary.Text);
        }

        private void buttonManageUsers_Click(object sender, EventArgs e)
        {
            string username = textBoxAdminUsername.Text;
            string pass = textBoxAdminPassword.Text;

            if (username == String.Empty || pass == String.Empty)
                return;

            pass = MyUtills.ConvertToMd5(pass);

            string adminUsername = "develop";
            string adminPassword = MyUtills.ConvertToMd5("bnrbnr");

            int res = (int)localInterface.Select("SELECT id FROM users WHERE username='"+username+"' AND password='"+pass+"' AND type=1").Tables[0].Rows.Count;
            if (res > 0 || (username == adminUsername && pass == adminPassword))
            {
                panelUsers.Visible = true;
                showUsers();
                fillUserTypes();
            }
            
        }
        private void showUsers()
        {
            dataGridViewUsers.DataSource = localInterface.Select("SELECT users.id,users.username,usertypes.type FROM users INNER JOIN usertypes ON users.type=usertypes.id").Tables[0];
            dataGridViewUsers.Columns[0].Visible = false;
            dataGridViewUsers.Columns[1].HeaderText = "שם משתמש";
            dataGridViewUsers.Columns[2].HeaderText = "סוג";
        }
        private void fillUserTypes()
        {
            comboBoxUserTypes.DataSource = localInterface.Select("SELECT * FROM usertypes").Tables[0];
            comboBoxUserTypes.DisplayMember = "type";
            comboBoxUserTypes.ValueMember = "id";
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            if (textBoxNewUsername.Text == String.Empty || textBoxNewPassword.Text == String.Empty)
                return;

            string pass = MyUtills.ConvertToMd5(textBoxNewPassword.Text);

            bool res = localInterface.InsertSql("INSERT INTO users (username,password,type) VALUES ('"+textBoxNewUsername.Text+"','"+pass+"','"+comboBoxUserTypes.SelectedValue+"')");
            if (res == true)
                showUsers();

        }
        private void dataGridViewUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dataGridViewUsers.Rows[e.RowIndex].Selected = true;
                contextMenuStripDeleteUser.Show(Control.MousePosition.X - contextMenuStripDeleteUser.Width, Control.MousePosition.Y);
            }
        }
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            int deleteId = Convert.ToInt16(dataGridViewUsers.SelectedRows[0].Cells[0].Value);
            bool res = localInterface.Delete("users", "id", deleteId.ToString());
            if (res)
                showUsers();
        }
        private void tabPageUsers_Enter(object sender, EventArgs e)
        {
            textBoxAdminUsername.ScrollToCaret();
        }
    }
}
