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
    public partial class Form_Expenses : Form
    {
        DBinterface localInterface;
        Form1 mainForm;
        public Form_Expenses(Form1 form, DBinterface dbInterface)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
            localInterface = dbInterface;
            mainForm = form;
        }

        private void Form_Expenses_Load(object sender, EventArgs e)
        {
            comboBoxSuppliers.DataSource = localInterface.Select("SELECT * FROM suppliers WHERE active='1' ORDER BY name").Tables[0];
            comboBoxSuppliers.DisplayMember = "name";
            comboBoxSuppliers.ValueMember = "id";
            comboBoxSuppliers.DropDownHeight = 1000;
            showExpenses();
            comboBoxDebitDuration.SelectedIndex = 0;
            comboBoxDebitInterval.SelectedIndex = 0;
        }
        private void Form_Expenses_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.showFinance();
        }
        private void showExpenses()
        {
            int idSupplier = Convert.ToInt16(comboBoxSuppliers.SelectedValue);
            string sqlCmd = "SELECT expenses.notes,expenses.id,expenses.date,expenses.amount,expenses.approved,expenses.idSupplier,expense_type.type FROM expenses INNER JOIN expense_type ON expenses.type=expense_type.id WHERE (idSupplier=" + idSupplier.ToString() + " AND bank=0) ORDER BY date ASC";
            dataGridViewExpenses.DataSource = localInterface.Select(sqlCmd).Tables[0];

            dataGridViewExpenses.Columns[0].HeaderText = "הערות";
            dataGridViewExpenses.Columns[1].Visible = false;
            dataGridViewExpenses.Columns[2].HeaderText = "תאריך";
            dataGridViewExpenses.Columns[3].HeaderText = "סכום";
            dataGridViewExpenses.Columns[4].HeaderText = "מאושר";
            dataGridViewExpenses.Columns[5].Visible = false;
            
            dataGridViewExpenses.Columns[6].HeaderText = "סוג";
            
        }

        private void comboBoxSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                showExpenses();
            }
            catch { }
        }
        private void radioButtonSingle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSingle.Checked)
            {
                textBoxAmountSingle.Enabled = true;
                dateTimePickerSingle.Enabled = true;
                textBoxAmountDebit.Enabled = false;
                dateTimePickerDebit.Enabled = false;
                comboBoxDebitInterval.Enabled = false;
                comboBoxDebitDuration.Enabled = false;
            }
            else
            {
                textBoxAmountDebit.Enabled = true;
                dateTimePickerDebit.Enabled = true;
                comboBoxDebitInterval.Enabled = true;
                comboBoxDebitDuration.Enabled = true;
                textBoxAmountSingle.Enabled = false;
                dateTimePickerSingle.Enabled = false;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string query;
            if (radioButtonSingle.Checked)
            {
                double amount;
                if (!Double.TryParse(textBoxAmountSingle.Text, out amount))
                    return;
                DataTable dt = localInterface.Select("SELECT id FROM expenses WHERE idSupplier='" + comboBoxSuppliers.SelectedValue.ToString() + "' AND date='" + MyUtills.dateToSQL(dateTimePickerSingle.Value) + "' AND amount='" + amount.ToString() + "'").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("תשלום זהה קיים כבר", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    return;
                }
                query = "INSERT INTO expenses (date,amount,idSupplier,type,notes) VALUES ('" + MyUtills.dateToSQL(dateTimePickerSingle.Value) + "','" + amount.ToString() + "','" + comboBoxSuppliers.SelectedValue.ToString() + "','1','" + textBoxNotesSingle.Text + "')";
                localInterface.InsertSql(query);
            }
            else if (radioButtonDebit.Checked)
            {
                double amount;
                if (!Double.TryParse(textBoxAmountDebit.Text, out amount))
                {
                    return;
                }
                int interval = 0;
                switch (comboBoxDebitInterval.SelectedIndex)
                {
                    case 0:
                        interval = 1;
                        break;
                    case 1:
                        interval = 2;
                        break;
                    case 2:
                        interval = 3;
                        break;
                    case 3:
                        interval = 6;
                        break;
                    case 4:
                        interval = 12;
                        break;
                }
                int duration = 0;
                switch (comboBoxDebitDuration.SelectedIndex)
                {
                    case 0:
                        duration = 3;
                        break;
                    case 1:
                        duration = 6;
                        break;
                    case 2:
                        duration = 12;
                        break;
                    case 3:
                        duration = 24;
                        break;
                    case 4:
                        duration = 36;
                        break;
                    case 5:
                        duration = 60;
                        break;
                }
                if (interval > duration)
                {
                    MessageBox.Show("משך התקופה חייב להיות גדול מזמן החזרה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    return;
                }
                DateTime newdate;
                DataTable newdt;
                for (int i = 0; i < duration; i += interval)
                {
                    newdate = dateTimePickerDebit.Value.AddMonths(i);
                    newdt = localInterface.Select("SELECT id FROM expenses WHERE idSupplier='" + comboBoxSuppliers.SelectedValue.ToString() + "' AND date='" + MyUtills.dateToSQL(newdate) + "' AND amount='" + amount.ToString() + "'").Tables[0];
                    if (newdt.Rows.Count > 0)
                    {
                        MessageBox.Show("אחד או יותר מהתשלומים כבר קיימים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                        return;
                    }
                }
                for (int i = 0; i < duration; i+=interval)
                {
                    newdate = dateTimePickerDebit.Value.AddMonths(i);
                    query = "INSERT INTO expenses (date,amount,idsupplier,approved,type,notes) VALUES ('" + MyUtills.dateToSQL(newdate) + "','" + amount + "','" + comboBoxSuppliers.SelectedValue.ToString() + "',1,3,'" + newdate.ToString("MMMM") + "')";
                    localInterface.InsertSql(query);
                }
            }
            else if (radioButtonConst.Checked)
            {
                double amount;
                if (!Double.TryParse(textBoxAmountDebit.Text, out amount))
                {
                    return;
                }
                int interval = 0;
                switch (comboBoxDebitInterval.SelectedIndex)
                {
                    case 0:
                        interval = 1;
                        break;
                    case 1:
                        interval = 2;
                        break;
                    case 2:
                        interval = 3;
                        break;
                    case 3:
                        interval = 6;
                        break;
                    case 4:
                        interval = 12;
                        break;
                }
                int duration = 0;
                switch (comboBoxDebitDuration.SelectedIndex)
                {
                    case 0:
                        duration = 3;
                        break;
                    case 1:
                        duration = 6;
                        break;
                    case 2:
                        duration = 12;
                        break;
                    case 3:
                        duration = 24;
                        break;
                    case 4:
                        duration = 36;
                        break;
                    case 5:
                        duration = 60;
                        break;
                }
                if (interval > duration)
                {
                    MessageBox.Show("משך התקופה חייב להיות גדול מזמן החזרה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    return;
                }
                DateTime newdate;
                DataTable newdt;
                for (int i = 0; i < duration; i += interval)
                {
                    newdate = dateTimePickerDebit.Value.AddMonths(i);
                    newdt = localInterface.Select("SELECT id FROM expenses WHERE idSupplier='" + comboBoxSuppliers.SelectedValue.ToString() + "' AND date='" + MyUtills.dateToSQL(newdate) + "' AND amount='" + amount.ToString() + "'").Tables[0];
                    if (newdt.Rows.Count > 0)
                    {
                        MessageBox.Show("אחד או יותר מהתשלומים כבר קיימים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                        return;
                    }
                }
                for (int i = 0; i < duration; i += interval)
                {
                    newdate = dateTimePickerDebit.Value.AddMonths(i);
                    localInterface.InsertSql("INSERT INTO expenses (date,amount,idsupplier,approved,type,notes) VALUES ('" + MyUtills.dateToSQL(newdate) + "','" + amount + "','" + comboBoxSuppliers.SelectedValue.ToString() + "',0,2,'"+newdate.ToString("MMMM")+"')");
                }
            }
            showExpenses();
        }
        private void linkLabelDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewExpenses.SelectedRows)
            {
                localInterface.Delete("expenses","id",row.Cells[1].Value.ToString());
            }
            showExpenses();
        }
        
        private void dataGridViewExpenses_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridViewExpenses.Columns["amount"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
            }
            catch { }
        }
        private void dataGridViewExpenses_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double amount;
            DateTime date;
            int approved = 0;
            if (!DateTime.TryParse(dataGridViewExpenses.Rows[e.RowIndex].Cells[2].Value.ToString(), out date))
            {
                return;
            }
            if (!Double.TryParse(dataGridViewExpenses.Rows[e.RowIndex].Cells[3].Value.ToString(), out amount))
            {
                return;
            }
            if ((bool)dataGridViewExpenses.Rows[e.RowIndex].Cells[4].Value)
                approved = 1;
            localInterface.Update("UPDATE expenses SET amount='" + amount + "', date='" + MyUtills.dateToSQL(date) + "',approved='" + approved.ToString() + "',notes='" + dataGridViewExpenses.Rows[e.RowIndex].Cells[0].Value.ToString() + "' WHERE id='" + dataGridViewExpenses.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
            //showExpenses(Convert.ToInt16(comboBoxSuppliers.SelectedValue));
            //this.BeginInvoke(new MethodInvoker(showExpenses));
        }
        private void dataGridViewExpenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewExpenses.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dataGridViewExpenses_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex > -1)
            {
                int approved = 0;
                if ((bool)dataGridViewExpenses.Rows[e.RowIndex].Cells[4].Value)
                    approved = 1;
                localInterface.Update("UPDATE expenses SET approved='" + approved.ToString() + "' WHERE id='" + dataGridViewExpenses.Rows[e.RowIndex].Cells[0].Value.ToString() + "'");
            }
        }
    }
}
