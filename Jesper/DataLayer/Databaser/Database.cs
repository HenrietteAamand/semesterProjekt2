using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier.Interfaces;
using DataTier.Models;
using System.Data.SqlClient;

namespace DataTier.Databaser
{
    public class Database : ILocalDatabase
    {
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        private const String db = "F20ST2ITS2201908197";

        public Database()
        {
            

        }

        public void CreatePatient(string cpr)
        {

            throw new NotImplementedException();
        }

        public List<AnalyzedECGModel> GetAllAnalyzedECGs(string cpr)
        {
            //connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            //List<AnalyzedECGModel> weight = new List<AnalyzedECGModel>();


            //command = new SqlCommand("select * from dbo.Measurement where cpr = '" + cpr + "'", connection);
            //connection.Open();

            //reader = command.ExecuteReader();


            //while (reader.Read())
            //{

            //    weight.Add(new AnalyzedECGModel();

            //}

            //connection.Close();
            //return weight;

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
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<PatientModel> patienter = new List<PatientModel>();


            command = new SqlCommand("select * from dbo.Patient", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {

                patienter.Add(new PatientModel(Convert.ToInt32(reader["Tilknyttet EKG"]), Convert.ToString(reader["CPR"]), 
                    Convert.ToString(reader["ForNavn"]), Convert.ToString(reader["EfterNavn"]), GetAllECGs(Convert.ToString(reader["CPR"])), GetAllAnalyzedECGs(Convert.ToString(reader["CPR"]))));

            }

            connection.Close();
            return patienter;
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
