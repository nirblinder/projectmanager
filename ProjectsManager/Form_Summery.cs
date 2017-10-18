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
    public partial class Form_Summery : Form
    {
        DBinterface localInterface;
        DataSet ds;
        public Form_Summery(DBinterface dbInterface)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));

            dateTimePickerYear.Format = DateTimePickerFormat.Custom;
            dateTimePickerYear.CustomFormat = "yyyy";

            localInterface = dbInterface;
            
            ShowResults(dateTimePickerYear.Value.Year.ToString());
        }
        private void dateTimePickerYear_ValueChanged(object sender, EventArgs e)
        {
            ShowResults(dateTimePickerYear.Value.Year.ToString());
        }
        private void ShowResults(string year)
        {
            double Amount = 0, SumAmount = 0;
            double Paid = 0, SumPaid = 0;
            double Collect = 0, SumCollect = 0;
            DataTable dt = new DataTable();
            if (year == "בחירה")
                return;
            createDataTable();
            for (int i = 1; i <= 12; i++)
            {
                string startDate = year + "-" + i + "-01";
                string endtDate = year + "-" + i + "-31";
                string query = "SELECT " +
                "SUM(projects.amount * bills.curencyRate * bills_projects.percent / 100) AS amount, SUM(projects.amount * bills.curencyRate * bills_projects.paid / 100) AS paid " +
                "FROM bills " +
                "INNER JOIN bills_projects ON bills.id = bills_projects.idBill " +
                "INNER JOIN projects ON projects.idProjects = bills_projects.idProject " +
                "WHERE bills.billDate>='" + startDate + "' AND bills.billDate<='" + endtDate + "'";
                /*string query = "SELECT " + 
                                "SUM(amount),SUM(paid) " + 
                                "FROM bills " + 
                                "WHERE billDate>='" + startDate + "' AND billDate<='" + endtDate + "'";*/
                dt = localInterface.Select(query).Tables[0];
                try
                {
                    Amount = Convert.ToDouble(dt.Rows[0][0]);
                }
                catch
                {
                    Amount = 0;
                }
                try
                {
                    Paid = Convert.ToDouble(dt.Rows[0][1]);
                }
                catch
                {
                    Paid = 0;
                }
                if (Amount == 0)
                    Collect = 0;
                else
                    Collect = (Paid / Amount) * 100;
                SumAmount += Amount;
                SumPaid += Paid;

                ds.Tables["MonthlySums"].Rows.Add(i.ToString().PadLeft(2, '0'),
                                                  Amount,
                                                  Paid.ToString(),
                                                  (Amount - Paid).ToString(),
                                                  Collect.ToString());
            }
            if (SumAmount == 0)
                SumCollect = 0;
            else
                SumCollect = (SumPaid / SumAmount) * 100;
            ds.Tables["MonthlySums"].Rows.Add("סיכום",
                                                  SumAmount,
                                                  SumPaid.ToString(),
                                                  (SumAmount - SumPaid).ToString(),
                                                  SumCollect.ToString());
            dataGridViewSummery.DataSource = null;
            dataGridViewSummery.DataSource = ds.Tables["MonthlySums"];

            dataGridViewSummery.Columns[0].HeaderText = "חודש";
            dataGridViewSummery.Columns[1].HeaderText = "חשבון";
            dataGridViewSummery.Columns[2].HeaderText = "נפרע";
            dataGridViewSummery.Columns[3].HeaderText = "יתרה";
            dataGridViewSummery.Columns[4].HeaderText = "גביה ב-%";
            
            dataGridViewSummery.Rows[12].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
        }
        private void createDataTable()
        {
            ds = new DataSet();
            ds.Tables.Add("MonthlySums");
            ds.Tables["MonthlySums"].Columns.Add("month", typeof(string));
            ds.Tables["MonthlySums"].Columns.Add("amount", typeof(double));
            ds.Tables["MonthlySums"].Columns.Add("paid", typeof(double));
            ds.Tables["MonthlySums"].Columns.Add("left", typeof(double));
            ds.Tables["MonthlySums"].Columns.Add("collect", typeof(double));
        }

        private void dataGridViewSummery_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridViewSummery.Columns["amount"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewSummery.Columns["paid"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewSummery.Columns["left"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewSummery.Columns["collect"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
            }
            catch { }
        }
    }
}
