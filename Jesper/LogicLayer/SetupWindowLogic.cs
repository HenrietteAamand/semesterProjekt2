using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicTier
{
    public class SetupWindowLogic
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

            //Opretter en patient, og gemmer den i databasen
            //Kalder CreatePatient()
            //Sker når der trykeks på "Opret patient"
        }

        public void LinkECGToPatient(string ecgID, string cpr)
        {
            //Linker ECG-monitor til et patient objekt
        }

        public void resetECGMonitor(string ecgID)
        { 
            //Finder patient med pågældende ECG-monitor tilknyttet
            //Fjerner ECG-monitoren fra patient objektet
            //Sætter InUse på ECG-monitoren til false
            //Sker når der trykkes på "Nulstil EKG-måler", og når ern EKG-måler der er i brug, er valgt
        }


        public List<ECGMonitorModel> getAllMonitors()
        {
            //Henter alle ECG monitors til liste
            throw new NotImplementedException();
        }

    }
}
