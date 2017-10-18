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
    public partial class Form_NewNote : Form
    {
        private DBinterface localInterface;
        private string idBill;
        private string numberBill;
        Form1 mainForm;
        public Form_NewNote(DBinterface dbInterface, string billId,string billNumber, Form1 form)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            idBill = billId;
            numberBill = billNumber;
            mainForm = form;
        }

        private void Form_NewNote_Load(object sender, EventArgs e)
        {
            //textBoxIdBill.Text = idBill;
            textBoxIdBill.Text = numberBill;
            textBoxName.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxText.Text == String.Empty)
            {
                labelText.ForeColor = Color.Red;
                return;
            }
            textBoxText.Text = textBoxText.Text.Replace(@"\", @"\\").Replace("'", @"\'").Replace('"', '\"');
            string date = MyUtills.dateToSQL(DateTime.Now) + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
            localInterface.Insert("notes", "idBill,name,date,text", idBill + "','" +
                                                                    textBoxName.Text + "','" +
                                                                    date + "','" +
                                                                    textBoxText.Text);
            MessageBox.Show("הודעת מעקב חדשה נשמרה במערכת", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            mainForm.refreshBillNotes(idBill);
            this.Close();
        }
    }
}
