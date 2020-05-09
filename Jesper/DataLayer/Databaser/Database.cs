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
        } //virker

        public IllnessModel getIllness(int id)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            IllnessModel illness = new IllnessModel(0,"NN", " ", 0,0,false,false);

            command = new SqlCommand("SELECT * FROM dbo.Illness WHERE ID = " + id, connection);
            connection.Open();

            reader = command.ExecuteReader();


            if (reader.Read()) //kunne godt laves om til if
            {

                illness = new IllnessModel(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Name"]),
                    Convert.ToString(reader["About"]), Convert.ToDouble(reader["stMax"]), Convert.ToDouble(reader["srMax"]), 
                    Convert.ToBoolean(reader["STSegmentElevated"]), Convert.ToBoolean(reader["STSegmentDepressed"]));

            }

            connection.Close();
            return illness;
        } //virker

        public List<AnalyzedECGModel> GetAllAnalyzedECGs()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<AnalyzedECGModel> ameasurements = new List<AnalyzedECGModel>();
            

            command = new SqlCommand("select * from dbo.AnalyzedECG", connection);
            connection.Open();

            reader = command.ExecuteReader();

            
            while (reader.Read())
            {
                byte[] bytesArr = new byte[] { };
                double[] values = new double[800];
                byte[] bytesArr1 = new byte[] { };
                double[] STValues = new double[800];

                bytesArr = (byte[])reader["BLOBValues"];

                for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
                {
                    values[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }

                bytesArr1 = (byte[])reader["BLOBstValues"];

                for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
                {
                    STValues[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }

                ameasurements.Add(new AnalyzedECGModel(Convert.ToString(reader["CPR"]), Convert.ToInt32(reader["ECGID"]), Convert.ToInt32(reader["AECGID"]),
                    Convert.ToDateTime(reader["Date"]), Convert.ToDouble(reader["Samplerate"]), values.ToList<double>(), 
                    Convert.ToString(reader["MonitorID"]), Convert.ToBoolean(reader["IsRead"]), getIllness(Convert.ToInt16(reader["Illness"])), STValues.ToList<double>(), true));

            }
            
            connection.Close();
            return ameasurements;
        } //virker

        public List<ECGMonitorModel> GetAllECGMonitors()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<ECGMonitorModel> ecgMonitors = new List<ECGMonitorModel>();


            command = new SqlCommand("select * from dbo.ECGMonitor", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {

                ecgMonitors.Add(new ECGMonitorModel(Convert.ToString(reader["ECGMonitorID"]), Convert.ToBoolean(reader["InUse"])));

            }

            connection.Close();
            return ecgMonitors;

        } // virker

        public List<ECGModel> GetAllECGs()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<ECGModel> measurements = new List<ECGModel>();


            command = new SqlCommand("select * from dbo.ECG", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {
                byte[] bytesArr = new byte[] { };
                double[] tal = new double[800];

                bytesArr = (byte[])reader["BLOBValues"];
                
                for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
                {
                    tal[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                }

                measurements.Add(new ECGModel(Convert.ToString(reader["CPR"]),Convert.ToInt32(reader["ECGID"]),
                    Convert.ToDateTime(reader["Date"]), Convert.ToDouble(reader["Samplerate"]), tal.ToList<double>(), Convert.ToString(reader["MonitorID"]), 
                    Convert.ToBoolean(reader["IsAnalyzed"])));

            }
            reader.Close();
            connection.Close();
            return measurements;
        } // virker

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

        } //virker

        public List<PatientModel> GetAllPatients()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<PatientModel> patients = new List<PatientModel>();


            command = new SqlCommand("select * from dbo.Patient", connection);
            connection.Open();

            reader = command.ExecuteReader();


            while (reader.Read())
            {

                patients.Add(new PatientModel(Convert.ToString(reader["LinkedECG"]), Convert.ToString(reader["CPR"]), 
                    Convert.ToString(reader["FirstName"]), Convert.ToString(reader["LastName"])));

            }

            connection.Close();
            return patients;
            
        } //Virker 

        public void UpdateIsAnalyzed(ECGModel ecg)
        {

            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

                string insertStringParam = "UPDATE dbo.ECG SET IsAnalyzed = " + ecg.IsAnalyzed + " WHERE ECGID =" + ecg.ECGID;
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
        } // skal ikke være der

        public void UpdatePatient(PatientModel patient)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            connection.Open();

           
            string insertStringParam = "UPDATE dbo.Patient SET LinkedECG = @LinkedECG WHERE CPR = '" + patient.CPR + "'";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                cmd.Parameters.AddWithValue("@LinkedECG", patient.ECGMonitorID);

                reader = cmd.ExecuteReader();
                reader.Read();    
            }
            connection.Close();
        } // virker

        public void UpdateECGMonitor(ECGMonitorModel ecgMonitor)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

                string insertStringParam = "UPDATE dbo.ECGMonitor SET inUse = " + Convert.ToByte(ecgMonitor.InUse)+ " WHERE ECGMonitorID = '" + ecgMonitor.ID + "'";
                using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                }
            connection.Close();  
        } //Virker

        public void UpdateAnalyzedECG(AnalyzedECGModel analyzedEcg)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            string insertStringParam = "UPDATE dbo.AnalyzedECG SET STStartIndex = " + analyzedEcg.STStartIndex + ", Baseline = " + analyzedEcg.Baseline + ", IsRead = " + analyzedEcg.IsRead + " WHERE AECGID = " + analyzedEcg.AECGID;
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
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
                cmd.Parameters.AddWithValue("@Date", (analyzedEcg.Date).ToLongDateString());
                cmd.Parameters.AddWithValue("@Samplerate", analyzedEcg.SampleRate);
                cmd.Parameters.AddWithValue("@IsRead", analyzedEcg.IsRead);
                cmd.Parameters.AddWithValue("@MonitorID", analyzedEcg.MonitorID);
                cmd.Parameters.AddWithValue("@BLOBValues", analyzedEcg.Values.SelectMany(value => BitConverter.GetBytes(value).ToList()).ToArray());
                cmd.Parameters.AddWithValue("@BLOBstValues", analyzedEcg.STValues.SelectMany(value => BitConverter.GetBytes(value).ToList()).ToArray());

                

                reader = cmd.ExecuteReader(); 
                
                reader.Read();
            }

            connection.Close();
        }
    }
}
