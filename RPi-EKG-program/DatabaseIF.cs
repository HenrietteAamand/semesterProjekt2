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
        private SqlCommand Readcmd;
        private SqlCommand Writecmd;
        private SqlConnection connection;
        private SqlDataReader reader;

        public DatabaseIF()
        {
            connection = new SqlConnection("Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + "; User ID=" + db + ";Password=" + db + "");


        }



        public bool isConnected()
        {
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                return false;
            }
            connection.Close();

            return true; 
        }

        public void sendData(Measurement Maalinger)
        {
            Writecmd = new SqlCommand("INSERT INTO dbo.Measurement (CPR-ID,BLOB-measurement,Dato,Samplerate,MeasurerID) VALUES(@CPR,@Data,@Dato,@SampleRate,@MeasurerID)", connection);
            Writecmd.Parameters.AddWithValue("@CPR",Maalinger.CPRNr);
            Writecmd.Parameters.AddWithValue("@Dato", Maalinger.Dato);
            Writecmd.Parameters.AddWithValue("@Samplerate", Maalinger.samplerate);
            Writecmd.Parameters.AddWithValue("@MeasurerID", Maalinger.MeasurerID);
            Writecmd.Parameters.AddWithValue("@Data", Maalinger.Measurements.SelectMany(value => BitConverter.GetBytes(value)).ToArray());
            //double[] DatArray = new double[Maalinger.Measurements.Count];
            //for (int i = 0; i < DatArray.Length; i++)
            //{
            //    DatArray[i] = Maalinger.Measurements[i];
            //}
            //Writecmd.Parameters.AddWithValue("@Data", DatArray.SelectMany(value => BitConverter.GetBytes(value)).ToArray());
            connection.Open();
            try
            {
                Writecmd.ExecuteScalar();
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

        public string RecieveData(string MeasurerID)
        {
            Readcmd = new SqlCommand("Select CPR,ForNavn from dbo.Patient WHERE tilknyttet EKG = @MeasurerID", connection);
            Readcmd.Parameters.AddWithValue("@MeasurerID", MeasurerID);

            string CPR;
            string Fornavn;
            string Return ="";

            connection.Open();
            try
            {
                reader = Readcmd.ExecuteReader();
                if (reader.Read())
                {
                    CPR = (string)reader["CPR"];
                    
                    for (int i = 0; i < 6; i++)
                    {
                        Return += CPR[i];
                    }

                    Fornavn = (string)reader["ForNavn"];

                    Return += Fornavn;
                }
            }
            catch (SqlException)
            {

                throw;
            }

            return Return;
        }

        

    }
}
