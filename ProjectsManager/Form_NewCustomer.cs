using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ProjectsManager
{
    public partial class Form_NewCustomer : Form
    {
        private DBinterface localInterface;
        private Form_SelectCustomer formSelectCustomer;
        Form1 mainForm;

        public Form_NewCustomer(DBinterface dbInterface, Form_SelectCustomer formCustomer, Form1 form1)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));

            this.localInterface = dbInterface;
            this.formSelectCustomer = formCustomer;
            this.mainForm = form1;
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxDetailsCustomerName.Text == String.Empty)
            {
                labelName.ForeColor = Color.Red;
                return;
            }
            localInterface.Insert("customers", "name,city,address,zipCode,poBox,phoneNumber,fax,eMail,contactPerson,notes",
                                textBoxDetailsCustomerName.Text.Replace("'","\\'") + "','" +
                                textBoxDetailsCustomerCity.Text.Replace("'", "\\'") + "','" +
                                textBoxDetailsCustomerAddress.Text.Replace("'", "\\'") + "','" +
                                textBoxDetailsCustomerZip.Text.Replace("'", "\\'") + "','" +
                                textBoxDetailsCustomerPO.Text.Replace("'", "\\'") + "','" +
                                textBoxDetailsCustomerPhone.Text.Replace("'", "\\'") + "','" +
                                textBoxDetailsCustomerFax.Text.Replace("'", "\\'") + "','" +
                                textBoxDetailsCustomerEmail.Text.Replace("'", "\\'") + "','" +
                                textBoxDetailsCustomerContactPerson.Text.Replace("'", "\\'") + "','" +
                                textBoxDetailsCustomerNotes.Text.Replace("'", "\\'"));
            MessageBox.Show("לקוח חדש נשמר במערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            this.Close();
            if (formSelectCustomer != null)
                formSelectCustomer.refreshAndfillSearchTextBox(textBoxDetailsCustomerName.Text);
            try
            {
                this.mainForm.refreshView();
            }
            catch { }
        }
    }
}
