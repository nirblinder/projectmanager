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
    public partial class Form_handlers : Form
    {
        DBinterface localInterface;
        DataTable data;
        public Form_handlers(DBinterface dbInterface)
        {
            InitializeComponent();
            localInterface = dbInterface;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void Form_handlers_Load(object sender, EventArgs e)
        {
            DataSet ds = localInterface.Select("SELECT * FROM handlers ORDER BY name");
            dataGridViewHandlers.DataSource = ds.Tables[0];
            dataGridViewHandlers.Columns[0].Width = dataGridViewHandlers.Width - 3;
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
                localInterface.Insert("handlers", "name", textBoxName.Text);
                textBoxName.Text = "";
                DataSet ds = localInterface.Select("SELECT * FROM handlers ORDER BY name");
                dataGridViewHandlers.DataSource = ds.Tables[0];
                dataGridViewHandlers.Columns[0].Width = dataGridViewHandlers.Width - 3;
                data = ds.Tables[0];
                MessageBox.Show("מטפל נוסף למערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            catch
            {
                MessageBox.Show("מטפל כבר קיים במערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (localInterface.Delete("handlers", "name", dataGridViewHandlers.SelectedCells[0].Value.ToString()))
            {
                DataSet ds = localInterface.Select("SELECT * FROM handlers ORDER BY name");
                dataGridViewHandlers.DataSource = ds.Tables[0];
                dataGridViewHandlers.Columns[0].Width = dataGridViewHandlers.Width - 3;
                data = ds.Tables[0];
                MessageBox.Show("מטפל הוסר מהמערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

    }
}
