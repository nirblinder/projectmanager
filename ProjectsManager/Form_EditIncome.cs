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
    public partial class Form_EditIncome : Form
    {
        string id;
        bool definite;
        DBinterface localInterface;
        Form1 mainForm;
        public Form_EditIncome(Form1 form, DBinterface dbInterface, string id, bool def)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));
            localInterface = dbInterface;
            mainForm = form;
            this.id = id;
            this.definite = def;

            if (definite)
                dateTimePicker1.Value = MyUtills.dateFromSQL(localInterface.Select("SELECT payDate FROM bills WHERE id=" + this.id).Tables[0].Rows[0][0].ToString());
            else
                dateTimePicker1.Value = MyUtills.dateFromSQL(localInterface.Select("SELECT toPayDate FROM bills WHERE id=" + this.id).Tables[0].Rows[0][0].ToString());

        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            DateTime newDate = dateTimePicker1.Value;
            if (this.definite)
                localInterface.Update("UPDATE bills SET payDate='" + MyUtills.dateToSQL(newDate) + "' WHERE id=" + this.id);
            else
                localInterface.Update("UPDATE bills SET toPayDate='" + MyUtills.dateToSQL(newDate) + "' WHERE id=" + this.id);
            this.Close();
            mainForm.showFinance();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGoToBill_Click(object sender, EventArgs e)
        {
            this.Close();
            mainForm.financeGoToBill(this.id);
        }
    }
}
