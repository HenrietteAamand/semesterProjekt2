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
        void UpdateLinkECGToPatient(PatientModel patient, ECGMonitorModel monitor);

        void CreatePatient(PatientModel patient);

        void UpdateECGMonitor(ECGMonitorModel monitor);

        void UpdateIsAnalyzed(ECGModel ecgID);

        void UploadAnalyzedECGs(AnalyzedECGModel analyzedEcg);

        void UpdateIsRead(AnalyzedECGModel aECGID);
        void UpdatePatient(PatientModel patient);



        //int HowManyNewECG();

        //List<PatientModel> PatientsWithNewECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNew(PatientModel patient);

        //int HowManyNewIllkECG();

        //List<PatientModel> PatientsWithNewIllECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNewAndIll(PatientModel patient);




    }
}
