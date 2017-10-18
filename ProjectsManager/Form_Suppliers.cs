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
    public partial class Form_Suppliers : Form
    {
        DBinterface localInterface;
        public Form_Suppliers(DBinterface db)
        {
            InitializeComponent(); 
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = db;
        }
        private void Form_Suppliers_Load(object sender, EventArgs e)
        {
            showSuppliers("");
        }
        private void showSuppliers(string search)
        {
            string sqlCmd = "SELECT id,name FROM suppliers WHERE name LIKE '%"+search+"%' AND active=1 ORDER BY name ASC";
            DataTable dt = localInterface.Select(sqlCmd).Tables[0];
            dataGridViewSuppliers.DataSource = dt;

            
            dataGridViewSuppliers.Columns[0].Visible = false;
            if (dt.Rows.Count>0)
                textBoxEdit.Text = dataGridViewSuppliers.SelectedRows[0].Cells[1].Value.ToString();
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            showSuppliers(textBoxSearch.Text);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBoxNew.Text == "")
            {
                MessageBox.Show("יש להזין שם ספק חדש", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                return;
            }
            try
            {
                string sqlCmd = "INSERT INTO suppliers (name) VALUES ('" + textBoxNew.Text.Replace("'", "\\'") + "')";
                localInterface.InsertSql(sqlCmd);
                showSuppliers("");
                MessageBox.Show("ספק חדש נוסף בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
            }
            catch { }
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (textBoxEdit.Text == "")
            {
                MessageBox.Show("יש להזין שם לספק", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                return;
            }
            localInterface.Update("UPDATE suppliers SET name='" + textBoxEdit.Text.Replace("'", "\\'") + "' WHERE id='" + dataGridViewSuppliers.SelectedRows[0].Cells[0].Value.ToString() + "'");
            showSuppliers("");
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("האם למחוק ספק זה ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
            if (result == DialogResult.No)
                return;
            localInterface.Update("Update suppliers SET active='0' WHERE id='" + dataGridViewSuppliers.SelectedRows[0].Cells[0].Value.ToString() + "'");
            showSuppliers("");
        }
        private void dataGridViewSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxEdit.Text = dataGridViewSuppliers.SelectedRows[0].Cells[1].Value.ToString();
            if ((int)dataGridViewSuppliers.SelectedRows[0].Cells[0].Value == 10)
                linkLabel3.Enabled = false;
            else
                linkLabel3.Enabled = true;
        }
    }
}
