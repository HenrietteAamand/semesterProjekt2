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

        List<AnalyzedECGModel> GetAllAnalyzedECGs();

        List<IllnessModel> GetAllIllnesses();


        //Gem
        void LinkECGToPatient(string ecgMonitorID, string cpr);

        void CreatePatient(string cpr);

        void ResetECGMonitor(string ecgMonitorID);

        void IsAnalyzed(ECGModel ecgMearsurement);

        void UpdateAnalyzedECGs();

        void IsRead(string ecgID);



        //int HowManyNewECG();

        //List<PatientModel> PatientsWithNewECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNew(PatientModel patient);

        //int HowManyNewIllkECG();

        //List<PatientModel> PatientsWithNewIllECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNewAndIll(PatientModel patient);




    }
}
