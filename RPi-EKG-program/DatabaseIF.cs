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

            //// SKAL VÆRE FALSE siden vi ikke har mulighed for at connecte til DB'en
            return true;
        }

        public void sendData(Measurement Maalinger)
        {

            //double[] DatArray = new double[Maalinger.Measurements.Count];
            //for (int i = 0; i < DatArray.Length; i++)
            //{
            //    DatArray[i] = Maalinger.Measurements[i];
            //}
            //writecmd.Parameters.AddWithValue("@Data", DatArray.SelectMany(value => BitConverter.GetBytes(value)).ToArray());

            try
            {
                writecmd = new SqlCommand("INSERT INTO dbo.Measurement (ID,CPRID,BLOBmeasurement,Dato,Samplerate,MeasurerID) VALUES(@NytID,@CPR,@Data,@Dato,@SampleRate,@MeasurerID)", connection);
                writecmd.Parameters.AddWithValue("@NytID", 3);
                writecmd.Parameters.AddWithValue("@CPR", Maalinger.CPRNr);
                writecmd.Parameters.AddWithValue("@Dato", Maalinger.Date);
                writecmd.Parameters.AddWithValue("@Samplerate", Maalinger.SampleRate);
                writecmd.Parameters.AddWithValue("@MeasurerID", Maalinger.MeasurerID);

                double[] dataArray = new double[Maalinger.Measurements.Count];

                for (int i = 0; i < dataArray.Length; i++)
                {
                    dataArray[i] = Maalinger.Measurements[i];
                }

                writecmd.Parameters.AddWithValue("@Data", dataArray.SelectMany(value => BitConverter.GetBytes(value)).ToArray());

                connection.Open();
                writecmd.ExecuteScalar();
            }
            catch (SqlException)
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
            readcmd = new SqlCommand("Select CPR,ForNavn from dbo.Patient WHERE tilknyttetEKG = @MeasurerID", connection);
            readcmd.Parameters.AddWithValue("@MeasurerID", MeasurerID);

            string CPR;
            string Fornavn;
            string CPRNavn = "";



            try
            {
                connection.Open();
                reader = readcmd.ExecuteReader();
                if (reader.Read())
                {
                    CPR = (string)reader["CPR"];


                    Fornavn = (string)reader["ForNavn"];

                    CPRNavn =CPR+";"+ Fornavn;
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

            return CPRNavn;
        }



    }
}
