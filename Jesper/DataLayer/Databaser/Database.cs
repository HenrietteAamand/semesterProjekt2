using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier.Interfaces;
using DataTier;
using System.Data.SqlClient;
using DataTier.Models;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace DataTier.Databaser
{
    public class Database : ILocalDatabase
    {
        private SqlConnection connection;
        private SqlConnection connection2;
        private SqlDataReader reader;
        private SqlDataReader reader2;
        private SqlCommand command;
        private SqlCommand command2;
        private const String db = "F20ST2ITS2201908197";
        IllnessModel illness;


        public Database()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            illness = new IllnessModel();
            illness = GetIllness(1);


        }

        public void CreatePatient(PatientModel patient)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db +
                ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            string insertStringParam = @"INSERT INTO dbo.Patient (CPR, FirstName, LastName) VALUES (@CPR, @FirstName, @LastName)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                cmd.Parameters.AddWithValue("@CPR", (patient.CPR).ToString());
                cmd.Parameters.AddWithValue("@FirstName", (patient.FirstName).ToString());
                cmd.Parameters.AddWithValue("@LastName", (patient.LastName).ToString());

                cmd.ExecuteNonQuery();
                
            }
            
            connection.Close();
        } //virker /

        public IllnessModel GetIllness(int id)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db +
                ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            //IllnessModel illness = new IllnessModel(0,"NN", " ", 0, 0, false, false);
            IllnessModel illness = null;
            command = new SqlCommand("SELECT * FROM dbo.Illness WHERE ID = " + id, connection);
            connection.Open();

            reader2 = command.ExecuteReader();


            if (reader2.Read())
            {

                illness = new IllnessModel(Convert.ToInt32(reader2["ID"]), Convert.ToString(reader2["Name"]),
                    Convert.ToString(reader2["About"]), Convert.ToDouble(reader2["stMax"]), Convert.ToDouble(reader2["srMax"]), 
                    Convert.ToBoolean(reader2["STSegmentElevated"]), Convert.ToBoolean(reader2["STSegmentDepressed"]));
                
            }

            connection.Close();
            return illness;
        } //virker /

        public List<AnalyzedECGModel> GetAllAnalyzedECGs()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            List<AnalyzedECGModel> ameasurements = new List<AnalyzedECGModel>();
            

            command = new SqlCommand("select * from dbo.AnalyzedECG", connection);
            connection.Open();

            reader = command.ExecuteReader();

            
            while (reader.Read())
            {
                int k = 0;
                byte[] bytesArr = new byte[] { };
                double[] values = new double[800];
                byte[] bytesArr1 = new byte[] { };
                double[] STValues = new double[800];
                List<double> valuesList = new List<double>();
                List<double> STValuesList = new List<double>();

                bytesArr = (byte[])reader["BLOBValues"];

                for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
                {
                    
                    //values[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                    valuesList.Add(BitConverter.ToDouble(bytesArr.ToArray(), i));
                    if (i > 4800)
                    {
                        i = bytesArr.Length;
                    }
                }             

                

                ameasurements.Add(new AnalyzedECGModel(Convert.ToString(reader["CPR"]), Convert.ToInt32(reader["ECGID"]), Convert.ToInt32(reader["AECGID"]),
                    Convert.ToDateTime(reader["Date"]), Convert.ToDouble(reader["Samplerate"]), valuesList,
                    Convert.ToString(reader["MonitorID"])));
             
                
                    if (reader["BLOBstValues"] != null)
                    {
                        bytesArr1 = (byte[])reader["BLOBstValues"];
                        for (int i = 0, j = 0; i < bytesArr1.Length; i += 8, j++)
                        {
                            
                            STValues[j] = BitConverter.ToDouble(bytesArr1.ToArray(), i);
                            STValuesList.Add(BitConverter.ToDouble(bytesArr1.ToArray(), i));
                            if (i > 4800)
                            {
                                i = bytesArr1.Length;
                            }
                        }

                        ameasurements[k].STValues = STValuesList;
                    }

                    if (reader["STStartIndex"].GetType() != typeof(DBNull))
                    {
                    ameasurements[k].STStartIndex = Convert.ToInt32(reader["STStartIndex"]);

                    }
                    
                    if (reader["Baseline"].GetType() != typeof(DBNull))
                    {
                    ameasurements[k].Baseline = (double)reader["Baseline"];
                    }

                    if (reader["IsRead"].GetType() != typeof(DBNull))
                    {
                    ameasurements[k].IsRead = (bool)reader["IsRead"];

                    }
                    if (reader["Illness"].GetType() != typeof(DBNull))
                    {
                    ameasurements[k].Illness = illness;
                    }


                //aECG.STDepressed = (bool)reader["STDepressed"];
                //aECG.STElevated = (bool)reader["STElevated"];

                k++;

            }
            
            connection.Close();
            return ameasurements;
        } //virker

        public List<ECGMonitorModel> GetAllECGMonitors()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" +
                db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

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

        } // virker /

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
                double[] tal = new double[80000];
                List<double> talList = new List<double>();

                bytesArr = (byte[])reader["BLOBValues"];
                
                for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
                {
                    tal[j] = BitConverter.ToDouble(bytesArr.ToArray(), i);
                    talList.Add(BitConverter.ToDouble(bytesArr.ToArray(), i));
                }

                measurements.Add(new ECGModel(Convert.ToString(reader["CPR"]),Convert.ToInt32(reader["ECGID"]),
                    Convert.ToDateTime(reader["Date"]), Convert.ToDouble(reader["Samplerate"]), talList,
                    Convert.ToString(reader["MonitorID"]), 
                    Convert.ToBoolean(reader["IsAnalyzed"])));

            }
            reader.Close();
            connection.Close();
            return measurements;
        } // virker /

        public List<IllnessModel> GetAllIllnesses()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + 
                db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

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

        } //virker /

        public List<PatientModel> GetAllPatients()
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db +
                ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

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
            
        } //Virker /

        public void UpdateIsAnalyzed(ECGModel ecg)
        {

            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            string insertStringParam = "UPDATE dbo.ECG SET IsAnalyzed = " + Convert.ToByte(ecg.IsAnalyzed) + " WHERE ECGID =" + ecg.ECGID;
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.ExecuteNonQuery();
            }

            connection.Close();
        } //virker
       
        public void UpdatePatient(PatientModel patient)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            connection.Open();

           
            string insertStringParam = "UPDATE dbo.Patient SET LinkedECG = @LinkedECG WHERE CPR = '" + patient.CPR + "'";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                if (patient.ECGMonitorID == null)
                {
                    cmd.Parameters.AddWithValue("@LinkedECG", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LinkedECG", patient.ECGMonitorID);
                }

                cmd.ExecuteNonQuery();    
            }
            connection.Close();
        } // virker /

        public void UpdateECGMonitor(ECGMonitorModel ecgMonitor)
        {
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            string insertStringParam = "UPDATE dbo.ECGMonitor SET inUse = " + Convert.ToByte(ecgMonitor.InUse)+ " WHERE ECGMonitorID = " + ecgMonitor.ID;
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        } //Virker /

        public void UpdateAnalyzedECG(AnalyzedECGModel analyzedEcg)
        {
            
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" +
                db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();
            
            string insertStringParam = "UPDATE dbo.AnalyzedECG SET " +
                "STStartIndex = " + Convert.ToInt32(analyzedEcg.STStartIndex) +
            ", Baseline = " + Convert.ToDouble(analyzedEcg.Baseline) +
            ", IsRead = " + Convert.ToByte(analyzedEcg.IsRead) +
            ", Illness = " + Convert.ToInt32(analyzedEcg.Illness.Id) +
            " WHERE AECGID = " + Convert.ToInt32(analyzedEcg.AECGID);

            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.ExecuteNonQuery();
            }
            connection.Close();

        } // virker /

        public void UploadAnalyzedECGs(AnalyzedECGModel analyzedEcg)
        {
            double[] values = (analyzedEcg.Values).ToArray();
            double[] stvalues = (analyzedEcg.STValues).ToArray();
            DateTime date = Convert.ToDateTime(analyzedEcg.Date);

            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            string insertStringParam = @"INSERT INTO dbo.AnalyzedECG (AECGID, ECGID, CPR, BLOBValues, Date, Samplerate, MonitorID, IsRead, BLOBstValues) VALUES (@AECGID, @ECGID, @CPR, @BLOBValues, @Date, @Samplerate, @MonitorID, @IsRead, @BLOBstValues)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                cmd.Parameters.AddWithValue("@AECGID", analyzedEcg.AECGID);
                cmd.Parameters.AddWithValue("@ECGID", analyzedEcg.ECGID);            
                cmd.Parameters.AddWithValue("@CPR", analyzedEcg.CPR);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Samplerate", analyzedEcg.SampleRate);
                cmd.Parameters.AddWithValue("@IsRead", analyzedEcg.IsRead);
                cmd.Parameters.AddWithValue("@MonitorID", analyzedEcg.MonitorID);
                cmd.Parameters.AddWithValue("@BLOBValues", values.SelectMany(value => BitConverter.GetBytes(value)).ToArray());
                cmd.Parameters.AddWithValue("@BLOBstValues", stvalues.SelectMany(value => BitConverter.GetBytes(value)).ToArray());




                cmd.ExecuteNonQuery(); 
                
                
            }

            connection.Close();
        } //Virker /
    }
}
