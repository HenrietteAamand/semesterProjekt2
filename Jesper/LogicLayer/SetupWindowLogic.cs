using DataTier.Databaser;
using DataTier.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            DB = new TestDB();
            patientList = DB.GetAllPatients();
            ecgMonitorList = DB.GetAllECGMonitors();
            //newPatient("112233-4455", "Jens", "Jensen");
            //LinkECGToPatient("112233-4455", 1);
            //ResetECGMonitor(1);
            //getAllMonitors();
            //getAllPatiens();
            //monitorInUse(1);
        }

        public void newPatient(string cpr, string firstName, string lastName)
        {
            //Opretter en patient, og gemmer den i databasen
            //Kalder CreatePatient()
            DB.CreatePatient(new PatientModel(cpr, firstName, lastName));
            //Sker når der trykeks på "Opret patient"
        }

        public void LinkECGToPatient(string cpr, int ecgMonitorID)
        {
            //Linker ECG-monitor til et patient objekt
            foreach (PatientModel patient in patientList.ToList())
            {
                if (patient.CPR == cpr)
                {
                    patient.ECGMonitorID = ecgMonitorID;
                    DB.UpdatePatient(patient);
                }
            }

            foreach (ECGMonitorModel monitor in ecgMonitorList.ToList())
            {
                if (monitor.ID == ecgMonitorID)
                {
                    monitor.InUse = true;
                    DB.UpdateECGMonitor(monitor);
                }
            }

        }

        public void ResetECGMonitor(int ecgID)
        {
            //Finder patient med pågældende ECG-monitor tilknyttet
            //Fjerner ECG-monitoren fra patient objektet
            foreach (PatientModel patient in patientList.ToList())
            {
                if (patient.ECGMonitorID == ecgID)
                {
                    patient.ECGMonitorID = 0;
                    DB.UpdatePatient(patient);
                    

                }
                
            }
            //Sætter InUse på ECG-monitoren til false
            foreach (ECGMonitorModel monitor in ecgMonitorList.ToList())
            {
                if (monitor.ID == ecgID)
                {
                    monitor.InUse = false;
                    DB.UpdateECGMonitor(monitor);
                   
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


        public bool monitorInUse(int ecgMonitorID)
        {
            bool result = false;
            foreach (ECGMonitorModel monitor in ecgMonitorList)
            {

                if (monitor.ID == ecgMonitorID)
                {
                    result = monitor.InUse;
                }
            }
            return result;

        }
    }
   
}
