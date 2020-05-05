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

        public void CreatePatient(string cpr)
        {

            throw new NotImplementedException();
        }

        public List<AnalyzedECGModel> GetAllAnalyzedECGs()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<AnalyzedECGModel> measurements = new List<AnalyzedECGModel>();
            IllnessModel illness = new IllnessModel(0, "NN", " ", false, false);

            command = new SqlCommand("select * from dbo.Measurement where IsAnalyzed = 'true'", connection);
            connection.Open();

            reader = command.ExecuteReader();

            
            while (reader.Read())
            {
                List<byte> bytesArr = new List<byte>();
                List<double> tal = new List<double>();

                string selectString = $"SELECT * FROM dbo.Measurement";

                using (SqlCommand cmd = new SqlCommand(selectString, connection))
                {
                    reader = cmd.ExecuteReader();
                }
                if (reader.Read())
                {
                    bytesArr = (List<byte>)reader["BLOB-measurement"];
                }
                for (int i = 0, j = 0; i < bytesArr.Count; i += 8, j++)
                {
                    tal[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }

               
                measurements.Add(new AnalyzedECGModel(Convert.ToString(reader["CPR-ID"]), Convert.ToInt32(reader["Id"]),
                    Convert.ToDateTime(reader["Dato"]), illness, tal,0));

            }
            reader.Close();
            connection.Close();
            return measurements;

            throw new NotImplementedException();
        }

        public List<ECGMonitorModel> GetAllECGMonitors()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<ECGMonitorModel> ecgMonitors = new List<ECGMonitorModel>();


            command = new SqlCommand("select * from dbo.EKG-Measurer", connection);
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


            command = new SqlCommand("select * from dbo.Measurement", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {
                List<byte> bytesArr = new List<byte>();
                List<double> tal = new List<double>();

                string selectString = $"SELECT * FROM dbo.Measurement";

                using (SqlCommand cmd = new SqlCommand(selectString, connection))
                {
                    reader = cmd.ExecuteReader();
                }
                if (reader.Read())
                {
                    bytesArr = (List<byte>)reader["BLOB-measurement"];
                }
                for (int i = 0, j = 0; i < bytesArr.Count; i += 8, j++)
                {
                    tal[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }


                measurements.Add(new ECGModel(Convert.ToString(reader["CPRID"]),Convert.ToInt32(reader["Id"]),
                    Convert.ToDateTime(reader["Date"]), Convert.ToInt32(reader["Samplerate"]),tal, ""));

            }
            reader.Close();
            connection.Close();
            return measurements;
        }

        public List<IllnessModel> GetAllIllnesses()
        {
            throw new NotImplementedException();
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

        public void IsAnalyzed(ECGModel ecgMearsurement)
        {
            //connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            //connection.Open();
            //if ( ecgMearsurement.IsAnalyzed == true)
            //{
            //    string insertStringParam = @"UPDATE dbo.Measurement SET IsAnalyzed = '1' where Id = '"+ ecgMearsurement.ECGID +"'";
            //    using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            //    {
            //        cmd.Parameters.AddWithValue("@data", tal.SelectMany(value => BitConverter.GetBytes(value)).ToArray());
            //        id = (int)cmd.ExecuteScalar();
            //    }
            //}
            

            //connection.Close();
            throw new NotImplementedException();
        }

        public void IsRead(string ecgID)
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
