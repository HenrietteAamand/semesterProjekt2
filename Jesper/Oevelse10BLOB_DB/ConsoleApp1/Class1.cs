using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oevelse10BLOB_DB
{
    class Class1
    {
        static void Main(string[] args)
        {
            double[] tal = { 3.56, 8.6, 9.432, 4.3, 6.987, 10.22 }, tal2;
            Console.WriteLine("Gemte tal:");
            for (int i = 0; i < tal.Length; i++)
            {
                Console.Write(tal[i] + " ");
            }
            Console.WriteLine();
            int index = gem(tal);
            tal2 = hent(index);
            Console.WriteLine("Hentede tal:");
            for (int i = 0; i < tal2.Length; i++)
            {
                Console.Write(tal2[i] + " ");
            }
            Console.WriteLine();
        }

        public static int gem(double[] arr)
        {
            SqlConnection conn;
            const String db = "F20ST2ITS2201811363";
            int retur;
            conn = new SqlConnection("Data Source = st-i4dab.uni.au.dk;Initial Catalog = " + db + ";Persist Security Info = True;User ID = " + db + ";Password = " + db + "");
            conn.Open();
            string insertStringParam = @"INSERT INTO Data (Værdier) OUTPUT INSERTED.Id VALUES(@data)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, conn))
            {
                cmd.Parameters.AddWithValue("@data",
                arr.SelectMany(value =>
                BitConverter.GetBytes(value)).ToArray());
                retur = (int)cmd.ExecuteScalar();
            }
            conn.Close();
            return retur;
        }

        public static double[] hent(int index)
        {
            SqlConnection conn;
            const String db = "F20ST2ITS2201811363";

            conn = new SqlConnection("Data Source = st-i4dab.uni.au.dk;Initial Catalog = " + db + ";Persist Security Info = True;User ID = " + db + ";Password = " + db + "");
            conn.Open();
            SqlDataReader rdr;
            byte[] bytesArr = new byte[8];
            double[] tal;
            string selectString = "Select * from Data where Id = " + index;
            using (SqlCommand cmd = new SqlCommand(selectString, conn))
            {
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                    bytesArr = (byte[])rdr["Værdier"];
                tal = new double[bytesArr.Length / 8];

                for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
                    tal[j] = BitConverter.ToDouble(bytesArr, i);
            }
            conn.Close();
            return tal;
        }
    }
}
