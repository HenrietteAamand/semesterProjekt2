using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier.Interfaces;
using DataTier;
using System.Data.SqlClient;
using Models.Models;


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

        public void CreatePatient(PatientModel patient)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

                string insertStringParam = @"INSERT INTO dbo.AnalyzedECG SET IsRead = 'TRUE'";
                using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
                {
                    reader = cmd.ExecuteReader();
                }
            
            connection.Close();

            throw new NotImplementedException();
        }

        public List<AnalyzedECGModel> GetAllAnalyzedECGs()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<AnalyzedECGModel> ameasurements = new List<AnalyzedECGModel>();
            IllnessModel illness = new IllnessModel(0, "NN", " ", 0, 0,false, false);

            command = new SqlCommand("select * from dbo.Analyzed ECG", connection);
            connection.Open();

            reader = command.ExecuteReader();

            
            while (reader.Read())
            {
                List<byte> bytesArr = new List<byte>();
                List<double> tal = new List<double>();

                string selectString = $"SELECT * FROM dbo.Analyzed ECG";

                using (SqlCommand cmd = new SqlCommand(selectString, connection))
                {
                    reader = cmd.ExecuteReader();
                }
                if (reader.Read())
                {
                    bytesArr = (List<byte>)reader["BLOB-Values"];
                }
                for (int i = 0, j = 0; i < bytesArr.Count; i += 8, j++)
                {
                    tal[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }

               
                ameasurements.Add(new AnalyzedECGModel(Convert.ToString(reader["CPR-ID"]), Convert.ToInt32(reader["ECGID"]), Convert.ToInt32(reader["AECGID"]),
                    Convert.ToDateTime(reader["Dato"]), illness, tal, Convert.ToInt32(reader["Samplerate"]), 
                    Convert.ToInt32(reader["MonitorID"]), Convert.ToBoolean(reader["IsRead"])));
               

            }
            reader.Close();
            connection.Close();
            return ameasurements;

            throw new NotImplementedException();
        }

        public List<ECGMonitorModel> GetAllECGMonitors()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<ECGMonitorModel> ecgMonitors = new List<ECGMonitorModel>();


            command = new SqlCommand("select * from dbo.ECG-Monitor", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {

                ecgMonitors.Add(new ECGMonitorModel(Convert.ToInt32(reader["ECGID"]), Convert.ToBoolean(reader["InUse"])));

            }

            connection.Close();
            return ecgMonitors;

        }

        public List<ECGModel> GetAllECGs()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<ECGModel> measurements = new List<ECGModel>();


            command = new SqlCommand("select * from dbo.ECG", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {
                List<byte> bytesArr = new List<byte>();
                List<double> tal = new List<double>();

                string selectString = $"SELECT * FROM dbo.ECG";

                using (SqlCommand cmd = new SqlCommand(selectString, connection))
                {
                    reader = cmd.ExecuteReader();
                }
                if (reader.Read())
                {
                    bytesArr = (List<byte>)reader["BLOB-Values"];
                }
                for (int i = 0, j = 0; i < bytesArr.Count; i += 8, j++)
                {
                    tal[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }


                measurements.Add(new ECGModel(Convert.ToString(reader["CPRID"]),Convert.ToInt32(reader["Id"]),
                    Convert.ToDateTime(reader["Date"]), Convert.ToInt32(reader["Samplerate"]),tal, Convert.ToString(reader["MonitorID"]), 
                    Convert.ToBoolean(reader["IsAnalyzed"])));

            }
            reader.Close();
            connection.Close();
            return measurements;
        }

        public List<IllnessModel> GetAllIllnesses()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<IllnessModel> illness = new List<IllnessModel>();


            command = new SqlCommand("select * from dbo.Illnesses", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {

                illness.Add(new IllnessModel(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Name"]),
                    Convert.ToString(reader["About"]), Convert.ToDouble(reader["stMax"]), Convert.ToDouble(reader["srMax"]), 
                    Convert.ToBoolean(reader["STSegmentelevated"]), Convert.ToBoolean(reader["STSegmentDepressed"])));

            }

            connection.Close();
            return illness;

        }

        public List<PatientModel> GetAllPatients()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<PatientModel> patients = new List<PatientModel>();


            command = new SqlCommand("select * from dbo.Patient", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {

                patients.Add(new PatientModel(Convert.ToInt32(reader["LinkedECG"]), Convert.ToString(reader["CPR"]), 
                    Convert.ToString(reader["FirstName"]), Convert.ToString(reader["LastName"])));

            }

            connection.Close();
            return patients;
            
        }

        public void IsAnalyzed(ECGModel ecgMearsurement, AnalyzedECGModel aEcgMeasurement)
        {

            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            if (ecgMearsurement.ECGID == aEcgMeasurement.ECGID)
            {
                string insertStringParam = "UPDATE dbo.ECG SET IsAnalyzed = 1";
                using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }
            }
            connection.Close();
        }

        public void IsRead(AnalyzedECGModel aEcgMeasurement)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            if (aEcgMeasurement.IsRead == true)
            {
                string insertStringParam = "UPDATE dbo.AnalyzedECG SET IsRead = 1";
                using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }
            }
            connection.Close();
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
