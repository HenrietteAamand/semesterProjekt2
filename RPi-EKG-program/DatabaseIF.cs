using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

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

        public void sendData(Measurement Målinger)
        {
            Writecmd = new SqlCommand("INSERT INTO dbo.Measurement (CPR-ID,BLOB-measurement,Dato,Samplerate,MeasurerID) VALUES(@CPR,@Data,@Dato,@SampleRate,@MeasurerID)", connection);
            Writecmd.Parameters.AddWithValue("@CPR",+Målinger.CPRNr+ );

            

        }

    }
}
