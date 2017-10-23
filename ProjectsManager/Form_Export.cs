using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office;
using Word = Microsoft.Office.Interop.Word;

namespace ProjectsManager
{
    public partial class Form_Export : Form
    {
        DBinterface localInterface;
        int idBill;
        double billHours;
        double billHourCost;
        enum PROJECTTYPE
        {
            NORMAL,      //0
            NORMAL_DESC, //1
            GOVERMENT,   //2
            ONE,         //3
            MULTI,       //4
        };
        public Form_Export(DBinterface dbInterface, int idbill)
        {
            InitializeComponent();
            localInterface = dbInterface;
            idBill = idbill;
            textBoxExportLibrary.Text = dbInterface.exportLibrary;
        }

        private void CreateTemplate(string outputLibrary, int projectType)
        {
            string query = "SELECT   bills.billDate," +
                                    "bills.idBill," +
                                    "projects.projectNumber," +
                                    "projects.approverName, " +
                                    "customers.address, " +
                                    "projects.approverFax, " +
                                    "projects.projectName, " +
                                    "customers.name, " +
                                    "projects.amount, " +
                                    "bills_projects.progress, " +
                                    "projects.priceIndex, " +
                                    "projects.priceIndexDate, " +
                                    "projects.amountInfo, " +
                                    "bills.amount, " +
                                    "bills.currentIndex, " +
                                    "billsPaid.totalPaid, " +
                                    "bills.increase, " +
                                    "bills.vat, " +
                                    "projects.contractNumber, " +
                                    "bills.billPart, " +
                                    "bills_projects.idProject, " +
                                    "bills.billNotes, " +
                                    "projects.payerAddress, " +
                                    "projects.approverAddress, " +
                                    "projects.approverEmail, " +
                                    "bills.isClosed, " +
                                    "bills.invoiceNumber, " +
                                    "bills.vatDate, " +
                                    "projects.toSubmit, " +
                                    "billsPaid.billSum, " +
                                    "bills_projects.percent, " +
                                    "bills_projects.progress, " +
                                    "bills.id " +
                                    "FROM bills " +
                                "LEFT JOIN bills_projects ON bills.id=bills_projects.idBill " +
                                "LEFT JOIN projects ON bills_projects.idProject=projects.idProjects " +
                                "LEFT JOIN customers ON projects.idCustomer=customers.idCustomer " +
                                "LEFT JOIN (SELECT idProjects AS id,SUM(bills_projects.paid/100*projects.amount) AS totalPaid,SUM(bills.amount) AS billSum FROM projects " +
                                "LEFT JOIN bills_projects ON projects.idProjects=bills_projects.idProject LEFT JOIN bills ON bills.id=bills_projects.idBill GROUP BY projects.idProjects) AS billsPaid ON bills_projects.idProject=billsPaid.id " +
                                "WHERE bills_projects.idBill=" + idBill.ToString();
            DataTable dt = localInterface.Select(query).Tables[0];
            double sum = 0,sumPaid = 0;
            DataTable dt1 = new DataTable();
            if (dt.Rows.Count > 1)
            {
                projectType = (int)PROJECTTYPE.MULTI;
                string query1 = "SELECT projectNumber FROM projects WHERE idProjects='" + dt.Rows[0][20].ToString() + "'";
                string result1 = ((int)Double.Parse(localInterface.Select(query1).Tables[0].Rows[0][0].ToString())).ToString();
                query = "SELECT amount,toSubmit,projectName FROM projects WHERE projectNumber LIKE '%" + result1 + ".%' OR projectNumber='" + result1 + "'";
                dt1 = localInterface.Select(query).Tables[0];
                foreach (DataRow row in dt1.Rows)
                    sum += (double)row[0] * ((double)row[1]/100.0);
            }
            MyBill bill = new MyBill();
            MyProject project = new MyProject();
            MyCustomer customer = new MyCustomer();
            bill.BillDate = Convert.ToDateTime(dt.Rows[0][0]);
            bill.IdBill = dt.Rows[0][1].ToString();
            project.Number = dt.Rows[0][2].ToString();
            project.ApproverName = dt.Rows[0][3].ToString();
            customer.Address = dt.Rows[0][4].ToString();
            project.ApproverFax = dt.Rows[0][5].ToString();
            project.Name = dt.Rows[0][6].ToString();
            customer.Name = dt.Rows[0][7].ToString();
            project.Amount = Convert.ToDouble(dt.Rows[0][8]);
            project.ToSumbit = Convert.ToInt16(dt.Rows[0][9]);
            
            if (dt.Rows[0][10].ToString() != String.Empty)
                project.PriceIndex = Convert.ToDouble(dt.Rows[0][10]);
            if (dt.Rows[0][11].ToString() != String.Empty)
                project.PriceIndexDate = Convert.ToDateTime(dt.Rows[0][11]);
            
            project.AmountNotes = dt.Rows[0][12].ToString();
            bill.Amount = Convert.ToDouble(dt.Rows[0][13]);
            if (dt.Rows[0][14].ToString() != String.Empty)
                bill.CurrentIndex = Convert.ToDouble(dt.Rows[0][14]);


            int selectedId = int.Parse(dt.Rows[0][32].ToString());
            string query11 = "SELECT projects.idProjects," +
                                           "projects.projectNumber," +
                                           "projects.amount," +
                                           "projects.amount " +
                                           "FROM projects INNER JOIN bills_projects ON projects.idProjects=bills_projects.idProject WHERE bills_projects.idBill='" + selectedId.ToString() + "'";

            DataTable dtProjectNumber = localInterface.Select(query11).Tables[0];
            double billTotal = 0, billPaid = 0;
            foreach (DataRow dr in dtProjectNumber.Rows)
            {
                billTotal += projectPartAmount((int)dr[0], selectedId);
                billPaid += projectPartPaid((int)dr[0], selectedId);
            }

            bill.Amount = billTotal;



            string totalPaidAmount = " ", totalPaidText = " ", totalPaidNIS = " ";
            foreach (DataRow row in dt.Rows)
            {
                sumPaid += Convert.ToDouble(row[15]);
            }
            project.TotalPaid = sumPaid;
            if (project.TotalPaid > 0)
            {
                totalPaidAmount = "(" + String.Format("{0:0,0.00}", project.TotalPaid) + ")";
                totalPaidText = "שולם מצטבר";
                totalPaidNIS = "₪";
            }
            bill.Increase = Convert.ToDouble(dt.Rows[0][16]);
            string increaseText = " ", increaseSum =" ", increaseNIS = " ";
            if (bill.Increase > 0)
            {
                increaseText = "התייקרות";//"התייקרות: " +String.Format("{0:0,0.00}", bill.Amount) + " * " + " ( " + " 1 - " + String.Format("{0:0.0}", project.PriceIndex) + " / " + String.Format("{0:0.0}", bill.CurrentIndex) + "  ) ";
                increaseSum = String.Format("{0:0,0.00}", bill.Increase);
                increaseNIS = "₪";
            }
            else
                increaseSum = "__________";
            //DataTable dt2 = localInterface.Select("SELECT vat FROM vat WHERE date<='" + MyUtills.dateToSQL(bill.BillDate) + "' ORDER BY date DESC").Tables[0];
            //bill.Vat = Convert.ToDouble(dt2.Rows[0][0]);
            if (dt.Rows[0][25].ToString() == "True")
                bill.IsClosed = 1;
            else
                bill.IsClosed = 0;
            bill.InvoiceNumber = dt.Rows[0][26].ToString();
            bill.VatDate = Convert.ToDateTime(dt.Rows[0][27]);
            
            DataTable dt2 = localInterface.Select("SELECT vat,date FROM vat ORDER BY date DESC").Tables[0];
            foreach (DataRow dro in dt2.Rows)
            {
                bill.Vat = Convert.ToDouble(dro[0]);
                if (MyUtills.dateFromSQL(dro[1].ToString()) <= bill.VatDate)
                    break;
            }
            if (bill.IsClosed == 0 && bill.InvoiceNumber == string.Empty)
                bill.Vat = Convert.ToDouble(dt2.Rows[0][0]);

            project.ContractNumber = dt.Rows[0][18].ToString();
            bill.BillPart = Convert.ToInt16(dt.Rows[0][19]);
            bill.ProjectId = Convert.ToInt16(dt.Rows[0][20]);
            bill.BillNotes = dt.Rows[0][21].ToString();
            project.PayerAddress = dt.Rows[0][22].ToString();
            project.ApproverAddress = dt.Rows[0][23].ToString();
            project.ApproverEmail = dt.Rows[0][24].ToString();

            string openbillText = " ";
            string concat = "הוגש וטרם שולם בחשבון חלקי ";
            double openSum = 0;
            string openSumText = " ", openSumNIS = " ";

            if (bill.BillPart > 1)
            {
                string sqlCmd = "SELECT bills_projects.idProject,bills.amount,bills.paid,bills.billPart " + 
                                "FROM bills " + 
                                "INNER JOIN bills_projects ON bills.id=bills_projects.idBill " + 
                                "INNER JOIN projects ON bills_projects.idProject=projects.idProjects " + 
                                "WHERE projects.idProjects=" + bill.ProjectId + " AND bills.id<>" + idBill.ToString() + " AND projects.isClosed=0";
                DataTable dt11 = localInterface.Select(sqlCmd).Tables[0];
                for (int i = 0; i < dt11.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dt11.Rows[i][1]) > Convert.ToDouble(dt11.Rows[i][2]))
                    {
                        openbillText += concat + dt11.Rows[i][3].ToString();
                        concat = "+";
                        openSum += Convert.ToDouble(dt11.Rows[i][1]) - Convert.ToDouble(dt11.Rows[i][2]);
                    }
                }
                if (openSum > 0)
                {
                    openSumText = String.Format("{0:0,0.00}", openSum);
                    openSumNIS = "₪";
                }
                else
                {
                    openSumText = " ";
                    openSumNIS = " ";
                }
            }

            



