using DataTier;
using System;
using System.Collections.Generic;
using System.Text;
using DataTier.Models;

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

        IllnessModel GetIllness(int id);


        //Gem
        void UpdatePatient(PatientModel patient);

        void CreatePatient(PatientModel patient);

        void UpdateECGMonitor(ECGMonitorModel ecgMonitor);

        void UpdateIsAnalyzed(ECGModel ecgID);

        void UploadAnalyzedECGs(AnalyzedECGModel analyzedEcg);

        void UpdateAnalyzedECG(AnalyzedECGModel analyzedEcg);

    }
}
