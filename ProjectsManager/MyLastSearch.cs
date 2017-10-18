using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectsManager
{
    class MyLastSearch
    {
        #region ATTRIBUTES SECTION

        private bool isSearch;
        private short panel;
        private string idQuery;
        Form1 mainForm;
        DBinterface localInterface;

        #endregion
        #region CONSTRACTORS SECTION

        public MyLastSearch(Form1 form, DBinterface dbInterface)
        {
            this.isSearch = true;
            this.panel = 0;
            this.idQuery = "";
            this.mainForm = form;
            this.localInterface = dbInterface;
        }

        #endregion
        #region SETTERS AND GETTERS SECTION
        public string IdQuery
        {
            get { return idQuery; }
            set { idQuery = value; }
        }
        public short Panel
        {
            get { return panel; }
            set { panel = value; }
        }
        public bool IsSearch
        {
            get { return isSearch; }
            set { isSearch = value; }
        }
        #endregion
        #region METHODS SECTION

        #endregion
    }
}
