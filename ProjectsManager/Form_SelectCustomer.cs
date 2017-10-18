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
    public partial class Form_SelectCustomer : Form
    {
        DBinterface localInterface;
        Form_NewProject formNewProject;
        Form_NewCustomer formNewCustomer;
        Form1 formMain;

        DataTable data;
        public Form_SelectCustomer(DBinterface dbInterface, Form_NewProject form)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            formNewProject = form;
        }
        public Form_SelectCustomer(DBinterface dbInterface, Form1 form)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            formMain = form;
        }
        private void Form_SelectCustomer_Load(object sender, EventArgs e)
        {
            DataSet ds = localInterface.Select("SELECT idCustomer,name FROM customers ORDER BY name");
            dataGridViewCustomers.DataSource = ds.Tables[0];
            dataGridViewCustomers.Columns[0].Visible = false;
            dataGridViewCustomers.Columns[1].Width = dataGridViewCustomers.Width -3;
            data = ds.Tables[0];
            textBoxSearch.Text = "";
        }
        private void dataGridViewCustomers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (formNewProject != null)
                formNewProject.insertCustomer(dataGridViewCustomers.SelectedCells[0].Value.ToString(),
                                              dataGridViewCustomers.Rows[dataGridViewCustomers.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            if (formMain !=null)
                formMain.insertCustomer(dataGridViewCustomers.SelectedCells[0].Value.ToString(),
                                              dataGridViewCustomers.Rows[dataGridViewCustomers.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            this.Close();
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            int index = 0;
            foreach (System.Windows.Forms.DataGridViewRow dr in dataGridViewCustomers.Rows)
            {
                dataGridViewCustomers.CurrentCell = null;
                if (dr.Cells[1].Value.ToString().Contains(textBoxSearch.Text))
                    dataGridViewCustomers.Rows[dr.Index].Visible = true;
                else
                    dataGridViewCustomers.Rows[dr.Index].Visible = false;
                index++;
            }
        }

        private void buttonNewCustomer_Click(object sender, EventArgs e)
        {
            formNewCustomer = new Form_NewCustomer(localInterface, this, formMain);
            formNewCustomer.ShowDialog();
        }
        public void refreshAndfillSearchTextBox(string customerName)
        {
            DataSet ds = localInterface.Select("SELECT idCustomer,name FROM customers ORDER BY name");
            dataGridViewCustomers.DataSource = null;
            dataGridViewCustomers.DataSource = ds.Tables[0];
            dataGridViewCustomers.Columns[0].Visible = false;
            dataGridViewCustomers.Columns[1].Width = dataGridViewCustomers.Width - 3;
            data = ds.Tables[0];
            textBoxSearch.Text = customerName;
        }
    }
}
