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
    public partial class Form_Engineers : Form
    {
        DBinterface localInterface;
        DataTable data;
        public Form_Engineers(DBinterface dbInterface)
        {
            InitializeComponent();
            localInterface = dbInterface;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void Form_Engineers_Load(object sender, EventArgs e)
        {
            DataSet ds = localInterface.Select("SELECT * FROM engineers ORDER BY name");
            dataGridViewEngineers.DataSource = ds.Tables[0];
            dataGridViewEngineers.Columns[0].Width = dataGridViewEngineers.Width - 3;
            data = ds.Tables[0];
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == String.Empty)
            {
                MessageBox.Show("יש להכניס שם להוספה", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }
            try
            {
                localInterface.Insert("engineers", "name", textBoxName.Text);
                textBoxName.Text = "";
                DataSet ds = localInterface.Select("SELECT * FROM engineers ORDER BY name");
                dataGridViewEngineers.DataSource = ds.Tables[0];
                dataGridViewEngineers.Columns[0].Width = dataGridViewEngineers.Width - 3;
                data = ds.Tables[0];
                MessageBox.Show("מהנדס נוסף למערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            catch
            {
                MessageBox.Show("מהנדס כבר קיים במערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (localInterface.Delete("engineers", "name", dataGridViewEngineers.SelectedCells[0].Value.ToString()))
            {
                DataSet ds = localInterface.Select("SELECT * FROM engineers ORDER BY name");
                dataGridViewEngineers.DataSource = ds.Tables[0];
                dataGridViewEngineers.Columns[0].Width = dataGridViewEngineers.Width - 3;
                data = ds.Tables[0];
                MessageBox.Show("מהנדס הוסר מהמערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }
    }
}
