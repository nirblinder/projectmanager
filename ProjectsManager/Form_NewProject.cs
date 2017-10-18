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
    public partial class Form_NewProject : Form
    {
        Form1 mainForm;
        DBinterface localInterface;
        Form_SelectCustomer formSelectCustomer;
        MyProject localProject = new MyProject();
        public Form_NewProject(Form1 form, DBinterface dbInterface)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            mainForm = form;
        }
        public Form_NewProject(Form1 form, DBinterface dbInterface, string customerName, string customerId)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            insertCustomer(customerName, customerId);
            mainForm = form;
        }
        public Form_NewProject(Form1 form, DBinterface dbInterface, string customerName, MyProject project)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            localProject = project;
            insertCustomer(customerName, project.CustomerId.ToString());
            mainForm = form;
        }
        private void Form_NewProject_Load(object sender, EventArgs e)
        {
            formSelectCustomer = new Form_SelectCustomer(localInterface, this);
            comboBoxCurency.SelectedIndex = 0;
            comboBoxDetailsProjectlinking.SelectedIndex = 0;
            textBoxDetailsProjectAmount.Text = "0";
            textBoxDetailsProjectPriceIndex.Text = "0";
            DataTable dt = localInterface.Select("Select * FROM engineers").Tables[0];
            comboBoxDetailsProjectHandler1.Items.Add("");
            comboBoxDetailsProjectHandler2.Items.Add("");
            foreach (DataRow dr in dt.Rows)
            {
                comboBoxDetailsProjectHandler1.Items.Add(dr[0]);
                comboBoxDetailsProjectHandler2.Items.Add(dr[0]);
            }
            dt = localInterface.Select("Select projectNumber FROM projects").Tables[0];
            double temp = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (double.Parse(row[0].ToString()) > temp)
                    if (double.Parse(row[0].ToString()) != 9999)
                    temp = double.Parse(row[0].ToString());
            }
            textBoxDetailsProjectNumber.Text = ((int)temp + 1).ToString();

            textBoxCustomerId.Text = localProject.CustomerId.ToString();
            textBoxDetailsProjectAmount.Text = localProject.Amount.ToString();
            comboBoxCurency.SelectedIndex = localProject.Curency;
            textBoxDetailsProjectAmountInfo.Text = localProject.AmountNotes;
            if (localProject.Type == 1)
                radioButtonProjectTypeElectricity.Checked = true;
            else
                radioButtonProjectTypeInstilation.Checked = false;
            try
            {
                comboBoxDetailsProjectHandler1.SelectedIndex = int.Parse(localProject.Handler1);
            }
            catch
            {
                comboBoxDetailsProjectHandler1.SelectedIndex = 0;
            }
            try
            {
                comboBoxDetailsProjectHandler2.SelectedIndex = int.Parse(localProject.Handler2);
            }
            catch
            {
                comboBoxDetailsProjectlinking.SelectedIndex = 0;
            }
            textBoxDetailsProjectPriceIndex.Text = localProject.PriceIndex.ToString();
            if (localProject.PriceIndexDateExists == 1)
            {
                dateTimePickerDetailsProjectPriceIndexDate.Checked = true;
                dateTimePickerDetailsProjectPriceIndexDate.Value = localProject.PriceIndexDate;
            }
            if (localProject.ContractExists == 1)
                checkBoxDetailsProjectContract.Checked = true;
            textBoxDetailsProjectContractNotes.Text = localProject.ContractNotes;
            textBoxDetailsProjectNotes.Text = localProject.ProjectNotes;
            textBoxDetailsProjectMileStones.Text = localProject.MileStonesNotes;
            //Payer
            textBoxPayerName.Text = localProject.PayerName;
            textBoxPayerPhone.Text = localProject.PayerPhone;
            textBoxPayerEmail.Text = localProject.PayerEmail;
            textBoxPayerFax.Text = localProject.PayerFax;
            textBoxPayerAddress.Text = localProject.PayerAddress;
            //Approver
            textBoxApproverName.Text = localProject.ApproverName;
            textBoxApproverPhone.Text = localProject.ApproverPhone;
            textBoxApproverEmail.Text = localProject.ApproverEmail;
            textBoxApproverFax.Text = localProject.ApproverFax;
            textBoxApproverAddress.Text = localProject.PayerAddress;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            labelName.ForeColor = Color.Black;
            labelProjectNumber.ForeColor = Color.Black;
            labelCustomerName.ForeColor = Color.Black;
            labelAmount.ForeColor = Color.Black;
            labelPriceIndex.ForeColor = Color.Black;

            bool returnFlag = false;
            if (textBoxDetailsProjectName.Text == String.Empty)
            {
                labelName.ForeColor = Color.Red;
                returnFlag = true;
            }
            if (textBoxDetailsProjectNumber.Text == String.Empty)
            {
                labelProjectNumber.ForeColor = Color.Red;
                returnFlag = true;
            }
            else 
            {
                try
                {
                    double.Parse(textBoxDetailsProjectNumber.Text);
                }
                catch 
                {
                    labelProjectNumber.ForeColor = Color.Red;
                    returnFlag = true;
                }
                DataTable tempNumbersTable;
                tempNumbersTable = localInterface.Select("SELECT projectNumber FROM projects WHERE projectNumber LIKE '%" + textBoxDetailsProjectNumber.Text + "%'").Tables[0];
                if (tempNumbersTable.Rows.Count > 0)
                {
                    double temp = 0;
                    foreach (DataRow row in tempNumbersTable.Rows)
                    {
                        if (double.Parse(row[0].ToString()) > temp)
                            temp = double.Parse(row[0].ToString());
                    }
                    textBoxDetailsProjectNumber.Text = (temp + 0.1).ToString();
                    MessageBox.Show("מספר פרוייקט כבר קיים," + "\r\n" + "המספר הוחלף במספר עוקב," + "\r\n" + "באפשרותך לשנותו או לשמור שנית", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    labelProjectNumber.ForeColor = Color.Red;
                    returnFlag = true;
                }


            }
            if (textBoxDetailsProjectCustomerName.Text == String.Empty)
            {
                labelCustomerName.ForeColor = Color.Red;
                returnFlag = true;
            }
            try
            {
                double.Parse(textBoxDetailsProjectAmount.Text);
            }
            catch 
            {
                labelAmount.ForeColor = Color.Red;
                textBoxDetailsProjectAmount.Text = "0";
                returnFlag = true;
            }
            try
            {
                Double.Parse(textBoxDetailsProjectPriceIndex.Text);
            }
            catch
            {
                labelPriceIndex.ForeColor = Color.Red;
                textBoxDetailsProjectPriceIndex.Text = "0";
                returnFlag = true;
            }
            if (returnFlag)
                return;
            short contract = 0;
            if (checkBoxDetailsProjectContract.Checked)
                contract = 1;
            short isClosed = 0;
            if (checkBoxDetailsProjectIsClosed.Checked)
                isClosed = 1;
            short projectType = 0;
            if (radioButtonProjectTypeInstilation.Checked)
                projectType = 1;
            localInterface.Insert("projects", "projectNumber,handler1,handler2,startDate,idCustomer,projectName,amount,curency,amountInfo,linking,priceIndex,contract,contractNotes," +
                                              "projectNotes,isClosed,archiveLocation,contractNumber,mileStones,projectType,approverName,approverPhone,approverEmail,approverFax," +
                                              "payerName,payerPhone,payerEmail,payerFax,payerAddress,approverAddress",
                                              textBoxDetailsProjectNumber.Text.Replace("'", "\\'") + "','" +
                                              comboBoxDetailsProjectHandler1.Text.Replace("'", "\\'") + "','" +
                                              comboBoxDetailsProjectHandler2.Text.Replace("'", "\\'") + "','" +
                                              MyUtills.dateToSQL(dateTimePickerDetailsProjectStart.Value) + "','" +
                                              textBoxCustomerId.Text + "','" +
                                              textBoxDetailsProjectName.Text.Replace("'", "\\'") + "','" +
                                              textBoxDetailsProjectAmount.Text + "','" +
                                              comboBoxCurency.SelectedIndex.ToString() + "','" +
                                              textBoxDetailsProjectAmountInfo.Text.Replace("'", "\\'") + "','" +
                                              comboBoxDetailsProjectlinking.SelectedIndex.ToString() + "','" +
                                              textBoxDetailsProjectPriceIndex.Text + "','" +
                                              contract.ToString() + "','" +
                                              textBoxDetailsProjectContractNotes.Text.Replace("'", "\\'") + "','" +
                                              textBoxDetailsProjectNotes.Text.Replace("'", "\\'") + "','" +
                                              isClosed.ToString() + "','" +
                                              textBoxDetailsProjectArchiveLocation.Text.Replace("'", "\\'") + "','" +
                                              textBoxDetailsProjectContractNumber.Text.Replace("'", "\\'") + "','" +
                                              textBoxDetailsProjectMileStones.Text.Replace("'", "\\'") + "','" +
                                              projectType.ToString() + "','" +
                                              textBoxApproverName.Text.Replace("'", "\\'") + "','" +
                                              textBoxApproverPhone.Text + "','" +
                                              textBoxApproverEmail.Text + "','" +
                                              textBoxApproverFax.Text + "','" +
                                              textBoxPayerName.Text.Replace("'", "\\'") + "','" +
                                              textBoxPayerPhone.Text + "','" +
                                              textBoxPayerEmail.Text + "','" +
                                              textBoxPayerFax.Text+ "','" +
                                              textBoxPayerAddress.Text.Replace("'", "\\'") + "','" +
                                              textBoxApproverAddress.Text.Replace("'", "\\'"));
            if (dateTimePickerDetailsProjectPriceIndexDate.Checked)
                localInterface.Update("UPDATE projects SET priceIndexDate = '" + MyUtills.dateToSQL(dateTimePickerDetailsProjectPriceIndexDate.Value) +
                                      "' WHERE projectNumber = '" + textBoxDetailsProjectNumber.Text + "' AND projectName = '" + textBoxDetailsProjectName.Text + "'");
            else
                localInterface.Update("UPDATE projects SET priceindexDate = NULL WHERE projectNumber = '" + textBoxDetailsProjectNumber.Text + "' AND projectName = '" + textBoxDetailsProjectName.Text + "'");
            MessageBox.Show("פרוייקט חדש נשמר במערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            this.Close();
            mainForm.refreshView();
        }

        private void buttonSelectCustomer_Click(object sender, EventArgs e)
        {
            formSelectCustomer.ShowDialog();
        }
        public void insertCustomer(string name, string id)
        {
            textBoxCustomerId.Text = id;
            textBoxDetailsProjectCustomerName.Text = name;
        }
    }
}
