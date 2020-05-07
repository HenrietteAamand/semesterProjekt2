using DataTier;
using System;
using System.Collections.Generic;
using System.Text;
using Models.Models;

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
        void UpdateLinkECGToPatient(string cpr, int ecgMonitorID);

        void CreatePatient(PatientModel patient);

        void UpdateResetECGMonitor(int ecgMonitorID);

        void UpdateIsAnalyzed(int ecgID);

        void UpdateAnalyzedECGs();

        void UpdateIsRead(int aECGID);



        //int HowManyNewECG();

        //List<PatientModel> PatientsWithNewECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNew(PatientModel patient);

        //int HowManyNewIllkECG();

        //List<PatientModel> PatientsWithNewIllECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNewAndIll(PatientModel patient);




    }
}
