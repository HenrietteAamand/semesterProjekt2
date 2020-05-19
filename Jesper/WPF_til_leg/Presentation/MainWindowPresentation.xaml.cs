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
using System.Timers;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowPresentation : MetroWindow, INotifyPropertyChanged
    {
        private ChartECG chartObj;
        private MainWindowLogic mainObj;
        //private ChartECG chartObj;
        private AnalyzeECG analyzeObj;
        List<PatientModel> Patients;
        List<AnalyzedECGModel> aECGS;
        private readonly CollectionViewSource viewSource = new CollectionViewSource();
        System.Timers.Timer opdaterTimer;
        public SetupPatientUC setupUC;

        private string filterText;
        private CollectionViewSource usersCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowPresentation()
        {
            InitializeComponent();
            //if (idT.Text == "")
            //{
            //    UploadB.IsEnabled = false;
            //}

            opdaterTimer = new System.Timers.Timer();
            mainObj = new MainWindowLogic();
            chartObj = new ChartECG();
            analyzeObj = new AnalyzeECG();
            analyzeObj.CreateAnalyzedECGs();
            ShowWelcomeDialog();

            

            aECGS = new List<AnalyzedECGModel>();
            Patients = new List<PatientModel>();
            Patients = mainObj.getAllPatiens();


            usersCollection = new CollectionViewSource();
            usersCollection.Source = Patients;
            usersCollection.Filter += usersCollection_Filter;
            DataContext = this;
            UploadB.IsEnabled = false;
        }





        async Task ShowWelcomeDialog()
        {

            await this.ShowMessageAsync("Velkommen", $"Der er {analyzeObj.NewAECGModelsList.Count} nye EKG målinger.", MessageDialogStyle.Affirmative);

            //if (result == MessageDialogResult.Affirmative)
            //{
            //    analyzeObj.CreateAnalyzedECGs();
                
            //    await this.ShowMessageAsync($"  EKG målinger er blevet opdateret","");
            //}

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            settingsFlyOut.IsOpen = true;
        }

        private void UploadB_Click(object sender, RoutedEventArgs e)
        {
            
            if (idT.Text != "" && commentT.Text != "")
            {

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
            commentT.Clear();
        }

        private void okB_Click(object sender, RoutedEventArgs e)
        {
           

            UploadB.Visibility = Visibility.Visible;
            idT.Visibility = Visibility.Visible;
            uploadPressed.Visibility = Visibility.Hidden;

            dynamic aECG = ecgLV.SelectedItem;
            dynamic patient = patientsLV.SelectedItem;
            mainObj.UploadData(idT.Text, commentT.Text, aECG, patient);
            idT.Text = "Måling uploaded.";
            idT.IsEnabled = false;
            commentT.IsEnabled = false;
            commentT.Clear();

        }

        private void patientsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (patientsLV.SelectedValue != null)
            {


                dynamic patient = patientsLV.SelectedItem;
                string cpr = patient.CPR;
                ecgLV.ItemsSource = mainObj.GetAECGListForPatient(cpr);

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

            }

        }

        private void ecgLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic aECG = new AnalyzedECGModel();
            idT.Clear();
            idT.IsEnabled = true;
            commentT.IsEnabled = true;


            if (ecgLV.SelectedItem != null)
            {
                aECG = ecgLV.SelectedItem;
                //AnalyzedECGModel aECG = mainObj.aECGList[1];

                chartUC.MakeCharts(mainObj.GetECGValues(aECG.AECGID), aECG.STValues.Count, aECG.STStartIndex, aECG.Baseline, aECG.SampleRate);
                

                if (aECG.Illness.Id == 2)
                {
                    chartUC.STawareTB.Text = "Mistanke: ST-segment eleveret";
                }
                else if (aECG.Illness.Id == 3)
                {
                    chartUC.STawareTB.Text = "Mistanke: ST-segment deprimeret";

                }
                else
                {
                    chartUC.STawareTB.Text = "Ingen mistanke";
                }

                chartUC.To = 2 / aECG.SampleRate;
                chartUC.From = 0;
            }
            //if (commentT.Text != "" && idT.Text != "")
            //{
            //    UploadB.IsEnabled = true;
            //}
            //if (commentT.Text == "" && idT.Text == "")
            //{
            //    UploadB.IsEnabled = false;
            //}


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
                OnPropertyChanged("FilterText");
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


        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private void UpdateB_Click(object sender, RoutedEventArgs e)
        {
            //dynamic patient = patientsLV.SelectedItem;
            analyzeObj.GetNewECG();
            if (analyzeObj.newECGList.Count() != 0)
            {
                UpdateView();
                updateBadge.Badge = 0;
                //ecgLV.ItemsSource = mainObj.GetAECGListForPatient(patient.ID);
            }
            ShowUpdateDialog();


        }

        async Task ShowUpdateDialog()
        {

            await this.ShowMessageAsync("Opdateret", $"Der er {analyzeObj.NewAECGModelsList.Count} nye EKG målinger.", MessageDialogStyle.Affirmative);

            //if (result == MessageDialogResult.Affirmative)
            //{
            //    analyzeObj.CreateAnalyzedECGs();

            //    await this.ShowMessageAsync($"  EKG målinger er blevet opdateret","");
            //}

        }

        public void UpdateView()
        {

            Patients = mainObj.getAllPatiens();
            analyzeObj.CreateAnalyzedECGs();

            usersCollection.Source = Patients;
            usersCollection.Filter += usersCollection_Filter;

            OnPropertyChanged("SourceCollection");
            DataContext = this;
        }
        private void idT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ecgLV.SelectedItem != null)
            {
                if (commentT.Text != "" && idT.Text != "")
                {
                    UploadB.IsEnabled = true;
                }
                else
                {
                    UploadB.IsEnabled = false;
                }
            }            
        }

        private void commentT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ecgLV.SelectedItem != null)
            {
                if (commentT.Text != "" && idT.Text != "")
                {
                    UploadB.IsEnabled = true;
                }
                else
                {
                    UploadB.IsEnabled = false;
                }
            }           
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //setupUC = new SetupPatientUC(this);
            updateBadge.Badge = 0;
            opdaterTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            opdaterTimer.Interval = 5000;
            opdaterTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            opdaterTimer.Stop();
            
            this.Dispatcher.Invoke(() =>
            {
                analyzeObj.GetNewECG();
                if (analyzeObj.newECGList.Count() !=  (int)updateBadge.Badge)
                {
                    updateBadge.Badge = analyzeObj.newECGList.Count();
                }
                
            });
            
            opdaterTimer.Start();
        }
    }
}
