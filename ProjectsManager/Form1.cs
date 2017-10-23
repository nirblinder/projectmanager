using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Globalization;


namespace ProjectsManager
{
    public partial class Form1 : Form
    {
        enum CurrentPanel
        {
            NONE,//0
            PROJECTS,//1
            BILLS,//2
            CUSTOMERS,//3
        };
        CurrentPanel currentPanel;
        DBinterface mainInterface;
        Form_NewProject formNewProject;
        Form_NewBill formNewBill;
        Form_NewCustomer formNewCustomer;
        Form_Engineers formEngineers;
        Form_handlers formHandlers;
        Form_NewNote formNewNote;
        Form_Settings formSettings;
        Form_ShowNote formShowNote;
        Form_Summery formSum;
        MyProject currentProject;
        MyBill currentBill;
        MyCustomer currentCustomer;
        Form_SelectCustomer formSelectCustomer;
        Form_Export formExport;
        MyLastSearch lastSearch;
        Form_UserEnter formUserEnter;
        Form_Suppliers formSuppliers;
        Form_Expenses formExpenses;
        Form_ApproveExpense formApproveExpense;
        Form_EditIncome formEditIncome;

        #region Variables
        ArrayList arrColumnLefts = new ArrayList(); //Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList(); //Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0; //Used as counter
        int iHeaderHeight = 0; //Used for the header height
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page

        bool searchById = false;
        string searchIdQuery = "";
        // Variables used to remember last position for refresh
        string selectedId = "";
        double lastProjectPartAmount = 0;
        double lastProjectPartPaid = 0;

        bool userEnter = false;
        int userType = 3;
        bool showSums = true;
        int lastFinanceSelectedRow = 0, lastFinanceScrolledRow = 0;
        double ScenarioPercent = 0.6;
        #endregion

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("he-IL"));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            File.AppendAllText(MyFiles.ExceptionFile, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n");
            MySettings.CreateFile();
            mainInterface = new DBinterface();
            mainInterface.LoadSettings();
            currentPanel = CurrentPanel.NONE;
            splitContainer.SplitterDistance = (splitContainer.Panel1.Height + splitContainer.Panel2.Height) / 2;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            comboBoxCustomerRate.SelectedIndex = 0;
            try
            {
                int height = int.Parse(MySettings.getHeight());
                int width = int.Parse(MySettings.getWidth());
                this.Size = new System.Drawing.Size(width, height);
                if (int.Parse(MySettings.getX()) < 0 || int.Parse(MySettings.getY()) < 0)
                {
                    this.Left = 0;
                    this.Top = 0;
                    this.Size = new System.Drawing.Size(1024, 700);
                }
                else
                {
                    this.Left = int.Parse(MySettings.getX());
                    this.Top = int.Parse(MySettings.getY());
                }
            }
            catch { }
            lastSearch = new MyLastSearch(this, mainInterface);
            resizeView();
            fillRelations();
            updateCurrency();
        }
        private void updateCurrency()
        {
            try
            {
                DataTable dt = mainInterface.Select("SELECT id FROM bills WHERE curencyRate=0").Tables[0];
                foreach (DataRow row in dt.Rows)
                    mainInterface.Update("UPDATE bills SET curencyRate=1 WHERE id=" + row[0].ToString());
            }
            catch { }
        }
        private void fillRelations()
        {
            // 1
            /*
            DataTable dt = mainInterface.Select("SELECT idProjects,amount FROM projects").Tables[0];
            DataTable dt1;
            double sum, percent;
            foreach (DataRow dr in dt.Rows)
            {
                dt1 = mainInterface.Select("SELECT id,amount,paid FROM bills WHERE idProject='" + dr[0].ToString() + "' ORDER BY billDate ASC").Tables[0];
                sum = 0;
                foreach (DataRow dr1 in dt1.Rows)
                {
                    percent = (double)dr1[1] / (double)dr[1] * 100;
                    sum += percent;
                    mainInterface.InsertSql("INSERT INTO bills_projects (idBill,idProject,progress,percent) VALUES ('" + dr1[0].ToString() + "','" + dr[0].ToString() + "','" + sum.ToString() + "','" + percent.ToString() + "')");
                }
            }
            */
            // 2
            /*
            DataTable dt = mainInterface.Select("SELECT bills_projects.id,bills.paid,projects.amount "+
                                                "FROM bills_projects "+
                                                "INNER JOIN bills ON bills_projects.idBill=bills.id "+
                                                "INNER JOIN projects ON bills_projects.idProject=projects.idProjects").Tables[0];
            double paid;
            foreach (DataRow dr in dt.Rows)
            {
                if ((double)dr[2] == 0)
                    paid = 0;
                else
                    paid = (double)dr[1] / (double)dr[2] * 100;
                mainInterface.Update("UPDATE bills_projects SET paid='"+ paid.ToString() +"' WHERE id='" + dr[0].ToString() + "'");
            }
            */
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int height = this.Height;
            int width = this.Width;
            MySettings.setSize(height.ToString(), width.ToString());
            MySettings.setLocation(this.Top.ToString(), this.Left.ToString());
        }
        
        private void timerClock_Tick(object sender, EventArgs e)
        {
            labelClock.Text = DateTime.Now.ToLongTimeString();
        }

        private void ToolStripMenuItemNewProject_Click(object sender, EventArgs e)
        {
            formNewProject = new Form_NewProject(this, mainInterface);
            formNewProject.ShowDialog();
        }
        private void שכפלפרוייקטToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string customerName = textBoxDetailsProjectCustomerName.Text;

            MyProject project = new MyProject();
            project.CustomerId = int.Parse(textBoxCustomerId.Text);
            project.Amount = double.Parse(textBoxDetailsProjectAmount.Text);
            project.Curency = comboBoxProjectCurency.SelectedIndex;
            project.AmountNotes = textBoxDetailsProjectAmountInfo.Text;
            project.Type = (short)comboBoxDetailsProjectType.SelectedIndex;
            project.Handler1 = comboBoxDetailsProjectHandler1.SelectedIndex.ToString();
            project.Handler2 = comboBoxDetailsProjectHandler2.SelectedIndex.ToString();
            project.LinkingType = comboBoxDetailsProjectlinking.SelectedIndex;
            project.PriceIndex = double.Parse(textBoxDetailsProjectPriceIndex.Text);
            project.PriceIndexDate = dateTimePickerDetailsProjectPriceIndexDate.Value;
            if (dateTimePickerDetailsProjectPriceIndexDate.Checked)
                project.PriceIndexDateExists = 1;
            else
                project.PriceIndexDateExists = 0;
            if (labelProjectProjectContractExists.Checked)
                project.ContractExists = 1;
            else
                project.ContractExists = 0;

            project.ContractNotes = textBoxDetailsProjectContractNotes.Text;
            project.ProjectNotes = textBoxDetailsProjectNotes.Text;
            project.MileStonesNotes = textBoxDetailsProjectMileStones.Text;
            project.ContractNumber = textBoxDetailsProjectContractNumber.Text;
            //Payer
            project.PayerName = textBoxDetailsProjectPayerName.Text;
            project.PayerPhone = textBoxDetailsProjectPayerPhone.Text;
            project.PayerFax = textBoxDetailsProjectPayerFax.Text;
            project.PayerEmail = textBoxDetailsProjectPayerEmail.Text;
            project.PayerAddress = textBoxDetailsProjectPayerAddress.Text;
            //Approver
            project.ApproverName = textBoxDetailsProjectApproverName.Text;
            project.ApproverPhone = textBoxDetailsProjectApproverPhone.Text;
            project.ApproverFax = textBoxDetailsProjectApproverFax.Text;
            project.ApproverEmail = textBoxDetailsProjectApproverEmail.Text;
            project.ApproverAddress = textBoxDetailsProjectApproverAddress.Text;

