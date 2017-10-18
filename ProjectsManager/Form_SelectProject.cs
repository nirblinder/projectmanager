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
    public partial class Form_SelectProject : Form
    {
        DBinterface localInterface;
        Form_NewBill formNewBill;
        List<int> projectIds;
        DataTable data;
        int projectsSelected = 0;
        public Form_SelectProject(DBinterface dbInterface, Form_NewBill form)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            this.formNewBill = form;
            this.projectIds = formNewBill.projectIds;
        }
        private void Form_SelectProject_Load(object sender, EventArgs e)
        {
            string query = "";
            query = "SELECT idProjects,projectNumber,projectName,amountInfo FROM projects"; 
            if (projectIds.Count > 0)
            {
                string query1 = "SELECT projectNumber FROM projects WHERE idProjects='" + this.projectIds[0].ToString() + "'";
                string result1 = ((int)Double.Parse(localInterface.Select(query1).Tables[0].Rows[0][0].ToString())).ToString();
                query += " WHERE projectNumber LIKE '%" + result1 + ".%' OR projectNumber='" + result1 + "'";
            }
            query += " ORDER BY projectNumber";
            DataTable dt = localInterface.Select(query).Tables[0];
            dataGridViewProjects.DataSource = dt;
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            {
                column.HeaderText = "Selected";
                column.Name = "Selected";
            }
            dataGridViewProjects.Columns.Insert(0, column);
            dataGridViewProjects.Columns[1].Visible = false;
            dataGridViewProjects.Columns[4].Visible = false;

            dataGridViewProjects.Columns[0].Width = 50;
            dataGridViewProjects.Columns[2].Width = 60;
            dataGridViewProjects.Columns[3].Width = dataGridViewProjects.Width - dataGridViewProjects.Columns[0].Width
                                                                               - dataGridViewProjects.Columns[2].Width - 3;
            data = dt;
            foreach (int id in projectIds)
            {
                foreach (DataGridViewRow dr in dataGridViewProjects.Rows)
                {
                    if ((int)dr.Cells[1].Value == id)
                    {
                        dr.Cells[0].Value = true;
                        projectsSelected++;
                    }
                }
            }
            toolStripStatusLabelProjectsSelected.Text = projectsSelected + " פרויקטים נבחרו ";
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            int index = 0;
            foreach (System.Windows.Forms.DataGridViewRow dr in dataGridViewProjects.Rows)
            {
                if (dr.Cells[2].Value.ToString().Contains(textBoxSearch.Text) || dr.Cells[3].Value.ToString().Contains(textBoxSearch.Text))
                    dataGridViewProjects.Rows[dr.Index].Visible = true;
                else
                    dataGridViewProjects.Rows[dr.Index].Visible = false;
                index++;
            }
        }
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (projectIds.Count > 0)
            {
                string query1 = "SELECT projectNumber FROM projects";
                string concat = " WHERE ";
                foreach (int id in projectIds)
                {
                    query1 += concat;
                    concat = " OR ";
                    query1 += "idProjects='" + id + "'";
                }
                DataTable dt = localInterface.Select(query1).Tables[0];
                int number = (int)Double.Parse(dt.Rows[0][0].ToString());
                foreach (DataRow row in dt.Rows)
                {
                    if ((int)Double.Parse(row[0].ToString()) != number)
                    {
                        MessageBox.Show("יש לבחור פרוייקטים מאותה קבוצה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("לא נבחרו פרוייקטים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return;
            }
            formNewBill.updateProject(projectIds);
            this.Close();
        }
        private void dataGridViewProjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowId = (int)dataGridViewProjects.Rows[e.RowIndex].Cells[1].Value;
            if (e.ColumnIndex == 0)
            {
                if (projectIds.Contains(rowId))
                {
                    projectIds.Remove(rowId);
                    projectsSelected--;
                }
                else
                {
                    projectIds.Add(rowId);
                    projectsSelected++;
                }
                toolStripStatusLabelProjectsSelected.Text = projectsSelected + " פרויקטים נבחרו ";
            }
        }
    }
}
