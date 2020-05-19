using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace RPi_EKG_program
{
    class DatabaseIF
    {
        private const string db = "F20ST2ITS2201908197";
        private SqlCommand readcmd;
        private SqlCommand writecmd;
        private SqlConnection connection;
        private SqlDataReader reader;

        public DatabaseIF()
        {
           connection=new SqlConnection("Data Source=192.168.0.17\\PRIVATESQLDB;Initial Catalog="+db+";User ID="+db+";Password="+db+";Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


        public bool isConnected()
        {
            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                return false;
            }
            connection.Close();

            return true;
        }

        public void sendData(Measurement measurement)
        {

      
            try
            {
                writecmd = new SqlCommand("INSERT INTO ECG (CPR,BLOBValues,Date,Samplerate,MonitorID) VALUES(@CPR,@Data,@Date,@SampleRate,@MeasurerID)", connection);
                writecmd.Parameters.AddWithValue("@CPR", measurement.CPRNr);
                writecmd.Parameters.AddWithValue("@Date", measurement.Date);
                writecmd.Parameters.AddWithValue("@Samplerate", measurement.SampleRate);
                writecmd.Parameters.AddWithValue("@MeasurerID", measurement.MeasurerID);

                double[] dataArray = new double[measurement.Measurements.Count];

                for (int i = 0; i < dataArray.Length; i++)
                {
                    dataArray[i] = measurement.Measurements[i];
                }

                writecmd.Parameters.AddWithValue("@Data", dataArray.SelectMany(value => BitConverter.GetBytes(value)).ToArray());

                connection.Open();
                writecmd.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public string recieveData(string MeasurerID)
        {
            readcmd = new SqlCommand("Select CPR,FirstName from Patient WHERE LinkedECG = @MeasurerID", connection);
            readcmd.Parameters.AddWithValue("@MeasurerID", MeasurerID);

            string cpr;
            string firstName;
            string cprName = "";



            try
            {
                connection.Open();
                reader = readcmd.ExecuteReader();
                if (reader.Read())
                {
                    cpr = (string)reader["CPR"];


                    firstName = (string)reader["FirstName"];

                    cprName =cpr+";"+ firstName;
                }
            }
            catch (SqlException)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return cprName;
        }



    }
}
