using DataTier.Interfaces;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Databaser
{
    public class Database : ILocalDatabase
    {
        ECGModel ecg1 = new ECGModel("010156-7890", 1, (new DateTime(2011, 1, 1)), 50,
            (new List<double> { 4, 4, 4, 5, 4, 3.5, 9, 1, 2, 4, 4, 4.5, 4, 4 }), 1, false);

        //AnalyzedECGModel aECG = new AnalyzedECGModel("123456-7890", 1, 1, (new DateTime(2011, 1, 1)), 50,
        //    (new List<double> { 4, 4, 4, 5, 4, 3.5, 9, 1, 2, 4, 4, 4.5, 4, 4 }), 1, false,
        //    (new IllnessModel(1, "st", "not good", 2, 4, false, false)),
        //    new List<double> { 4, 4, 4, 5, 4, 3.5, 4.5, 4, 4 });
        ECGMonitorModel monitor = new ECGMonitorModel(1, false);
        PatientModel patient = new PatientModel("010156-7890", "Peter", "Petersen");
        IllnessModel illness = new IllnessModel(1, "st", "not good", 2, 4, false, false);
        List<PatientModel> patientList = new List<PatientModel>();

        List<AnalyzedECGModel> aECGList = new List<AnalyzedECGModel>();
        List<ECGModel> ecgList = new List<ECGModel>();
        List<ECGMonitorModel> monitorList = new List<ECGMonitorModel>();
        List<IllnessModel> illnessList = new List<IllnessModel>();
        public Database()
        {
            //aECG.STStartIndex = 1;
            //aECGList.Add(aECG);
            ecgList.Add(ecg1);
            patientList.Add(patient);
            monitorList.Add(monitor);
            illnessList.Add(illness);
        }

        public void CreatePatient(PatientModel patient)
        {
            patientList.Add(patient);
        }

        public List<AnalyzedECGModel> GetAllAnalyzedECGs()
        {
            return aECGList;
        }

        public List<ECGMonitorModel> GetAllECGMonitors()
        {
            return monitorList;
        }

        public List<ECGModel> GetAllECGs()
        {
            return ecgList;
        }

        public List<IllnessModel> GetAllIllnesses()
        {
            return illnessList;
        }

        public List<PatientModel> GetAllPatients()
        {
            return patientList;
        }


        public void UpdateIsAnalyzed(ECGModel ecgID)
        {
            throw new NotImplementedException();
        }

        public void UpdateIsRead(AnalyzedECGModel aECGID)
        {
            throw new NotImplementedException();
        }

        //public void UpdateIsRead(AnalyzedECGModel aECGID)
        //{
        //    foreach (AnalyzedECGModel aECG in aECGList)
        //    {
        //        if (aECG.AECGID == aECGID)
        //        {
        //            aECG.IsRead = true;
        //        }
        //    }
        //}

        //public void UpdateLinkECGToPatient(PatientModel patient, ECGMonitorModel monitor)
        //{
        //    //UpdatePatient(patient);

        //    //UpdateECGMonitor(monitor);
        //    //for (int i = 0; i < monitorList.Count; i++)
        //    //{
        //    //    if (monitorList[i].ID == patient.ECGMonitorID)
        //    //    {
        //    //        monitorList[i].InUse = true;
        //    //    }
        //    //}
        //}

        public void UpdatePatient(PatientModel patient)
        {
            for (int i = 0; i < patientList.Count; i++)
            {
                if (patientList[i].CPR == patient.CPR)
                {
                    patientList[i] = patient;
                }
            }
        }

        public void UpdateECGMonitor(ECGMonitorModel monitor)
        {
            for (int i = 0; i < monitorList.Count; i++)
            {
                if (monitorList[i].ID == monitor.ID)
                {
                    monitorList[i] = monitor;
                }
            }
        }

        public void UploadAnalyzedECGs(AnalyzedECGModel analyzedEcg)
        {
            aECGList.Add(analyzedEcg);
        }
    }
}
