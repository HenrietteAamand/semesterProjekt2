using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTier.Interfaces
{
    public interface ILocalDatabase
    {

        //Hent
        List<PatientModel> GetAllPatients();

        List<ECGMonitorModel> GetAllECGMonitors();

        List<ECGModel> GetAllECGs();

        List<AnalyzedECGModel> GetAllAnalyzedECGs(string cpr);

        List<IllnessModel> GetAllIllnesses();


        //Gem
        void LinkECGToPatient(string ecgMonitorID, string cpr);

        void CreatePatient(string cpr);

        void ResetECGMonitor(string ecgMonitorID);

        void IsAnalyzed(string ecgID);

        void UpdateAnalyzedECGs();

        void IsRead(string aECGID);



        //int HowManyNewECG();

        //List<PatientModel> PatientsWithNewECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNew(PatientModel patient);

        //int HowManyNewIllkECG();

        //List<PatientModel> PatientsWithNewIllECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNewAndIll(PatientModel patient);




    }
}
