using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;

namespace Oevelse2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Random random;
        double[] tal = new double[100];
        public SeriesCollection SeriesCollection { get; set; }
        private LineSeries ChartLine;
        SqlConnection conn;
        const String db = "F20ST2ITS2201811363";

        public MainWindow()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            ChartLine = new LineSeries();
            ChartLine.Values = new ChartValues<double>() {};
            SeriesCollection.Add(ChartLine);
            random = new Random();
            DataContext = this;
            LoadIndex();



        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void valuesB_Click(object sender, RoutedEventArgs e)
        {
            int id;
            ChartLine.Values.Clear();
            for (int i = 0; i < 100; i++)
            {
                tal[i] = random.NextDouble();
            }

            OnPropertyChanged("tal");

            conn = new SqlConnection("Data Source = st-i4dab.uni.au.dk;Initial Catalog = " + 
                db + ";Persist Security Info = True;User ID = " + db + ";Password = " + db + "");
            conn.Open();

            string insertStringParam = @"INSERT INTO Data2 (Værdier) OUTPUT INSERTED.Id VALUES(@data)";
            using (SqlCommand cmd = new SqlCommand(insertStringParam, conn))
            {
                cmd.Parameters.AddWithValue("@data", tal.SelectMany(value =>BitConverter.GetBytes(value)).ToArray());
                id = (int)cmd.ExecuteScalar();
            }

            selectedCB.Items.Add(id);
            selectedCB.SelectedIndex = id;

            conn.Close();
        }

        private void selectedCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            conn = new SqlConnection("Data Source = st-i4dab.uni.au.dk;Initial Catalog = " + 
                db + ";Persist Security Info = True;User ID = " + db + ";Password = " + db + "");
            conn.Open();

            SqlDataReader rdr;
            byte[] bytesArr = new byte[800];
            double[] tal = new double[100];

            string selectString = $"SELECT * FROM Data2 WHERE Id ={selectedCB.SelectedItem}";

            using (SqlCommand cmd = new SqlCommand(selectString, conn))
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

            rdr.Close();
            conn.Close();

        }

        public void LoadIndex()
        {
            conn = new SqlConnection("Data Source = st-i4dab.uni.au.dk;Initial Catalog = " + 
                db + ";Persist Security Info = True;User ID = " + db + ";Password = " + db + "");
            conn.Open();

            SqlCommand cmdCount = new SqlCommand("SELECT COUNT(Id) FROM Data2", conn);
            int count = (int)cmdCount.ExecuteScalar();

            SqlDataReader rdr;
            List<int> idArr = new List<int>();
            List<int> id = new List<int>();


            string selectString = $"SELECT * FROM Data2";

            using (SqlCommand cmd = new SqlCommand(selectString, conn))
            {
                rdr = cmd.ExecuteReader();
            }

            for (int i = 0; i < count; i++)
            {
                idArr.Add(i);
            }
            
            //while(rdr.Read())
            //    for (int i = 0; i < idArr.Count(); i++)
            //    {
            //        idArr.Add((int)rdr["Id"]);
       
            //    }
                

            
            for (int i = 0; i < idArr.Count(); i++)
            {
                selectedCB.Items.Add(idArr[i]);
            }

            rdr.Close();
            conn.Close();
        }
    }
}
