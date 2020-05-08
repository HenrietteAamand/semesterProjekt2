using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier.Interfaces;
using DataTier;
using System.Data.SqlClient;
using Models.Models;
using System.ComponentModel;

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

            string insertStringParam = @"INSERT INTO dbo.Patient (CPR, FirstName, LastName) VALUES (@CPR, @FirstName, @LastName)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                cmd.Parameters.AddWithValue("@CPR", patient.CPR);
                cmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);

                reader = cmd.ExecuteReader();
                reader.Read();
            }
            
            connection.Close();
        }

        public IllnessModel getIllness(int id)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            IllnessModel illness = new IllnessModel(0,"NN", " ", 0,0,false,false);

            command = new SqlCommand("SELECT * FROM dbo.Illness WHERE ID = " + id, connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {

                illness = new IllnessModel(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Name"]),
                    Convert.ToString(reader["About"]), Convert.ToDouble(reader["stMax"]), Convert.ToDouble(reader["srMax"]), 
                    Convert.ToBoolean(reader["STSegmentElevated"]), Convert.ToBoolean(reader["STSegmentDepressed"]));

            }

            connection.Close();
            return illness;
        }

        public List<AnalyzedECGModel> GetAllAnalyzedECGs()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<AnalyzedECGModel> ameasurements = new List<AnalyzedECGModel>();
            

            command = new SqlCommand("select * from dbo.AnalyzedECG", connection);
            connection.Open();

            reader = command.ExecuteReader();

            
            while (reader.Read())
            {
                List<byte> bytesArr = new List<byte>();
                List<double> values = new List<double>();
                List<byte> bytesArr1 = new List<byte>();
                List<double> STValues = new List<double>();

                string selectString = $"SELECT * FROM dbo.AnalyzedECG";

                using (SqlCommand cmd = new SqlCommand(selectString, connection))
                {
                    reader = cmd.ExecuteReader();
                }
                if (reader.Read())
                {
                    bytesArr = (List<byte>)reader["BLOBValues"];
                }
                for (int i = 0, j = 0; i < bytesArr.Count; i += 8, j++)
                {
                    values[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }

                string selectString1 = $"SELECT * FROM dbo.AnalyzedECG";

                using (SqlCommand cmd = new SqlCommand(selectString1, connection))
                {
                    reader = cmd.ExecuteReader();
                }
                if (reader.Read())
                {
                    bytesArr1 = (List<byte>)reader["BLOBstValues"];
                }
                for (int i = 0, j = 0; i < bytesArr.Count; i += 8, j++)
                {
                    STValues[j] = BitConverter.ToDouble(bytesArr1.ToArray(), i);
                }


                ameasurements.Add(new AnalyzedECGModel(Convert.ToString(reader["CPR"]), Convert.ToInt32(reader["ECGID"]), Convert.ToInt32(reader["AECGID"]),
                    Convert.ToDateTime(reader["Dato"]), Convert.ToInt32(reader["Samplerate"]), values, 
                    Convert.ToInt32(reader["MonitorID"]), Convert.ToBoolean(reader["IsRead"]), getIllness(Convert.ToInt16(reader["Illness"])), STValues));
               

            }
            reader.Close();
            connection.Close();
            return ameasurements;
        }

        public List<ECGMonitorModel> GetAllECGMonitors()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<ECGMonitorModel> ecgMonitors = new List<ECGMonitorModel>();


            command = new SqlCommand("select * from dbo.ECGMonitor", connection);
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
                    bytesArr = (List<byte>)reader["BLOBValues"];
                }
                for (int i = 0, j = 0; i < bytesArr.Count; i += 8, j++)
                {
                    tal[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }


                measurements.Add(new ECGModel(Convert.ToString(reader["CPRID"]),Convert.ToInt32(reader["Id"]),
                    Convert.ToDateTime(reader["Date"]), Convert.ToInt32(reader["Samplerate"]),tal, Convert.ToInt16(reader["MonitorID"]), 
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


            command = new SqlCommand("select * from dbo.Illness", connection);
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

        public void UpdateIsAnalyzed(ECGModel ecgID)
        {

            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

                string insertStringParam = "UPDATE dbo.ECG SET IsAnalyzed = 1 WHERE ECGID =" + ecgID.ECGID;
                using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }

            connection.Close();
        }

        public void UpdateIsRead(AnalyzedECGModel analyzedECG)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();


                string insertStringParam = "UPDATE dbo.AnalyzedECG SET IsRead = 1 WHERE AECGID =" + analyzedECG.AECGID;
                using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }
            connection.Close();
        }

        public void UpdateLinkECGToPatient(PatientModel patient, ECGMonitorModel ecgMonitor)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

           
                string insertStringParam = "UPDATE dbo.Patient SET LinkedECG = '" + Convert.ToString(ecgMonitor.ID) + "' WHERE = cpr = '" + patient.CPR + "'";
                using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }

                string insertStringParam2 = "UPDATE dbo.ECGMonitor SET inUse = 1 WHERE ECGMonitorID = " + ecgMonitor.ID;
                using (SqlCommand cmd = new SqlCommand(insertStringParam2, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }
            connection.Close();
        }

        public void UpdateResetECGMonitor(ECGMonitorModel ecgMonitor)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();
                string insertStringParam = "UPDATE dbo.Patient SET LinkedECG = NULL WHERE LinkedECG = '" + ecgMonitor.ID + "'"; //Meget i tvivl
                using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }

                string insertStringParam2 = "UPDATE dbo.ECGMonitor SET inUse = 0 WHERE ECGMonitorID = '" + ecgMonitor.ID + "'";
                using (SqlCommand cmd = new SqlCommand(insertStringParam2, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }

            connection.Close();

            
        }

        public void UploadAnalyzedECGs(AnalyzedECGModel analyzedEcg)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            string insertStringParam = @"INSERT INTO dbo.AnalyzedECG (AECGID, ECGID, CPR, Illness, Date, BLOBstValues, Samplerate, MonitorID, IsRead) VALUES (@AECGID, @ECGID, @CPR, @BLOBValues, @Illness, @Date, @BLOBstValues, @Samplerate, @MonitorID, @IsRead)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                cmd.Parameters.AddWithValue("@AECGID", analyzedEcg.AECGID);
                cmd.Parameters.AddWithValue("@ECGID", analyzedEcg.ECGID);            
                cmd.Parameters.AddWithValue("@CPR", analyzedEcg.CPR);
                cmd.Parameters.AddWithValue("@Illness", analyzedEcg.Illnes.Id);
                cmd.Parameters.AddWithValue("@Date", analyzedEcg.Date);
                cmd.Parameters.AddWithValue("@Samplerate", analyzedEcg.SampleRate);
                cmd.Parameters.AddWithValue("@IsRead", analyzedEcg.IsRead);
                cmd.Parameters.AddWithValue("@MonitorID", analyzedEcg.MonitorID);
                cmd.Parameters.AddWithValue("@BLOBValues", analyzedEcg.Values.SelectMany(value => BitConverter.GetBytes(value).ToArray()));
                cmd.Parameters.AddWithValue("@BLOBstValues", analyzedEcg.STValues.SelectMany(value1 => BitConverter.GetBytes(value1).ToArray()));

                

                reader = cmd.ExecuteReader(); 
                double id = (double)cmd.ExecuteScalar();
                reader.Read();
            }

            connection.Close();
        }
    }
}