            formNewProject = new Form_NewProject(this, mainInterface, customerName, project);
            formNewProject.ShowDialog();
        }
        private void ToolStripMenuItemNewBill_Click(object sender, EventArgs e)
        {
            formNewBill = new Form_NewBill(this, mainInterface);
            formNewBill.ShowDialog();
        }
        private void ToolStripMenuItemNewCustomer_Click(object sender, EventArgs e)
        {
            formNewCustomer = new Form_NewCustomer(mainInterface, null,this);
            formNewCustomer.ShowDialog();
        }
        private void textBoxFiltersBillsAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                int.Parse(textBoxFiltersBillsAmount.Text);
            }
            catch
            {
                textBoxFiltersBillsAmount.Text = "";
            }
        }

        private void buttonSearchProjects_Click(object sender, EventArgs e)
        {
            lastSearch.Panel = (short)CurrentPanel.PROJECTS;
            string query;
            DataSet ds = new DataSet();
            buttonSearchProjects.Enabled = false;
            if (searchById)
            {
                searchById = false;
                lastSearch.IsSearch = false;
                lastSearch.IdQuery = searchIdQuery;
                query = "SELECT projects.idProjects,projects.isClosed,projects.projectNumber,projects.projectName,projects.handler1,customers.name," +
                           "projects.amount,projects.toSubmit,COALESCE(ROUND(SUM(bills_projects.percent)),0) AS percentageSubmitted,SUM(bills_projects.paid/100*projects.amount) AS billsSumPaid,COALESCE(ROUND(SUM(bills_projects.paid)),0) AS percentagePaid,SUM(bills.amount) AS billsSumSubmitted,projects.startDate,projects.projectType,projects.archiveLocation FROM projects " +
                           "INNER JOIN customers ON projects.idCustomer = customers.idCustomer LEFT JOIN bills_projects ON projects.idProjects=bills_projects.idProject LEFT JOIN bills ON bills.id=bills_projects.idBill WHERE " + searchIdQuery + " GROUP BY projects.idProjects";
            }
            else
            {
                searchById = false;
                lastSearch.IsSearch = true;
                query = "SELECT projects.idProjects,projects.isClosed,projects.projectNumber,projects.projectName,projects.handler1,customers.name," +
                           "projects.amount,projects.toSubmit,COALESCE(ROUND(SUM(bills_projects.percent)),0) AS percentageSubmitted,SUM(bills_projects.paid/100*projects.amount) AS billsSumPaid,COALESCE(ROUND(SUM(bills_projects.paid)),0) AS percentagePaid,SUM(bills.amount) AS billsSumSubmitted,projects.startDate,projects.projectType,projects.archiveLocation FROM projects " +
                           "INNER JOIN customers ON projects.idCustomer = customers.idCustomer LEFT JOIN bills_projects ON projects.idProjects=bills_projects.idProject LEFT JOIN bills ON bills.id=bills_projects.idBill";
                query += " GROUP BY projects.idProjects";
                string concatenator = " HAVING ";
                if (checkBoxFiltersProjectsBillNeeded.Checked)
                {
                    if (checkBoxFiltersProjectsBillsToSubmit.Checked)
                    {
                        query += concatenator;
                        query += "(percentageSubmitted < FLOOR(projects.toSubmit) OR (projects.toSubmit>0 AND percentageSubmitted IS NULL))";
                        concatenator = " AND ";
                    }
                    else
                    {
                        query += concatenator;
                        query += "(percentageSubmitted >= projects.toSubmit OR (projects.toSubmit=0 AND percentageSubmitted IS NULL))";
                        concatenator = " AND ";
                    }
                }
                if (checkBoxFiltersProjectsProjectType.Checked)
                {
                    if (radioButtonFiltersProjectsProjectTypeElectricity.Checked)
                    {
                        query += concatenator;
                        query += "projects.projectType = 0";
                        concatenator = " AND ";
                    }
                    else
                    {
                        query += concatenator;
                        query += "projects.projectType = 1";
                        concatenator = " AND ";
                    }
                }
                if (textBoxFiltersProjectsFreeText.Text != String.Empty)
                {
                    query += concatenator;
                    query += "(projects.idProjects like '%" + textBoxFiltersProjectsFreeText.Text + "%' ";
                    query += "OR projects.projectName like '%" + textBoxFiltersProjectsFreeText.Text + "%' ";
                    query += "OR projects.projectNumber like '%" + textBoxFiltersProjectsFreeText.Text + "%')";
                    concatenator = " AND ";
                }
                if (checkBoxFiltersProjectsStatus.Checked)
                {
                    if (radioButtonFiltersProjectsStatusOpen.Checked)
                    {
                        query += concatenator;
                        query += "projects.isClosed = 0";
                        concatenator = " AND ";
                    }
                    else
                    {
                        query += concatenator;
                        query += "projects.isClosed = 1";
                        concatenator = " AND ";
                    }
                }
                if (checkBoxFiltersProjectsDates.Checked)
                {
                    query += concatenator;
                    query += " projects.startDate BETWEEN '" + MyUtills.dateToSQL(dateTimePickerFiltersProjectsFrom.Value.Date) +
                                            "' AND '" + MyUtills.dateToSQL(dateTimePickerFiltersProjectsTo.Value.Date) + "'";
                    concatenator = " AND ";
                }
                if (checkBoxFiltersProjectsArchive.Checked)
                {
                    if (checkBoxFiltersProjectsArchiveExist.Checked)
                    {
                        query += concatenator;
                        query += "(projects.archiveLocation<>'' AND projects.archiveLocation IS NOT NULL)";
                        concatenator = " AND ";
                    }
                    else
                    {
                        query += concatenator;
                        query += "(projects.archiveLocation='' OR projects.archiveLocation IS NULL)";
                        concatenator = " AND ";
                    }
                }
                if (checkBoxFiltersProjectsByAmount.Checked)
                {
                    if (textBoxFiltersProjectsAmount.Text != String.Empty)
                    {
                        if (radioButtonFiltersProjectsByAmountSmaller.Checked)
                        {
                            query += concatenator;
                            query += "projects.amount < " + textBoxFiltersProjectsAmount.Text;
                            concatenator = " AND ";
                        }
                        else if (radioButtonFiltersProjectsByAmountBigger.Checked)
                        {
                            query += concatenator;
                            query += "projects.amount > " + textBoxFiltersProjectsAmount.Text;
                            concatenator = " AND ";
                        }
                        else
                        {
                            query += concatenator;
                            query += "projects.amount = " + textBoxFiltersProjectsAmount.Text;
                            concatenator = " AND ";
                        }
                    }
                }
            }
            
            ds = mainInterface.Select(query);
            dataGridView1.DataSource = null;
            DataTable dtable = ds.Tables[0];
            dataGridView1.DataSource = dtable;

            double sumAmount = 0;
            double sumPaid = 0;
            try
            {
                sumAmount = (double)dtable.Compute("Sum(amount)", "True");
            }
            catch { }
            try
            {
                sumPaid = (double)dtable.Compute("Sum(billsSumPaid)", "True");
            }
            catch { }
            
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "סגור";
            dataGridView1.Columns[2].HeaderText = "מספר פרוייקט";
            dataGridView1.Columns[3].HeaderText = "שם פרוייקט";
            dataGridView1.Columns[4].HeaderText = "מהנדס מטפל";
            dataGridView1.Columns[5].HeaderText = "שם לקוח";
            dataGridView1.Columns[6].HeaderText = "שכר טירחה";
            dataGridView1.Columns[7].HeaderText = "להגשה";
            dataGridView1.Columns[8].HeaderText = "הוגש";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].HeaderText = "נפרע";
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;

            dataGridView1.Columns[6].DefaultCellStyle.Format="0.00";

            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 70;
            dataGridView1.Columns[7].Width = 50;
            dataGridView1.Columns[8].Width = 50;
            dataGridView1.Columns[10].Width = 50;
            dataGridView1.Columns[3].Width = dataGridView1.Width -
                                             dataGridView1.Columns[2].Width -
                                             dataGridView1.Columns[1].Width -
                                             dataGridView1.Columns[4].Width -
                                             dataGridView1.Columns[5].Width -
                                             dataGridView1.Columns[6].Width -
                                             dataGridView1.Columns[7].Width -
                                             dataGridView1.Columns[8].Width -
                                             dataGridView1.Columns[10].Width - 20;

            updateStatusStripSum(1, dataGridView1.RowCount,sumAmount, sumPaid);
            currentPanel = CurrentPanel.PROJECTS;
            labelCurrentView.Text = "פרוייקטים";

            buttonSearchProjects.Enabled = true;
            if (dataGridView1.RowCount == 0)
            {
                panelDetailsBill.Visible = false;
                panelDetailsProject.Visible = false;
                panelDetailsCustomer.Visible = false;
            }
            else
            {
                dataGridView1.Rows[0].Selected = true;
                showCurrentRowDetails(0);
            }
        }
        private void buttonSearchBills_Click(object sender, EventArgs e)
        {
            lastSearch.Panel = (short)CurrentPanel.BILLS;
            string query = "";
            buttonSearchBills.Enabled = false;
            if (searchById)
            {
                searchById = false;
                lastSearch.IsSearch = false;
                lastSearch.IdQuery = searchIdQuery;
                query = "SELECT bills.id, " + 
                               "bills.isClosed, " +
                               "bills.idBill, " +
                               "projects.projectName, " +
                               "bills.billPart, " +
                               "bills.curencyRate*bills_projects.percent*projects.amount/100 AS amount, " +
                               "bills_projects.percent, " +
                               "customers.name AS paid, " +
                               /*
                               "bills.toPayDate, " +
                               "bills.handler, " +
                               "projects.handler1, " +
                               "lastNotes.date, " +
                               "bills.callback, " + 
                               "bills_projects.idProject " +
                               */
                               "bills.billDate, " +
                               "bills.toPayDate, " +
                               "bills.handler, " +
                               //"projects.handler1, " +
                               "lastNotes.date, " +
                               "bills.callback, " +
                               "bills_projects.idProject, " +
                               "bills.curencyRate*bills_projects.paid*projects.amount/100 AS paid " +
                           "FROM bills " +
                           "INNER JOIN bills_projects ON bills.id = bills_projects.idBill " +
                           "INNER JOIN projects ON bills_projects.idProject = projects.idProjects  " + 
                           "INNER JOIN customers ON projects.idCustomer=customers.idCustomer " +
                           "LEFT JOIN lastNotes ON lastNotes.idBill = bills.id WHERE " + searchIdQuery;
            }
            else
            {
                searchById = false;
                lastSearch.IsSearch = true;
                query = "SELECT bills.id, " +
                               "bills.isClosed, " +
                               "bills.idBill, " +
                               "projects.projectName, " +
                               "bills.billPart, " +
                               "bills.curencyRate*bills_projects.percent*projects.amount/100 AS amount, " +
                               "bills_projects.percent, " +
                               "customers.name AS paid, " +
                               "bills.billDate, " +
                               "bills.toPayDate, " +
                               "bills.handler, " +
                               //"projects.handler1, " +
                               "lastNotes.date, " +
                               "bills.callback, " + 
                               "bills_projects.idProject, " +
                               "bills.curencyRate*bills_projects.paid*projects.amount/100 AS paid " +
                        "FROM bills  " +
                        "INNER JOIN bills_projects ON bills.id = bills_projects.idBill  " +
                        "INNER JOIN projects ON bills_projects.idProject = projects.idProjects  " +
                        "INNER JOIN customers ON projects.idCustomer=customers.idCustomer " +
                        "LEFT JOIN lastNotes ON lastNotes.idBill = bills.id";
                string concatenator = " WHERE ";
                if (checkBoxFiltersBillsProjectType.Checked)
                {
                    if (radioButtonFiltersBillsProjectTypeElectricity.Checked)
                    {
                        query += concatenator;
                        query += "projects.projectType = 0";
                        concatenator = " AND ";
                    }
                    else
                    {
                        query += concatenator;
                        query += "projects.projectType = 1";
                        concatenator = " AND ";
                    }
                }
                if (textBoxFiltersBillsFreeText.Text != String.Empty)
                {
                    query += concatenator;
                    query += "bills.idBill like '%" + textBoxFiltersBillsFreeText.Text + "%' ";
                    //   query += "OR projects.projectName like '%" + textBoxFiltersBillsFreeText.Text + "%' ";
                    //   query += "OR projects.projectName like '%" + textBoxFiltersBillsFreeText.Text + "%')";
                    concatenator = " AND ";
                }
                if (checkBoxFiltersBillsBillStatus.Checked)
                {
                    if (radioButtonFiltersBillsProjectStatusOpen.Checked)
                    {
                        query += concatenator;
                        query += "bills.isClosed = 0";
                        concatenator = " AND ";
                    }
                    else
                    {
                        query += concatenator;
                        query += "bills.isClosed = 1";
                        concatenator = " AND ";
                    }
                }
                if (checkBoxFiltersBillsPaymentStatus.Checked)
                {
                    if (checkBoxFiltersBillsReciptExists.Checked && checkBoxFiltersBillsInvoiceExists.Checked)
                    {
                        query += concatenator;
                        query += "(bills.receiptNumber NOT LIKE '' AND bills.invoiceNumber NOT LIKE '')";
                        concatenator = " AND ";
                    }
                    else if (!checkBoxFiltersBillsReciptExists.Checked && checkBoxFiltersBillsInvoiceExists.Checked)
                    {
                        query += concatenator;
                        query += "(bills.receiptNumber LIKE '' AND bills.invoiceNumber NOT LIKE '')";
                        concatenator = " AND ";
                    }
                    else if (checkBoxFiltersBillsReciptExists.Checked && !checkBoxFiltersBillsInvoiceExists.Checked)
                    {
                        query += concatenator;
                        query += "(bills.receiptNumber NOT LIKE '' AND bills.invoiceNumber LIKE '')";
                        concatenator = " AND ";
                    }
                    else if (!checkBoxFiltersBillsReciptExists.Checked && !checkBoxFiltersBillsInvoiceExists.Checked)
                    {
                        query += concatenator;
                        query += "(bills.receiptNumber LIKE '' AND bills.invoiceNumber LIKE '')";
                        concatenator = " AND ";
                    }
                }
                if (checkBoxFiltersBillsDates.Checked)
                {
                    query += concatenator;
                    query += " bills.billDate BETWEEN '" + MyUtills.dateToSQL(dateTimePickerFiltersBillsFrom.Value.Date) +
                                            "' AND '" + MyUtills.dateToSQL(dateTimePickerFiltersBillsTo.Value.Date) + "'";
                    concatenator = " AND ";
                }
                if (checkBoxFiltersBillsByAmount.Checked)
                {
                    if (textBoxFiltersBillsAmount.Text != String.Empty)
                    {
                        if (radioButtonFiltersBillsByAmountSmaller.Checked)
                        {
                            query += concatenator;
                            query += "bills.amount < " + textBoxFiltersBillsAmount.Text;
                            concatenator = " AND ";
                        }
                        else if (radioButtonFiltersBillsByAmountBigger.Checked)
                        {
                            query += concatenator;
                            query += "bills.amount > " + textBoxFiltersBillsAmount.Text;
                            concatenator = " AND ";
                        }
                        else
                        {
                            query += concatenator;
                            query += "bills.amount = " + textBoxFiltersBillsAmount.Text;
                            concatenator = " AND ";
                        }
                    }
                }
                if (checkBoxFiltersBillsByCustomer.Checked)
                {
                    query += concatenator;
                    query += " projects.idCustomer = " + comboBoxFiltersBillsCustomers.SelectedValue.ToString();
                    concatenator = " AND ";
                }
            }
            //query += " GROUP BY bills_projects.idBill";
            dataGridView1.DataSource = null;
            DataTable dtable = mainInterface.Select(query).Tables[0];
            dataGridView1.DataSource = dtable;
            double sumAmount = 0;
            double sumPaid = 0;
            for (int i=0; i<dtable.Rows.Count; i++)
            {
                sumAmount   += (double)dtable.Rows[i][5];
                sumPaid     += (double)dtable.Rows[i][14];
            }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "סגור";
            dataGridView1.Columns[2].HeaderText = "מספר חשבון";
            dataGridView1.Columns[3].HeaderText = "שם פרוייקט";
            dataGridView1.Columns[4].HeaderText = "ח\"ח";
            dataGridView1.Columns[5].HeaderText = "סכום";
            dataGridView1.Columns[6].HeaderText = "אחוז מפרויקט";
            dataGridView1.Columns[7].HeaderText = "לקוח";
            dataGridView1.Columns[8].HeaderText = "תאריך חשבון";
            dataGridView1.Columns[9].HeaderText = "תאריך לתשלום";
            dataGridView1.Columns[10].HeaderText = "גורם מטפל";
            //dataGridView1.Columns[10].HeaderText = "מהנדס מטפל";
            dataGridView1.Columns[11].HeaderText = "תאריך הודעה אחרונה";
            dataGridView1.Columns[12].HeaderText = "לטלפן";

            dataGridView1.Columns[5].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "0.00";

            dataGridView1.Columns[1].Width -= 60;
            dataGridView1.Columns[2].Width -= 55;
            dataGridView1.Columns[3].Width += 50;
            dataGridView1.Columns[4].Width -= 70;
            dataGridView1.Columns[5].Width -= 35;
            dataGridView1.Columns[6].Width -= 50;
            dataGridView1.Columns[7].Width -= 25;
            dataGridView1.Columns[8].Width -= 35;
            dataGridView1.Columns[9].Width -= 35;
            dataGridView1.Columns[10].Width -= 50;
            dataGridView1.Columns[11].Width += 0;
            dataGridView1.Columns[12].Width = dataGridView1.Width -
                                             dataGridView1.Columns[1].Width -
                                             dataGridView1.Columns[3].Width -
                                             dataGridView1.Columns[4].Width -
                                             dataGridView1.Columns[5].Width -
                                             dataGridView1.Columns[6].Width -
                                             dataGridView1.Columns[7].Width -
                                             dataGridView1.Columns[8].Width -
                                             dataGridView1.Columns[9].Width -
                                             dataGridView1.Columns[10].Width -
                                             dataGridView1.Columns[11].Width -
                                             dataGridView1.Columns[2].Width;
            // Console.WriteLine(sumAmount.ToString() + "    " + sumPaid.ToString());
            updateStatusStripSum(0, dataGridView1.RowCount, sumAmount, sumPaid);
            currentPanel = CurrentPanel.BILLS;
            labelCurrentView.Text = "חשבונות";
            showCurrentRowDetails(0);
            buttonSearchBills.Enabled = true;
            if (dataGridView1.RowCount == 0)
            {
                panelDetailsBill.Visible = false;
                panelDetailsProject.Visible = false;
                panelDetailsCustomer.Visible = false;
            }

        }
        private void buttonSearchCustomers_Click(object sender, EventArgs e)
        {
            string query;
            lastSearch.Panel = (short)CurrentPanel.CUSTOMERS;
            buttonSearchCustomers.Enabled = false;
            if (searchById)
            {
                searchById = false;
                lastSearch.IsSearch = false;
                lastSearch.IdQuery = searchIdQuery;
                query = "SELECT customers.idCustomer,customers.name,customers.city,customers.address,customers.phoneNumber,customers.rate FROM customers WHERE " + searchIdQuery;
            }
            else
            {
                searchById = false;
                lastSearch.IsSearch = true;
                query = "SELECT customers.idCustomer,customers.name,customers.city,customers.address,customers.phoneNumber,customers.rate FROM customers ";
                string concatenator = " WHERE ";
                if (checkBoxFiltersCustomersHaveDebt.Checked)
                {
                    query += "INNER JOIN projects ON projects.idCustomer = customers.idCustomer INNER JOIN bills_projects ON projects.idProjects=bills_projects.idProject INNER JOIN bills ON bills.id = bills_projects.idBill WHERE bills.isClosed = 0";
                    concatenator = " AND ";
                }
                if (textBoxFiltersCustomersFreeText.Text != String.Empty)
                {
                    query += concatenator;
                    query += "(customers.name LIKE '%" + textBoxFiltersCustomersFreeText.Text + "%' OR customers.notes LIKE '%" + textBoxFiltersCustomersFreeText.Text + "%')";
                    concatenator = " AND ";
                }
                if (comboBoxCustomerRate.SelectedIndex>0)
                {
                    query += concatenator;
                    query += "customers.rate = " + comboBoxCustomerRate.SelectedIndex;
                    concatenator = " AND ";
                }
                query += " GROUP BY customers.idCustomer";
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = mainInterface.Select(query).Tables[0];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "שם לקוח";
            dataGridView1.Columns[2].HeaderText = "עיר";
            dataGridView1.Columns[3].HeaderText = "כתובת";
            dataGridView1.Columns[4].HeaderText = "מספר טלפון";
            dataGridView1.Columns[5].HeaderText = "דירוג";
            dataGridView1.Columns[1].Width = dataGridView1.Width -
                                             dataGridView1.Columns[2].Width -
                                             dataGridView1.Columns[3].Width -
                                             dataGridView1.Columns[4].Width -
                                             dataGridView1.Columns[5].Width - 3 - 15;
            updateStatusStripSum(2, dataGridView1.RowCount,0,0);
            currentPanel = CurrentPanel.CUSTOMERS;
            labelCurrentView.Text = "לקוחות";
            showCurrentRowDetails(0);
            buttonSearchCustomers.Enabled = true;
            if (dataGridView1.RowCount == 0)
            {
                panelDetailsBill.Visible = false;
                panelDetailsProject.Visible = false;
                panelDetailsCustomer.Visible = false;
            }
        }

        private void checkBoxFiltersProjectsByAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersProjectsByAmount.Checked)
                panelFiltersProjectsAmount.Enabled = true;
            else
                panelFiltersProjectsAmount.Enabled = false;
        }
        private void checkBoxFiltersBillsByAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersBillsByAmount.Checked)
                panelFiltersBillsAmount.Enabled = true;
            else
                panelFiltersBillsAmount.Enabled = false;
        }
        private void checkBoxFiltersProjectsProjectType_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersProjectsProjectType.Checked)
                panelFiltersProjectsProjectType.Enabled = true;
            else
                panelFiltersProjectsProjectType.Enabled = false;
        }
        private void checkBoxFiltersProjectsStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersProjectsStatus.Checked)
                panelFiltersProjectsProjectStatus.Enabled = true;
            else
                panelFiltersProjectsProjectStatus.Enabled = false;
        }
        private void checkBoxFiltersProjectsBillNeeded_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersProjectsBillNeeded.Checked)
                checkBoxFiltersProjectsBillsToSubmit.Enabled = true;
            else
                checkBoxFiltersProjectsBillsToSubmit.Enabled = false;
        }
        private void checkBoxFiltersProjectsDates_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersProjectsDates.Checked)
            {
                dateTimePickerFiltersProjectsFrom.Enabled = true;
                dateTimePickerFiltersProjectsTo.Enabled = true;
                label63.Enabled = true;
                label61.Enabled = true;
            }
            else
            {
                dateTimePickerFiltersProjectsFrom.Enabled = false;
                dateTimePickerFiltersProjectsTo.Enabled = false;
                label61.Enabled = false;
                label63.Enabled = false;
            }
        }
        private void checkBoxFiltersBillsProjectType_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersBillsProjectType.Checked)
                panelFiltersBillsProjectType.Enabled = true;
            else
                panelFiltersBillsProjectType.Enabled = false;
        }
        private void checkBoxFiltersBillsBillStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersBillsBillStatus.Checked)
                panelFiltersBillsBillStatus.Enabled = true;
            else
                panelFiltersBillsBillStatus.Enabled = false;
        }
        private void checkBoxFiltersBillsPaymentStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersBillsPaymentStatus.Checked)
            {
                checkBoxFiltersBillsInvoiceExists.Enabled = true;
                checkBoxFiltersBillsReciptExists.Enabled = true;
            }
            else
            {
                checkBoxFiltersBillsInvoiceExists.Enabled = false;
                checkBoxFiltersBillsReciptExists.Enabled = false;
            }
        }
        private void checkBoxFiltersBillsDates_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersBillsDates.Checked)
            {
                dateTimePickerFiltersBillsFrom.Enabled = true;
                dateTimePickerFiltersBillsTo.Enabled = true;
                label67.Enabled = true;
                label69.Enabled = true;
            }
            else
            {
                dateTimePickerFiltersBillsFrom.Enabled = false;
                dateTimePickerFiltersBillsTo.Enabled = false;
                label67.Enabled = false;
                label69.Enabled = false;
            }
        }
        private void checkBoxFiltersBillsByCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersBillsByCustomer.Checked)
            {
                refreshFiltersBillCustomersComboBox();
                comboBoxFiltersBillsCustomers.Enabled = true;
            }
            else
                comboBoxFiltersBillsCustomers.Enabled = false;
        }
        private void checkBoxFiltersProjectsArchive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFiltersProjectsArchive.Checked)
                checkBoxFiltersProjectsArchiveExist.Enabled = true;
            else
                checkBoxFiltersProjectsArchiveExist.Enabled = false;
        }

        private void refreshFiltersBillCustomersComboBox()
        {
            DataTable dt = new DataTable();
            dt = mainInterface.Select("SELECT idCustomer,name FROM customers ORDER BY name ASC").Tables[0];
            comboBoxFiltersBillsCustomers.DisplayMember = "name";
            comboBoxFiltersBillsCustomers.ValueMember = "idCustomer";
            comboBoxFiltersBillsCustomers.DataSource = dt;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dataGridView1.SelectedRows[0].Selected = false;
                dataGridView1.Rows[e.RowIndex].Selected = true;
                showCurrentRowDetails(e.RowIndex);
                switch (currentPanel)
                {
                    case CurrentPanel.PROJECTS:
                        contextMenuStripProject.Show(Control.MousePosition.X - contextMenuStripProject.Width, Control.MousePosition.Y);
                        break;
                    case CurrentPanel.BILLS:
                        contextMenuStripBills.Show(Control.MousePosition.X - contextMenuStripProject.Width, Control.MousePosition.Y);
                        break;
                    case CurrentPanel.CUSTOMERS:
                        contextMenuStripCustomer.Show(Control.MousePosition.X - contextMenuStripProject.Width, Control.MousePosition.Y);
                        break;
                }
                showCurrentRowDetails(e.RowIndex);
            }
            //else if (e.Button == System.Windows.Forms.MouseButtons.
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            extendPanelDetailsHeight();
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            showCurrentRowDetails(e.RowIndex);
        }
        
        private void dataGridViewDetailsBillNotes_DoubleClick(object sender, EventArgs e)
        {
            formNewNote = new Form_NewNote(mainInterface, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), this);
            formNewNote.ShowDialog();
        }

        private void initProjectsFilters()
        {
            checkBoxFiltersProjectsDates.Checked = false;
            checkBoxFiltersProjectsStatus.Checked = true;
            checkBoxFiltersProjectsProjectType.Checked = false;
            checkBoxFiltersProjectsBillNeeded.Checked = false;
            checkBoxFiltersProjectsByAmount.Checked = false;
            checkBoxFiltersProjectsArchive.Checked = false;
            textBoxFiltersProjectsFreeText.Text = "";
        }
        private void initBillsFilters()
        {
            checkBoxFiltersBillsDates.Checked = false;
            checkBoxFiltersBillsBillStatus.Checked = true;
            checkBoxFiltersBillsProjectType.Checked = false;
            checkBoxFiltersBillsPaymentStatus.Checked = false;
            checkBoxFiltersBillsByAmount.Checked = false;
            textBoxFiltersBillsFreeText.Text = "";
        }

        private void   showCurrentRowDetails(int rowIndex)
        {
            string value = "";
            string temp = "";
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            switch (currentPanel)
            {
                case CurrentPanel.PROJECTS:
                    #region Project
                    labelProjectProjectName.ForeColor = Color.Black;
                    labelProjectProjectNumber.ForeColor = Color.Black;
                    currentProject = new MyProject();
                    try
                    {
                        panelDetailsProject.Visible = true;
                        panelDetailsBill.Visible = false;
                        panelDetailsCustomer.Visible = false;
                        if (dataGridView1.RowCount == 0)
                        {
                            panelDetailsProject.Visible = false;
                            break;
                        }
                        value = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                        temp = "SELECT projects.idProjects,projects.projectNumber,projects.handler1,projects.handler2,projects.startDate,projects.idCustomer,projects.projectName, " +
                                      "projects.amount,projects.amountInfo,projects.linking,projects.priceIndex,projects.priceIndexDate,projects.toSubmit,projects.approverName,projects.payerName," +
                                      "projects.contract,projects.contractNotes,projects.projectNotes,projects.isClosed,projects.archiveLocation,projects.contractNumber,projects.mileStones," +
                                      "projects.curency,projects.projectType,customers.name,customers.idCustomer,projects.curency,projects.approverPhone,projects.approverEmail,projects.approverFax,projects.payerPhone,projects.payerEmail," +
                                      "projects.payerFax,projects.payerAddress,projects.approverAddress,projects.toSubmitNotes,SUM(bills_projects.paid/100*projects.amount),ROUND(SUM(bills_projects.percent)) " +
                                      "FROM projects INNER JOIN customers ON projects.idCustomer = customers.idCustomer INNER JOIN bills_projects ON projects.idProjects=bills_projects.idProject WHERE bills_projects.idProject=" + value;
                        
                        dt = mainInterface.Select(temp).Tables[0];

                        textBoxDetailsProjectID.Text = dt.Rows[0][0].ToString();
                        currentProject.Id = dt.Rows[0][0].ToString();
                        selectedId = currentProject.Id;

                        textBoxDetailsProjectToSubmit.Text = "";
                        currentProject.ToSumbit = Convert.ToDouble(dt.Rows[0][12]);
                        labelProjectToSubmitPast.Text = "(" + String.Format("{0:0}", Convert.ToDouble(dt.Rows[0][12])) + " %)";
                        currentProject.ToSubmitNotes = dt.Rows[0][35].ToString();
                        textBoxDetailsProjectToSubmitNotes.Text = dt.Rows[0][35].ToString();

                        textBoxDetailsProjectName.Text = dt.Rows[0][6].ToString();
                        currentProject.Name = dt.Rows[0][6].ToString();

                        textBoxDetailsProjectNumber.Text = dt.Rows[0][1].ToString();
                        currentProject.Number = dt.Rows[0][1].ToString();

                        comboBoxDetailsProjectType.SelectedIndex = int.Parse(dt.Rows[0][23].ToString());
                        currentProject.Type = short.Parse(dt.Rows[0][23].ToString());

                        textBoxDetailsProjectCustomerName.Text = dt.Rows[0][24].ToString();

                        textBoxCustomerId.Text = dt.Rows[0][25].ToString();
                        currentProject.CustomerId = int.Parse(dt.Rows[0][25].ToString());

                        dateTimePickerDetailsProjectStart.Value = MyUtills.dateFromSQL(dt.Rows[0][4].ToString());
                        currentProject.StartDate = dateTimePickerDetailsProjectStart.Value;

                        textBoxDetailsProjectAmount.Text = String.Format("{0:0,0.00}", double.Parse(dt.Rows[0][7].ToString()));
                        currentProject.Amount = double.Parse(dt.Rows[0][7].ToString());

                        comboBoxProjectCurency.SelectedIndex = int.Parse(dt.Rows[0][26].ToString());
                        currentProject.Curency = comboBoxProjectCurency.SelectedIndex;

                        textBoxDetailsProjectApproverPhone.Text = dt.Rows[0][27].ToString();
                        currentProject.ApproverPhone = dt.Rows[0][27].ToString();

                        textBoxDetailsProjectApproverEmail.Text = dt.Rows[0][28].ToString();
                        currentProject.ApproverEmail = dt.Rows[0][28].ToString();

                        textBoxDetailsProjectApproverFax.Text = dt.Rows[0][29].ToString();
                        currentProject.ApproverFax = dt.Rows[0][29].ToString();

                        textBoxDetailsProjectPayerPhone.Text = dt.Rows[0][30].ToString();
                        currentProject.PayerPhone = dt.Rows[0][30].ToString();

                        textBoxDetailsProjectPayerEmail.Text = dt.Rows[0][31].ToString();
                        currentProject.PayerEmail = dt.Rows[0][31].ToString();

                        textBoxDetailsProjectPayerFax.Text = dt.Rows[0][32].ToString();
                        currentProject.PayerFax = dt.Rows[0][32].ToString();

                        textBoxDetailsProjectPayerAddress.Text = dt.Rows[0][33].ToString();
                        currentProject.PayerAddress = dt.Rows[0][33].ToString();

                        textBoxDetailsProjectApproverAddress.Text = dt.Rows[0][34].ToString();
                        currentProject.ApproverAddress = dt.Rows[0][34].ToString();

                        textBoxDetailsProjectAmountInfo.Text = dt.Rows[0][8].ToString();
                        currentProject.AmountNotes = dt.Rows[0][8].ToString();

                        textBoxDetailsProjectApproverName.Text = dt.Rows[0][13].ToString();
                        currentProject.ApproverName = dt.Rows[0][13].ToString();

                        textBoxDetailsProjectPayerName.Text = dt.Rows[0][14].ToString();
                        currentProject.PayerName = dt.Rows[0][14].ToString();

                        if (dt.Rows[0][9].ToString() == "")
                            dt.Rows[0][9] = "0";
                        comboBoxDetailsProjectlinking.SelectedIndex = int.Parse(dt.Rows[0][9].ToString());
                        currentProject.LinkingType = int.Parse(dt.Rows[0][9].ToString());

                        if (dt.Rows[0][10].ToString() == "")
                            dt.Rows[0][10] = "0";
                        textBoxDetailsProjectPriceIndex.Text = dt.Rows[0][10].ToString();
                        currentProject.PriceIndex = Double.Parse(dt.Rows[0][10].ToString());

                        if (dt.Rows[0][11].ToString() != "")
                        {
                            dateTimePickerDetailsProjectPriceIndexDate.Checked = true;
                            dateTimePickerDetailsProjectPriceIndexDate.Value = MyUtills.dateFromSQL(dt.Rows[0][11].ToString());
                            currentProject.PriceIndexDate = dateTimePickerDetailsProjectPriceIndexDate.Value;
                            currentProject.PriceIndexDateExists = 1;
                        }
                        else
                        {
                            dateTimePickerDetailsProjectPriceIndexDate.Checked = false;
                            currentProject.PriceIndexDateExists = 0;
                        }
                        
                        if (dt.Rows[0][15].ToString() == "True")
                        {
                            labelProjectProjectContractExists.Checked = true;
                            currentProject.ContractExists = 1;
                        }
                        else
                        {
                            labelProjectProjectContractExists.Checked = false;
                            currentProject.ContractExists = 0;
                        }

                        textBoxDetailsProjectContractNotes.Text = dt.Rows[0][16].ToString();
                        currentProject.ContractNotes = dt.Rows[0][16].ToString();

                        textBoxDetailsProjectNotes.Text = dt.Rows[0][17].ToString();
                        currentProject.ProjectNotes = dt.Rows[0][17].ToString();

                        textBoxDetailsProjectMileStones.Text = dt.Rows[0][21].ToString();
                        currentProject.MileStonesNotes = dt.Rows[0][21].ToString();

                        textBoxDetailsProjectArchiveLocation.Text = dt.Rows[0][19].ToString();
                        currentProject.ArchieveLocation = dt.Rows[0][19].ToString();

                        textBoxDetailsProjectContractNumber.Text = dt.Rows[0][20].ToString();
                        currentProject.ContractNumber = dt.Rows[0][20].ToString();

                        if (dt.Rows[0][18].ToString() == "True")
                        {
                            checkBoxDetailsProjectIsClosed.Checked = true;
                            currentProject.IsClosed = 1;
                        }
                        else
                        {
                            checkBoxDetailsProjectIsClosed.Checked = false;
                            currentProject.IsClosed = 2;
                        }
                        DataTable dt1 = mainInterface.Select("Select * FROM engineers").Tables[0];
                        comboBoxDetailsProjectHandler1.Items.Clear();
                        comboBoxDetailsProjectHandler2.Items.Clear();
                        comboBoxDetailsProjectHandler1.Items.Add("");
                        comboBoxDetailsProjectHandler2.Items.Add("");
                        foreach (DataRow dr in dt1.Rows)
                        {
                            comboBoxDetailsProjectHandler1.Items.Add(dr[0]);
                            comboBoxDetailsProjectHandler2.Items.Add(dr[0]);
                        }
                        comboBoxDetailsProjectHandler1.SelectedItem = dt.Rows[0][2].ToString();
                        if (comboBoxDetailsProjectHandler1.SelectedItem == null)
                            comboBoxDetailsProjectHandler1.SelectedIndex = 0;
                        currentProject.Handler1 = dt.Rows[0][2].ToString();

                        comboBoxDetailsProjectHandler2.SelectedItem = dt.Rows[0][3].ToString();
                        if (comboBoxDetailsProjectHandler2.SelectedItem == null)
                            comboBoxDetailsProjectHandler2.SelectedIndex = 0;
                        currentProject.Handler2 = dt.Rows[0][3].ToString();

                        double progress = 0;
                        Double.TryParse(dt.Rows[0][37].ToString(),out progress);
                        double paid = 0;
                        Double.TryParse(dt.Rows[0][36].ToString(),out paid);
                        
                        if (currentProject.Amount == 0)
                        {
                            textBoxDetailsProjectSubmited.Text = "0";
                            linkLabelFixProgress.Enabled = false;
                        }
                        else
                        {
                            textBoxDetailsProjectSubmited.Text = ((int)(progress+0.5)).ToString();
                            if (Double.Parse(textBoxDetailsProjectSubmited.Text) == 0)
                                linkLabelFixProgress.Enabled = false;
                            else
                                linkLabelFixProgress.Enabled = true;
                        }
                        textBoxDetailsProjectPayed.Text = String.Format("{0:0,0.00}",paid);
                        textBoxDetailsProjectToSubmitCalculated.Text = ((int)(currentProject.ToSumbit - Double.Parse(textBoxDetailsProjectSubmited.Text))).ToString();
                        if (Double.Parse(textBoxDetailsProjectToSubmitCalculated.Text) > 0)
                            textBoxDetailsProjectToSubmitCalculated.BackColor = Color.LightGreen;
                        else if (Double.Parse(textBoxDetailsProjectToSubmitCalculated.Text) < 0)
                            textBoxDetailsProjectToSubmitCalculated.BackColor = Color.Pink;
                        else
                            textBoxDetailsProjectToSubmitCalculated.BackColor = Color.White;
                    }
                    catch(Exception ex) 
                    {
                        File.AppendAllText(MyFiles.ExceptionFile,"ShowCurrrentProject: " + ex.Message + "\r\n");
                    }
                    break;
                    #endregion
                case CurrentPanel.BILLS:
                    #region Bill
                    labelBillNumber.ForeColor = Color.Black;
                    currentBill = new MyBill();
                    try
                    {
                        panelDetailsProject.Visible = false;
                        panelDetailsBill.Visible = true;
                        panelDetailsCustomer.Visible = false;
                        textBoxAmountNis.Visible = false;
                        if (dataGridView1.RowCount == 0)
                        {
                            panelDetailsBill.Visible = false;
                            break;
                        }
                        value = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();

                        temp = "SELECT bills.idBill,bills.idBill,bills.idBill,bills.amount,bills.paid,bills.payMethod," +
                                                    "bills.billDate,bills.approvalDate,bills.toPayDate,bills.payDate,bills.invoiceNumber," +
                                                    "bills.receiptNumber,bills.isClosed,projects.idProjects,projects.curency,bills.curencyRate,bills.handler,bills.callback,bills.currentIndex,bills.increase,bills.vat,bills.id,bills.billPart,bills.billNotes,projects.projectName,bills.vatDate " +
                                                    "FROM bills INNER JOIN bills_projects ON  bills.id=bills_projects.idBill INNER JOIN projects ON projects.idProjects=bills_projects.idProject WHERE bills.id='" + value + "' GROUP BY bills_projects.idBill";
                        
                        dt = mainInterface.Select(temp).Tables[0];
                        selectedId = dt.Rows[0][21].ToString();

                        //refreshDataGridViewBillsDetailsProjects(int.Parse(selectedId));


                        
                        string query = "SELECT projects.idProjects," +
                                           "projects.projectNumber," +
                                           "projects.amount," + 
                                           "projects.amount "+ 
                                           "FROM projects INNER JOIN bills_projects ON projects.idProjects=bills_projects.idProject WHERE bills_projects.idBill='" + selectedId + "'";

                        DataTable dtProjectNumber = mainInterface.Select(query).Tables[0];
                        textBoxDetailsBillProjectName.Text = "";
                        double billTotal = 0, billPaid = 0;
                        foreach (DataRow dr in dtProjectNumber.Rows)
                        {
                            textBoxDetailsBillProjectName.AppendText(dr[1].ToString() + "  (" + String.Format("{0:0,0.00}", projectPartAmount((int)dr[0], int.Parse(selectedId))) + ")\r\n");
                            dr[2] = String.Format("{0:0,0.00}", projectPartAmount((int)dr[0], int.Parse(selectedId)));
                            dr[3] = String.Format("{0:0,0.00}", projectPartPaid((int)dr[0], int.Parse(selectedId)));
                            billTotal += projectPartAmount((int)dr[0], int.Parse(selectedId));
                            billPaid += projectPartPaid((int)dr[0], int.Parse(selectedId));
                        }
                        dataGridViewBillsDetailsProjects.DataSource = dtProjectNumber;
                        
                        dataGridViewBillsDetailsProjects.Columns[0].Visible = false;
                        dataGridViewBillsDetailsProjects.Columns[2].Width = 70;
                        dataGridViewBillsDetailsProjects.Columns[3].Width = 70;
                        dataGridViewBillsDetailsProjects.Columns[1].Width = dataGridViewBillsDetailsProjects.Width - dataGridViewBillsDetailsProjects.Columns[2].Width - dataGridViewBillsDetailsProjects.Columns[3].Width;
                        dataGridViewBillsDetailsProjects.Columns[1].ReadOnly = true;

                        dataGridViewBillsDetailsProjects.Columns[1].HeaderText = "פרויקט";
                        dataGridViewBillsDetailsProjects.Columns[2].HeaderText = "סכום";
                        dataGridViewBillsDetailsProjects.Columns[3].HeaderText = "שולם";


                        currentBill.BillPart = Convert.ToInt16(dt.Rows[0][22]);
                        textBoxDetailsBillPart.Text = dt.Rows[0][22].ToString();

                        currentBill.BillNotes = dt.Rows[0][23].ToString();
                        textBoxDetailsBillNotes.Text = dt.Rows[0][23].ToString();

                        labelBillDetailsProjectName.Text = "פרויקט/ים" + "\r\n" + "\r\n" + dt.Rows[0][24].ToString();

                        dt.Rows[0][0] = dt.Rows[0][0].ToString().Substring(dt.Rows[0][0].ToString().IndexOf('/') + 1, dt.Rows[0][0].ToString().Length - dt.Rows[0][0].ToString().IndexOf('/') - 1);
                        textBoxDetailsBillNumber.Text = dt.Rows[0][0].ToString();
                        currentBill.Id = dt.Rows[0][0].ToString();


                        //textBoxDetailsBillProjectNumber.Text = dt.Rows[0][1].ToString();
                        //currentBill.ProjectNumber = dt.Rows[0][1].ToString();

                        //textBoxDetailsBillProjectName.Text = dt.Rows[0][2].ToString();

                        //textBoxDetailsBillAmount.Text = String.Format("{0:0,0.00}", double.Parse(dt.Rows[0][3].ToString()));//String.Format("{0:0,0.00}", billTotal);
                        //currentBill.Amount = double.Parse(dt.Rows[0][3].ToString());//double.Parse(billTotal.ToString());
                        textBoxDetailsBillAmount.Text = String.Format("{0:0,0.00}", billTotal);
                        currentBill.Amount = billTotal;


                        //textBoxDetailsBillPaid.Text = String.Format("{0:0,0.00}",double.Parse(dt.Rows[0][4].ToString()));
                        //currentBill.AmountPaid = double.Parse(dt.Rows[0][4].ToString());
                        textBoxDetailsBillPaid.Text = String.Format("{0:0,0.00}",billPaid);
                        currentBill.AmountPaid = billPaid;

                        comboBoxDetailsBillPayMethod.SelectedIndex = int.Parse(dt.Rows[0][5].ToString());
                        currentBill.PaymentMethod = short.Parse(dt.Rows[0][5].ToString());

                        dateTimePickerDetailsBillDate.Value = MyUtills.dateFromSQL(dt.Rows[0][6].ToString());
                        currentBill.BillDate = dateTimePickerDetailsBillDate.Value;

                        //if (dt.Rows[0][20].ToString() != "")
                        //    textBoxBillVat.Text = "% " + dt.Rows[0][20].ToString();
                        //else
                        //{
                        
                        //}

                        if (dt.Rows[0][7].ToString() != "")
                        {
                            dateTimePickerDetailsBillApproval.Checked = true;
                            dateTimePickerDetailsBillApproval.Value = MyUtills.dateFromSQL(dt.Rows[0][7].ToString());
                            currentBill.ApprovalDate = dateTimePickerDetailsBillApproval.Value;
                            currentBill.ApprovalDateExists = true;
                        }
                        else
                        {
                            dateTimePickerDetailsBillApproval.Value = DateTime.Now;
                            dateTimePickerDetailsBillApproval.Checked = false;
                            currentBill.ApprovalDateExists = false;
                        }

                        if (dt.Rows[0][8].ToString() != "")
                        {
                            dateTimePickerDetailsBillToPay.Checked = true;
                            dateTimePickerDetailsBillToPay.Value = MyUtills.dateFromSQL(dt.Rows[0][8].ToString());
                            currentBill.AmountToPayDate = dateTimePickerDetailsBillToPay.Value;
                            currentBill.AmountToPayDateExists = true;
                        }
                        else
                        {
                            dateTimePickerDetailsBillToPay.Value = DateTime.Now;
                            dateTimePickerDetailsBillToPay.Checked = false;
                            currentBill.AmountToPayDateExists = false;
                        }

                        if (dt.Rows[0][17].ToString() != "")
                        {
                            dateTimePickerDetailsBillCallback.Checked = true;
                            dateTimePickerDetailsBillCallback.Value = MyUtills.dateFromSQL(dt.Rows[0][17].ToString());
                            currentBill.Callback = dateTimePickerDetailsBillCallback.Value;
                            currentBill.CallbackExists = true;
                        }
                        else
                        {
                            dateTimePickerDetailsBillCallback.Value = DateTime.Now;
                            dateTimePickerDetailsBillCallback.Checked = false;
                            currentBill.CallbackExists = false;
                        }

                        if (dt.Rows[0][9].ToString() != "")
                        {
                            dateTimePickerDetailsBillPay.Checked = true;
                            dateTimePickerDetailsBillPay.Value = MyUtills.dateFromSQL(dt.Rows[0][9].ToString());
                            currentBill.PaymentDate = dateTimePickerDetailsBillPay.Value;
                            currentBill.PaymentDateExists = true;
                        }
                        else
                        {
                            dateTimePickerDetailsBillPay.Value = DateTime.Now;
                            dateTimePickerDetailsBillPay.Checked = false;
                            currentBill.PaymentDateExists = false;
                        }
                        dateTimePickerDetailsBillVat.Value = MyUtills.dateFromSQL(dt.Rows[0][25].ToString());
                        currentBill.VatDate = dateTimePickerDetailsBillVat.Value;

                        textBoxDetailsBillInvoiceNumber.Text = dt.Rows[0][10].ToString();
                        currentBill.InvoiceNumber = dt.Rows[0][10].ToString();

                        textBoxDetailsBillReceiptNumber.Text = dt.Rows[0][11].ToString();
                        currentBill.ReceiptNumber = dt.Rows[0][11].ToString();

                        textBoxDetailsBillCurencyRate.Text = dt.Rows[0][15].ToString();
                        currentBill.CurencyRate = double.Parse(dt.Rows[0][15].ToString());
                        //if (currentBill.CurencyRate != 1 && currentBill.CurencyRate != 0)
                        //    textBoxAmountNis.Visible = false;
                        //else 
                        //    textBoxAmountNis.Visible = false;
                        if (dt.Rows[0][12].ToString() == "True")
                        {
                            checkBoxDetailsBillIsClosed.Checked = true;
                            currentBill.IsClosed = 1;
                        }
                        else
                        {
                            checkBoxDetailsBillIsClosed.Checked = false;
                            currentBill.IsClosed = 0;
                        }
                        //DataTable dt2 = mainInterface.Select("SELECT vat FROM vat WHERE date<='" + MyUtills.dateToSQL(currentBill.BillDate) + "' ORDER BY date DESC").Tables[0];
                        //textBoxBillVat.Text = "% " + dt2.Rows[0][0].ToString();

                        DataTable dt2 = mainInterface.Select("SELECT vat,date FROM vat ORDER BY date DESC").Tables[0];
                        foreach (DataRow dro in dt2.Rows)
                        {
                            textBoxBillVat.Text = "%" + dro[0].ToString();
                            if (MyUtills.dateFromSQL(dro[1].ToString()) <= currentBill.VatDate)
                                break;
                        }
                        if (currentBill.IsClosed == 0 && currentBill.InvoiceNumber == string.Empty)
                            textBoxBillVat.Text = "%" + dt2.Rows[0][0].ToString();

                        //textBoxBillVat.Text = "% " + dt2.Rows[0][0].ToString();

                        if (currentBill.CurencyRate > 0)
                        {
                            textBoxAmountNis.Text = String.Format("{0:0,0.00}", Double.Parse(textBoxDetailsBillAmount.Text.Replace(",", "")) * currentBill.CurencyRate) + " שקל";
                            textBoxAmountNis.Visible = true;
                        }
                        else
                        {
                            textBoxAmountNis.Text = "יש להזין שער מטח";
                            textBoxAmountNis.Visible = true;
                        }
                        switch (dt.Rows[0][14].ToString())
                        {
                            case "0":
                                labelDetailsBillCurrency.Text = "שקל";
                                textBoxAmountNis.Visible = false;
                                break;
                            case "1":
                                labelDetailsBillCurrency.Text = "דולר";
                                break;
                            case "2":
                                labelDetailsBillCurrency.Text = "אירו";
                                break;
                        }
                        DataTable dt1 = mainInterface.Select("Select * FROM handlers").Tables[0];
                        comboBoxDetailsBillHandlers.Items.Clear();
                        comboBoxDetailsBillHandlers.Items.Add("");
                        foreach (DataRow dr in dt1.Rows)
                        {
                            comboBoxDetailsBillHandlers.Items.Add(dr[0]);
                        }
                        comboBoxDetailsBillHandlers.SelectedItem = dt.Rows[0][16].ToString();
                        currentBill.Handler = dt.Rows[0][16].ToString();

                        textBoxBillIndex.Text = dt.Rows[0][18].ToString();
                        currentBill.CurrentIndex = Double.Parse(dt.Rows[0][18].ToString());

                        textBoxBillIncrease.Text = dt.Rows[0][19].ToString();
                        currentBill.Increase = Double.Parse(dt.Rows[0][19].ToString());

                        string approverPayerTemp = "SELECT approverName,approverPhone,approverEmail,approverFax,payerName,payerPhone,payerEmail,payerFax FROM projects WHERE idProjects like '%" + dt.Rows[0][13].ToString() + "%' ";
                        DataTable aprroverPayerDt = mainInterface.Select(approverPayerTemp).Tables[0];
                        dataGridViewDetailsBillAprroverPayerDetails.Rows.Clear();
                        dataGridViewDetailsBillAprroverPayerDetails.Rows.Add(2);
                        dataGridViewDetailsBillAprroverPayerDetails.Rows[0].Cells[0].Value = aprroverPayerDt.Rows[0][0].ToString();
                        dataGridViewDetailsBillAprroverPayerDetails.Rows[0].Cells[1].Value = aprroverPayerDt.Rows[0][1].ToString();
                        dataGridViewDetailsBillAprroverPayerDetails.Rows[0].Cells[2].Value = aprroverPayerDt.Rows[0][2].ToString();
                        dataGridViewDetailsBillAprroverPayerDetails.Rows[0].Cells[3].Value = aprroverPayerDt.Rows[0][3].ToString();
                        dataGridViewDetailsBillAprroverPayerDetails.Rows[1].Cells[0].Value = aprroverPayerDt.Rows[0][4].ToString();
                        dataGridViewDetailsBillAprroverPayerDetails.Rows[1].Cells[1].Value = aprroverPayerDt.Rows[0][5].ToString();
                        dataGridViewDetailsBillAprroverPayerDetails.Rows[1].Cells[2].Value = aprroverPayerDt.Rows[0][6].ToString();
                        dataGridViewDetailsBillAprroverPayerDetails.Rows[1].Cells[3].Value = aprroverPayerDt.Rows[0][7].ToString();
                        dataGridViewDetailsBillAprroverPayerDetails.Columns[0].Width = 150;
                        dataGridViewDetailsBillAprroverPayerDetails.Columns[1].Width = 150;
                        dataGridViewDetailsBillAprroverPayerDetails.Columns[2].Width = 222;
                        dataGridViewDetailsBillAprroverPayerDetails.Columns[3].Width = 150;
                        dataGridViewDetailsBillAprroverPayerDetails.SelectedCells[0].Selected = false;
                        refreshBillNotes(value);
                        double amount = 0;
                        if (!textBoxAmountNis.Text.Contains("יש"))
                            amount = Double.Parse(textBoxAmountNis.Text.Replace(",", "").Replace(" שקל", ""));
                        else
                        {
                            Double.TryParse(textBoxDetailsBillAmount.Text.Replace(",", ""), out amount);
                        }

                        amount += Double.Parse(textBoxBillIncrease.Text.Replace(",",""));
                        textBoxBillAmountWithIncrease.Text = String.Format("{0:0,0.00}", amount);
                        textBoxBillAmountWithVAT.Text = String.Format("{0:0,0.00}", amount * (Double.Parse(textBoxBillVat.Text.Substring(1, textBoxBillVat.Text.Length - 1)) / 100 + 1));
                        string vat = "(" +String.Format("{0:0,0.00}", double.Parse(textBoxBillAmountWithVAT.Text) - double.Parse(textBoxBillAmountWithIncrease.Text)) + ") " + textBoxBillVat.Text;
                        textBoxBillVat.Text = vat;
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText(MyFiles.ExceptionFile, "ShowCurrrentBill: " + ex.Message + "\r\n");
                    }
                    break;
                    #endregion
                case CurrentPanel.CUSTOMERS:
                    #region Customer
                    labelName.ForeColor = Color.Black;
                    currentCustomer = new MyCustomer();
                    try
                    {
                        panelDetailsProject.Visible = false;
                        panelDetailsBill.Visible = false;
                        panelDetailsCustomer.Visible = true;
                        if (dataGridView1.RowCount == 0)
                        {
                            panelDetailsCustomer.Visible = false;
                            break;
                        }
                        value = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                        dt = mainInterface.Select("SELECT * FROM customers WHERE idCustomer = " + value).Tables[0];

                        currentCustomer.Id = int.Parse(dt.Rows[0][0].ToString());
                        selectedId = currentCustomer.Id.ToString();

                        textBoxDetailsCustomerName.Text = dt.Rows[0][1].ToString();
                        currentCustomer.Name = dt.Rows[0][1].ToString();

                        textBoxDetailsCustomerContactPerson.Text = dt.Rows[0][9].ToString();
                        currentCustomer.ContactPersonDetails = dt.Rows[0][9].ToString();

                        textBoxDetailsCustomerPhone.Text = dt.Rows[0][6].ToString();
                        currentCustomer.PhoneNumber = dt.Rows[0][6].ToString();

                        textBoxDetailsCustomerFax.Text = dt.Rows[0][7].ToString();
                        currentCustomer.Fax = dt.Rows[0][7].ToString();

                        textBoxDetailsCustomerEmail.Text = dt.Rows[0][8].ToString();
                        currentCustomer.EMail = dt.Rows[0][8].ToString();

                        textBoxDetailsCustomerCity.Text = dt.Rows[0][2].ToString();
                        currentCustomer.City = dt.Rows[0][2].ToString();

                        textBoxDetailsCustomerAddress.Text = dt.Rows[0][3].ToString();
                        currentCustomer.Address = dt.Rows[0][3].ToString();

                        textBoxDetailsCustomerZip.Text = dt.Rows[0][4].ToString();
                        currentCustomer.ZipCode = dt.Rows[0][4].ToString();

                        textBoxDetailsCustomerPO.Text = dt.Rows[0][5].ToString();
                        currentCustomer.PoBox = dt.Rows[0][5].ToString();

                        textBoxDetailsCustomerNotes.Text = dt.Rows[0][11].ToString();
                        currentCustomer.Notes = dt.Rows[0][11].ToString();

                        comboBoxDetailsCustomerRate.SelectedIndex = int.Parse(dt.Rows[0][10].ToString());
                        currentCustomer.Rate = short.Parse(dt.Rows[0][10].ToString());
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText(MyFiles.ExceptionFile, "ShowCurrrentCustomer: " + ex.Message + "\r\n");
                    }
                    break;
                    #endregion
            }
        }
        private double projectPartAmount(int idProject, int idBill)
        {
            DataTable dt = mainInterface.Select("SELECT bills_projects.percent,projects.amount FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idProject='" + idProject.ToString() + "' AND bills_projects.idBill='"+ idBill.ToString() + "'").Tables[0];
            return (double)dt.Rows[0][0] / 100 * (double)dt.Rows[0][1];
        }
        private double projectPartPaid(int idProject, int idBill)
        {
            DataTable dt = mainInterface.Select("SELECT bills_projects.paid,projects.amount FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idProject='" + idProject.ToString() + "' AND bills_projects.idBill='"+ idBill.ToString() + "'").Tables[0];
            return (double)dt.Rows[0][0] / 100 * (double)dt.Rows[0][1];
        }
        private void   extendPanelDetailsHeight()
        {
            switch (currentPanel)
            {
                case CurrentPanel.PROJECTS:
                    splitContainer.SplitterDistance = splitContainer.Height - 400;
                    break;
                case CurrentPanel.BILLS:
                    splitContainer.SplitterDistance = splitContainer.Height - 400;
                    break;
                case CurrentPanel.CUSTOMERS:
                    splitContainer.SplitterDistance = splitContainer.Height - 320;
                    break;
            }
        }

        private void pictureBoxUpArrow_Click(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = splitContainer.MinimumSize.Height;
        }
        private void pictureBoxDownArrow_Click(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = splitContainer.Height - splitContainer.MinimumSize.Height - 50;
        }
        private void labelSaveChanges_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
        private void labelSaveChanges_MouseHover(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
        private void pictureBoxUpArrow_MouseHover(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
        private void pictureBoxDownArrow_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }

        private void toolStripMenuItemEngineers_Click(object sender, EventArgs e)
        {
            formEngineers = new Form_Engineers(mainInterface);
            formEngineers.ShowDialog();
        }

        private void labelShowAllBillsProject_Click(object sender, EventArgs e)
        {
            tabControlFilters.SelectedTab = tabPageBill;
            searchById = true;
            searchIdQuery = "bills_projects.idProject="+dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            buttonSearchBills.PerformClick();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                showCurrentRowDetails(dataGridView1.Rows.Count - 1);
            }
        }
        private void labelCreateNewBillProject_Click(object sender, EventArgs e)
        {
            List<int> projectId = new List<int>();
            projectId.Add(Convert.ToInt16(textBoxDetailsProjectID.Text));
            formNewBill = new Form_NewBill(this, mainInterface, projectId);
            formNewBill.ShowDialog();
        }

        private void labelCreateNewProjectCustomer_Click(object sender, EventArgs e)
        {
            formNewProject = new Form_NewProject(this, mainInterface, dataGridView1.SelectedRows[0].Cells[1].Value.ToString(),
                                                                dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            formNewProject.ShowDialog();
        }
        private void labelShowAllProjectsCustomer_Click(object sender, EventArgs e)
        {
            tabControlFilters.SelectedTab = tabPageProject;
            searchById = true;
            searchIdQuery = "projects.idCustomer="+dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            buttonSearchProjects.PerformClick();
        }

        private void pictureBoxUpDownArrowCustomer_Click(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = (splitContainer.Panel1.Height + splitContainer.Panel2.Height) / 2;
        }
        private void pictureBoxUpDownArrowProject_Click(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = (splitContainer.Panel1.Height + splitContainer.Panel2.Height) / 2;
        }
        private void pictureBoxUpDownArrowBill_Click(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = (splitContainer.Panel1.Height + splitContainer.Panel2.Height) / 2;
        }

        private void updateStatusStripSum(short bpc, int sumRows, double sumAmount, double sumPaid) 
        { // bills-projects-customers
            if (bpc == 1) // Projects
            {
                int percentPaid, percentLeft;
                if (sumAmount == 0)
                {
                    percentPaid = 0;
                    percentLeft = 0;
                }
                else
                {
                    percentPaid = (int)(sumPaid / sumAmount * 100 + 0.5);
                    percentLeft = (int)((sumAmount - sumPaid) / sumAmount * 100 + 0.5);
                }
                toolStripStatusSum.Text = " " + sumRows.ToString() + " |"
                                              + "    סה\"כ " + String.Format("{0:0,0.00}", sumAmount) + " |"
                                              + " נפרע " + String.Format("{0:0,0.00}", sumPaid) + " (" + percentPaid.ToString() + "%) | "
                                              + " יתרה " + String.Format("{0:0,0.00}", (sumAmount - sumPaid)) + " (" + percentLeft.ToString() + "%)";

            }
            else if (bpc == 0)// Bills
            {
                int percentPaid, percentLeft;
                if (sumAmount == 0)
                {
                    percentPaid = 0;
                    percentLeft = 0;
                }
                else
                {
                    percentPaid = (int)(sumPaid / sumAmount * 100 + 0.5);
                    percentLeft = (int)((sumAmount - sumPaid) / sumAmount * 100 + 0.5);
                }
                toolStripStatusSum.Text = " " + sumRows.ToString() + " |"
                                              + "    סה\"כ " + String.Format("{0:0,0.00}",sumAmount) + " |"
                                              + " שולם " + String.Format("{0:0,0.00}",sumPaid) + " (" + percentPaid.ToString() + "%) | "
                                              + " יתרה " + String.Format("{0:0,0.00}",(sumAmount - sumPaid)) + " (" + percentLeft.ToString() + "%)";
            }
            else // Customers
                toolStripStatusSum.Text = " " + sumRows.ToString();
                                                
        }

        private void saveChangesBill()
        {
            labelBillNumber.ForeColor = Color.Black;
            labelPaid.ForeColor = Color.Black;
            labelAmount.ForeColor = Color.Black;
            labelBillDetailsPayMethod.ForeColor = Color.Black;
            labelDetailsBillCurencyRate.ForeColor = Color.Black;
            labelBillIncrease.ForeColor = Color.Black;
            labelBillIndex.ForeColor = Color.Black;
            labelBillDetailsBillPart.ForeColor = Color.Black;

            bool flag = false;
            bool returnFlag = false;
            bool updateApprovalDate = false;
            bool updateToPayDate = false;
            bool updatePayDate = false;
            bool updateCallback = false;
            double currentVat = Convert.ToDouble(mainInterface.Select("SELECT vat FROM vat ORDER BY date DESC").Tables[0].Rows[0][0]);
            if (textBoxDetailsBillNumber.Text == String.Empty)
            {
                labelBillNumber.ForeColor = Color.Red;
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
                double.Parse(textBoxDetailsBillCurencyRate.Text);
            }
            catch
            {
                labelDetailsBillCurencyRate.ForeColor = Color.Red;
                returnFlag = true;
            }
            try
            {
                int.Parse(textBoxDetailsBillPart.Text);
            }
            catch
            {

                labelBillDetailsBillPart.ForeColor = Color.Red;
                returnFlag = true;
            }
            if (returnFlag)
            {
                pictureBoxBillsX.Visible = true;
                pictureBoxBillsV.Visible = false;
                Application.DoEvents();
                Thread.Sleep(2000);
                pictureBoxBillsX.Visible = false;
                Application.DoEvents();
                return;
            }
            string query = "UPDATE bills SET ";
            string concatenator = " ";
            if (currentBill.Id != textBoxDetailsBillNumber.Text)
            {
                query += concatenator;
                query += "idBill='" + textBoxDetailsBillNumber.Text + "'";
                concatenator = ",";
                flag = true;
            }
            /*if (currentBill.ProjectNumber != textBoxDetailsBillProjectNumber.Text)
            {
                query += concatenator;
                query += "idProject='" + textBoxDetailsBillProjectNumber.Text + "'";
                concatenator = ",";
                flag = true;
            }*/
            if (currentBill.Amount != double.Parse(textBoxDetailsBillAmount.Text))
            {
                query += concatenator;
                query += "amount='" + textBoxDetailsBillAmount.Text.Replace(",", "") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentBill.BillPart != int.Parse(textBoxDetailsBillPart.Text))
            {
                query += concatenator;
                query += "billPart='" + textBoxDetailsBillPart.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentBill.BillNotes != textBoxDetailsBillNotes.Text)
            {
                query += concatenator;
                query += "billNotes='" + textBoxDetailsBillNotes.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (double.Parse(textBoxDetailsBillCurencyRate.Text) != currentBill.CurencyRate)
            {
                query += concatenator;
                query += "curencyRate='" + textBoxDetailsBillCurencyRate.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if ((double.Parse(textBoxDetailsBillAmount.Text) <= double.Parse(textBoxDetailsBillPaid.Text)) && (double.Parse(textBoxDetailsBillAmount.Text) != 0) && (textBoxDetailsBillReceiptNumber.Text != String.Empty))
                checkBoxDetailsBillIsClosed.Checked = true;
            if (currentBill.IsClosed == 1)
            {
                if (!checkBoxDetailsBillIsClosed.Checked)
                {
                    query += concatenator;
                    query += "isClosed='0'";
                    concatenator = ",";
                    query += concatenator;
                    query += "vat=NULL";
                    concatenator = ",";
                    flag = true;
                }
            }
            else
            {
                if (checkBoxDetailsBillIsClosed.Checked)
                {
                    query += concatenator;
                    query += "isClosed='1'";
                    concatenator = ",";
                    query += concatenator;
                    query += "vat='" + currentVat.ToString() + "'";
                    concatenator = ",";
                    flag = true;
                    updateCallback = true;
                    //updateVat = true;
                }
            }
            if (currentBill.BillDate != dateTimePickerDetailsBillDate.Value)
            {
                query += concatenator;
                query += "billDate='" + MyUtills.dateToSQL(dateTimePickerDetailsBillDate.Value) + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentBill.VatDate != dateTimePickerDetailsBillVat.Value)
            {
                query += concatenator;
                query += "vatDate='" + MyUtills.dateToSQL(dateTimePickerDetailsBillVat.Value) + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentBill.ApprovalDateExists)
            {
                if (dateTimePickerDetailsBillApproval.Checked)
                {
                    if (currentBill.ApprovalDate != dateTimePickerDetailsBillApproval.Value)
                        updateApprovalDate = true;
                }
                else
                    updateApprovalDate = true;
            }
            else
            {
                if (dateTimePickerDetailsBillApproval.Checked)
                    updateApprovalDate = true;
            }
            if (currentBill.AmountToPayDateExists)
            {
                if (dateTimePickerDetailsBillToPay.Checked)
                {
                    if (currentBill.AmountToPayDate != dateTimePickerDetailsBillToPay.Value)
                        updateToPayDate = true;
                }
                else
                    updateToPayDate = true;
            }
            else
            {
                if (dateTimePickerDetailsBillToPay.Checked)
                    updateToPayDate = true;
            }
            if (currentBill.CallbackExists)
            {
                if (dateTimePickerDetailsBillCallback.Checked)
                {
                    if (currentBill.Callback != dateTimePickerDetailsBillCallback.Value)
                        updateCallback = true;
                }
                else
                    updateCallback = true;
            }
            else
            {
                if (dateTimePickerDetailsBillCallback.Checked)
                    updateCallback = true;
            }
            if (currentBill.AmountPaid != double.Parse(textBoxDetailsBillPaid.Text))
            {
                query += concatenator;
                query += "paid='" + textBoxDetailsBillPaid.Text.Replace(",", "") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentBill.PaymentMethod != short.Parse(comboBoxDetailsBillPayMethod.SelectedIndex.ToString()))
            {
                query += concatenator;
                query += "payMethod='" + comboBoxDetailsBillPayMethod.SelectedIndex.ToString() + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentBill.PaymentDateExists)
            {
                if (dateTimePickerDetailsBillPay.Checked)
                {
                    if (currentBill.AmountToPayDate != dateTimePickerDetailsBillPay.Value)
                        updatePayDate = true;
                }
                else
                    updatePayDate = true;
            }
            else
            {
                if (dateTimePickerDetailsBillPay.Checked)
                    updatePayDate = true;
            }
            if (currentBill.InvoiceNumber != textBoxDetailsBillInvoiceNumber.Text)
            {
                query += concatenator;
                query += "invoiceNumber='" + textBoxDetailsBillInvoiceNumber.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentBill.ReceiptNumber != textBoxDetailsBillReceiptNumber.Text)
            {
                query += concatenator;
                query += "receiptNumber='" + textBoxDetailsBillReceiptNumber.Text + "'";
                concatenator = ",";
                flag = true;
            }
            try
            {
                if (currentBill.Handler != comboBoxDetailsBillHandlers.SelectedItem.ToString())
                {
                    query += concatenator;
                    query += "handler='" + comboBoxDetailsBillHandlers.SelectedItem + "'";
                    concatenator = ",";
                    flag = true;
                }
            }
            catch { }
            if (currentBill.Increase != double.Parse(textBoxBillIncrease.Text))
            {
                query += concatenator;
                query += "increase='" + textBoxBillIncrease.Text.Replace(",", "") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentBill.CurrentIndex != double.Parse(textBoxBillIndex.Text))
            {
                query += concatenator;
                query += "currentIndex='" + textBoxBillIndex.Text.Replace(",", "") + "'";
                concatenator = ",";
                flag = true;
            }


            query += " WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";

            if (flag)
            {
                mainInterface.Update(query);
                //mainInterface.Update("UPDATE bills ");
                pictureBoxBillsV.Visible = true;
                pictureBoxBillsX.Visible = false;
                Application.DoEvents();
                Thread.Sleep(1500);
                pictureBoxBillsV.Visible = false;
                Application.DoEvents();
            }
            if (updateApprovalDate || updateToPayDate || updatePayDate || updateCallback)
            {
                if (updateApprovalDate)
                    if (dateTimePickerDetailsBillApproval.Checked)
                        mainInterface.Update("UPDATE bills SET approvalDate = '" + MyUtills.dateToSQL(dateTimePickerDetailsBillApproval.Value) +
                                              "' WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    else
                        mainInterface.Update("UPDATE bills SET approvalDate = NULL WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                if (updateToPayDate)
                    if (dateTimePickerDetailsBillToPay.Checked)
                        mainInterface.Update("UPDATE bills SET toPayDate = '" + MyUtills.dateToSQL(dateTimePickerDetailsBillToPay.Value) +
                                              "' WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    else
                        mainInterface.Update("UPDATE bills SET toPayDate = NULL WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                if (updatePayDate)
                    if (dateTimePickerDetailsBillPay.Checked)
                        mainInterface.Update("UPDATE bills SET payDate = '" + MyUtills.dateToSQL(dateTimePickerDetailsBillPay.Value) +
                                              "' WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    else
                        mainInterface.Update("UPDATE bills SET payDate = NULL WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                if (updateCallback)
                    if (checkBoxDetailsBillIsClosed.Checked)
                    {
                        mainInterface.Update("UPDATE bills SET callback = NULL WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    }
                    else if (dateTimePickerDetailsBillCallback.Checked)
                        mainInterface.Update("UPDATE bills SET callback = '" + MyUtills.dateToSQL(dateTimePickerDetailsBillCallback.Value) +
                                              "' WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                    else
                        mainInterface.Update("UPDATE bills SET callback = NULL WHERE id='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                if (!flag)
                {
                    pictureBoxBillsV.Visible = true;
                    pictureBoxBillsX.Visible = false;
                    Application.DoEvents();
                    Thread.Sleep(1500);
                    pictureBoxBillsV.Visible = false;
                    Application.DoEvents();
                }
            }
        }

        private void labelSaveChangesCustomer_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (textBoxDetailsCustomerName.Text == String.Empty)
            {
                labelName.ForeColor = Color.Red;
                pictureBoxCustomerX.Visible = true;
                pictureBoxCustomerV.Visible = false;
                Application.DoEvents();
                Thread.Sleep(1500);
                pictureBoxCustomerX.Visible = false;
                Application.DoEvents();
                return;
            }
            labelName.ForeColor = Color.Black;
            string query = "UPDATE customers SET ";
            string concatenator = " ";
            if (currentCustomer.Name != textBoxDetailsCustomerName.Text)
            {
                query += concatenator;
                query += "name='" + textBoxDetailsCustomerName.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.PhoneNumber != textBoxDetailsCustomerPhone.Text)
            {
                query += concatenator;
                query += "phoneNumber='" + textBoxDetailsCustomerPhone.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.Address != textBoxDetailsCustomerAddress.Text)
            {
                query += concatenator;
                query += "address='" + textBoxDetailsCustomerAddress.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.City != textBoxDetailsCustomerCity.Text)
            {
                query += concatenator;
                query += "city='" + textBoxDetailsCustomerCity.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.ZipCode != textBoxDetailsCustomerZip.Text)
            {
                query += concatenator;
                query += "zipCode='" + textBoxDetailsCustomerZip.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.PoBox != textBoxDetailsCustomerPO.Text)
            {
                query += concatenator;
                query += "poBox='" + textBoxDetailsCustomerPO.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.Fax != textBoxDetailsCustomerFax.Text)
            {
                query += concatenator;
                query += "fax='" + textBoxDetailsCustomerFax.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.EMail != textBoxDetailsCustomerEmail.Text)
            {
                query += concatenator;
                query += "eMail='" + textBoxDetailsCustomerEmail.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.ContactPersonDetails != textBoxDetailsCustomerContactPerson.Text)
            {
                query += concatenator;
                query += "contactPerson='" + textBoxDetailsCustomerContactPerson.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.Notes != textBoxDetailsCustomerNotes.Text)
            {
                query += concatenator;
                query += "notes='" + textBoxDetailsCustomerNotes.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentCustomer.Rate != comboBoxDetailsCustomerRate.SelectedIndex)
            {
                query += concatenator;
                query += "rate='" + comboBoxDetailsCustomerRate.SelectedIndex.ToString() + "'";
                concatenator = ",";
                flag = true;
            }
            query += " WHERE idCustomer='" + currentCustomer.Id + "'";

            if (flag)
            {
                mainInterface.Update(query);
                pictureBoxCustomerV.Visible = true;
                pictureBoxCustomerX.Visible = false;
                Application.DoEvents();
                Thread.Sleep(1500);
                pictureBoxCustomerV.Visible = false;
                Application.DoEvents();
            }
            //showCurrentRowDetails(dataGridView1.SelectedRows[0].Index);
            refreshView();
        }
        private void labelSaveChangesBill_Click(object sender, EventArgs e)
        {
            saveChangesBill();
            refreshView();
        }
        private void labelSaveChangesProject_Click(object sender, EventArgs e)
        {
            labelProjectProjectName.ForeColor = Color.Black;
            labelProjectProjectNumber.ForeColor = Color.Black;
            labelProjectProjectIndex.ForeColor = Color.Black;
            labelProjectProjectAmount.ForeColor = Color.Black;
            labelToSubmit.ForeColor = Color.Black;

            bool flag = false;
            bool updatePriceIndexDate = false;
            bool returnFlag = false;
            if (textBoxDetailsProjectName.Text == String.Empty)
            {
                labelProjectProjectName.ForeColor = Color.Red;
                returnFlag = true;
            }
            if (textBoxDetailsProjectNumber.Text == String.Empty)
            {
                labelProjectProjectNumber.ForeColor = Color.Red;
                returnFlag = true;
            }
            if (textBoxDetailsProjectToSubmit.Text != String.Empty)
            {
                try
                {
                    double.Parse(textBoxDetailsProjectToSubmit.Text);
                }
                catch
                {
                    labelToSubmit.ForeColor = Color.Red;
                    returnFlag = true;
                }
            }
            try
            {
                double.Parse(textBoxDetailsProjectAmount.Text);
            }
            catch
            {
                labelProjectProjectAmount.ForeColor = Color.Red;
                returnFlag = true;
            }
            try
            {
                Double.Parse(textBoxDetailsProjectPriceIndex.Text);
            }
            catch
            {
                labelProjectProjectIndex.ForeColor = Color.Red;
                returnFlag = true;
            }
            if (returnFlag)
            {
                pictureBoxProjectX.Visible = true;
                pictureBoxProjectV.Visible = false;
                Application.DoEvents();
                Thread.Sleep(1500);
                pictureBoxProjectX.Visible = false;
                Application.DoEvents();
                return;
            }

            string query = "UPDATE projects SET ";
            string concatenator = " ";
            bool amountChanged = false;
            if (textBoxDetailsProjectToSubmit.Text != String.Empty)
            {
                if (currentProject.ToSumbit != double.Parse(textBoxDetailsProjectToSubmit.Text))
                {
                    query += concatenator;
                    query += "toSubmit='" + textBoxDetailsProjectToSubmit.Text + "'";
                    concatenator = ",";
                    flag = true;
                }
            }
            if (textBoxDetailsProjectToSubmitNotes.Text != String.Empty)
            {
                if (currentProject.ToSubmitNotes != textBoxDetailsProjectToSubmitNotes.Text)
                {
                    query += concatenator;
                    query += "toSubmitNotes='" + textBoxDetailsProjectToSubmitNotes.Text.Replace("'", "\\'") + "'";
                    concatenator = ",";
                    flag = true;
                }
            }
            if (currentProject.Name != textBoxDetailsProjectName.Text)
            {
                query += concatenator;
                query += "projectName='" + textBoxDetailsProjectName.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.Number != textBoxDetailsProjectNumber.Text)
            {
                query += concatenator;
                query += "projectNumber='" + textBoxDetailsProjectNumber.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.CustomerId != int.Parse(textBoxCustomerId.Text))
            {
                query += concatenator;
                query += "idCustomer='" + textBoxCustomerId.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.StartDate != dateTimePickerDetailsProjectStart.Value)
            {
                query += concatenator;
                query += "startDate='" + MyUtills.dateToSQL(dateTimePickerDetailsProjectStart.Value) + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.Amount != double.Parse(textBoxDetailsProjectAmount.Text))
            {
                query += concatenator;
                query += "amount='" + textBoxDetailsProjectAmount.Text.Replace(",","") + "'";
                concatenator = ",";
                flag = true;
                amountChanged = true;
            }
            if (currentProject.Curency != comboBoxProjectCurency.SelectedIndex)
            {
                query += concatenator;
                query += "curency='" + comboBoxProjectCurency.SelectedIndex.ToString() + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.AmountNotes != textBoxDetailsProjectAmountInfo.Text)
            {
                query += concatenator;
                query += "amountInfo='" + textBoxDetailsProjectAmountInfo.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.Handler1 != comboBoxDetailsProjectHandler1.SelectedItem.ToString())
            {
                query += concatenator;
                query += "handler1='" + comboBoxDetailsProjectHandler1.SelectedItem.ToString() + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.Handler2 != comboBoxDetailsProjectHandler2.SelectedItem.ToString())
            {
                query += concatenator;
                query += "handler2='" + comboBoxDetailsProjectHandler2.SelectedItem.ToString() + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.PayerName != textBoxDetailsProjectPayerName.Text)
            {
                query += concatenator;
                query += "payerName='" + textBoxDetailsProjectPayerName.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.PayerPhone != textBoxDetailsProjectPayerPhone.Text)
            {
                query += concatenator;
                query += "payerPhone='" + textBoxDetailsProjectPayerPhone.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.PayerEmail != textBoxDetailsProjectPayerEmail.Text)
            {
                query += concatenator;
                query += "payerEmail='" + textBoxDetailsProjectPayerEmail.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.PayerFax != textBoxDetailsProjectPayerFax.Text)
            {
                query += concatenator;
                query += "payerFax='" + textBoxDetailsProjectPayerFax.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.ApproverName != textBoxDetailsProjectApproverName.Text)
            {
                query += concatenator;
                query += "approverName='" + textBoxDetailsProjectApproverName.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.ApproverPhone != textBoxDetailsProjectApproverPhone.Text)
            {
                query += concatenator;
                query += "approverPhone='" + textBoxDetailsProjectApproverPhone.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.ApproverEmail != textBoxDetailsProjectApproverEmail.Text)
            {
                query += concatenator;
                query += "approverEmail='" + textBoxDetailsProjectApproverEmail.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.ApproverFax != textBoxDetailsProjectApproverFax.Text)
            {
                query += concatenator;
                query += "approverFax='" + textBoxDetailsProjectApproverFax.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.ApproverAddress != textBoxDetailsProjectApproverAddress.Text)
            {
                query += concatenator;
                query += "approverAddress='" + textBoxDetailsProjectApproverAddress.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.PayerAddress != textBoxDetailsProjectPayerAddress.Text)
            {
                query += concatenator;
                query += "payerAddress='" + textBoxDetailsProjectPayerAddress.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.LinkingType != comboBoxDetailsProjectlinking.SelectedIndex)
            {
                query += concatenator;
                query += "linking='" + comboBoxDetailsProjectlinking.SelectedIndex.ToString() + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.PriceIndex != Double.Parse(textBoxDetailsProjectPriceIndex.Text))
            {
                query += concatenator;
                query += "priceIndex='" + textBoxDetailsProjectPriceIndex.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.PriceIndexDateExists == 1)
            {
                if (dateTimePickerDetailsProjectPriceIndexDate.Checked)
                {
                    if (currentProject.PriceIndexDate != dateTimePickerDetailsProjectPriceIndexDate.Value)
                        updatePriceIndexDate = true;
                }
                else
                    updatePriceIndexDate = true;
            }
            else
            {
                if (dateTimePickerDetailsProjectPriceIndexDate.Checked)
                    updatePriceIndexDate = true;
            }
            if (currentProject.ContractExists == 1)
            {
                if (!labelProjectProjectContractExists.Checked)
                {
                    query += concatenator;
                    query += "contract='0'";
                    concatenator = ",";
                    flag = true;
                }
            }
            else
            {
                if (labelProjectProjectContractExists.Checked)
                {
                    query += concatenator;
                    query += "contract='1'";
                    concatenator = ",";
                    flag = true;
                }
            }
            if (currentProject.ContractNotes != textBoxDetailsProjectContractNotes.Text)
            {
                query += concatenator;
                query += "contractNotes='" + textBoxDetailsProjectContractNotes.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.ProjectNotes != textBoxDetailsProjectNotes.Text)
            {
                query += concatenator;
                query += "projectNotes='" + textBoxDetailsProjectNotes.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.MileStonesNotes != textBoxDetailsProjectMileStones.Text)
            {
                query += concatenator;
                query += "mileStones='" + textBoxDetailsProjectMileStones.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.ArchieveLocation != textBoxDetailsProjectArchiveLocation.Text)
            {
                query += concatenator;
                query += "archiveLocation='" + textBoxDetailsProjectArchiveLocation.Text.Replace("'", "\\'") + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.ContractNumber != textBoxDetailsProjectContractNumber.Text)
            {
                query += concatenator;
                query += "contractNumber='" + textBoxDetailsProjectContractNumber.Text + "'";
                concatenator = ",";
                flag = true;
            }
            if (currentProject.IsClosed == 1)
            {
                if (!checkBoxDetailsProjectIsClosed.Checked)
                {
                    query += concatenator;
                    query += "isClosed='0'";
                    concatenator = ",";
                    flag = true;
                }
            }
            else
            {
                if (checkBoxDetailsProjectIsClosed.Checked)
                {
                    query += concatenator;
                    query += "isClosed='1'";
                    concatenator = ",";
                    flag = true;
                }
            }
            if (currentProject.Type != comboBoxDetailsProjectType.SelectedIndex)
            {
                query += concatenator;
                query += "projectType='" + comboBoxDetailsProjectType.SelectedIndex.ToString() + "'";
                concatenator = ",";
                flag = true;
            }
            query += " WHERE idProjects='" + currentProject.Id + "'";

            if (flag)
            {
                mainInterface.Update(query);
                pictureBoxProjectV.Visible = true;
                pictureBoxProjectX.Visible = false;
                Application.DoEvents();
                Thread.Sleep(1500);
                pictureBoxProjectV.Visible = false;
                Application.DoEvents();
            }
            if (updatePriceIndexDate)
            {
                if (dateTimePickerDetailsProjectPriceIndexDate.Checked)
                    mainInterface.Update("UPDATE projects SET priceIndexDate = '" + MyUtills.dateToSQL(dateTimePickerDetailsProjectPriceIndexDate.Value) +
                                          "' WHERE projectNumber = '" + textBoxDetailsProjectNumber.Text + "' AND projectName = '" + textBoxDetailsProjectName.Text + "'");
                else
                    mainInterface.Update("UPDATE projects SET priceindexDate = NULL WHERE projectNumber = '" + textBoxDetailsProjectNumber.Text + "' AND projectName = '" + textBoxDetailsProjectName.Text + "'");
                if (!flag)
                {
                    pictureBoxProjectV.Visible = true;
                    pictureBoxProjectX.Visible = false;
                    Application.DoEvents();
                    Thread.Sleep(1500);
                    pictureBoxProjectV.Visible = false;
                    Application.DoEvents();
                }
            }
            if (amountChanged)
            {
                double maxProgress = 0, prevProgress = 0;
                double oldAmount = currentProject.Amount;
                double newAmount = double.Parse(textBoxDetailsProjectAmount.Text);
                double oldPercent, newPercent, oldProgress, newProgress, oldPaid, newPaid;
                DataTable dt = mainInterface.Select("SELECT id,percent,progress,paid FROM bills_projects WHERE idProject=" + currentProject.Id + " ORDER BY progress").Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    oldPercent = Convert.ToDouble(row[1]);
                    oldProgress = Convert.ToDouble(row[2]);
                    oldPaid = Convert.ToDouble(row[3]);
                    if (newAmount != 0)
                    {
                        newPercent = oldPercent * (oldAmount / newAmount);
                        newProgress = prevProgress + newPercent; //oldProgress - (oldPercent - newPercent);
                        newPaid = oldPaid * (oldAmount / newAmount);
                        prevProgress = newProgress;
                    }
                    else
                    {
                        newPercent = 0;
                        newProgress = 0;
                        newPaid = 0;
                    }
                    mainInterface.Update("UPDATE bills_projects SET percent='" + newPercent.ToString() + "',progress='" + newProgress.ToString() + "',paid='" + newPaid.ToString() + "' WHERE id=" + row[0].ToString());
                    if (newProgress > maxProgress)
                        maxProgress = newProgress;
                }
                mainInterface.Update("UPDATE projects SET toSubmit='"+maxProgress+"' WHERE idProjects="+currentProject.Id);
            }
            refreshView();
        }

        private void ToolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            formSettings = new Form_Settings(mainInterface);
            formSettings.ShowDialog();
        }

        private void buttonSelectCustomer_Click(object sender, EventArgs e)
        {
            formSelectCustomer = new Form_SelectCustomer(mainInterface, this);
            formSelectCustomer.ShowDialog();
        }
        public void  insertCustomer(string name, string id)
        {
            textBoxCustomerId.Text = id;
            textBoxDetailsProjectCustomerName.Text = name;
        }
        public void refreshBillNotes(string billNumber)
        {
            string temp = "SELECT * FROM notes WHERE idBill='" + billNumber + "' ORDER BY date DESC";
            dataGridViewDetailsBillNotes.DataSource = mainInterface.Select(temp).Tables[0];
            dataGridViewDetailsBillNotes.Columns[0].Visible = false;
            dataGridViewDetailsBillNotes.Columns[1].Visible = false;
            dataGridViewDetailsBillNotes.Columns[2].HeaderText = "שם";
            dataGridViewDetailsBillNotes.Columns[3].HeaderText = "תאריך";
            dataGridViewDetailsBillNotes.Columns[4].HeaderText = "טקסט";
            dataGridViewDetailsBillNotes.Columns[2].Width = 100;
            dataGridViewDetailsBillNotes.Columns[3].Width = 100;
            dataGridViewDetailsBillNotes.Columns[4].Width = dataGridViewDetailsBillNotes.Width -
                                                            dataGridViewDetailsBillNotes.Columns[2].Width -
                                                            dataGridViewDetailsBillNotes.Columns[3].Width - 3;
            //dataGridViewDetailsBillNotes.AutoResizeRows();
            //dataGridViewDetailsBillNotes.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dataGridViewDetailsBillNotes.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void אודותToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_About formAbout = new Form_About();
            formAbout.ShowDialog();
        }

        private void labelDetailsCustomerToProject_Click(object sender, EventArgs e)//NOTE:!!! this is bills to Project
        {
            tabControlFilters.SelectedTab = tabPageProject;
            string billId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            DataTable dt = mainInterface.Select("SELECT idProject FROM bills_projects WHERE idBill="+billId).Tables[0];
            searchIdQuery = String.Empty;
            string concat = String.Empty;
            foreach (DataRow row in dt.Rows)
            {
                searchIdQuery += concat;
                searchIdQuery += "bills_projects.idProject="+row[0];
                concat = " OR ";
            }
            searchById = true;
            buttonSearchProjects.PerformClick();
        }

        private void textBoxFiltersProjectsFreeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearchProjects.PerformClick();
        }
        private void textBoxFiltersProjectsAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearchProjects.PerformClick();
        }

        private void textBoxFiltersBillsFreeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearchBills.PerformClick();
        }
        private void textBoxFiltersBillsAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearchProjects.PerformClick();
        }

        private void textBoxFiltersCustomersFreeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearchCustomers.PerformClick();
        }

        private void textBoxFiltersProjectsAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(textBoxFiltersProjectsAmount.Text);
            }
            catch
            {
                textBoxFiltersProjectsAmount.Text = "0";
            }
        }
        private void textBoxFiltersBillsAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(textBoxFiltersBillsAmount.Text);
            }
            catch
            {
                textBoxFiltersBillsAmount.Text = "0";
            }
        }

        private void pictureBoxPrint_Click(object sender, EventArgs e)
        {
            //Open the print dialog
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;
            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                switch (currentPanel)
                {
                    case CurrentPanel.PROJECTS:
                        printDocument1.DefaultPageSettings.Landscape = false;
                        break;
                    case CurrentPanel.BILLS:
                        printDocument1.DefaultPageSettings.Landscape = true;
                        break;
                    case CurrentPanel.CUSTOMERS:
                        printDocument1.DefaultPageSettings.Landscape = false;
                        break;
                }
                printDocument1.Print();
            }
            /*
            Note: In case you want to show the Print Preview Dialog instead of 
            Print Dialog then comment the above code and uncomment the following code
            */

            //Open the print preview dialog
            //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            //objPPdialog.Document = printDocument1;
            //objPPdialog.ShowDialog();
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DataGridViewColumn GridCol;
            DataGridViewColumn GridCol1;
            DataGridViewCell Cel;
            string title = "";
            StringFormat strFormat1 = new System.Drawing.StringFormat(); //Used to format the grid rows.
            strFormat1.Alignment = System.Drawing.StringAlignment.Far;
            switch (currentPanel)
            {
                case CurrentPanel.PROJECTS:
                    title = "פרוייקטים";
                    break;
                case CurrentPanel.BILLS:
                    title = "חשבונות";
                    break;
                case CurrentPanel.CUSTOMERS:
                    title = "לקוחות";
                    break;
            }
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    for (int i = dataGridView1.Columns.Count - 1; i >= 0; i--)
                    {
                        GridCol = new DataGridViewColumn();
                        GridCol = dataGridView1.Columns[i];
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)iTotalWidth * (double)iTotalWidth *
                            ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                            GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headers
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dataGridView1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView1.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString(title,
                                new Font(dataGridView1.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString(title,
                                new Font(dataGridView1.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);
                            
                            String strDate = DateTime.Now.ToLongDateString() + " " +
                                DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(dataGridView1.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(dataGridView1.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString(title,
                                new Font(new Font(dataGridView1.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            for (int i = dataGridView1.Columns.Count - 1; i >= 0; i--)
                            {
                                GridCol1 = new DataGridViewColumn();
                                GridCol1 = dataGridView1.Columns[i];
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));
                                
                                e.Graphics.DrawString(GridCol1.HeaderText,
                                    GridCol1.InheritedStyle.Font,
                                    new SolidBrush(GridCol1.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat1);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        for (int i = GridRow.Cells.Count - 1; i >= 0; i--)
                        {
                            Cel = GridRow.Cells[i];
                            if (Cel.Value != null)
                            {
                                if (Cel.Value.ToString() == "True")
                                {
                                    strFormat1.Alignment = System.Drawing.StringAlignment.Center;
                                    e.Graphics.DrawString("V",
                                    Cel.InheritedStyle.Font,
                                    new SolidBrush(Cel.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount],
                                    (float)iTopMargin,
                                    (int)arrColumnWidths[iCount], (float)iCellHeight),
                                    strFormat1);
                                    strFormat1.Alignment = System.Drawing.StringAlignment.Far;
                                }
                                else if (Cel.Value.ToString() == "False")
                                {
                                    e.Graphics.DrawString("",
                                        Cel.InheritedStyle.Font,
                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount],
                                        (float)iTopMargin,
                                        (int)arrColumnWidths[iCount], (float)iCellHeight),
                                        strFormat1);
                                }
                                else
                                {
                                    e.Graphics.DrawString(Cel.Value.ToString(),
                                        Cel.InheritedStyle.Font,
                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount],
                                        (float)iTopMargin,
                                        (int)arrColumnWidths[iCount], (float)iCellHeight),
                                        strFormat1);
                                }
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black,
                                new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                (int)arrColumnWidths[iCount], iCellHeight));
                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //resizeView();
        }

        private void pictureBoxClearBills_Click(object sender, EventArgs e)
        {
            textBoxFiltersBillsFreeText.Text = "";
        }
        private void pictureBoxClearProjects_Click(object sender, EventArgs e)
        {
            textBoxFiltersProjectsFreeText.Text = "";
        }
        private void pictureBoxClearCustomers_Click(object sender, EventArgs e)
        {
            textBoxFiltersCustomersFreeText.Text = "";
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (currentPanel)
            {
                case CurrentPanel.PROJECTS:
                    try
                    {
                        if (e.ColumnIndex == dataGridView1.Columns["amount"].Index)
                        {
                            e.Value = String.Format("{0:0,0.00}", e.Value);
                            e.FormattingApplied = true;
                        }
                        if (e.ColumnIndex == dataGridView1.Columns["billsSumPaid"].Index)
                        {
                            e.Value = String.Format("{0:0,0.00}", e.Value);
                            e.FormattingApplied = true;
                        }
                        if (e.ColumnIndex == dataGridView1.Columns["percentagePaid"].Index)
                        {
                            e.Value = String.Format("{0:0,0.00}", e.Value);
                            e.FormattingApplied = true;
                        }
                        if (e.ColumnIndex == dataGridView1.Columns["toSubmit"].Index)
                        {
                            e.Value = Math.Round((double)e.Value).ToString();
                            e.FormattingApplied = true;
                        }
                        /*if (e.ColumnIndex == dataGridView1.Columns["percentageSubmitted"].Index)
                        {
                            e.Value = String.Format("{0:0,0.00}", e.Value);
                            e.FormattingApplied = true;
                        }*/
                    }
                    catch { }
                    break;
                case CurrentPanel.BILLS:
                    try
                    {
                        if (e.ColumnIndex == dataGridView1.Columns["amount"].Index)
                        {
                            e.Value = String.Format("{0:0,0.00}", e.Value);
                            e.FormattingApplied = true;
                        }
                        if (e.ColumnIndex == dataGridView1.Columns["paid"].Index)
                        {
                            e.Value = String.Format("{0:0,0.00}", e.Value);
                            e.FormattingApplied = true;
                        }
                        if (e.ColumnIndex == dataGridView1.Columns["percent"].Index)
                        {
                            e.Value = String.Format("{0:0.0}", e.Value);
                            e.FormattingApplied = true;
                        }
                    }
                    catch { }
                    break;
                default:
                    break;
            }
        }
        public string getNewBillAmount()
        {
            string amount = "0";
            amount = String.Format("{0:0,0.00}",(int.Parse(textBoxDetailsProjectToSubmitCalculated.Text)*double.Parse(textBoxDetailsProjectAmount.Text)/100));
            return amount;
        }

        private void contextMenuStripBills_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if  (e.ClickedItem == toolStripMenuItemDeleteBill)
            {
                DialogResult result;
                result = MessageBox.Show("האם למחוק חשבון זה ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                if (result == DialogResult.No)
                    return;
                mainInterface.Delete("bills", "id", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                refreshView();
            }
            else if (e.ClickedItem == toolStripMenuItemShowProject)
            {
                tabControlFilters.SelectedTab = tabPageProject;
                string billId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dt = mainInterface.Select("SELECT idProject FROM bills_projects WHERE idBill=" + billId).Tables[0];
                searchIdQuery = String.Empty;
                string concat = String.Empty;
                foreach (DataRow row in dt.Rows)
                {
                    searchIdQuery += concat;
                    searchIdQuery += "bills_projects.idProject=" + row[0];
                    concat = " OR ";
                }
                searchById = true;
                buttonSearchProjects.PerformClick();
            }
        }
        private void contextMenuStripProject_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "toolStripMenuItemProjectShowBills":
                    tabControlFilters.SelectedTab = tabPageBill;
                    searchById = true;
                    searchIdQuery = "bills_projects.idProject=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    buttonSearchBills.PerformClick();
                    break;
                case "toolStripMenuItemProjectShowCustomer":
                    tabControlFilters.SelectedTab = tabPageCustomer;
                    string projectId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    searchById = true;
                    searchIdQuery = "customers.idCustomer=" + mainInterface.Select("SELECT idCustomer FROM projects WHERE idProjects=" + projectId).Tables[0].Rows[0][0].ToString();
                    buttonSearchCustomers.PerformClick();
                    break;
                case "toolStripMenuItemProjectCreateBill":
                    /*formNewBill = new Form_NewBill(this, mainInterface, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                                                                  dataGridView1.SelectedRows[0].Cells[2].Value.ToString(),
                                                                  dataGridView1.SelectedRows[0].Cells[3].Value.ToString(),
                                                                  textBoxDetailsProjectAmountInfo.Text);*/
                    //formNewBill.ShowDialog();
                    break;
                case "toolStripMenuItemProjectDelete":
                    DialogResult result = MessageBox.Show("מחיקת פרויקט תגרור מחיקה של כל החשבונות שלו,\r\n האם להמשיך?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    if (result == DialogResult.No)
                        return;
                    mainInterface.Delete("projects", "idProjects", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    refreshView();
                    break;
            }
        }
        private void contextMenuStripCustomer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "toolStripMenuItemShowProjects":
                    tabControlFilters.SelectedTab = tabPageProject;
                    searchById = true;
                    searchIdQuery = "projects.idCustomer=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    buttonSearchProjects.PerformClick();
                    break;
                case "toolStripMenuItemCustomerNewProject":
                    formNewProject = new Form_NewProject(this, mainInterface, dataGridView1.SelectedRows[0].Cells[1].Value.ToString(),
                                                                        dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    formNewProject.ShowDialog();
                    break;
                case "ToolStripMenuItemDeleteCustomer":
                    DialogResult result;
                    result = MessageBox.Show("מחיקת לקוח תגרור מחיקה של כל הפרויקטים והחשבונות שלו,\r\n האם להמשיך?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    if (result == DialogResult.No)
                        return;
                    mainInterface.Delete("customers", "idCustomer", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    break;
            }
        }

        private void dataGridViewDetailsBillNotes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                string text = "", id = "";
                try
                {
                    id = dataGridViewDetailsBillNotes.SelectedRows[0].Cells[0].Value.ToString();
                    text = dataGridViewDetailsBillNotes.SelectedRows[0].Cells[4].Value.ToString();
                }
                catch { return; }
                formShowNote = new Form_ShowNote(id, text, mainInterface, this);
                formShowNote.ShowDialog();               
            }
        }
        private void ToolStripMenuItemHandlers_Click(object sender, EventArgs e)
        {
            formHandlers = new Form_handlers(mainInterface);
            formHandlers.ShowDialog();
        }
        private void סיכומיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formSum = new Form_Summery(mainInterface);
            formSum.ShowDialog();
        }

        private void linkLabelCalcIncrease_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string billId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            double currentIndex=0;
            if (!Double.TryParse(textBoxBillIndex.Text, out currentIndex))
            {
                labelBillIndex.ForeColor = Color.Red;
                return;
            }
            else
            {
                labelBillIndex.ForeColor = Color.Black;
                DataTable dt = mainInterface.Select("SELECT projects.priceIndex,bills_projects.idProject, bills_projects.idBill FROM bills INNER JOIN bills_projects ON bills_projects.idBill=bills.id INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills.id=" + billId).Tables[0];
                double priceIndex;
                double amount = 0;
                double increase = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    priceIndex = 0;
                    Double.TryParse(dt.Rows[i][0].ToString(), out priceIndex);

                    amount = 0;
                    /*if (!textBoxAmountNis.Text.Contains("יש"))
                        amount = Double.Parse(textBoxAmountNis.Text.Replace(",", "").Replace(" שקל", ""));
                    else
                    {
                        Double.TryParse(textBoxDetailsBillAmount.Text.Replace(",", ""), out amount);
                    }*/
                    amount = projectPartAmount((int)dt.Rows[i][1], (int)dt.Rows[i][2]);
                    increase += getIncrease(amount, currentIndex, priceIndex);
                }

                textBoxBillIncrease.Text = String.Format("{0:0,0.00}", increase);
                textBoxBillAmountWithIncrease.Text = String.Format("{0:0,0.00}", amount + increase);
                textBoxBillAmountWithVAT.Text = String.Format("{0:0,0.00}", (amount + increase) * (Double.Parse(textBoxBillVat.Text.Substring(textBoxBillVat.Text.Length - 2, 2)) / 100 + 1));
            }

        }
        private double getIncrease(double amount, double currentIndex, double priceIndex)
        {
            double increase=0;
            if (priceIndex != 0)
                increase = (currentIndex / priceIndex - 1) * amount;
            if (currentIndex == 0)
                increase = 0;
            return increase;
        }

        public void refreshView()
        {
            DataGridViewColumn oldcolumn;
            ListSortDirection direction;
            DataGridViewColumn newColumn;

            int selectedRow = 0;
            string localSelectedId = "";
            if (lastSearch.IsSearch)
            {
                searchById = false;
            }
            else // By Id
            {
                searchById = true;
                searchIdQuery = lastSearch.IdQuery;
            }
            switch (lastSearch.Panel)
            {
                case (short)CurrentPanel.BILLS:
                    localSelectedId = selectedId;

                    oldcolumn = dataGridView1.SortedColumn;
                    if (dataGridView1.SortOrder == SortOrder.Ascending) direction = ListSortDirection.Ascending;
                    else direction = ListSortDirection.Descending;

                    buttonSearchBills.PerformClick();

                    if (oldcolumn != null)
                    {
                        newColumn = dataGridView1.Columns[oldcolumn.Name.ToString()];
                        dataGridView1.Sort(newColumn, direction);
                        newColumn.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
                    }

                    if (dataGridView1.Rows.Count == 0)
                        break;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == localSelectedId)
                            break;
                        selectedRow++;
                    }
                    try
                    {
                        dataGridView1.Rows[selectedRow].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = selectedRow;
                        showCurrentRowDetails(selectedRow);
                    }
                    catch { }
                    break;
                case (short)CurrentPanel.PROJECTS:
                    localSelectedId = selectedId;

                    oldcolumn = dataGridView1.SortedColumn;
                    if (dataGridView1.SortOrder == SortOrder.Ascending) direction = ListSortDirection.Ascending;
                    else direction = ListSortDirection.Descending;

                    buttonSearchProjects.PerformClick();

                    if (oldcolumn != null)
                    {
                        newColumn = dataGridView1.Columns[oldcolumn.Name.ToString()];
                        dataGridView1.Sort(newColumn, direction);
                        newColumn.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
                    }

                    if (dataGridView1.Rows.Count == 0)
                        break;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == localSelectedId)
                            break;
                        selectedRow++;
                    }
                    try
                    {
                        dataGridView1.Rows[selectedRow].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = selectedRow;
                        showCurrentRowDetails(selectedRow);
                    }
                    catch { }
                    break;
                case (short)CurrentPanel.CUSTOMERS:
                    localSelectedId = selectedId;

                    oldcolumn = dataGridView1.SortedColumn;
                    if (dataGridView1.SortOrder == SortOrder.Ascending) direction = ListSortDirection.Ascending;
                    else direction = ListSortDirection.Descending;

                    buttonSearchCustomers.PerformClick();

                    if (oldcolumn != null)
                    {
                        newColumn = dataGridView1.Columns[oldcolumn.Name.ToString()];
                        dataGridView1.Sort(newColumn, direction);
                        newColumn.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
                    }

                    if (dataGridView1.Rows.Count == 0)
                        break;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == localSelectedId)
                            break;
                        selectedRow++;
                    }
                    try
                    {
                        dataGridView1.Rows[selectedRow].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = selectedRow;
                        showCurrentRowDetails(selectedRow);
                    }
                    catch { }
                    break;
                default:
                    break;
            }
        }
        private void resizeView()
        {
            Point tablocation = new System.Drawing.Point();
            tablocation.X = (this.Width - (tabControlFilters.Width + splitContainer.Width+3)) / 2-7;
            tablocation.Y = tabControlFilters.Location.Y;
            tabControlFilters.Location = tablocation;
            Point splitlocation = new System.Drawing.Point();
            splitlocation.X = (this.Width - (tabControlFilters.Width + splitContainer.Width)) / 2 + tabControlFilters.Width + 3-7;
            splitlocation.Y = splitContainer.Location.Y;
            splitContainer.Location = splitlocation;
            Point headerlocation = new System.Drawing.Point();
            headerlocation.X = (this.Width - (tabControlFilters.Width + splitContainer.Width)) / 2 + tabControlFilters.Width + 3-7;
            headerlocation.Y = labelCurrentView.Location.Y;
            labelCurrentView.Location = headerlocation;
            Point printlocation = new System.Drawing.Point();
            printlocation.X = splitContainer.Right - pictureBoxPrint.Width;
            printlocation.Y = pictureBoxPrint.Location.Y;
            pictureBoxPrint.Location = printlocation;

            if (dataGridViewFinance.RowCount > 0)
                refreshFinanceColumnsWidth();
            //panelFinance.Size = new System.Drawing.Size(this.Width, this.Height);
        }
        private void labelProjectShowCustomer_Click(object sender, EventArgs e)
        {
            tabControlFilters.SelectedTab = tabPageCustomer;
            string projectId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            searchById = true;
            searchIdQuery = "customers.idCustomer=" + mainInterface.Select("SELECT idCustomer FROM projects WHERE idProjects=" + projectId).Tables[0].Rows[0][0].ToString();
            buttonSearchCustomers.PerformClick();
        }
        private void labelCreateBillFile_Click(object sender, EventArgs e)
        {
            int idBill = Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[0].Value);
            formExport = new Form_Export(mainInterface,idBill);
            formExport.ShowDialog();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            resizeView();
            MySettings.setSize(this.Height.ToString(), this.Width.ToString());
        }

        private void linkLabelFixProgress_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelFixProgress.Visible = true;
            textBoxFixProgress.Text = "";
        }
        private void buttonAbortFixProgress_Click(object sender, EventArgs e)
        {
            panelFixProgress.Visible = false;
        }

        private void buttonSaveFixProgress_Click(object sender, EventArgs e)
        {
            int newProgress;
            double sum = 0;
            if (int.TryParse(textBoxFixProgress.Text, out newProgress))
            {
                DialogResult result;
                result = MessageBox.Show("סכום החשבון יתעדכן אוטומטית, האם להמשיך?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                if (result == DialogResult.No)
                    return;
                mainInterface.Update("UPDATE projects SET toSubmit='" + newProgress.ToString() + "' WHERE idProjects='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                string progress = labelProjectToSubmitPast.Text.Substring(1, labelProjectToSubmitPast.Text.IndexOf('%') - 2);
                DataTable dt = mainInterface.Select("SELECT percent,idBill FROM bills_projects WHERE idProject='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "' AND progress='" + progress + "'").Tables[0];
                double oldPercent = (double)dt.Rows[0][0];
                double newPercent = oldPercent - (int.Parse(progress) - newProgress);
                mainInterface.Update("UPDATE bills_projects SET percent='" + newPercent.ToString() + "',progress='" + newProgress.ToString() + "' WHERE idProject='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "' AND progress='" + progress + "'");
                DataTable dtProjects = mainInterface.Select("SELECT SUM(projects.amount*bills_projects.percent/100) FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idBIll='" + dt.Rows[0][1].ToString() + "'").Tables[0];
                foreach (DataRow dr in dtProjects.Rows)
                    sum += (double)dr[0];
                mainInterface.Update("UPDATE bills SET amount='" + sum + "' WHERE id='" + dt.Rows[0][1].ToString() + "'");
                refreshView();
                panelFixProgress.Visible = false;
            }
            else
                MessageBox.Show("אחוז להגשה חדש לא חוקי", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
        }

        private void dataGridViewBillsDetailsProjects_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2)
                lastProjectPartAmount = Double.Parse(dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            else if(e.ColumnIndex==3)
                lastProjectPartPaid = Double.Parse(dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
        private void dataGridViewBillsDetailsProjects_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                double currentProjectPartAmount;
                if (!Double.TryParse(dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out currentProjectPartAmount))
                {
                    dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = lastProjectPartAmount;
                    return;
                }


                //double currentProjectPartAmount = dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (currentProjectPartAmount != lastProjectPartAmount)
                {
                    DialogResult result = MessageBox.Show("האם לבצע שינוי זה ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    if (result == DialogResult.Yes)
                    {

                        int selectedIdBill = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                        int selectedIdProject = (int)dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[0].Value;
                        double projectAmount = (double)mainInterface.Select("SELECT amount FROM projects WHERE idProjects=" + selectedIdProject.ToString()).Tables[0].Rows[0][0];
                        //double newAmount = (double)dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        DataTable dt = mainInterface.Select("SELECT id,percent,progress FROM bills_projects WHERE idBill=" + selectedIdBill + " AND idProject=" + selectedIdProject).Tables[0];
                        double newPercent;
                        if (projectAmount == 0)
                            newPercent = 0;
                        else
                            newPercent = (currentProjectPartAmount / projectAmount * 100);
                        double newProgress = (double)dt.Rows[0][2] - ((double)dt.Rows[0][1] - newPercent);
                        mainInterface.Update("UPDATE bills_projects SET percent='" + newPercent.ToString() + "',progress=" + newProgress.ToString() + " WHERE id=" + dt.Rows[0][0].ToString());
                        mainInterface.Update("UPDATE projects SET toSubmit='" + newProgress.ToString() + "' WHERE idProjects=" + selectedIdProject.ToString());
                        double sum = 0;
                        DataTable dtProjects = mainInterface.Select("SELECT SUM(projects.amount*bills_projects.percent/100) FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idBIll='" + selectedIdBill.ToString() + "'").Tables[0];
                        foreach (DataRow dr in dtProjects.Rows)
                            sum += (double)dr[0];
                        mainInterface.Update("UPDATE bills SET amount='" + sum + "' WHERE id='" + selectedIdBill.ToString() + "'");
                        refreshView();
                    }
                    else
                    {
                        string query = "SELECT projects.idProjects,projects.projectNumber,projects.amount,projects.amount FROM projects INNER JOIN bills_projects ON projects.idProjects=bills_projects.idProject WHERE bills_projects.idBill='" + ((int)dataGridView1.SelectedRows[0].Cells[0].Value).ToString() + "'";

                        DataTable dtProjectNumber = mainInterface.Select(query).Tables[0];
                        textBoxDetailsBillProjectName.Text = "";
                        double billTotal = 0;
                        foreach (DataRow dr in dtProjectNumber.Rows)
                        {
                            textBoxDetailsBillProjectName.AppendText(dr[1].ToString() + "  (" + String.Format("{0:0,0.00}", projectPartAmount((int)dr[0], int.Parse(selectedId))) + ")\r\n");
                            dr[2] = String.Format("{0:0,0.00}", projectPartAmount((int)dr[0], int.Parse(selectedId)));
                            dr[3] = String.Format("{0:0,0.00}", projectPartPaid((int)dr[0], int.Parse(selectedId)));
                            billTotal += projectPartAmount((int)dr[0], int.Parse(selectedId));
                        }
                        dataGridViewBillsDetailsProjects.DataSource = dtProjectNumber;
                    }
                }
            }
            else if (e.ColumnIndex == 3)
            {
                double currentProjectPartPaid;
                if (!Double.TryParse(dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out currentProjectPartPaid))
                {
                    dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = lastProjectPartPaid;
                    return;
                }


                //double currentProjectPartAmount = dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (currentProjectPartPaid != lastProjectPartPaid)
                {
                    DialogResult result = MessageBox.Show("האם לבצע שינוי זה ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
                    if (result == DialogResult.Yes)
                    {

                        int selectedIdBill = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                        int selectedIdProject = (int)dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[0].Value;
                        double projectAmount = (double)mainInterface.Select("SELECT amount FROM projects WHERE idProjects=" + selectedIdProject.ToString()).Tables[0].Rows[0][0];
                        //double newAmount = (double)dataGridViewBillsDetailsProjects.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        DataTable dt = mainInterface.Select("SELECT id FROM bills_projects WHERE idBill=" + selectedIdBill + " AND idProject=" + selectedIdProject).Tables[0];
                        double newPaid;
                        if (projectAmount == 0)
                            newPaid = 0;
                        else
                            newPaid = (currentProjectPartPaid / projectAmount * 100);
                        //double newProgress = (double)dt.Rows[0][2] - ((double)dt.Rows[0][1] - newPercent);
                        mainInterface.Update("UPDATE bills_projects SET paid='" + newPaid.ToString() + "' WHERE id=" + dt.Rows[0][0].ToString());
                        //mainInterface.Update("UPDATE projects SET toSubmit='" + newProgress.ToString() + "' WHERE idProjects=" + selectedIdProject.ToString());
                        double sum = 0;
                        DataTable dtProjects = mainInterface.Select("SELECT SUM(projects.amount*bills_projects.paid/100) FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idBIll='" + selectedIdBill.ToString() + "'").Tables[0];
                        foreach (DataRow dr in dtProjects.Rows)
                            sum += (double)dr[0];
                        mainInterface.Update("UPDATE bills SET paid='" + sum + "' WHERE id='" + selectedIdBill.ToString() + "'");
                        refreshView();
                    }
                    else
                    {
                        //refreshDataGridViewBillsDetailsProjects((int)dataGridView1.SelectedRows[0].Cells[0].Value);
                        string query = "SELECT projects.idProjects,projects.projectNumber,projects.amount,projects.amount FROM projects INNER JOIN bills_projects ON projects.idProjects=bills_projects.idProject WHERE bills_projects.idBill='" + ((int)dataGridView1.SelectedRows[0].Cells[0].Value).ToString() +"'";

                        DataTable dtProjectNumber = mainInterface.Select(query).Tables[0];
                        textBoxDetailsBillProjectName.Text = "";
                        double billTotal = 0;
                        foreach (DataRow dr in dtProjectNumber.Rows)
                        {
                            textBoxDetailsBillProjectName.AppendText(dr[1].ToString() + "  (" + String.Format("{0:0,0.00}", projectPartAmount((int)dr[0], int.Parse(selectedId))) + ")\r\n");
                            dr[2] = String.Format("{0:0,0.00}", projectPartAmount((int)dr[0], int.Parse(selectedId)));
                            dr[3] = String.Format("{0:0,0.00}", projectPartPaid((int)dr[0], int.Parse(selectedId)));
                            billTotal += projectPartAmount((int)dr[0], int.Parse(selectedId));
                        }
                        //dataGridViewBillsDetailsProjects.Dispose();
                        dataGridViewBillsDetailsProjects.DataSource = dtProjectNumber;
                    }
                }
            }
        }
        private void refreshDataGridViewBillsDetailsProjects(int billId)
        {
            
        }
        private void linkLabelBillPaidAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("האם לפרוע חשבון זה במלואו ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading);
            if (result == DialogResult.No)
                return;
            
            string selectedIdBill = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            mainInterface.Update("UPDATE bills_projects SET paid=percent WHERE idBill='" + selectedIdBill + "'");
            double sum = 0;
            DataTable dtProjects = mainInterface.Select("SELECT SUM(projects.amount*bills_projects.paid/100) FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idBIll='" + selectedIdBill.ToString() + "'").Tables[0];
            foreach (DataRow dr in dtProjects.Rows)
                sum += (double)dr[0];
            mainInterface.Update("UPDATE bills SET paid='" + sum + "' WHERE id='" + selectedIdBill.ToString() + "'");
            textBoxDetailsBillPaid.Text = String.Format("{0:0,0.00}", sum);
            
            saveChangesBill();
            refreshView();
        }


        public void UserEnter(int type)
        {
            userEnter = true;
            userType = type;
            showSums = labelHideShow.Visible = (userType == 1) ? true : false;
        }

        private void ToolStripMenuItemSuppliers_Click(object sender, EventArgs e)
        {
            formSuppliers = new Form_Suppliers(mainInterface);
            formSuppliers.ShowDialog();
        }

        private void ToolStripMenuItemExpenses_Click(object sender, EventArgs e)
        {
            formExpenses = new Form_Expenses(this, mainInterface);
            formExpenses.ShowDialog();
        }
        
        ///////////////////////////////  FINANCE  //////////

        public void showFinance()
        {
            if (!double.TryParse(textBoxScenarioPercent.Text, out ScenarioPercent))
            {
                ScenarioPercent = 0.6;
                textBoxScenarioPercent.Text = "60";
            }
            else
                ScenarioPercent /= 100;
            DataTable dtUpdate = mainInterface.Select("SELECT date,amount,amountForeign,amountPapers,amountCalc FROM bankupdates ORDER BY id DESC LIMIT 1").Tables[0];
            double updatedAccount = (double)dtUpdate.Rows[0][1];
            DateTime updatedDate = MyUtills.dateFromSQL(dtUpdate.Rows[0][0].ToString());

            dateTimePickerLastUpdate.Value = updatedDate;

            textBoxBankAmount.Text = String.Format("{0:0,0.00}", updatedAccount);
            textBoxAmountForeign.Text = String.Format("{0:0,0.00}", (double)dtUpdate.Rows[0][2]);
            textBoxAmountPapers.Text = String.Format("{0:0,0.00}", (double)dtUpdate.Rows[0][3]);
            textBoxBankAmountTotal.Text = String.Format("{0:0,0.00}", updatedAccount + (double)dtUpdate.Rows[0][2] + (double)dtUpdate.Rows[0][3]);

            textBoxAmountCalc.Text = String.Format("{0:0,0.00}", (double)dtUpdate.Rows[0][4]);
            

            double accountLastBalance = updatedAccount, accountLastBalance60 = updatedAccount, accountLastSumBalance = updatedAccount;
            double currentDefeniteAmount = 0, currentForecastAmount = 0, currentExpensesAmount = 0, currentExpectedExpensesAmount = 0;

            DataTable dtFinance = new DataTable();

            DataTable dtIncomesDefinite = mainInterface.Select("SELECT bills.id,bills.payDate,SUM(projects.amount * bills.curencyRate * bills_projects.percent / 100) AS amount,projects.projectName,bills.idBill,bills.bank,bills.curencyRate,bills.increase FROM bills INNER JOIN bills_projects ON bills.id=bills_projects.idBill INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE (bills.payDate IS NOT NULL AND bills.payDate>='" + MyUtills.dateToSQL(updatedDate) + "') GROUP BY bills_projects.idBill ORDER BY bills.payDate ASC").Tables[0];
            DataTable dtIncomesForecast = mainInterface.Select("SELECT bills.id,bills.toPayDate,SUM(projects.amount * bills.curencyRate * bills_projects.percent / 100) AS amount,projects.projectName,bills.idBill,bills.bank,bills.curencyRate,bills.increase FROM bills INNER JOIN bills_projects ON bills.id=bills_projects.idBill INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE (bills.toPayDate IS NOT NULL AND bills.payDate IS NULL AND bills.toPayDate>='" + MyUtills.dateToSQL(updatedDate) + "') GROUP BY bills_projects.idBill ORDER BY bills.toPayDate ASC").Tables[0];
            DataTable dtExpenses = mainInterface.Select("SELECT expenses.id,expenses.date,expenses.amount,suppliers.name,expenses.approved,expenses.bank,expenses.type,expenses.notes FROM expenses INNER JOIN suppliers ON expenses.idSupplier=suppliers.id WHERE expenses.date>='" + MyUtills.dateToSQL(updatedDate) + "' ORDER BY expenses.date ASC").Tables[0];
            Double vat = (double)mainInterface.Select("SELECT vat FROM vat WHERE date<='" + MyUtills.dateToSQL(updatedDate) + "' ORDER BY date DESC LIMIT 1").Tables[0].Rows[0][0];

            DateTime firstDatedDtIncomesDefinite = new DateTime(2050,1,1);
            DateTime firstDatedDtIncomesForecast = new DateTime(2050, 1, 1);
            DateTime firstDatedDtExpenses = new DateTime(2050, 1, 1);

            DateTime lastDatedDtIncomesDefinite = new DateTime(2050, 1, 1);
            DateTime lastDatedDtIncomesForecast = new DateTime(2050, 1, 1);
            DateTime lastDatedDtExpenses = new DateTime(2050, 1, 1);

            if (dtIncomesDefinite.Rows.Count > 0)
            {
                firstDatedDtIncomesDefinite = MyUtills.dateFromSQL(dtIncomesDefinite.Rows[0][1].ToString());
                lastDatedDtIncomesDefinite = MyUtills.dateFromSQL(dtIncomesDefinite.Rows[dtIncomesDefinite.Rows.Count - 1][1].ToString());
            }
            if (dtIncomesForecast.Rows.Count > 0)
            {
                firstDatedDtIncomesForecast = MyUtills.dateFromSQL(dtIncomesForecast.Rows[0][1].ToString());
                lastDatedDtIncomesForecast = MyUtills.dateFromSQL(dtIncomesForecast.Rows[dtIncomesForecast.Rows.Count - 1][1].ToString());
            }
            if (dtExpenses.Rows.Count > 0)
            {
                firstDatedDtExpenses = MyUtills.dateFromSQL(dtExpenses.Rows[0][1].ToString());
                lastDatedDtExpenses = MyUtills.dateFromSQL(dtExpenses.Rows[dtExpenses.Rows.Count - 1][1].ToString());
            }


            DateTime startdate = findMinDate(firstDatedDtIncomesDefinite,
                                             firstDatedDtIncomesForecast,
                                             firstDatedDtExpenses);
            DateTime enddate = findMaxDate(lastDatedDtIncomesDefinite,
                                             lastDatedDtIncomesForecast,
                                             lastDatedDtExpenses);
            
            initFinanceTable(ref dtFinance);

            int indexDefinite = 0, indexForecast = 0, indexExpenses = 0;
            int countDefinite = 0, countForecast = 0, countExpenses = 0;
            while (startdate <= enddate)
            {
                countDefinite = countDatesInDataTableFromIndex(indexDefinite, startdate, dtIncomesDefinite);
                countForecast = countDatesInDataTableFromIndex(indexForecast, startdate, dtIncomesForecast);
                countExpenses = countDatesInDataTableFromIndex(indexExpenses, startdate, dtExpenses);

                for (int i = 0; i < findMax(countDefinite, countForecast, countExpenses); i++)
                {
                    dtFinance.Rows.Add();
                    if (i == 0)
                    {
                        dtFinance.Rows[dtFinance.Rows.Count - 1][0] = startdate;
                    }
                    if (countDefinite > i)
                    {
                        try
                        {
                            dtFinance.Rows[dtFinance.Rows.Count - 1][1] = dtIncomesDefinite.Rows[indexDefinite][0];
                            dtFinance.Rows[dtFinance.Rows.Count - 1][2] = dtIncomesDefinite.Rows[indexDefinite][5];
                            dtFinance.Rows[dtFinance.Rows.Count - 1][3] = ((double)dtIncomesDefinite.Rows[indexDefinite][2] * (double)dtIncomesDefinite.Rows[indexDefinite][6] + (double)dtIncomesDefinite.Rows[indexDefinite][7]) * (1 + vat / 100);
                            dtFinance.Rows[dtFinance.Rows.Count - 1][4] = dtIncomesDefinite.Rows[indexDefinite][4];
                            dtFinance.Rows[dtFinance.Rows.Count - 1][5] = dtIncomesDefinite.Rows[indexDefinite][3];
                            indexDefinite++;
                        }
                        catch { }
                    }
                    if (countForecast > i)
                    {
                        try
                        {
                            dtFinance.Rows[dtFinance.Rows.Count - 1][6] = dtIncomesForecast.Rows[indexForecast][0];
                            dtFinance.Rows[dtFinance.Rows.Count - 1][7] = dtIncomesForecast.Rows[indexForecast][5];
                            dtFinance.Rows[dtFinance.Rows.Count - 1][8] = ((double)dtIncomesForecast.Rows[indexForecast][2] * (double)dtIncomesForecast.Rows[indexForecast][6] + (double)dtIncomesForecast.Rows[indexForecast][7]) * (1 + vat / 100);
                            dtFinance.Rows[dtFinance.Rows.Count - 1][9] = dtIncomesForecast.Rows[indexForecast][4];
                            dtFinance.Rows[dtFinance.Rows.Count - 1][10] = dtIncomesForecast.Rows[indexForecast][3];
                            indexForecast++;
                        }
                        catch { }
                    }
                    if (countExpenses > i)
                    {
                        try
                        {
                            dtFinance.Rows[dtFinance.Rows.Count - 1][11] = dtExpenses.Rows[indexExpenses][0];
                            dtFinance.Rows[dtFinance.Rows.Count - 1][12] = dtExpenses.Rows[indexExpenses][5];

                            if (!(bool)dtExpenses.Rows[indexExpenses][4])
                                dtFinance.Rows[dtFinance.Rows.Count - 1][13] = dtExpenses.Rows[indexExpenses][2];
                            else
                                dtFinance.Rows[dtFinance.Rows.Count - 1][14] = dtExpenses.Rows[indexExpenses][2];
                            if (dtExpenses.Rows[indexExpenses][7].ToString() == "")
                                dtFinance.Rows[dtFinance.Rows.Count - 1][15] = dtExpenses.Rows[indexExpenses][3];
                            else
                                dtFinance.Rows[dtFinance.Rows.Count - 1][15] = dtExpenses.Rows[indexExpenses][3] + " ( " + dtExpenses.Rows[indexExpenses][7] + " ) ";
                            dtFinance.Rows[dtFinance.Rows.Count - 1][16] = dtExpenses.Rows[indexExpenses][6];
                           
                            indexExpenses++;
                        }
                        catch { }
                    }
                    if(!Double.TryParse(dtFinance.Rows[dtFinance.Rows.Count - 1][3].ToString(), out currentDefeniteAmount))
                        currentDefeniteAmount = 0;
                    if (!Double.TryParse(dtFinance.Rows[dtFinance.Rows.Count - 1][8].ToString(), out currentForecastAmount))
                        currentForecastAmount = 0;
                    if (!Double.TryParse(dtFinance.Rows[dtFinance.Rows.Count - 1][14].ToString(), out currentExpensesAmount))
                        currentExpensesAmount = 0;
                    if (!Double.TryParse(dtFinance.Rows[dtFinance.Rows.Count - 1][13].ToString(), out currentExpectedExpensesAmount))
                        currentExpectedExpensesAmount = 0;
                    
                    dtFinance.Rows[dtFinance.Rows.Count - 1][17] = accountLastBalance + currentDefeniteAmount + currentForecastAmount - currentExpensesAmount;
                    dtFinance.Rows[dtFinance.Rows.Count - 1][18] = accountLastBalance60 + currentDefeniteAmount + ScenarioPercent * currentForecastAmount - currentExpensesAmount;
                    dtFinance.Rows[dtFinance.Rows.Count - 1][19] = accountLastSumBalance + currentDefeniteAmount + ScenarioPercent * currentForecastAmount - (currentExpensesAmount + currentExpectedExpensesAmount);
                    accountLastBalance += currentDefeniteAmount + currentForecastAmount - currentExpensesAmount;
                    accountLastBalance60 += currentDefeniteAmount + ScenarioPercent * currentForecastAmount - currentExpensesAmount;
                    accountLastSumBalance += currentDefeniteAmount + ScenarioPercent * currentForecastAmount - (currentExpensesAmount + currentExpectedExpensesAmount);

                }
                startdate = startdate.AddDays(1);
            }
            //while (dtFinance.Rows[0][0].ToString() == "" || Convert.ToDateTime(dtFinance.Rows[0][0]) < DateTime.Now)
                //dtFinance.Rows.RemoveAt(0);
            dataGridViewFinance.DataSource = dtFinance;
            double sumSuppliers = 0, sumBoxes = 0;
            DateTime maxDate, curDate = updatedDate;
            foreach (DataGridViewRow row in dataGridViewFinance.Rows)
            {
                if (row.Cells[0].Value.ToString() != "")
                    curDate = DateTime.Parse(row.Cells[0].Value.ToString());
                if (row.Cells[11].Value.ToString() != String.Empty)
                {
                    if (row.Cells[13].Value.ToString() != String.Empty && row.Cells[15].Value.ToString() != "קופות וקרנות")
                    {
                        maxDate = new DateTime(updatedDate.Year, updatedDate.AddMonths(2).Month, 1);
                        if ((int)row.Cells[16].Value == 1)
                            sumSuppliers += (double)row.Cells[13].Value;
                        else
                        {
                            if (curDate <= maxDate.AddDays(-1))
                                sumSuppliers += (double)row.Cells[13].Value;
                        }
                    }
                    else if (row.Cells[14].Value.ToString() != String.Empty && row.Cells[15].Value.ToString() != "קופות וקרנות")
                    {
                        maxDate = new DateTime(updatedDate.Year, updatedDate.AddMonths(2).Month, 1);
                        if ((int)row.Cells[16].Value == 1)
                            sumSuppliers += (double)row.Cells[14].Value;
                        else
                        {
                            if (curDate <= maxDate.AddDays(-1))
                                sumSuppliers += (double)row.Cells[14].Value;
                        }
                    }
                    if (row.Cells[13].Value.ToString() != String.Empty && row.Cells[15].Value.ToString() == "קופות וקרנות")
                        sumBoxes += (double)row.Cells[13].Value;
                    else if (row.Cells[14].Value.ToString() != String.Empty && row.Cells[15].Value.ToString() == "קופות וקרנות")
                        sumBoxes += (double)row.Cells[14].Value;
                }
            }
            textBoxAmountSuppliers.Text = String.Format("{0:0,0.00}", sumSuppliers);
            textBoxAmountBox.Text = String.Format("{0:0,0.00}", sumBoxes);
            textBoxTotalExpenses.Text = String.Format("{0:0,0.00}", sumSuppliers + sumBoxes);
            styleFinance();
            
        }
        private void styleFinance()
        {
            dataGridViewFinance.Columns[0].HeaderText = "תאריך";

            dataGridViewFinance.Columns[1].Visible = false;
            dataGridViewFinance.Columns[2].HeaderText = "בוצע";
            dataGridViewFinance.Columns[3].HeaderText = "תקבול ודאי";
            dataGridViewFinance.Columns[4].HeaderText = "מס' חשבון";
            dataGridViewFinance.Columns[5].HeaderText = "תיאור";

            dataGridViewFinance.Columns[6].Visible = false;
            dataGridViewFinance.Columns[7].HeaderText = "בוצע";
            dataGridViewFinance.Columns[8].HeaderText = "תקבול צפוי";
            dataGridViewFinance.Columns[9].HeaderText = "מס' חשבון";
            dataGridViewFinance.Columns[10].HeaderText = "תיאור";

            dataGridViewFinance.Columns[11].Visible = false;
            dataGridViewFinance.Columns[12].HeaderText = "בוצע";
            dataGridViewFinance.Columns[13].HeaderText = "הוצאה לאישור";
            dataGridViewFinance.Columns[14].HeaderText = "הוצאה מאושרת";
            dataGridViewFinance.Columns[15].HeaderText = "תיאור";
            dataGridViewFinance.Columns[16].Visible = false;

            dataGridViewFinance.Columns[17].HeaderText = "תרחיש 100%";
            dataGridViewFinance.Columns[18].HeaderText = "תרחיש פסימי";
            dataGridViewFinance.Columns[19].HeaderText = "תרחיש פסימי + הוצאות מלאות";

            if (showSums)
            {
                tableLayoutFinance.Visible = true;

                dataGridViewFinance.Columns[12].Visible = true;
                dataGridViewFinance.Columns[13].Visible = true;
                dataGridViewFinance.Columns[14].Visible = true;
                dataGridViewFinance.Columns[15].Visible = true;

                dataGridViewFinance.Columns[17].Visible = true;
                dataGridViewFinance.Columns[18].Visible = true;
                dataGridViewFinance.Columns[19].Visible = true;
            }
            else
            {
                tableLayoutFinance.Visible = false;

                dataGridViewFinance.Columns[12].Visible = false;
                dataGridViewFinance.Columns[13].Visible = false;
                dataGridViewFinance.Columns[14].Visible = false;
                dataGridViewFinance.Columns[15].Visible = false;

                dataGridViewFinance.Columns[17].Visible = false;
                dataGridViewFinance.Columns[18].Visible = false;
                dataGridViewFinance.Columns[19].Visible = false;
            }

            dataGridViewFinance.Columns[0].Width = 70;

            dataGridViewFinance.Columns[2].Width = 40;
            dataGridViewFinance.Columns[7].Width = 40;
            dataGridViewFinance.Columns[12].Width = 40;
            
            dataGridViewFinance.Columns[3].Width = 70;
            dataGridViewFinance.Columns[8].Width = 70;
            dataGridViewFinance.Columns[13].Width = 70;
            dataGridViewFinance.Columns[14].Width = 70;

            dataGridViewFinance.Columns[17].Width = 80;
            dataGridViewFinance.Columns[18].Width = 80;
            dataGridViewFinance.Columns[19].Width = 80;

            dataGridViewFinance.Columns[4].Width = 40;
            dataGridViewFinance.Columns[9].Width = 40;

            refreshFinanceColumnsWidth();

            dataGridViewFinance.Columns[0].ReadOnly = true;
            dataGridViewFinance.Columns[3].ReadOnly = true;
            dataGridViewFinance.Columns[4].ReadOnly = true;
            dataGridViewFinance.Columns[5].ReadOnly = true;
            dataGridViewFinance.Columns[6].ReadOnly = true;

            dataGridViewFinance.Columns[8].ReadOnly = true;
            dataGridViewFinance.Columns[9].ReadOnly = true;
            dataGridViewFinance.Columns[10].ReadOnly = true;
            dataGridViewFinance.Columns[11].ReadOnly = true;

            dataGridViewFinance.Columns[13].ReadOnly = true;
            dataGridViewFinance.Columns[14].ReadOnly = true;
            dataGridViewFinance.Columns[15].ReadOnly = true;
            dataGridViewFinance.Columns[17].ReadOnly = true;
            dataGridViewFinance.Columns[18].ReadOnly = true;
            dataGridViewFinance.Columns[19].ReadOnly = true;

            foreach (DataGridViewColumn col in dataGridViewFinance.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            if (dataGridViewFinance.SelectedRows.Count > 0)
                dataGridViewFinance.SelectedRows[0].Selected = false;
            if (lastFinanceSelectedRow >= 0)
            {
                try
                {
                    dataGridViewFinance.FirstDisplayedScrollingRowIndex = lastFinanceScrolledRow;
                    dataGridViewFinance.Rows[lastFinanceSelectedRow].Selected = true;
                }
                catch { dataGridViewFinance.Rows[0].Selected = true; }
            }
        }
        private void refreshFinanceColumnsWidth()
        {
            try
            {
                int totalWidth = dataGridViewFinance.Columns[0].Width * 1 + dataGridViewFinance.Columns[2].Width * 3 + dataGridViewFinance.Columns[3].Width * 4 + dataGridViewFinance.Columns[17].Width * 3 + dataGridViewFinance.Columns[4].Width * 2;

                dataGridViewFinance.Columns[5].Width = (dataGridViewFinance.Width - totalWidth) / 3 - 6;
                dataGridViewFinance.Columns[10].Width = (dataGridViewFinance.Width - totalWidth) / 3 - 6;
                dataGridViewFinance.Columns[15].Width = (dataGridViewFinance.Width - totalWidth) / 3 - 6;
            }
            catch(Exception ex)
            {
                File.AppendAllText(MyFiles.ExceptionFile, ex.Message + "\r\n");
            }
        }
        private void panelFinance_VisibleChanged(object sender, EventArgs e)
        {
            if (!panelFinance.Visible)
                return;
            showFinance();
        }
        private void initFinanceTable(ref DataTable dt)
        {
            dt.Columns.Add("תאריך", typeof(DateTime));
            dt.Columns.Add("idDefinite", typeof(string));
            dt.Columns.Add("ו", typeof(bool));
            dt.Columns.Add("ודאי", typeof(double));//3
            dt.Columns.Add("חשבון ודאי", typeof(string));
            dt.Columns.Add("סעיף ודאי", typeof(string));
            dt.Columns.Add("idForecast", typeof(string));
            dt.Columns.Add("צ", typeof(bool));
            dt.Columns.Add("צפוי", typeof(double));//8
            dt.Columns.Add("חשבון צפוי", typeof(string));
            dt.Columns.Add("סעיף צפוי", typeof(string));
            dt.Columns.Add("idExpense", typeof(string));
            dt.Columns.Add("ה", typeof(bool));
            dt.Columns.Add("סכום לאישור", typeof(double));
            dt.Columns.Add("סכום", typeof(double));
            dt.Columns.Add("סעיף", typeof(string));
            dt.Columns.Add("סוג", typeof(int));
            dt.Columns.Add("תרחיש 100%", typeof(double));
            dt.Columns.Add("תרחיש 60%", typeof(double));
            dt.Columns.Add("אופטימי", typeof(double));
        }
        private int countDatesInDataTableFromIndex(int startIndex, DateTime date, DataTable dt)
        {
            int i = startIndex;
            int count = 0;
            if (startIndex > dt.Rows.Count - 1)
                return 0;
            while ((DateTime)dt.Rows[i][1] == date)
            {
                count++;
                i++;
                if (i > dt.Rows.Count - 1)
                    break;
            }
            return count;
        }
        private int findMax(int a, int b, int c)
        {
            int max = a;
            if (b > max)
                max = b;
            if (c > max)
                max = c;
            return max;
        }
        private DateTime findMinDate(DateTime a, DateTime b, DateTime c)
        {
            List<DateTime> list = new List<DateTime>();
            list.Add(a);
            list.Add(b);
            list.Add(c);

            return list.Min();
        }
        private DateTime findMaxDate(DateTime a, DateTime b, DateTime c)
        {
            List<DateTime> list = new List<DateTime>();
            list.Add(a);
            list.Add(b);
            list.Add(c);

            return list.Max();
        }

        private void dataGridViewFinance_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridViewFinance.Columns["ודאי"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewFinance.Columns["צפוי"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewFinance.Columns["סכום לאישור"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewFinance.Columns["סכום"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewFinance.Columns["תרחיש 100%"].Index)
                {
                    if ((double)e.Value < 0)
                    {
                        e.Value = Math.Abs((double)e.Value);
                        e.Value = String.Format("{0:0,0.00}", e.Value) + "-";
                    }
                    else
                        e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewFinance.Columns["תרחיש 60%"].Index)
                {
                    if ((double)e.Value < 0)
                    {
                        e.Value = Math.Abs((double)e.Value);
                        e.Value = String.Format("{0:0,0.00}", e.Value) + "-";
                    }
                    else
                        e.Value = String.Format("{0:0,0.00}", e.Value);
                    
                    e.FormattingApplied = true;
                }
                if (e.ColumnIndex == dataGridViewFinance.Columns["אופטימי"].Index)
                {
                    if ((double)e.Value < 0)
                    {
                        e.Value = Math.Abs((double)e.Value);
                        e.Value = String.Format("{0:0,0.00}", e.Value) + "-";
                    }
                    else
                        e.Value = String.Format("{0:0,0.00}", e.Value);
                    
                    e.FormattingApplied = true;
                }
                /*if (e.ColumnIndex == dataGridView1.Columns["percentageSubmitted"].Index)
                {
                    e.Value = String.Format("{0:0,0.00}", e.Value);
                    e.FormattingApplied = true;
                }*/
            }
            catch { }
        }
        private void dataGridViewFinance_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridViewFinance.Columns[e.ColumnIndex].Name == "ו" && dataGridViewFinance.Rows[e.RowIndex].Cells[1].Value.ToString() == "")
                {
                    e.PaintBackground(e.ClipBounds, true);
                    e.Handled = true;
                }
                if (dataGridViewFinance.Columns[e.ColumnIndex].Name == "צ" && dataGridViewFinance.Rows[e.RowIndex].Cells[6].Value.ToString() == "")
                {
                    e.PaintBackground(e.ClipBounds, true);
                    e.Handled = true;
                }
                if (dataGridViewFinance.Columns[e.ColumnIndex].Name == "ה" && dataGridViewFinance.Rows[e.RowIndex].Cells[11].Value.ToString() == "")
                {
                    e.PaintBackground(e.ClipBounds, true);
                    e.Handled = true;
                }
            }
            if (e.ColumnIndex == 0 || e.ColumnIndex == 5 || e.ColumnIndex == 10 || e.ColumnIndex == 15)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                using (Pen p = new Pen(this.dataGridView1.GridColor))
                {
                    e.Graphics.DrawLine(p, e.CellBounds.X, e.CellBounds.Top,
                        e.CellBounds.X, e.CellBounds.Bottom);
                }
                e.Handled = true;
            }

        }
        private void dataGridViewFinance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewFinance.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dataGridViewFinance_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            lastFinanceSelectedRow = e.RowIndex;
            try
            {
                if ((bool)dataGridViewFinance.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    switch (e.ColumnIndex)
                    {
                        case 2:
                            mainInterface.Update("UPDATE bills SET bank=1 WHERE id="+dataGridViewFinance.Rows[e.RowIndex].Cells[1].Value.ToString());
                            break;
                        case 7:
                            mainInterface.Update("UPDATE bills SET bank=1 WHERE id="+dataGridViewFinance.Rows[e.RowIndex].Cells[6].Value.ToString());
                            break;
                        case 12:
                            mainInterface.Update("UPDATE expenses SET bank=1 WHERE id="+dataGridViewFinance.Rows[e.RowIndex].Cells[11].Value.ToString());
                            break;
                    }
                }
                else
                {
                    switch (e.ColumnIndex)
                    {
                        case 2:
                            mainInterface.Update("UPDATE bills SET bank=0 WHERE id=" + dataGridViewFinance.Rows[e.RowIndex].Cells[1].Value.ToString());
                            break;
                        case 7:
                            mainInterface.Update("UPDATE bills SET bank=0 WHERE id=" + dataGridViewFinance.Rows[e.RowIndex].Cells[6].Value.ToString());
                            break;
                        case 12:
                            mainInterface.Update("UPDATE expenses SET bank=0 WHERE id=" + dataGridViewFinance.Rows[e.RowIndex].Cells[11].Value.ToString());
                            break;
                    }
                }
                showFinance();
            }
            catch
            {

            }
        }

        private void buttonBankUpdate_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            bool allow = true;

            DateTime currentDate = (DateTime)dataGridViewFinance.Rows[rowIndex].Cells[0].Value;
            DateTime newDate = dateTimePickerLastUpdate.Value;

            if (newDate >= currentDate)
            {
                while (dataGridViewFinance.Rows[rowIndex].Cells[0].Value.ToString() == String.Empty|| (DateTime)dataGridViewFinance.Rows[rowIndex].Cells[0].Value < newDate)
                {
                    if (((dataGridViewFinance.Rows[rowIndex].Cells[2].Value.ToString() != String.Empty) && !(bool)dataGridViewFinance.Rows[rowIndex].Cells[2].Value) ||
                        ((dataGridViewFinance.Rows[rowIndex].Cells[7].Value.ToString() != String.Empty) &&!(bool)dataGridViewFinance.Rows[rowIndex].Cells[7].Value) ||
                        ((dataGridViewFinance.Rows[rowIndex].Cells[12].Value.ToString() != String.Empty) &&!(bool)dataGridViewFinance.Rows[rowIndex].Cells[12].Value))
                    {
                        allow = false;
                        break;
                    }
                    rowIndex++;
                }
            }
            if (!allow)
            {
                MessageBox.Show("יש תקבולים או תשלומים שעדיין לא התבצעו בבנק", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                dateTimePickerLastUpdate.Value = currentDate;
                return;
            }
            double amount;
            double amountF;
            double amountP;
            //double amountS;
            double amountC;
            double amountB;
            if (!Double.TryParse(textBoxBankAmount.Text, out amount))
                return;
            if (!Double.TryParse(textBoxAmountForeign.Text, out amountF))
                return;
            if (!Double.TryParse(textBoxAmountPapers.Text, out amountP))
                return;
            //if (!Double.TryParse(textBoxAmountSuppliers.Text, out amountS))
                //return;
            if (!Double.TryParse(textBoxAmountCalc.Text, out amountC))
                return;
            //if (!Double.TryParse(textBoxAmountBox.Text, out amountB))
                //return;
            mainInterface.InsertSql("INSERT INTO bankupdates (date,amount,amountForeign,amountPapers,amountCalc) VALUES ('" + MyUtills.dateToSQL(dateTimePickerLastUpdate.Value) + "'," + amount.ToString() + "," + amountF.ToString() + "," + amountP.ToString() + "," + amountC.ToString() + ")");
            showFinance();
        }
        private void dataGridViewFinance_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int col = e.ColumnIndex;
                string id;
                List<string> ids = new List<string>();
                lastFinanceSelectedRow = e.RowIndex;
                lastFinanceScrolledRow = dataGridViewFinance.FirstDisplayedScrollingRowIndex;
                if (col == 13 || col == 14)
                {
                    //id = dataGridViewFinance.Rows[e.RowIndex].Cells[11].Value.ToString();
                    foreach (DataGridViewRow row in dataGridViewFinance.SelectedRows)
                        ids.Add(row.Cells[11].Value.ToString());
                    if (ids.Count > 0)
                    {
                        formApproveExpense = new Form_ApproveExpense(this, mainInterface, ids);
                        formApproveExpense.ShowDialog();
                    }
                }
                else if (col == 3 || col == 4 || col == 5)
                {
                    id = dataGridViewFinance.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if (id != String.Empty)
                    {
                        formEditIncome = new Form_EditIncome(this, mainInterface, id, true);
                        formEditIncome.ShowDialog();
                    }
                }
                else if (col == 8 || col == 9 || col == 10)
                {
                    id = dataGridViewFinance.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (id != String.Empty)
                    {
                        formEditIncome = new Form_EditIncome(this, mainInterface, id, false);
                        formEditIncome.ShowDialog();
                    }
                }
            }
        }
        private void ToolStripMenuItemFinanceView_Click(object sender, EventArgs e)
        {
            panelFinance.SendToBack();
            panelFinance.Visible = false;
        }
        private void ToolStripMenuItemView_Click(object sender, EventArgs e)
        {
            if (!userEnter)
            {
                formUserEnter = new Form_UserEnter(this, mainInterface);
                formUserEnter.ShowDialog();
            }
            if (userEnter)
            {
                panelFinance.BringToFront();
                panelFinance.Visible = true;
            }
        }

        private void ToolStripMenuItemRefreshFinance_Click(object sender, EventArgs e)
        {
            try
            {
                lastFinanceScrolledRow = dataGridViewFinance.FirstDisplayedScrollingRowIndex;
                lastFinanceSelectedRow = dataGridViewFinance.SelectedRows[0].Index;
            }
            catch { }
            showFinance();
        }
        public void financeGoToBill(string id)
        {
            panelFinance.SendToBack();
            panelFinance.Visible = false;
            tabControlFilters.SelectedTab = tabPageBill;
            searchById = true;
            searchIdQuery = "bills_projects.idBill=" + id;
            buttonSearchBills.PerformClick();
        }

        private void labelHideShow_Click(object sender, EventArgs e)
        {
            if (showSums)
            {
                showSums = false;
                dataGridViewFinance.Columns[12].Visible = false;
                dataGridViewFinance.Columns[13].Visible = false;
                dataGridViewFinance.Columns[14].Visible = false;
                dataGridViewFinance.Columns[15].Visible = false;

                dataGridViewFinance.Columns[17].Visible = false;
                dataGridViewFinance.Columns[18].Visible = false;
                dataGridViewFinance.Columns[19].Visible = false;
                tableLayoutFinance.Visible = false;
            }
            else
            {
                showSums = true;
                dataGridViewFinance.Columns[12].Visible = true;
                dataGridViewFinance.Columns[13].Visible = true;
                dataGridViewFinance.Columns[14].Visible = true;
                dataGridViewFinance.Columns[15].Visible = true;

                dataGridViewFinance.Columns[17].Visible = true;
                dataGridViewFinance.Columns[18].Visible = true;
                dataGridViewFinance.Columns[19].Visible = true;
                tableLayoutFinance.Visible = true;
            }

        }

        private void textBoxDetailsProjectNumber_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string theText = textBox.Text;                
                if (theText == currentProject.Number)
                {
                    // number possible
                    pictureBoxProjectNumberV.Visible = true;
                    pictureBoxProjectNumberX.Visible = false;
                }
                else
                {
                    DataTable dt = mainInterface.Select("Select idProjects from projects WHERE projectNumber='" + theText + "'").Tables[0];
                    if (dt.Rows.Count > 0 || theText == String.Empty)
                    {
                        // number not possible
                        pictureBoxProjectNumberV.Visible = false;
                        pictureBoxProjectNumberX.Visible = true;
                    }
                    else
                    {
                        // number possible
                        pictureBoxProjectNumberV.Visible = true;
                        pictureBoxProjectNumberX.Visible = false;
                    }
                }
                
            }
            else
            {

            }
        }
    }
}
