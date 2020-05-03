using System;
using System.Collections.Generic;
using System.Text;

namespace DataTier.Models
{
    public class PatientModel
    {

        #region Attributes

        #endregion

        #region Properties
        //private int id;

        //public int ID
        //{
        //    get { return id; }
        //    private set { id = value; }
        //}

        private int ecgMonitorID;

        public int ECGMonitorID
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

        private List<ECGModel> ecgList;

        public List<ECGModel> ECGList
        {
            get { return ecgList; }
            set { ecgList = value; }
        }

        private List<AnalyzedECGModel> aECGList;

        public List<AnalyzedECGModel> AECGList
        {
            get { return aECGList; }
            set { aECGList = value; }
        }


        #endregion

        #region Constructor
        public PatientModel()
        {

        }

        public PatientModel(int ecgMonitorID, string cpr, string firstName, string lastName,
            List<ECGModel> ecgList, List<AnalyzedECGModel> aECGList)
        {
            //ID = id;
            ECGMonitorID = ecgMonitorID;
            CPR = cpr;
            LastName = lastName;
            ECGList = ecgList;
            AECGList = aECGList;
        }

        #endregion

        #region Methods

        #endregion

    }
}
