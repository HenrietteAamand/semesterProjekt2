using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oevelse10BLOB_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random;
        int basis;
        private const String db = "F20ST2ITS2201811363"; // Tilrettes jeres egen DB
        private SqlConnection OpenConnectionST
        {
            get
            {
                var con = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;Initial Catalog=" + db + ";Integrated Security=False;User ID=" +
                    db + ";Password=" + db + ";Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
                con.Open();
                return con;
            }
        }
        public SeriesCollection SeriesCollection { get; set; }
        private LineSeries ChartLine;
        public MainWindow()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            ChartLine = new LineSeries();
            ChartLine.Values = new ChartValues<double>();
            random = new Random();
            basis = 0;
            SeriesCollection.Add(ChartLine);
            DataContext = this;
        }

        private void GenererB_Click(object sender, RoutedEventArgs e)
        {
            double[] tal = new double[100];
            int id = 0;
            for (int i = 0; i < 100; i++)
            {
                tal[i] = random.NextDouble() * 10 + basis;
            }
            string insertStringParam = @"INSERT INTO Data (Værdier) OUTPUT INSERTED.Id VALUES(@data)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnectionST))
            {
                cmd.Parameters.AddWithValue("@data", tal.SelectMany(value => BitConverter.GetBytes(value)).ToArray());
                id = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
            Console.WriteLine("ID brugt: " + id);
            DataSetCB.Items.Add(id);
            basis += 10;


        }

        private void DataSetCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double[] tal = new double[100];
            byte[] bytesArr = new byte[800];
            SqlDataReader rdr;
            string selectString = "Select * from Data where Id =" + DataSetCB.SelectedItem;
            using (SqlCommand cmd = new SqlCommand(selectString, OpenConnectionST))
            {
                rdr = cmd.ExecuteReader();
            }
            if (rdr.Read())
            {
                bytesArr = (byte[])rdr["Værdier"];
            }
            for (int i = 0, j = 0; i < bytesArr.Length; i += 8, j++)
            {
                tal[j] = BitConverter.ToDouble(bytesArr, i);
            }
            ChartLine.Values.Clear();
            for (int i = 0; i < tal.Length; i++)
            {
                ChartLine.Values.Add(tal[i]);
            }

        }


    }
}
