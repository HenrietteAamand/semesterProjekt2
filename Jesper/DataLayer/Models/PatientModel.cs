using System;

namespace DataTier.Models
{
    public class PatientModel
    {

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

        private String fullName;

        public String FullName
        {
            get { return firstName + " " + lastName; }
            set { FullName = value; }
        }

        #endregion

        #region Constructor
        public PatientModel()
        {
            
        }

        public PatientModel(string ecgMonitorID, string cpr, string firstName, string lastName)
        {
            ECGMonitorID = ecgMonitorID;
            CPR = cpr;
            FirstName = firstName;
            LastName = lastName;
            
           
        }

        public PatientModel(string cpr, string firstName, string lastName)
        {
            CPR = cpr;
            FirstName = firstName;
            LastName = lastName;
          
        }
        #endregion

    }
}
