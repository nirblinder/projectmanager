using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectsManager
{
    public class MyBill
    {
        #region ATTRIBUTES SECTION
        string id;
        string idBill;
        int projectId;
        int billPart;
        string projectNumber;
        double amount;
        double amountPaid;
        short paymentMethod;
        DateTime billDate;
        DateTime approvalDate;
        bool approvalDateExists;
        DateTime amountToPayDate;
        bool amountToPayDateExists;
        string amountToPayNotes;
        DateTime paymentDate;
        DateTime vatDate;
        bool paymentDateExists;
        string invoiceNumber;
        string receiptNumber;
        short isClosed;
        double curencyRate;
        string handler;
        DateTime callback;
        bool callbackExists;
        double currentIndex;
        double increase;
        double vat;
        string billNotes;
        #endregion
        #region CONSTRACTORS SECTION
        public MyBill()
        {
            this.id = "";
            this.idBill = "";
            this.projectId = -1;
            this.projectNumber = "";
            this.amount = 0;
            this.amountPaid = 0;
            this.paymentMethod = 0;
            this.billDate = DateTime.Now;
            this.approvalDate = DateTime.Now;
            this.approvalDateExists = false;
            this.amountToPayDate = DateTime.Now;
            this.amountToPayDateExists = false;
            this.amountToPayNotes = "";
            this.paymentDate = DateTime.Now;
            this.paymentDateExists = false;
            this.invoiceNumber = "";
            this.receiptNumber = "";
            this.isClosed = 0;
            this.curencyRate = 0;
            this.handler = "";
            this.callback = DateTime.Now;
            this.currentIndex = 0;
            this.increase = 0;
            this.vat = 0;
            this.billNotes = "";
        }
        #endregion
        #region SETTERS AND GETTERS SECTION
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string IdBill
        {
            get { return idBill; }
            set { idBill = value; }
        }
        public int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }
        public int BillPart
        {
            get { return billPart; }
            set { billPart = value; }
        }
        public string ProjectNumber
        {
            get { return projectNumber; }
            set { projectNumber = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public double AmountPaid
        {
            get { return amountPaid; }
            set { amountPaid = value; }
        }
        public short PaymentMethod
        {
            get { return paymentMethod; }
            set { paymentMethod = value; }
        }
        public DateTime BillDate
        {
            get { return billDate; }
            set { billDate = value; }
        }
        public DateTime ApprovalDate
        {
            get { return approvalDate; }
            set { approvalDate = value; }
        }
        public bool ApprovalDateExists
        {
            get { return approvalDateExists; }
            set { approvalDateExists = value; }
        }
        public DateTime AmountToPayDate
        {
            get { return amountToPayDate; }
            set { amountToPayDate = value; }
        }
        public bool AmountToPayDateExists
        {
            get { return amountToPayDateExists; }
            set { amountToPayDateExists = value; }
        }
        public string AmountToPayNotes
        {
            get { return amountToPayNotes; }
            set { amountToPayNotes = value; }
        }
        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }
        public DateTime VatDate
        {
            get { return vatDate; }
            set { vatDate = value; }
        }
        public bool PaymentDateExists
        {
            get { return paymentDateExists; }
            set { paymentDateExists = value; }
        }
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }
        public string ReceiptNumber
        {
            get { return receiptNumber; }
            set { receiptNumber = value; }
        }
        public short IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }
        public double CurencyRate
        {
            get { return curencyRate; }
            set { curencyRate = value; }
        }
        public string Handler
        {
            get { return handler; }
            set { handler = value; }
        }
        public DateTime Callback
        {
            get { return callback; }
            set { callback = value; }
        }
        public bool CallbackExists
        {
            get { return callbackExists; }
            set { callbackExists = value; }
        }
        public double CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }
        public double Increase
        {
            get { return increase; }
            set { increase = value; }
        }
        public double Vat
        {
            get { return vat; }
            set { vat = value; }
        }
        public string BillNotes
        {
            get { return billNotes; }
            set { billNotes = value; }
        }
        #endregion
        #region METHODS SECTION
        #endregion
    }
}
