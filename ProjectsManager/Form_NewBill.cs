using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace ProjectsManager
{
    public partial class Form_NewBill : Form
    {
        DBinterface localInterface;
        Form_SelectProject formSelectProject;
        Form_Export formExport;
        Form1 mainForm;
        public List<int> projectIds;
        double lastAmount = 0;

        public Form_NewBill(Form1 form, DBinterface dbInterface)
        {
            InitializeComponent();
            projectIds = new List<int>();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            mainForm = form;
            textBoxBillPart.Text = "1";
            labelAmountNotes.Visible = false;
            textBoxProjectAmountInfo.Visible = false;
        }
        public Form_NewBill(Form1 form, DBinterface dbInterface, List<int> idProjects)
        {
            InitializeComponent();
            projectIds = idProjects;
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            updateProject(projectIds);
            mainForm = form;
            
            if (true)//(amountInfo == String.Empty)
            {
                labelAmountNotes.Visible = false;
                textBoxProjectAmountInfo.Visible = false;
            }
            else
            {
                labelAmountNotes.Visible = true;
                textBoxProjectAmountInfo.Visible = true;
            }
        }
        private void Form_NewBill_Load(object sender, EventArgs e)
        {
            //textBoxDetailsBillAmount.Text = amount;
            textBoxDetailsBillPaid.Text = "0";
            comboBoxDetailsBillPayMethod.SelectedIndex = 0;
            textBoxCurencyRate.Text = "0";
            textBoxBillIndex.Text = "0";
            textBoxBillIncrease.Text = "0";
            DataTable dt = localInterface.Select("Select * FROM handlers").Tables[0];
            comboBoxDetailsBillHandlers.Items.Add("");
            foreach (DataRow dr in dt.Rows)
                comboBoxDetailsBillHandlers.Items.Add(dr[0]);
            comboBoxDetailsBillHandlers.SelectedItem = "מזכירה";
            DataTable dt2 = localInterface.Select("SELECT vat FROM vat WHERE date<='" + MyUtills.dateToSQL(DateTime.Now) + "' ORDER BY date DESC").Tables[0];
            textBoxBillVat.Text = "% " + dt2.Rows[0][0].ToString();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void buttonSelectProject_Click(object sender, EventArgs e)
        {
            formSelectProject = new Form_SelectProject(localInterface, this);
            formSelectProject.ShowDialog();
        }

        public void updateProject(List<int> id)
        {
            // Project Numbers
            projectIds = id;
            string query = "SELECT idProjects,projectNumber,amount,amount FROM projects WHERE ";
            string concat = "";

            foreach (int i in projectIds)
            {
                query += concat;
                query += "idProjects=" + i.ToString();
                concat = " OR ";
            }
            DataTable dt = localInterface.Select(query).Tables[0];
            double billTotal = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dr[2] = String.Format("{0:0,0.00}", projectPartAmount((int)dr[0]));
                dr[3] = String.Format("{0:0,0.00}", 0);
                billTotal += Double.Parse(dr[2].ToString());
            }
            dataGridViewProjects.DataSource = dt;

            dataGridViewProjects.Columns[0].Visible = false;
            dataGridViewProjects.Columns[1].ReadOnly = true;

            dataGridViewProjects.Columns[1].HeaderText = "פרויקט";
            dataGridViewProjects.Columns[2].HeaderText = "סכום";
            dataGridViewProjects.Columns[3].HeaderText = "שולם";

            // Bill Number
            int tempIndex = 0, maxIndex = 0;
            DataTable dt1 = localInterface.Select("SELECT idBill FROM bills").Tables[0];
            foreach (DataRow dr in dt1.Rows)
            {
                tempIndex = (int)dr[0];
                if (tempIndex > maxIndex && tempIndex < 100000)
                    maxIndex = tempIndex;
            }
            textBoxDetailsBillNumber.Text = (maxIndex + 1).ToString();

            // Bill Amount
            textBoxDetailsBillAmount.Text = String.Format("{0:0,0.0}", billTotal);
            
            labelBillNumber.ForeColor = Color.Black;
            labelProjectNumber.ForeColor = Color.Black;
            labelProjectName.ForeColor = Color.Black;
            buttonSelectProject.ForeColor = Color.Black;
            labelPayMethod.ForeColor = Color.Black;
            string query1 = String.Empty;
            if (this.projectIds.Count == 1)
            {
                query1 = "SELECT toSubmitNotes FROM projects WHERE idProjects='" + projectIds[0].ToString() + "'";
                textBoxBillNotes.Text = localInterface.Select(query1).Tables[0].Rows[0][0].ToString();
            }

            if (projectIds.Count > 0)
            {
                query1 = "SELECT projectNumber FROM projects WHERE idProjects='" + projectIds[0].ToString() + "'";
                string projectNumber = ((int)(Double.Parse(localInterface.Select(query1).Tables[0].Rows[0][0].ToString()))).ToString();
                string query2 = "SELECT id FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE projects.projectNumber LIKE '%" + projectNumber + ".%' OR projects.projectNumber='" + projectNumber + "' GROUP BY bills_projects.idBill";
                int billPart = localInterface.Select(query2).Tables[0].Rows.Count + 1;
                textBoxBillPart.Text = billPart.ToString();
            }
            else
                textBoxBillPart.Text = "1";
        }
        private string calcBillAmount(List<int> projectId)
        {
            double totalAmount = 0;
            foreach (int id in projectId)
                totalAmount += projectPartAmount(id);

            return String.Format("{0:0,0.00}", totalAmount);
        }
        private double projectPartAmount(int idProject)
        {
            double prevBillsAmount = 0, newProgressAmount = 0;
            DataTable dt = localInterface.Select("SELECT COALESCE(SUM(bills_projects.percent),0),projects.amount FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idProject=" + idProject.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
                prevBillsAmount = (double)dt.Rows[0][0] / 100 * (double)dt.Rows[0][1];

            dt = localInterface.Select("SELECT projects.amount,projects.toSubmit FROM projects WHERE projects.idProjects=" + idProject.ToString()).Tables[0];
            newProgressAmount = (double)dt.Rows[0][0] * (double)dt.Rows[0][1] / 100;

            return (newProgressAmount - prevBillsAmount);
        }

        private void linkLabelCalcIncrease_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //string billId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            double currentIndex = 0;
            if (!Double.TryParse(textBoxBillIndex.Text, out currentIndex))
            {
                labelBillIndex.ForeColor = Color.Red;
                return;
            }
            else
            {
                labelBillIndex.ForeColor = Color.Black;
                DataTable dt;
                try
                {
                    //dt = localInterface.Select("SELECT projects.priceIndex FROM bills INNER JOIN bills_projects ON bills_projects.idBill=bills.id INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills.id=" + billId).Tables[0];
                    dt = localInterface.Select("SELECT projects.priceIndex FROM projects WHERE projects.idProjects=" + projectIds[0].ToString()).Tables[0];
                }
                catch 
                {
                    MessageBox.Show("יש לבחור פרויקט", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }

                double priceIndex = 0;
                Double.TryParse(dt.Rows[0][0].ToString(), out priceIndex);

                double amount = 0;
                //if (!textBoxAmountNis.Text.Contains("יש"))
                //    amount = Double.Parse(textBoxAmountNis.Text.Replace(",", "").Replace(" שקל", ""));
                //else
                //{
                    Double.TryParse(textBoxDetailsBillAmount.Text.Replace(",", ""), out amount);
                //}
                showFinalAmount(amount, currentIndex, priceIndex);
            }
        }
        private void showFinalAmount(double amount, double currentIndex, double priceIndex)
        {
            double increase = 0;
            double temp = 0;
            if (priceIndex != 0)
                increase = (currentIndex / priceIndex - 1) * amount;
            if (currentIndex == 0)
                increase = 0;
            textBoxBillIncrease.Text = String.Format("{0:0,0.00}", increase);
            temp = amount + increase;
            textBoxBillAmountWithIncrease.Text = String.Format("{0:0,0.00}", temp);
            textBoxBillAmountWithVAT.Text = String.Format("{0:0,0.00}", temp * (Double.Parse(textBoxBillVat.Text.Substring(2, textBoxBillVat.Text.Length - 2)) / 100 + 1));
        }

        private void linkLabelCalcAmounts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            double amount = Double.Parse(textBoxDetailsBillAmount.Text.Replace(",", ""));
            //if (!textBoxAmountNis.Text.Contains("יש"))
            //    amount = Double.Parse(textBoxAmountNis.Text.Replace(",", "").Replace(" שקל", ""));
            //else
            //{
                
            //}
            amount += Double.Parse(textBoxBillIncrease.Text.Replace(",", ""));
            textBoxBillAmountWithIncrease.Text = String.Format("{0:0,0.00}", amount);
            textBoxBillAmountWithVAT.Text = String.Format("{0:0,0.00}", amount * (Double.Parse(textBoxBillVat.Text.Substring(2, textBoxBillVat.Text.Length - 2)) / 100 + 1));
        }
        private void dateTimePickerDetailsBillDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = dateTimePickerDetailsBillDate.Value;
            DataTable dt2 = localInterface.Select("SELECT vat FROM vat WHERE date<='" + MyUtills.dateToSQL(dt) + "' ORDER BY date DESC").Tables[0];
            textBoxBillVat.Text = "% " + dt2.Rows[0][0].ToString();

        }
        private void buttonSaveExport_Click(object sender, EventArgs e)
        {
            bool saveOk = Save();
            if (saveOk)
            {
                int idBill = Convert.ToInt16(localInterface.Select("SELECT MAX(id) from bills").Tables[0].Rows[0][0]);
                formExport = new Form_Export(localInterface, idBill);
                formExport.ShowDialog();
            }
            
        }

        private bool Save()
        {
            labelBillNumber.ForeColor = Color.Black;
            labelProjectNumber.ForeColor = Color.Black;
            labelProjectName.ForeColor = Color.Black;
            buttonSelectProject.ForeColor = Color.Black;
            labelAmount.ForeColor = Color.Black;
            labelPaid.ForeColor = Color.Black;
            labelPayMethod.ForeColor = Color.Black;
            labelCurencyRate.ForeColor = Color.Black;
            labelBillIncrease.ForeColor = Color.Black;
            labelBillIndex.ForeColor = Color.Black;
            labelBillPart.ForeColor = Color.Black;

            bool returnFlag = false;
            if (textBoxDetailsBillNumber.Text == String.Empty)
            {
                labelBillNumber.ForeColor = Color.Red;
                labelProjectNumber.ForeColor = Color.Red;
                labelProjectName.ForeColor = Color.Red;
                buttonSelectProject.ForeColor = Color.Red;
                returnFlag = true;
            }
            try
            {
                double.Parse(textBoxDetailsBillAmount.Text);
            }
            catch
            {
                labelAmount.ForeColor = Color.Red;
                returnFlag = true;
            }
            try
            {
                double.Parse(textBoxBillIncrease.Text);
            }
            catch
            {
                labelBillIncrease.ForeColor = Color.Red;
                returnFlag = true;
            }
            try
            {
                double.Parse(textBoxBillIndex.Text);
            }
            catch
            {
                labelBillIndex.ForeColor = Color.Red;
                returnFlag = true;
            }
            try
            {
                double.Parse(textBoxCurencyRate.Text);
                if (double.Parse(textBoxCurencyRate.Text) == 0)
                    textBoxCurencyRate.Text = "1";
            }
            catch
            {
                labelCurencyRate.ForeColor = Color.Red;
                returnFlag = true;
            }
            try
            {
                int.Parse(textBoxBillPart.Text);
            }
            catch
            {
                labelBillPart.ForeColor = Color.Red;
                returnFlag = true;
            }
            short isClosed = 0;
            if (returnFlag)
                return false;
            if (checkBoxDetailsBillIsClosed.Checked)
                isClosed = 1;
            int billPart = GetBillPart();//localInterface.Select("SELECT idBill FROM bills WHERE idProject=" + textBoxID.Text).Tables[0].Rows.Count + 1;
            bool queryOk;
            queryOk = localInterface.Insert("bills", "idBill,amount,paid,payMethod,billDate,vatDate,approvalDate,toPayDate,payDate,invoiceNumber,receiptNumber,isClosed,curencyRate,handler,billNotes,increase,billPart,currentIndex,callback",
                                    textBoxDetailsBillNumber.Text + "','" +
                                    textBoxDetailsBillAmount.Text.Replace(",", "") + "','" +
                                    textBoxDetailsBillPaid.Text.Replace(",", "") + "','" +
                                    comboBoxDetailsBillPayMethod.SelectedIndex + "','" +
                                    MyUtills.dateToSQL(dateTimePickerDetailsBillDate.Value) + "','" +
                                    MyUtills.dateToSQL(dateTimePickerDetailsBillDate.Value) + "','" +
                                    MyUtills.dateToSQL(dateTimePickerDetailsBillApproval.Value) + "','" +
                                    MyUtills.dateToSQL(dateTimePickerDetailsBillToPay.Value) + "','" +
                                    MyUtills.dateToSQL(dateTimePickerDetailsBillPay.Value) + "','" +
                                    textBoxDetailsBillInvoiceNumber.Text + "','" +
                                    textBoxDetailsBillReceiptNumber.Text + "','" +
                                    isClosed.ToString() + "','" +
                                    textBoxCurencyRate.Text + "','" +
                                    comboBoxDetailsBillHandlers.SelectedItem + "','" +
                                    textBoxBillNotes.Text.Replace("'", "\\'") + "','" +
                                    textBoxBillIncrease.Text + "','" +
                                    textBoxBillPart.Text + "','" +
                                    textBoxBillIndex.Text + "','" +
                                    MyUtills.dateToSQL(dateTimePickerDetailsBillDate.Value.AddDays(2)));


            if (!queryOk)
            {
                MessageBox.Show("שמירת חשבון חדש נכשלה", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                return false;
            }
            else
            {
                int maxBillId = (int)localInterface.Select("SELECT MAX(id) FROM bills").Tables[0].Rows[0][0];
                DataTable dtProject;
                Double projectAmount;
                double projectToSubmit;
                double oldProgress = 0;
                for (int i = 0; i < dataGridViewProjects.RowCount; i++)
                {
                    dtProject = localInterface.Select("SELECT projects.amount,projects.toSubmit,COALESCE(SUM(bills_projects.percent),0) FROM projects LEFT JOIN bills_projects ON projects.idProjects=bills_projects.idProject WHERE projects.idProjects=" + dataGridViewProjects.Rows[i].Cells[0].Value.ToString() + " ORDER BY bills_projects.progress DESC LIMIT 1").Tables[0];
                    projectAmount = Convert.ToDouble(dtProject.Rows[0][0]);
                    projectToSubmit = Convert.ToDouble(dtProject.Rows[0][1]);
                    try
                    {
                        oldProgress = Convert.ToDouble(dtProject.Rows[0][2]);
                    }
                    catch { }
                    localInterface.Insert("bills_projects", "idBill,idProject,percent,progress,paid", maxBillId.ToString() + "','" +
                                                                                        dataGridViewProjects.Rows[i].Cells[0].Value.ToString() + "','" +
                                                                                        (Convert.ToDouble(dataGridViewProjects.Rows[i].Cells[2].Value) / projectAmount * 100).ToString() + "','" +
                                                                                        (oldProgress + Convert.ToDouble(dataGridViewProjects.Rows[i].Cells[2].Value) / projectAmount * 100).ToString() + "','" +
                                                                                        (Convert.ToDouble(dataGridViewProjects.Rows[i].Cells[3].Value) / projectAmount * 100)).ToString();
                    if (projectToSubmit != oldProgress + Convert.ToDouble(dataGridViewProjects.Rows[i].Cells[2].Value) / projectAmount * 100)
                    {
                        localInterface.Update("UPDATE projects SET toSubmit=" + (oldProgress + Convert.ToDouble(dataGridViewProjects.Rows[i].Cells[2].Value) / projectAmount * 100).ToString() + " WHERE idProjects=" + dataGridViewProjects.Rows[i].Cells[0].Value.ToString());
                    }
                }

                //for (int i=0;i<projectIds.Count;i++)
                //    localInterface.Insert("bills_projects", "idBill,idProject,percent,progress", maxBillId.ToString() + "','" + 
                //                                                                        projectIds[i].ToString() + "','" + 
                //                                                                        projectProgress[i].ToString() + "','" +
                //                                                                        progress[i].ToString());
            }
            if (dateTimePickerDetailsBillApproval.Checked)
                localInterface.Update("UPDATE bills SET approvalDate = '" + MyUtills.dateToSQL(dateTimePickerDetailsBillApproval.Value) +
                                      "' WHERE idBill = '" + textBoxDetailsBillNumber.Text + "'");
            else
                localInterface.Update("UPDATE bills SET approvalDate = NULL WHERE idBill = '" + textBoxDetailsBillNumber.Text + "'");
            if (dateTimePickerDetailsBillToPay.Checked)
                localInterface.Update("UPDATE bills SET toPayDate = '" + MyUtills.dateToSQL(dateTimePickerDetailsBillToPay.Value) +
                                      "' WHERE idBill = '" + textBoxDetailsBillNumber.Text + "'");
            else
                localInterface.Update("UPDATE bills SET toPayDate = NULL WHERE idBill = '" + textBoxDetailsBillNumber.Text + "'");
            if (dateTimePickerDetailsBillPay.Checked)
                localInterface.Update("UPDATE bills SET payDate = '" + MyUtills.dateToSQL(dateTimePickerDetailsBillPay.Value) +
                                      "' WHERE idBill = '" + textBoxDetailsBillNumber.Text + "'");
            else
                localInterface.Update("UPDATE bills SET payDate = NULL WHERE idBill = '" + textBoxDetailsBillNumber.Text + "'");
            MessageBox.Show("חשבון חדש נשמר במערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            this.Close();
            mainForm.refreshView();
            return true;
        }
        private int GetBillPart()
        {
            string where = String.Empty;
            string concat = String.Empty;
            for (int i = 0; i < projectIds.Count; i++)
            {
                where += concat;
                where += "idProject=" + projectIds[i];
                concat = " AND ";
            }
            int billPart = localInterface.Select("SELECT idBill FROM bills_projects WHERE " + where).Tables[0].Rows.Count;

            return (billPart+1);
        }

        private void dataGridViewProjects_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            lastAmount = Convert.ToDouble(dataGridViewProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }
        private void dataGridViewProjects_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double currentProjectPartAmount;
            double currentProjectAmount = 0;
            double currentProjectPaid = 0;

            if (!Double.TryParse(dataGridViewProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out currentProjectPartAmount))
            {
                dataGridViewProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = lastAmount;
                return;
            }
            foreach (DataGridViewRow dr in dataGridViewProjects.Rows)
            {
                currentProjectAmount += Convert.ToDouble(dr.Cells[2].Value);
                currentProjectPaid += Convert.ToDouble(dr.Cells[3].Value);
            }
            textBoxDetailsBillAmount.Text = String.Format("{0:0,0.00}", currentProjectAmount);
            textBoxDetailsBillPaid.Text = String.Format("{0:0,0.00}", currentProjectPaid);
        }
    }
}
