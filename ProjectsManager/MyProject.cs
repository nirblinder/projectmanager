using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectsManager
{
    public class MyProject
    {
        #region ATTRIBUTES SECTION
        string id;
        string number;
        string handler1;
        string handler2;
        DateTime startDate;
        int customerId;
        string name;
        double amount;
        int curency;
        string amountNotes;
        int linkingType;
        double priceIndex;
        short priceIndexDateExists;
        DateTime priceIndexDate;
        double toSubmit;
        string toSubmitNotes;
        string approverName;
        string approverPhone;
        string approverEmail;
        string approverFax;
        string payerName;
        string payerPhone;
        string payerEmail;
        string payerFax;
        short contractExists;
        string contractNotes;
        string projectNotes;
        short isClosed;
        string archiveLocation;
        string contractNumber;
        string mileStonesNotes;
        short type;
        double totalPaid;
        string approverAddress;
        string payerAddress;
        #endregion
        #region CONSTARCTORS SECTION
        public MyProject()
        {
            id = "0";
            number = "0";
            handler1 = "";
            handler2 = "";
            startDate = DateTime.Now;
            customerId = 0;
            name = "";
            amount = 0;
            curency = 0;
            amountNotes = "";
            linkingType = 0;
            priceIndex = 0;
            priceIndexDateExists = 0;
            priceIndexDate = DateTime.Now;
            toSubmit = 0;
            toSubmitNotes = "";
            approverName = "";
            approverPhone = String.Empty;
            approverEmail = String.Empty;
            approverFax = String.Empty;
            payerName = String.Empty;
            payerPhone = String.Empty;
            payerEmail = String.Empty;
            payerFax = String.Empty;
            contractExists = 0;
            contractNotes = String.Empty;
            projectNotes = String.Empty;
            isClosed = 0;
            archiveLocation = String.Empty;
            contractNumber = String.Empty;
            mileStonesNotes = String.Empty;
            type = 0;
            totalPaid = 0;
            payerAddress = String.Empty;
            approverAddress = String.Empty;
        }
        #endregion
        #region SETTERS AND GETTERS SECTION
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        public string Handler1
        {
            get { return handler1; }
            set { handler1 = value; }
        }
        public string Handler2
        {
            get { return handler2; }
            set { handler2 = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public int Curency
        {
            get { return curency; }
            set { curency = value; }
        }
        public string AmountNotes
        {
            get { return amountNotes; }
            set { amountNotes = value; }
        }
        public int LinkingType
        {
            get { return linkingType; }
            set { linkingType = value; }
        }
        public double PriceIndex
        {
            get { return priceIndex; }
            set { priceIndex = value; }
        }
        public short PriceIndexDateExists
        {
            get { return priceIndexDateExists; }
            set { priceIndexDateExists = value; }
        }
        public DateTime PriceIndexDate
        {
            get { return priceIndexDate; }
            set { priceIndexDate = value; }
        }
        public double ToSumbit
        {
            get { return toSubmit; }
            set { toSubmit = value; }
        }
        public string ToSubmitNotes
        {
            get { return toSubmitNotes; }
            set { toSubmitNotes = value; }
        }
        public string ApproverName
        {
            get { return approverName; }
            set { approverName = value; }
        }
        public string ApproverPhone
        {
            get { return approverPhone; }
            set { approverPhone = value; }
        }
        public string ApproverEmail
        {
            get { return approverEmail; }
            set { approverEmail = value; }
        }
        public string ApproverFax
        {
            get { return approverFax; }
            set { approverFax = value; }
        }
        public string ApproverAddress
        {
            get { return approverAddress; }
            set { approverAddress = value; }
        }
        public string PayerName
        {
            get { return payerName; }
            set { payerName = value; }
        }
        public string PayerPhone
        {
            get { return payerPhone; }
            set { payerPhone = value; }
        }
        public string PayerEmail
        {
            get { return payerEmail; }
            set { payerEmail = value; }
        }
        public string PayerFax
        {
            get { return payerFax; }
            set { payerFax = value; }
        }
        public string PayerAddress
        {
            get { return payerAddress; }
            set { payerAddress = value; }
        }
        public short ContractExists
        {
            get { return contractExists; }
            set { contractExists = value; }
        }
        public string ContractNotes
        {
            get { return contractNotes; }
            set { contractNotes = value; }
        }
        public string ProjectNotes
        {
            get { return projectNotes; }
            set { projectNotes = value; }
        }
        public short IsClosed
        {
            get { return isClosed; }
            set { isClosed = value; }
        }
        public string ArchieveLocation
        {
            get { return archiveLocation; }
            set { archiveLocation = value; }
        }
        public string ContractNumber
        {
            get { return contractNumber; }
            set { contractNumber = value; }
        }
        public string MileStonesNotes
        {
            get { return mileStonesNotes; }
            set { mileStonesNotes = value; }
        }
        public short Type
        {
            get { return type; }
            set { type = value; }
        }
        public double TotalPaid
        {
            get { return totalPaid; }
            set { totalPaid = value; }
        }
        #endregion
        #region METHODS SECTION

        #endregion
    }
}
