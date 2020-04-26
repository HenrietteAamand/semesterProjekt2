using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicTier
{
    class SetupWindowLogic
    {
        private List<ECGMonitorModel> ecgMonitorList;
        private List<PatientModel> patientList;
        public SetupWindowLogic()
        {
            ecgMonitorList = new List<ECGMonitorModel>();
            patientList = new List<PatientModel>();
        }

        public PatientModel newPatient(string cpr, string firstName, string lastName) 
        {
            throw new NotImplementedException();
        }

        public void addPatient() { }

        public void resetECGMonitor() { }


    }
}
