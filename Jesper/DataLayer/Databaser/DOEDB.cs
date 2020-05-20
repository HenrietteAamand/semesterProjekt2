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
        #region Attributes
        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlCommand command;
        private const String db = "ST2PRJ2OffEKGDatabase";
        private int ecgid;
        #endregion

        #region Ctors
        public DOEDB()
        {
        }
        #endregion

        #region Methods
        public void UploadMaeling(PatientModel patient, string workerID, string note, DateTime date)
        {

            string dato = date.ToString("dd/MM/yyyy");
            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();


            string insertStringParam = @"INSERT INTO dbo.EKGMAELING (dato, antalmaalinger, sfp_ansvrmedarbjnr, sfp_ans_org, borger_fornavn, borger_efternavn, borger_cprnr, sfp_anskommentar) 
                   VALUES (@dato, @antalmaalinger, @sfp_ansvrmedarbjnr, @sfp_ans_org, @borger_fornavn, @borger_efternavn, @borger_cprnr, @sfp_anskommentar)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                cmd.Parameters.AddWithValue("@antalmaalinger", 1);
                cmd.Parameters.AddWithValue("@sfp_ansvrmedarbjnr", workerID);
                cmd.Parameters.AddWithValue("@dato", date);
                cmd.Parameters.AddWithValue("@sfp_ans_org", "Gruppe 5");
                cmd.Parameters.AddWithValue("@borger_fornavn", patient.FirstName);
                cmd.Parameters.AddWithValue("@borger_efternavn", patient.LastName);
                cmd.Parameters.AddWithValue("@borger_cprnr", patient.CPR);
                cmd.Parameters.AddWithValue("@sfp_anskommentar", note);

                cmd.ExecuteNonQuery();
            }

            command = new SqlCommand("SELECT ekgmaaleid FROM dbo.EKGMAELING WHERE borger_cprnr = '" + patient.CPR + "' AND sfp_ansvrmedarbjnr = '" + workerID + "' AND sfp_anskommentar = '" + note + "'", connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                ecgid = Convert.ToInt32(reader["ekgmaaleid"]);
            }
            connection.Close();
        }

        public void UploadData(AnalyzedECGModel analyzedEcg)
        {
            double[] values = (analyzedEcg.Values).ToArray();

            DateTime date = Convert.ToDateTime(analyzedEcg.Date);

            double samplerate = 1 / (analyzedEcg.SampleRate);

            connection = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" + db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

            connection.Open();

            string insertStringParam = @"INSERT INTO dbo.EKGDATA (raa_data, interval_sec, data_format, bin_eller_tekst, start_tid, samplerate_hz, maalenehed_identifikation, maaleformat_type, ekgmaaleid) VALUES (@raa_data, @interval_sec, @data_format, @bin_eller_tekst, @start_tid, @samplerate_hz, @maalenehed_identifikation, @maaleformat_type, @ekgmaaleid)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, connection))
            {
                cmd.CommandText = insertStringParam;
                cmd.Parameters.AddWithValue("@interval_sec", 40);
                cmd.Parameters.AddWithValue("@data_format", "Andet");
                cmd.Parameters.AddWithValue("@start_tid", date);
                cmd.Parameters.AddWithValue("@samplerate_hz", samplerate);
                cmd.Parameters.AddWithValue("@bin_eller_tekst", "B");
                cmd.Parameters.AddWithValue("@maaleformat_type", "double");
                cmd.Parameters.AddWithValue("@maalenehed_identifikation", Convert.ToInt32(analyzedEcg.MonitorID));
                cmd.Parameters.AddWithValue("@raa_data", values.SelectMany(value => BitConverter.GetBytes(value)).ToArray());
                cmd.Parameters.AddWithValue("@ekgmaaleid", ecgid);

                cmd.ExecuteNonQuery();
            }

            connection.Close();

        }
        #endregion

    }
}
