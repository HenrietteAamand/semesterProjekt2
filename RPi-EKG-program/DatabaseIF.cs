﻿using System;
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
            //connection = new SqlConnection("Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + "; User ID=" + db + ";Password=" + db + "");
            //connection = new SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PrivateDatabase.mdf;Integrated Security=True");
            //Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\emils\source\repos\semesterProjekt2\RPi - EKG - program\PrivateDatabase.mdf; Integrated Security = True
            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\EMILS\SOURCE\REPOS\SEMESTERPROJEKT2\RPI-EKG-PROGRAM\PRIVATEDATABASE.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False


            //NY
            //DEN FRA DATA SOURCE//Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PrivateDataBase1v.mdf;Integrated Security=True
            //String fra properties //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\EMILS\SOURCE\REPOS\SEMESTERPROJEKT2\RPI-EKG-PROGRAM\PRIVATEDATABASE1V.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
           //connection = new SqlConnection(@"Data Source=SNDERGAARD\PRIVATESQLDB;User ID="+db+";Password="+db+";Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection=new SqlConnection("Data Source=SNDERGAARD\\PRIVATESQLDB;Initial Catalog="+db+";User ID="+db+";Password="+db+";Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
            //Writecmd.Parameters.AddWithValue("@Data", DatArray.SelectMany(value => BitConverter.GetBytes(value)).ToArray());

            try
            {
                Writecmd = new SqlCommand("INSERT INTO dbo.Measurement (ID,CPRID,BLOBmeasurement,Dato,Samplerate,MeasurerID) VALUES(@NytID,@CPR,@Data,@Dato,@SampleRate,@MeasurerID)", connection);
                Writecmd.Parameters.AddWithValue("@NytID", 3);
                Writecmd.Parameters.AddWithValue("@CPR", Maalinger.CPRNr);
                Writecmd.Parameters.AddWithValue("@Dato", Maalinger.Dato);
                Writecmd.Parameters.AddWithValue("@Samplerate", Maalinger.samplerate);
                Writecmd.Parameters.AddWithValue("@MeasurerID", Maalinger.MeasurerID);

                double[] DataArray = new double[Maalinger.Measurements.Count];

                for (int i = 0; i < DataArray.Length; i++)
                {
                    DataArray[i] = Maalinger.Measurements[i];
                }

                Writecmd.Parameters.AddWithValue("@Data", DataArray.SelectMany(value => BitConverter.GetBytes(value)).ToArray());

                connection.Open();
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
            Readcmd = new SqlCommand("Select CPR,ForNavn from dbo.Patient WHERE tilknyttetEKG = @MeasurerID", connection);
            Readcmd.Parameters.AddWithValue("@MeasurerID", MeasurerID);

            string CPR;
            string Fornavn;
            string CPRNavn = "";



            try
            {
                connection.Open();
                reader = Readcmd.ExecuteReader();
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
