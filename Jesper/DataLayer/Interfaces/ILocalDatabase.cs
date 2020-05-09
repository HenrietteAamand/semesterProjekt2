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

        IllnessModel getIllness(int id);


        //Gem
        void UpdateLinkECGToPatient(PatientModel patient, ECGMonitorModel ecgMonitor);

        void CreatePatient(PatientModel patient);

        void UpdateResetECGMonitor(ECGMonitorModel ecgMonitor);

        void UpdateIsAnalyzed(ECGModel ecgID);

        void UploadAnalyzedECGs(AnalyzedECGModel analyzedEcg);

        void UpdateIsRead(AnalyzedECGModel analyzedECG);



        //int HowManyNewECG();

        //List<PatientModel> PatientsWithNewECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNew(PatientModel patient);

        //int HowManyNewIllkECG();

        //List<PatientModel> PatientsWithNewIllECGs();

        //List<AnalyzedECGModel> ECGFromPatientWhichIsNewAndIll(PatientModel patient);




    }
}
