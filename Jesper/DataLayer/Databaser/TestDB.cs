using DataTier.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Databaser
{
    public class TestDB : ILocalDatabase
    {
        ECGModel ecg1 = new ECGModel("123456-7890", 1,( new DateTime(2011,1,1)), 50,
            (new List<double> { 4, 4, 4, 5, 4, 3.5, 9, 1, 2, 4, 4, 4.5, 4, 4 }), 1, false);
        
        AnalyzedECGModel aECG = new AnalyzedECGModel("123456-7890", 1, 1,(new DateTime(2011, 1,1)), 50,
            (new List<double> { 4, 4, 4, 5, 4, 3.5, 9, 1, 2, 4, 4, 4.5, 4, 4 }), 1, false, 
            (new IllnessModel(1, "st", "not good", 2, 4, false, false)),
            new List<double> { 4, 4, 4, 5, 4, 3.5, 4.5, 4, 4 });
        PatientModel patient = new PatientModel("123456-7890", "Peter", "Petersen");
        List<PatientModel> patientList = new List<PatientModel>();
        
        List<AnalyzedECGModel> aECGList = new List<AnalyzedECGModel>();
        List<ECGModel> ecgList = new List<ECGModel>();
        public TestDB()
        {
            aECG.STStartIndex = 1;
            aECGList.Add(aECG);
            ecgList.Add(ecg1);
            patientList.Add(patient);
        }

        public void CreatePatient(PatientModel patient)
        {
            throw new NotImplementedException();
        }

        public List<AnalyzedECGModel> GetAllAnalyzedECGs()
        {
            return aECGList;
        }

        public List<ECGMonitorModel> GetAllECGMonitors()
        {
            throw new NotImplementedException();
        }

        public List<ECGModel> GetAllECGs()
        {
            return ecgList;
        }

        public List<IllnessModel> GetAllIllnesses()
        {
            throw new NotImplementedException();
        }

        public List<PatientModel> GetAllPatients()
        {
            return patientList;
        }

        public void UpdateIsAnalyzed(int ecgID)
        {
            throw new NotImplementedException();
        }

        public void UpdateIsRead(int aECGID)
        {
            throw new NotImplementedException();
        }

        public void UpdateLinkECGToPatient(string cpr, int ecgMonitorID)
        {
            throw new NotImplementedException();
        }

        public void UpdateResetECGMonitor(int ecgMonitorID)
        {
            throw new NotImplementedException();
        }

        public void UploadAnalyzedECGs(AnalyzedECGModel analyzedEcg)
        {
            throw new NotImplementedException();
        }
    }
}
