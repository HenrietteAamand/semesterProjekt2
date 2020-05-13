using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using LogicTier;
using DataTier.Models;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowPresentation : MetroWindow
    {
        private MainWindowLogic mainObj;
        //private ChartECG chartObj;
        private AnalyzeECG analyzeObj;
        List<PatientModel> Patients;
        List<AnalyzedECGModel> aECGS;

        public MainWindowPresentation()
        {
            InitializeComponent();
            ShowDialog();

            
            while (idT.Text == null)
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
            patientsLV.ItemsSource = Patients;

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
            uploadPressed.Visibility = Visibility.Hidden;
            okB.Visibility = Visibility.Visible;
            cancelB.Visibility = Visibility.Visible;
        }

        private void cancelB_Click(object sender, RoutedEventArgs e)
        {
            uploadPressed.Visibility = Visibility.Visible;
            okB.Visibility = Visibility.Hidden;
            cancelB.Visibility = Visibility.Hidden;
            idT.Clear();
        }

        private void okB_Click(object sender, RoutedEventArgs e)
        {
            uploadPressed.Visibility = Visibility.Visible;
            okB.Visibility = Visibility.Hidden;
            cancelB.Visibility = Visibility.Hidden;
                        
            mainObj.UploadData(idT.Text);

            idT.Text = "Måling uploaded.";

        }

        private void patientsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            dynamic patient = patientsLV.SelectedItem;
            string cpr = patient.CPR;

            CPRTB.Text = cpr;
            NavnTB.Text = mainObj.GetPatient(cpr).FirstName + " " + mainObj.GetPatient(cpr).LastName;
            AlderTB.Text = Convert.ToString(mainObj.GetAge(cpr));
            KonTB.Text = Convert.ToString(mainObj.IsAMan(cpr));
            ecgLV.ItemsSource = mainObj.GetAECGListForPatient(cpr);

        }

        private void ecgLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ecgLV.SelectedItem != null)
            {
                dynamic aECG = ecgLV.SelectedItem;
                //AnalyzedECGModel aECG = mainObj.aECGList[1];

                chartUC.MakeECGLine(mainObj.GetECGValues(aECG.AECGID), aECG.STValues.Count, aECG.STStartIndex);
            }
            
            //chartUC.MakeCharts(mainObj.GetECGValues(aECG.AECGID), a, 3);

            //chartUC.MakeECGChart(mainObj.GetECGValues(aECG.AECGID));
            //chartUC.MakeST(mainObj.GetECGValues(aECG.AECGID), aECG.STValues.Count, aECG.STStartIndex);

        }
    }
}