            Object oMissing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            Word.Application oWord = new Word.Application();
            Word.Document oWordDoc = new Word.Document();
            //oWord.Visible = true;


            if (projectType == (int)PROJECTTYPE.NORMAL)
            {
                #region BillNormal
                if (!File.Exists(localInterface.importLibrary + "\\" + "TempBillNormal.dot"))
                {
                    MessageBox.Show("קובץ תבנית לא נמצא", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
                Object oTemplatePath = localInterface.importLibrary + "\\" + "TempBillNormal.dot";
                try
                {
                    oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("התוכנה נתקלה בבעיה עם תוכנת האופיס המותקנת במחשב זה" + "\r\n" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                    return;
                }
                foreach (Word.Field myMergeField in oWordDoc.Fields)
                {
                    //iTotalFields++;
                    Word.Range rngFieldCode = myMergeField.Code;
                    String fieldText = rngFieldCode.Text;
                    String field = String.Empty;
                    if (fieldText.StartsWith(" MERGEFIELD"))
                    {
                        Int32 endMerge = fieldText.IndexOf("\\");
                        Int32 fieldNameLength = fieldText.Length - endMerge;
                        String fieldName = fieldText.Substring(11, endMerge - 11);
                        fieldName = fieldName.Trim();
                        myMergeField.Select();
                       
                        switch (fieldName)
                        {
                            case "BillDate":
                                field = bill.BillDate.ToShortDateString();
                                break;
                            case "Reference":
                                field = project.Number + "-" + bill.IdBill;
                                break;
                            case "Customer":
                                field = customer.Name;
                                break;
                            case "Approver":
                                field = project.ApproverName;
                                break;
                            case "Address":
                                field = project.ApproverAddress;
                                break;
                            case "Fax":
                                field = project.ApproverFax;
                                break;
                            case "Email":
                                field = project.ApproverEmail;
                                break;
                            case "ProjectName":
                                field = project.Name;
                                break;
                            case "BillPart":
                                field = bill.BillPart.ToString();
                                break;
                            case "AmountNotes":
                                field = bill.BillNotes;
                                break;
                            case "ProjectAmount":
                                field = String.Format("{0:0,0.00}", project.Amount);
                                break;
                            case "Submitted":
                                field = String.Format("{0:0}", project.ToSumbit);
                                break;
                            case "Index":
                                if (project.PriceIndex > 0 && bill.CurrentIndex > 0)
                                    field = "מדד בסיס: " + " תאריך " + project.PriceIndexDate.Month.ToString().PadLeft(2, '0') + "/" + project.PriceIndexDate.Year.ToString().PadLeft(2, '0') + ", ערך " + String.Format("{0:0.0}", project.PriceIndex);
                                else
                                    field = " ";
                                break;
                            case "CurrentIndex":
                                DateTime temp;
                                if (bill.BillDate.Day >= 15)
                                    temp = bill.BillDate.AddMonths(-1);
                                else
                                    temp = bill.BillDate.AddMonths(-2);
                                string date = temp.Month.ToString().PadLeft(2, '0') + "/" + temp.Year.ToString();
                                if (project.PriceIndex > 0 && bill.CurrentIndex > 0)
                                    field = "מדד קובע: " + " תאריך " + date + ", ערך " + String.Format("{0:0.0}", bill.CurrentIndex);
                                else
                                    field = " ";
                                break;
                            case "ProAmTBP":
                                field = String.Format("{0:0,0.00}", project.Amount * project.ToSumbit / 100);
                                break;
                            case "AmountPaid":
                                field = totalPaidAmount;
                                break;
                            case "APnis":
                                field = totalPaidNIS;
                                break;
                            case "AmountPaidText":
                                field = totalPaidText;
                                break;
                            case "OpenBillAm":
                                field = openSumText;
                                break;
                            case "OpenBillText":
                                field = openbillText;
                                break;
                            case "OBAnis":
                                field = openSumNIS;
                                break;
                            case "IncAmount":
                                field = increaseSum;
                                break;
                            case "IncreaseText":
                                field = increaseText;
                                break;
                            case "IAnis":
                                field = increaseNIS;
                                break;
                            case "TotalAmount":
                                field = String.Format("{0:0,0.00}", bill.Amount + bill.Increase);
                                break;
                            case "VatAmount":
                                field = String.Format("{0:0,0.00}", (bill.Amount + bill.Increase) * (bill.Vat / 100));
                                break;
                            case "AmWithVat":
                                field = String.Format("{0:0,0.00}", (bill.Amount + bill.Increase) * (bill.Vat / 100 + 1));
                                break;
                            case "VAT":
                                field = String.Format("{0:0,0.00}", bill.Vat);
                                break;
                            case "BillAmount":
                                field = String.Format("{0:0,0.00}", bill.Amount);
                                break;
                        }
                        //if (field == String.Empty)
                            //field = "לא ידוע";
                        oWord.Selection.TypeText(field);
                    }
                }
                string file = outputLibrary + "\\" + project.Number + "-" + bill.IdBill + ".doc";
                if (File.Exists(file))
                    if (MessageBox.Show("קובץ בשם זה כבר נמצא, האם להחליף ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) != DialogResult.Yes)
                        return;
                try
                {
                    oWordDoc.SaveAs(file);
                }
                catch 
                {
                    MessageBox.Show("יצירת המסמך נכשלה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    this.Close();
                    return;
                }
                //oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                
                MessageBox.Show("המסמך נוצר בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                oWord.Visible = true;
                this.Close();
                
                #endregion
            }
            else if (projectType == (int)PROJECTTYPE.NORMAL_DESC)
            {
                #region BillNormalDescription
                if (!File.Exists(localInterface.importLibrary + "\\" + "TempBillNormalDesc.dot"))
                {
                    MessageBox.Show("קובץ תבנית לא נמצא", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
                Object oTemplatePath = localInterface.importLibrary + "\\" + "TempBillNormalDesc.dot";
                try
                {
                    oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("התוכנה נתקלה בבעיה עם תוכנת האופיס המותקנת במחשב זה" + "\r\n" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                    return;
                }

                string sqlCmd = "SELECT " +
                                    "bills.billPart, " + 
                                    "bills.idBill, " + 
                                    "bills.billDate, " +
                                    "bills.payDate, " +
                                    "bills.invoiceNumber, " +
                                    "bills_projects.percent, " +
                                    "bills_projects.progress, " +
                                    "bills.amount, " +
                                    "bills.id, " +
                                    "projects.idProjects " +
                                "FROM bills " + 
                                "INNER JOIN bills_projects ON bills.id=bills_projects.idBill " +
                                "INNER JOIN projects ON bills_projects.idProject=projects.idProjects " + 
                                "WHERE projects.idProjects=" + bill.ProjectId + " AND bills.billPart>=1 " + "AND bills.billPart<" + bill.BillPart.ToString() + " ORDER BY bills.billPart ASC";
                DataTable dtBills = localInterface.Select(sqlCmd).Tables[0];



                object missObj = System.Reflection.Missing.Value;
                double sumBills = 0;
                DateTime tmpDate;
                double amount;
                double progress = 0;
                for (int i = 2; i <= (dtBills.Rows.Count + 1); i++)
                {
                    oWordDoc.Tables[1].Cell(i, 1).Range.Text = "ח\"ח " + dtBills.Rows[i - 2][0].ToString();
                    oWordDoc.Tables[1].Cell(i, 2).Range.Text = dtBills.Rows[i - 2][1].ToString();
                    if (dtBills.Rows[i - 2][2].ToString() != String.Empty)
                    {
                        tmpDate = MyUtills.dateFromSQL(dtBills.Rows[i - 2][2].ToString());
                        oWordDoc.Tables[1].Cell(i, 3).Range.Text = tmpDate.Day.ToString().PadLeft(2, '0') + "/" + tmpDate.Month.ToString().PadLeft(2, '0') + "/" + tmpDate.Year.ToString();
                    }
                    else
                    {
                        oWordDoc.Tables[1].Cell(i, 3).Range.Text = String.Empty;
                    }
                    if (dtBills.Rows[i - 2][3].ToString() != String.Empty)
                    {
                        tmpDate = MyUtills.dateFromSQL(dtBills.Rows[i - 2][3].ToString());
                        oWordDoc.Tables[1].Cell(i, 4).Range.Text = tmpDate.Day.ToString().PadLeft(2, '0') + "/" + tmpDate.Month.ToString().PadLeft(2, '0') + "/" + tmpDate.Year.ToString();
                    }
                    else
                    {
                        oWordDoc.Tables[1].Cell(i, 4).Range.Text = String.Empty;
                    }
                    oWordDoc.Tables[1].Cell(i, 5).Range.Text = dtBills.Rows[i - 2][4].ToString();
                    oWordDoc.Tables[1].Cell(i, 6).Range.Text = String.Format("{0:0.00}", (double)dtBills.Rows[i - 2][5]); progress += (double)dtBills.Rows[i - 2][5];
                    oWordDoc.Tables[1].Cell(i, 7).Range.Text = String.Format("{0:0.00}", progress);


                    if (oWordDoc.Tables[1].Cell(i, 4).Range.Text != String.Empty)
                    {
                        amount = projectPartPaid((int)dtBills.Rows[i - 2][9], (int)dtBills.Rows[i - 2][8]);
                        oWordDoc.Tables[1].Cell(i, 8).Range.Text = String.Format("{0:#,0.00}", amount); sumBills += amount;
                    }
                    else
                    {
                        oWordDoc.Tables[1].Cell(i, 8).Range.Text = String.Empty;
                    }

                    oWordDoc.Tables[1].Rows.Add(ref missObj);
                }
                oWordDoc.Tables[1].Cell(dtBills.Rows.Count + 2, 1).Range.Text = "סה\"כ בש\"ח";
                //oWordDoc.Tables[1].Cell(dtBills.Rows.Count + 2, 1).Range.Font.Bold = 1;
                oWordDoc.Tables[1].Cell(dtBills.Rows.Count + 2, 8).Range.Text = String.Format("{0:#,0.00}", sumBills);
                //oWordDoc.Tables[1].Cell(dtBills.Rows.Count + 2, 8).Range.Font.Bold = 1;
                

                foreach (Word.Field myMergeField in oWordDoc.Fields)
                {
                    //iTotalFields++;
                    Word.Range rngFieldCode = myMergeField.Code;
                    String fieldText = rngFieldCode.Text;
                    String field = String.Empty;
                    if (fieldText.StartsWith(" MERGEFIELD"))
                    {
                        Int32 endMerge = fieldText.IndexOf("\\");
                        Int32 fieldNameLength = fieldText.Length - endMerge;
                        String fieldName = fieldText.Substring(11, endMerge - 11);
                        fieldName = fieldName.Trim();
                        myMergeField.Select();

                        switch (fieldName)
                        {
                            case "BillDate":
                                field = bill.BillDate.ToShortDateString();
                                break;
                            case "Reference":
                                field = project.Number + "-" + bill.IdBill;
                                break;
                            case "Customer":
                                field = customer.Name;
                                break;
                            case "Approver":
                                field = project.ApproverName;
                                break;
                            case "Address":
                                field = project.ApproverAddress;
                                break;
                            case "Fax":
                                field = project.ApproverFax;
                                break;
                            case "Email":
                                field = project.ApproverEmail;
                                break;
                            case "ProjectName":
                                field = project.Name;
                                break;
                            case "BillPart":
                                field = bill.BillPart.ToString();
                                break;
                            case "AmountNotes":
                                field = bill.BillNotes;
                                break;
                            case "ProjectAmount":
                                field = String.Format("{0:0,0.00}", project.Amount);
                                break;
                            case "Submitted":
                                field = String.Format("{0:0}", project.ToSumbit);
                                break;
                            case "Index":
                                if (project.PriceIndex > 0 && bill.CurrentIndex > 0)
                                    field = "מדד בסיס: " + " תאריך " + project.PriceIndexDate.Month.ToString().PadLeft(2, '0') + "/" + project.PriceIndexDate.Year.ToString().PadLeft(2, '0') + ", ערך " + String.Format("{0:0.0}", project.PriceIndex);
                                else
                                    field = " ";
                                break;
                            case "CurrentIndex":
                                DateTime temp;
                                if (bill.BillDate.Day >= 15)
                                    temp = bill.BillDate.AddMonths(-1);
                                else
                                    temp = bill.BillDate.AddMonths(-2);
                                string date = temp.Month.ToString().PadLeft(2, '0') + "/" + temp.Year.ToString();
                                if (project.PriceIndex > 0 && bill.CurrentIndex > 0)
                                    field = "מדד קובע: " + " תאריך " + date + ", ערך " + String.Format("{0:0.0}", bill.CurrentIndex);
                                else
                                    field = " ";
                                break;
                            case "ProAmTBP":
                                field = String.Format("{0:0,0.00}", project.Amount * project.ToSumbit / 100);
                                break;
                            case "AmountPaid":
                                field = totalPaidAmount;
                                break;
                            case "APnis":
                                field = totalPaidNIS;
                                break;
                            case "AmountPaidText":
                                field = totalPaidText;
                                break;
                            case "OpenBillAm":
                                field = openSumText;
                                break;
                            case "OpenBillText":
                                field = openbillText;
                                break;
                            case "OBAnis":
                                field = openSumNIS;
                                break;
                            case "IncAmount":
                                field = increaseSum;
                                break;
                            case "IncreaseText":
                                field = increaseText;
                                break;
                            case "IAnis":
                                field = increaseNIS;
                                break;
                            case "TotalAmount":
                                field = String.Format("{0:0,0.00}", bill.Amount + bill.Increase);
                                break;
                            case "VatAmount":
                                field = String.Format("{0:0,0.00}", (bill.Amount + bill.Increase) * (bill.Vat / 100));
                                break;
                            case "AmWithVat":
                                field = String.Format("{0:0,0.00}", (bill.Amount + bill.Increase) * (bill.Vat / 100 + 1));
                                break;
                            case "VAT":
                                field = String.Format("{0:0,0.00}", bill.Vat);
                                break;
                            case "BillAmount":
                                field = String.Format("{0:0,0.00}", bill.Amount);
                                break;
                        }
                        //if (field == String.Empty)
                        //field = "לא ידוע";
                        oWord.Selection.TypeText(field);
                    }
                }
                string file = outputLibrary + "\\" + project.Number + "-" + bill.IdBill + ".doc";
                if (File.Exists(file))
                    if (MessageBox.Show("קובץ בשם זה כבר נמצא, האם להחליף ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) != DialogResult.Yes)
                        return;
                try
                {
                    oWordDoc.SaveAs(file);
                }
                catch
                {
                    MessageBox.Show("יצירת המסמך נכשלה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    this.Close();
                    return;
                }
                //oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);

                MessageBox.Show("המסמך נוצר בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                oWord.Visible = true;
                this.Close();

                #endregion
            }
            else if (projectType==(int)PROJECTTYPE.GOVERMENT)
            {
                #region BillGoverment
                if (!File.Exists(localInterface.importLibrary + "\\" + "TempBillGov.dot"))
                {
                    MessageBox.Show("קובץ תבנית לא נמצא", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
                Object oTemplatePath = localInterface.importLibrary + "\\" + "TempBillGov.dot";
                try
                {
                    oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("התוכנה נתקלה בבעיה עם תוכנת האופיס המותקנת במחשב זה" + "\r\n" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                    return;
                }
                foreach (Word.Field myMergeField in oWordDoc.Fields)
                {
                    //iTotalFields++;
                    Word.Range rngFieldCode = myMergeField.Code;
                    String fieldText = rngFieldCode.Text;
                    String field = String.Empty;
                    if (fieldText.StartsWith(" MERGEFIELD"))
                    {
                        Int32 endMerge = fieldText.IndexOf("\\");
                        Int32 fieldNameLength = fieldText.Length - endMerge;
                        String fieldName = fieldText.Substring(11, endMerge - 11);
                        fieldName = fieldName.Trim();
                        myMergeField.Select();

                        switch (fieldName)
                        {
                            case "BillDate":
                                field = bill.BillDate.ToShortDateString();
                                break;
                            case "BillNumber":
                                field = bill.IdBill;
                                break;
                            case "Reference":
                                field = project.Number + "-" + bill.IdBill;
                                break;
                            case "Customer":
                                field = customer.Name;
                                break;
                            case "Approver":
                                field = project.ApproverName;
                                break;
                            case "Address":
                                field = project.ApproverAddress;
                                break;
                            case "Email":
                                field = project.ApproverEmail;
                                break;
                            case "ProjectName":
                                field = project.Name;
                                break;
                            case "BillPart":
                                field = bill.BillPart.ToString();
                                break;
                            case "ContractNumber":
                                field = project.ContractNumber;
                                break;
                            case "VatAmount":
                                field = String.Format("{0:0,0.00}", (bill.Amount) * bill.Vat / 100);
                                break;
                            case "AmWithVat":
                                field = String.Format("{0:0,0.00}", (bill.Amount) * (bill.Vat / 100 + 1));
                                break;
                            case "VAT":
                                field = String.Format("{0:0,0.00}", bill.Vat);
                                break;
                            case "BillAmount":
                                field = String.Format("{0:0,0.00}", bill.Amount);
                                break;
                        }
                        //if (field == String.Empty)
                        //field = "לא ידוע";
                        oWord.Selection.TypeText(field);
                    }
                }
                string file = outputLibrary + "\\" + project.Number + "-" + bill.IdBill + ".doc";
                if (File.Exists(file))
                    if (MessageBox.Show("קובץ בשם זה כבר נמצא, האם להחליף ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) != DialogResult.Yes)
                        return;
                try
                {
                    oWordDoc.SaveAs(file);
                }
                catch
                {
                    MessageBox.Show("יצירת המסמך נכשלה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                    this.Close();
                    return;
                }
                //oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);

                MessageBox.Show("המסמך נוצר בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                oWord.Visible = true;
                this.Close();
                #endregion
            }
            else if (projectType == (int)PROJECTTYPE.ONE)
            {
                #region BillOne
                if (!File.Exists(localInterface.importLibrary + "\\" + "TempBillOne.dot"))
                {
                    MessageBox.Show("קובץ תבנית לא נמצא", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
                Object oTemplatePath = localInterface.importLibrary + "\\" + "TempBillOne.dot";
                try
                {
                    oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("התוכנה נתקלה בבעיה עם תוכנת האופיס המותקנת במחשב זה" + "\r\n" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                    return;
                }
                foreach (Word.Field myMergeField in oWordDoc.Fields)
                {
                    //iTotalFields++;
                    Word.Range rngFieldCode = myMergeField.Code;
                    String fieldText = rngFieldCode.Text;
                    String field = String.Empty;
                    if (fieldText.StartsWith(" MERGEFIELD"))
                    {
                        Int32 endMerge = fieldText.IndexOf("\\");
                        Int32 fieldNameLength = fieldText.Length - endMerge;
                        String fieldName = fieldText.Substring(11, endMerge - 11);
                        fieldName = fieldName.Trim();
                        myMergeField.Select();

                        switch (fieldName)
                        {
                            case "BillDate":
                                field = bill.BillDate.ToShortDateString();
                                break;
                            case "BillNumber":
                                field = bill.IdBill;
                                break;
                            case "Reference":
                                field = project.Number + "-" + bill.IdBill;
                                break;
                            case "Customer":
                                field = customer.Name;
                                break;
                            case "Approver":
                                field = project.ApproverName;
                                break;
                            case "Address":
                                field = project.ApproverAddress;
                                break;
                            case "Email":
                                field = project.ApproverEmail;
                                break;
                            case "ProjectName":
                                field = project.Name;
                                break;
                            //case "BillPart":
                                //field = bill.BillPart.ToString();
                                //break;
                            case "AmountNotes":
                                field = bill.BillNotes;
                                break;
                            case "VatAmount":
                                field = String.Format("{0:0,0.00}", (billHours * billHourCost) * bill.Vat / 100);
                                break;
                            case "AmWithVat":
                                field = String.Format("{0:0,0.00}", (billHours * billHourCost) * (bill.Vat / 100 + 1));
                                break;
                            case "VAT":
                                field = String.Format("{0:0,0.00}", bill.Vat);
                                break;
                            case "BillAmount":
                                field = String.Format("{0:0,0.00}", billHours*billHourCost);
                                break;
                            case "BillHours":
                                field = String.Format("{0:0.00}", billHours);
                                break;
                            case "BillHourCost":
                                field = String.Format("{0:0,0.00}", billHourCost);
                                break;
                        }
                        //if (field == String.Empty)
                        //field = "לא ידוע";
                        oWord.Selection.TypeText(field);
                    }
                }
                string file = outputLibrary + "\\" + project.Number + "-" + bill.IdBill + ".doc";
                if (File.Exists(file))
                    if (MessageBox.Show("קובץ בשם זה כבר נמצא, האם להחליף ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) != DialogResult.Yes)
                        return;
                try
                {
                    oWordDoc.SaveAs(file);
                }
                catch
                {
                    MessageBox.Show("יצירת המסמך נכשלה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                    this.Close();
                    return;
                }
                //oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);

                MessageBox.Show("המסמך נוצר בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                oWord.Visible = true;
                this.Close();
                #endregion
            }
            else if (projectType == (int)PROJECTTYPE.MULTI)
            {
                #region BillMulti
                if (!File.Exists(localInterface.importLibrary + "\\" + "TempBillMulti.dot"))
                {
                    MessageBox.Show("קובץ תבנית לא נמצא", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    return;
                }
                Object oTemplatePath = localInterface.importLibrary + "\\" + "TempBillMulti.dot";
                try
                {
                    oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("התוכנה נתקלה בבעיה עם תוכנת האופיס המותקנת במחשב זה" + "\r\n" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                    return;
                }
                object missObj = System.Reflection.Missing.Value;
                double amountProj = 0, sumProj = 0, sumIncProj = 0, increaseProj = 0;
                for (int i = 2; i <= (dt.Rows.Count+1); i++)
                {
                    oWordDoc.Tables[1].Cell(i, 1).Range.Text = dt.Rows[i - 2][6].ToString();
                    oWordDoc.Tables[1].Cell(i, 2).Range.Text = String.Format("{0:#,0.00}", (double)dt.Rows[i - 2][8]);
                    oWordDoc.Tables[1].Cell(i, 3).Range.Text = String.Format("{0:0.00}", (double)dt.Rows[i - 2][31]);
                    oWordDoc.Tables[1].Cell(i, 4).Range.Text = String.Format("{0:#,0.00}", (double)dt.Rows[i - 2][8] * (double)dt.Rows[i - 2][31] / 100.0);
                    oWordDoc.Tables[1].Cell(i, 5).Range.Text = String.Format("{0:#,0.00}", (double)dt.Rows[i - 2][8] * ((double)dt.Rows[i - 2][31] - (double)dt.Rows[i - 2][30]) / 100.0);
                    amountProj = (double)dt.Rows[i - 2][8] * (double)dt.Rows[i - 2][30] / 100.0;
                    oWordDoc.Tables[1].Cell(i, 6).Range.Text = String.Format("{0:#,0.00}", amountProj); sumProj += amountProj;
                    oWordDoc.Tables[1].Cell(i, 7).Range.Text = String.Format("{0:0.00}", (double)dt.Rows[i - 2][10]);

                    increaseProj = getIncrease(amountProj, (double)dt.Rows[i - 2][14], (double)dt.Rows[i - 2][10]);
                    oWordDoc.Tables[1].Cell(i, 8).Range.Text = String.Format("{0:#,0.00}", increaseProj); sumIncProj += increaseProj;
                    oWordDoc.Tables[1].Rows.Add(ref missObj);
                }
                oWordDoc.Tables[1].Cell(dt.Rows.Count + 2, 1).Range.Text = "סה\"כ בש\"ח";
                oWordDoc.Tables[1].Cell(dt.Rows.Count + 2, 1).Range.Font.Bold = 1;
                oWordDoc.Tables[1].Cell(dt.Rows.Count + 2, 6).Range.Text = String.Format("{0:#,0.00}", sumProj);
                oWordDoc.Tables[1].Cell(dt.Rows.Count + 2, 6).Range.Font.Bold = 1;
                oWordDoc.Tables[1].Cell(dt.Rows.Count + 2, 8).Range.Text = String.Format("{0:#,0.00}", sumIncProj);
                oWordDoc.Tables[1].Cell(dt.Rows.Count + 2, 8).Range.Font.Bold = 1;

                foreach (Word.Field myMergeField in oWordDoc.Fields)
                {
                    //iTotalFields++;
                    Word.Range rngFieldCode = myMergeField.Code;
                    String fieldText = rngFieldCode.Text;
                    String field = String.Empty;
                    if (fieldText.StartsWith(" MERGEFIELD"))
                    {
                        Int32 endMerge = fieldText.IndexOf("\\");
                        Int32 fieldNameLength = fieldText.Length - endMerge;
                        String fieldName = fieldText.Substring(11, endMerge - 11);
                        fieldName = fieldName.Trim();
                        myMergeField.Select();
                       
                        switch (fieldName)
                        {
                            case "BillDate":
                                field = bill.BillDate.ToShortDateString();
                                break;
                            case "Reference":
                                try
                                {
                                    field = project.Number.Substring(0, project.Number.IndexOf('.')) + "-" + bill.IdBill;
                                }
                                catch
                                {
                                    field = project.Number + "-" + bill.IdBill;
                                }
                                break;
                            case "Customer":
                                field = customer.Name;
                                break;
                            case "Approver":
                                field = project.ApproverName;
                                break;
                            case "Address":
                                field = project.ApproverAddress;
                                break;
                            case "Fax":
                                field = project.ApproverFax;
                                break;
                            case "Email":
                                field = project.ApproverEmail;
                                break;
                            case "ProjectName":
                                try
                                {
                                    field = project.Name.Substring(0, project.Name.IndexOf('-'));
                                }
                                catch
                                {
                                    field = project.Name;
                                }
                                break;
                            case "BillPart":
                                field = bill.BillPart.ToString();
                                break;
                            case "AmountNotes":
                                field = bill.BillNotes;
                                break;
                            case "FinalIndex":
                                field = String.Format("{0:0.0}", bill.CurrentIndex);
                                break;
                            case "Index":
                                if (project.PriceIndex > 0 && bill.CurrentIndex > 0)
                                    field = "מדד בסיס: " + " תאריך " + project.PriceIndexDate.Month.ToString().PadLeft(2, '0') + "/" + project.PriceIndexDate.Year.ToString().PadLeft(2, '0') + ", ערך " + String.Format("{0:0.0}", project.PriceIndex);
                                else
                                    field = " ";
                                break;
                            case "CurrentIndex":
                                DateTime temp;
                                if (bill.BillDate.Day >= 15)
                                    temp = bill.BillDate.AddMonths(-1);
                                else
                                    temp = bill.BillDate.AddMonths(-2);
                                string date = temp.Month.ToString().PadLeft(2, '0') + "/" + temp.Year.ToString();
                                if (project.PriceIndex > 0 && bill.CurrentIndex > 0)
                                    field = "מדד קובע: " + " תאריך " + date + ", ערך " + String.Format("{0:0.0}", bill.CurrentIndex);
                                else
                                    field = " ";
                                break;
                            case "ProAmTBP":
                                field = String.Format("{0:0,0.00}", sum);
                                break;
                            case "AmountPaid":
                                field = totalPaidAmount;
                                break;
                            case "APnis":
                                field = totalPaidNIS;
                                break;
                            case "AmountPaidText":
                                field = totalPaidText;
                                break;
                            case "OpenBillAm":
                                field = openSumText;
                                break;
                            case "OpenBillText":
                                field = openbillText;
                                break;
                            case "OBAnis":
                                field = openSumNIS;
                                break;
                            case "IncAmount":
                                field = increaseSum;
                                break;
                            case "IncreaseText":
                                field = increaseText;
                                break;
                            case "IAnis":
                                field = increaseNIS;
                                break;
                            case "TotalAmount":
                                field = String.Format("{0:0,0.00}", bill.Amount + bill.Increase);
                                break;
                            case "VatAmount":
                                field = String.Format("{0:0,0.00}", (bill.Amount + bill.Increase) * (bill.Vat / 100));
                                break;
                            case "AmWithVat":
                                field = String.Format("{0:0,0.00}", (bill.Amount + bill.Increase) * (bill.Vat / 100 + 1));
                                break;
                            case "VAT":
                                field = String.Format("{0:0,0.00}", bill.Vat);
                                break;
                            case "BillAmount":
                                field = String.Format("{0:0,0.00}", bill.Amount);
                                break;
                        }
                        //if (field == String.Empty)
                            //field = "לא ידוע";
                        oWord.Selection.TypeText(field);
                    }
                }
                string file = outputLibrary + "\\" + project.Number + "-" + bill.IdBill + ".doc";
                if (File.Exists(file))
                    if (MessageBox.Show("קובץ בשם זה כבר נמצא, האם להחליף ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) != DialogResult.Yes)
                        return;
                try
                {
                    oWordDoc.SaveAs(file);
                }
                catch 
                {
                    MessageBox.Show("יצירת המסמך נכשלה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                    this.Close();
                    return;
                }
                //oWordDoc.Application.Quit(false, ref oMissing, ref oMissing);
                
                MessageBox.Show("המסמך נוצר בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                oWord.Visible = true;
                this.Close();
                
                #endregion
            }
        }
        private double getIncrease(double amount, double currentIndex, double priceIndex)
        {

            double increase = 0;
            if (priceIndex != 0)
                increase = (currentIndex / priceIndex - 1) * amount;
            if (currentIndex == 0)
                increase = 0;
            return increase;
        }
        private void buttonBrowseExportLibrary_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = System.Environment.SpecialFolder.Desktop;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
                textBoxExportLibrary.Text = fbd.SelectedPath;
        }
        private void buttonExport_Click(object sender, EventArgs e)
        {
            string outputLibrary = textBoxExportLibrary.Text;
            int projectType = 0;
            bool returnFlag = false;
            if (radioButtonBillNormal.Checked == true)
                projectType = (int)PROJECTTYPE.NORMAL;
            else if (radioButtonBillNormalDesc.Checked == true)
                projectType = (int)PROJECTTYPE.NORMAL_DESC;
            else if (radioButtonBillGov.Checked == true)
                projectType = (int)PROJECTTYPE.GOVERMENT;
            else if (radioButtonBillOne.Checked == true)
            {
                labelBillHourCost.ForeColor = Color.Black;
                labelBillHours.ForeColor = Color.Black;
                if (!Double.TryParse(textBoxBillHourCost.Text, out billHourCost))
                {
                    labelBillHourCost.ForeColor = Color.Red;
                    returnFlag = true;
                }
                if (!Double.TryParse(textBoxBillHours.Text, out billHours))
                {
                    labelBillHours.ForeColor = Color.Red;
                    returnFlag = true;
                }
                if (returnFlag)
                    return;
                else
                {
                    labelBillHourCost.ForeColor = Color.Black;
                    labelBillHours.ForeColor = Color.Black;
                }
                projectType = (int)PROJECTTYPE.ONE;
            }

            CreateTemplate(outputLibrary, projectType);

        }
        private void radioButtonBillOne_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBillOne.Checked)
                groupBoxOneBillDetails.Visible = true;
            else
                groupBoxOneBillDetails.Visible = false;
        }

        private void Form_Export_Load(object sender, EventArgs e)
        {
            /*
            int currentPart;
            int id;
            string currentIdProject;
            DateTime currentDate;
            int lastCount = 1;
            DataTable dt = localInterface.Select("SELECT idProject,billPart,billDate,id FROM Bills ORDER BY billDate ASC").Tables[0];
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                lastCount = 1;
                currentIdProject = dt.Rows[i][0].ToString();
                currentPart = Convert.ToInt16(dt.Rows[i][1]);
                if (dt.Rows[i][2].ToString() == "")
                    continue;
                currentDate = MyUtills.dateFromSQL(dt.Rows[i][2].ToString());
                id = Convert.ToInt16(dt.Rows[i][3]);

                for (int j = 0; j < i; j++)
                {
                    if (currentIdProject == dt.Rows[j][0].ToString())
                        lastCount++;
                }
                if (lastCount > 1)
                {
                    localInterface.Update("UPDATE bills SET billPart="+lastCount+" WHERE id="+id);
                }
            }
            */
        }
        private void Form_Export_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        private double projectPartAmount(int idProject, int idBill)
        {
            DataTable dt = localInterface.Select("SELECT bills_projects.percent,projects.amount FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idProject='" + idProject.ToString() + "' AND bills_projects.idBill='" + idBill.ToString() + "'").Tables[0];
            return (double)dt.Rows[0][0] / 100 * (double)dt.Rows[0][1];
        }
        private double projectPartPaid(int idProject, int idBill)
        {
            DataTable dt = localInterface.Select("SELECT bills_projects.paid,projects.amount FROM bills_projects INNER JOIN projects ON bills_projects.idProject=projects.idProjects WHERE bills_projects.idProject='" + idProject.ToString() + "' AND bills_projects.idBill='" + idBill.ToString() + "'").Tables[0];
            return (double)dt.Rows[0][0] / 100 * (double)dt.Rows[0][1];
        }
    }
}
