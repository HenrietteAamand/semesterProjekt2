using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier.Interfaces;
using DataTier.Models;

namespace DataTier.Databaser
{
    public class Database : ILocalDatabase
    {
        public Database()
        {
        }

        public void CreatePatient(string cpr)
        {
            throw new NotImplementedException();
        }

        public List<AnalyzedECGModel> GetAllAnalyzedECGs(string cpr)
        {
            throw new NotImplementedException();
        }

        public List<ECGMonitorModel> GetAllECGMonitors()
        {
            throw new NotImplementedException();
        }

        public List<ECGModel> GetAllECGs(string cpr)
        {
            throw new NotImplementedException();
        }

        public List<IllnessModel> GetAllIllnesses()
        {
            throw new NotImplementedException();
        }

        public List<PatientModel> GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public void IsAnalyzed(string ecgID)
        {
            throw new NotImplementedException();
        }

        public void IsRead(string aECGID)
        {
            throw new NotImplementedException();
        }

        public void LinkECGToPatient(string ecgMonitorID, string cpr)
        {
            throw new NotImplementedException();
        }

        public void ResetECGMonitor(string ecgMonitorID)
        {
            throw new NotImplementedException();
        }

        public void UpdateAnalyzedECGs()
        {
            throw new NotImplementedException();
        }
    }
}
