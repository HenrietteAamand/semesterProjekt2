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
    public class DOEDB : IDOEDB
    {
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        private const String db = "ST2PRJ2OffEKGDatabase";

        public DOEDB()
        {
        }


        public void UploadData(AnalyzedECGModel analyzedEcg)
        {
            double[] values = (analyzedEcg.Values).ToArray();
            
            DateTime date = Convert.ToDateTime(analyzedEcg.Date);

            double samplerate = 1 / (analyzedEcg.SampleRate);

            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            string insertStringParam = @"INSERT INTO dbo.EKGDATA (raa_data, intervel_sec, data_format, bin_eller_tekst, start_tid, samplerate_hz, ekgmaaleid, maaleformat_type) VALUES (@raa_data, @intervel_sec, @data_format, @bin_eller_tekst, @start_tid, @samplerate_hz, @ekgmaaleid, @maaleformat_type)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                cmd.Parameters.AddWithValue("@intervel_sec", 40);
                cmd.Parameters.AddWithValue("@data_format", "Andet");
                cmd.Parameters.AddWithValue("@start_tid", date);
                cmd.Parameters.AddWithValue("@samplerate_hz", samplerate);
                cmd.Parameters.AddWithValue("@bin_eller_tekst", "B");
                cmd.Parameters.AddWithValue("@maaleformat_type", "B");
                cmd.Parameters.AddWithValue("@ekgmaaleid", Convert.ToInt32(analyzedEcg.MonitorID));
                cmd.Parameters.AddWithValue("@raa_data", values.SelectMany(value => BitConverter.GetBytes(value)).ToArray());

                cmd.ExecuteNonQuery();
            }

            connection.Close();
            
        }
    }
}
