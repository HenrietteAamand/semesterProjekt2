using DataTier.Databaser;
using DataTier.Interfaces;
using DataTier.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogicTier
{
    public class SetupWindowLogic
    {
        #region Attributes
        private ILocalDatabase DB;
        private List<ECGMonitorModel> ecgMonitorList;
        private List<PatientModel> patientList; 
        #endregion

        #region Ctor
        public SetupWindowLogic()
        {
            ecgMonitorList = new List<ECGMonitorModel>();
            patientList = new List<PatientModel>();
            DB = new Database();

            patientList = DB.GetAllPatients();
            ecgMonitorList = DB.GetAllECGMonitors();
        } 
        #endregion

        #region Methods
        public void newPatient(string cpr, string firstName, string lastName)
        {   //Sker når der trykeks på "Opret patient"
            //Opretter en patient, og gemmer den i databasen
            DB.CreatePatient(new PatientModel(cpr, firstName, lastName));
        }

        public void LinkECGToPatient(string cpr, string ecgMonitorID)
        {
            //Linker ECG-monitor til et patient objekt
            foreach (PatientModel patient in patientList.ToList())
            {
                if (patient.CPR == cpr)
                {
                    patient.ECGMonitorID = ecgMonitorID;
                    DB.UpdatePatient(patient);
                    patientList = DB.GetAllPatients();
                }
            }

            foreach (ECGMonitorModel monitor in ecgMonitorList.ToList())
            {
                if (monitor.ID.Trim() == ecgMonitorID)
                {
                    monitor.InUse = true;
                    DB.UpdateECGMonitor(monitor);
                    ecgMonitorList = DB.GetAllECGMonitors();
                }
            }
        }

        public void ResetECGMonitor(string ecgID)
        {
            //Finder patient med pågældende ECG-monitor tilknyttet
            //Fjerner ECG-monitoren fra patient objektet
            foreach (PatientModel patient in patientList.ToList())
            {
                if (patient.ECGMonitorID.Trim() == ecgID)
                {
                    patient.ECGMonitorID = null;
                    DB.UpdatePatient(patient);
                    patientList = DB.GetAllPatients();
                }
            }
            //Sætter InUse på ECG-monitoren til false
            foreach (ECGMonitorModel monitor in ecgMonitorList.ToList())
            {
                if (monitor.ID.Trim() == ecgID)
                {
                    monitor.InUse = false;
                    DB.UpdateECGMonitor(monitor);
                    ecgMonitorList = DB.GetAllECGMonitors();
                }
            }
        }


        public List<ECGMonitorModel> getAllMonitors()
        {
            //Henter alle ECG monitors til liste
            List<ECGMonitorModel> monitorList = new List<ECGMonitorModel>();
            monitorList = DB.GetAllECGMonitors();
            return monitorList;
        }


        public List<PatientModel> getAllPatiens()
        {
            //Henter alle Patient til liste
            List<PatientModel> patientList = new List<PatientModel>();
            patientList = DB.GetAllPatients();
            return patientList;
        }


        public bool monitorInUse(string ecgMonitorID)
        {
            bool result = false;
            foreach (ECGMonitorModel monitor in ecgMonitorList)
            {

                if (monitor.ID.Trim() == ecgMonitorID)
                {
                    result = monitor.InUse;
                }
            }
            return result;

        }

        public bool IsPatientAlreadyCreated(string cpr)
        {
            bool result = false;
            foreach (PatientModel patient in patientList)
            {
                if (patient.CPR == cpr)
                {
                    result = true;
                }
            }

            return result;
        } 
        #endregion
    }
}
