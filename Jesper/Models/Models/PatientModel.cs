using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class PatientModel
    {

        #region Attributes

        #endregion

        #region Properties
        private int id;

        public int ID
        {
            get { return id; }
            private set { id = value; }
        }

        private string ecgMonitorID;

        public string ECGMonitorID
        {
            get { return ecgMonitorID; }
            set { ecgMonitorID = value; }
        }

        private String cpr;

        public String CPR
        {
            get { return cpr; }
            set { cpr = value; }
        }

        private String firstName;

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private String lastName;

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        //private List<ECGModel> ecgList;

        //public List<ECGModel> ECGList
        //{
        //    get { return ecgList; }
        //    set { ecgList = value; }
        //}

        //private List<AnalyzedECGModel> aECGList;

        //public List<AnalyzedECGModel> AECGList
        //{
        //    get { return aECGList; }
        //    set { aECGList = value; }
        //}


        #endregion

        #region Constructor
        public PatientModel()
        {

        }

        public PatientModel(string ecgMonitorID, string cpr, string firstName, string lastName)
        {
            //ID = id;
            ECGMonitorID = ecgMonitorID;
            CPR = cpr;
            FirstName = firstName;
            LastName = lastName;
            //ECGList = ecgList;
            //AECGList = aECGList;
        }

        public PatientModel(string cpr, string firstName, string lastName)
        {
            CPR = cpr;
            FirstName = firstName;
            LastName = lastName;
        }
        #endregion

        #region Methods

        #endregion

    }
}
