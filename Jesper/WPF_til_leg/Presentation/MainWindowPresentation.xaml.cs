using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using LogicTier;
using DataTier.Models;
using System.Windows.Forms;
using System.Windows.Data;
using ListViewItem = System.Windows.Controls.ListViewItem;
using System.Linq;
using System.Data;
using System.ComponentModel;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowPresentation : MetroWindow, INotifyPropertyChanged
    {
        private MainWindowLogic mainObj;
        //private ChartECG chartObj;
        private AnalyzeECG analyzeObj;
        List<PatientModel> Patients;
        List<AnalyzedECGModel> aECGS;
        private readonly CollectionViewSource viewSource = new CollectionViewSource();

        private string filterText;
        private CollectionViewSource usersCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowPresentation()
        {
            InitializeComponent();
            ShowDialog();

            if (idT.Text == "")
            {
                UploadB.IsEnabled = false;
            }

            mainObj = new MainWindowLogic();
            //chartObj = new ChartECG();
            analyzeObj = new AnalyzeECG();

            aECGS = new List<AnalyzedECGModel>();
            Patients = new List<PatientModel>();
            Patients = mainObj.getAllPatiens();
            analyzeObj.CreateAnalyzedECGs();

            usersCollection = new CollectionViewSource();
            usersCollection.Source = Patients;
            usersCollection.Filter += usersCollection_Filter;
            DataContext = this;
        }

        


        async Task ShowDialog()
        {
            var result = await this.ShowMessageAsync("Velkommen", $"Der er {0} nye EKG målinger. Vil du opdatere?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                analyzeObj.CreateAnalyzedECGs();
                
                await this.ShowMessageAsync($" {analyzeObj.NewAECGModelsList.Count} EKG målinger er blevet opdateret","");
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            settingsFlyOut.IsOpen = true;
        }

        private void UploadB_Click(object sender, RoutedEventArgs e)
        {
            if (idT.Text != "")
            {
                //uploadPressed.Visibility = Visibility.Hidden;
                UploadB.Visibility = Visibility.Hidden;
                idT.Visibility = Visibility.Hidden;
                uploadPressed.Visibility = Visibility.Visible;
            }
        }

        private void cancelB_Click(object sender, RoutedEventArgs e)
        {
            UploadB.Visibility = Visibility.Visible;
            idT.Visibility = Visibility.Visible;
            uploadPressed.Visibility = Visibility.Hidden;
            idT.Clear();
        }

        private void okB_Click(object sender, RoutedEventArgs e)
        {
            UploadB.Visibility = Visibility.Visible;
            idT.Visibility = Visibility.Visible;
            uploadPressed.Visibility = Visibility.Hidden;

            mainObj.UploadData(idT.Text);

            idT.Text = "Måling uploaded.";

        }

        private void patientsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (patientsLV.SelectedValue != null)
            {


                dynamic patient = patientsLV.SelectedItem;
                string cpr = patient.CPR;

                CPRTB.Text = "CPR-NUMMER: " + cpr;
                NavnTB.Text = "NAVN: " + mainObj.GetPatient(cpr).FirstName + " " + mainObj.GetPatient(cpr).LastName;
                AlderTB.Text = "ALDER: " + Convert.ToString(mainObj.GetAge(cpr)) + " år";

                if (mainObj.IsAMan(cpr) == true)
                {
                    KonTB.Text = "KØN: Mand";
                }
                else
                {
                    KonTB.Text = "KØN: Kvinde";
                }

                ecgLV.ItemsSource = mainObj.GetAECGListForPatient(cpr);
            }

        }

        private void ecgLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ecgLV.SelectedItem != null && patientsLV.SelectedValue != null)
            {
                dynamic aECG = ecgLV.SelectedItem;
                //AnalyzedECGModel aECG = mainObj.aECGList[1];

                chartUC.MakeCharts(mainObj.GetECGValues(aECG.AECGID), aECG.STValues.Count, aECG.STStartIndex, aECG.Baseline);
                ecgLV.ItemsSource = mainObj.GetAECGListForPatient(aECG.CPR);
            }
            
            //chartUC.MakeCharts(mainObj.GetECGValues(aECG.AECGID), a, 3);

            //chartUC.MakeECGChart(mainObj.GetECGValues(aECG.AECGID));
            //chartUC.MakeST(mainObj.GetECGValues(aECG.AECGID), aECG.STValues.Count, aECG.STStartIndex);

        }

        private void idT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(idT.Text != "")
            {
                UploadB.IsEnabled = true;
            }
            else
            {
                UploadB.IsEnabled = false;
            }
            
        }

        public ICollectionView SourceCollection
        {
            get
            {
                return this.usersCollection.View;
            }
        }

        public string FilterText
        {
            get
            {
                return filterText;
            }
            set
            {
                filterText = value;
                this.usersCollection.View.Refresh();
                RaisePropertyChanged("FilterText");
            }
        }

        void usersCollection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            PatientModel usr = e.Item as PatientModel;
            if (usr.CPR.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
                patientsLV.SelectedItem = null;
                ecgLV.ItemsSource = null;
            }
        }


        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
