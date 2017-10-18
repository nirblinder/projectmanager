using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectsManager
{
    public class MyCustomer
    {
        #region ATTRIBUTES SECTION
        int id;
        string name;
        string city;
        string address;
        string zipCode;
        string poBox;
        string phoneNumber;
        string fax;
        string eMail;
        string contactPersonDetails;
        short rate;
        string notes;
        #endregion
        #region CONSTRACTORS SECTION
        public MyCustomer()
        {
            this.id = 0;
            this.name = "";
            this.city = "";
            this.address = "";
            this.zipCode = "";
            this.poBox = "";
            this.phoneNumber = "";
            this.fax = "";
            this.eMail = "";
            this.contactPersonDetails = "";
            this.rate = 0;
            this.notes = "";
        }
        #endregion
        #region SETTERS AND GETTERS SECTION
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }
        public string PoBox
        {
            get { return poBox; }
            set { poBox = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        public string EMail
        {
            get { return eMail; }
            set { eMail = value; }
        }
        public string ContactPersonDetails
        {
            get { return contactPersonDetails; }
            set { contactPersonDetails = value; }
        }
        public short Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        #endregion
        #region METHODS SECTION

        #endregion
    }
}
