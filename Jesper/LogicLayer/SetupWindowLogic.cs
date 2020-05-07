using DataTier.Databaser;
using DataTier.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicTier
{
    public class SetupWindowLogic
    {

        
        
        private ILocalDatabase DB;

        private List<ECGMonitorModel> ecgMonitorList;
        private List<PatientModel> patientList;

        public SetupWindowLogic()
        {
            ecgMonitorList = new List<ECGMonitorModel>();
            patientList = new List<PatientModel>();
            DB = new Database();
            patientList = DB.GetAllPatients();
            ecgMonitorList = DB.GetAllECGMonitors();
        }

        public void newPatient(string cpr, string firstName, string lastName)
        {
            //Opretter en patient, og gemmer den i databasen
            //Kalder CreatePatient()
            DB.CreatePatient(new PatientModel(cpr, firstName, lastName));
            //Sker når der trykeks på "Opret patient"
        }

        public void LinkECGToPatient(string cpr, int ecgID)
        {
            //Linker ECG-monitor til et patient objekt
            foreach (PatientModel patient in patientList)
            {
                if (patient.CPR == cpr)
                {
                    patient.ECGMonitorID = ecgID;
                    DB.UpdateLinkECGToPatient(cpr, ecgID);
                }
            }

            foreach (ECGMonitorModel monitor in ecgMonitorList)
            {
                if (monitor.ID == ecgID)
                {
                    monitor.InUse = true;
                }
            }
            
        }

        public void resetECGMonitor(int ecgID)
        {
            //Finder patient med pågældende ECG-monitor tilknyttet
            //Fjerner ECG-monitoren fra patient objektet
            foreach (PatientModel patient in patientList)
            {
                if (patient.ECGMonitorID == ecgID)
                {
                    patient.ECGMonitorID = 0;
                    
                }
            }
            //Sætter InUse på ECG-monitoren til false
            foreach(ECGMonitorModel monitor in ecgMonitorList)
            {
                if (monitor.ID == ecgID)
                {
                    monitor.InUse = false;
                    DB.UpdateResetECGMonitor(ecgID);
                }
            }


            //Sker når der trykkes på "Nulstil EKG-måler", og når ern EKG-måler der er i brug, er valgt
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


        public bool monitorInUse(ECGMonitorModel monitor)
        {
            return monitor.InUse;
        }
    }
}
