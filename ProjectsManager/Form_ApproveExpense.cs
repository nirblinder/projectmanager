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
    public partial class Form_ApproveExpense : Form
    {
        List<string> ids;
        bool approved;
        DateTime date;
        DBinterface localInterface;
        Form1 mainForm;
        public Form_ApproveExpense(Form1 form, DBinterface dbInterface, List<string> id)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("en-US"));
            localInterface = dbInterface;
            mainForm = form;
            this.ids = id;
            DataTable dt = localInterface.Select("SELECT approved,date,amount FROM expenses WHERE id=" + ids[0]).Tables[0];
            approved = (bool)dt.Rows[0][0];
            comboBox1.SelectedIndex = 0;
            if (approved)
                comboBox1.SelectedIndex = 1;
            date = MyUtills.dateFromSQL(dt.Rows[0][1].ToString());
            dateTimePicker1.Value = date;
            if (this.ids.Count > 1)
            {
                checkBoxDate.Checked = false;
                textBox1.Visible = false;
                label1.Visible = false;
            }
            else
                textBox1.Text = String.Format("{0:0,0.00}", double.Parse(dt.Rows[0][2].ToString()));
            
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            string app = comboBox1.SelectedIndex.ToString();
            if (ids.Count > 1)
            {
                foreach (string id in ids)
                {
                    if (checkBoxDate.Checked)
                    {
                        if (checkBoxPaiment.Checked)
                            localInterface.Update("UPDATE expenses SET date='" + MyUtills.dateToSQL(dateTimePicker1.Value) + "',approved='" + app + "' WHERE id=" + id);
                        else
                            localInterface.Update("UPDATE expenses SET date='" + MyUtills.dateToSQL(dateTimePicker1.Value) + "' WHERE id=" + id);
                    }
                    else
                    {
                        if (checkBoxPaiment.Checked)
                            localInterface.Update("UPDATE expenses SET approved='" + app + "' WHERE id=" + id);
                    }
                }
            }
            else
            {
                string amount = textBox1.Text.Replace(",","");
                if (checkBoxDate.Checked)
                {
                    if (checkBoxPaiment.Checked)
                        localInterface.Update("UPDATE expenses SET date='" + MyUtills.dateToSQL(dateTimePicker1.Value) + "',approved='" + app + "',amount='" + amount + "' WHERE id=" + ids[0]);
                    else
                        localInterface.Update("UPDATE expenses SET date='" + MyUtills.dateToSQL(dateTimePicker1.Value) + "',amount='" + amount + "' WHERE id=" + ids[0]);
                }
                else
                {
                    localInterface.Update("UPDATE expenses SET approved='" + app + "',amount='" + amount + "' WHERE id=" + ids[0]);
                }
            }
            this.Close();
            mainForm.showFinance();
        }

        private void checkBoxDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDate.Checked)
                dateTimePicker1.Enabled = true;
            else
                dateTimePicker1.Enabled = false;
        }
        private void checkBoxPaiment_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPaiment.Checked)
                comboBox1.Enabled = true;
            else
                comboBox1.Enabled = false;
        }
    }
}
