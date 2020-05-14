using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using LogicTier;
using DataTier.Models;


//using System.Windows.Forms;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for SetupECGUC.xaml
    /// </summary>
    public partial class SetupECGUC : UserControl
    {
        private SetupWindowLogic setupObj;
        
        private List<ECGMonitorModel> monitorList;
        private List<PatientModel> patientList;
       

        public SetupECGUC()
        {
            InitializeComponent();
            SetupTB.Visibility = Visibility.Hidden;

            setupObj = new SetupWindowLogic();

            LoadCB();

            ResetECGB.IsEnabled = false;
            LinkECGB.IsEnabled = false;
        }

        public void LoadCB()
        {
            monitorList = new List<ECGMonitorModel>();
            patientList = new List<PatientModel>();

            monitorList = setupObj.getAllMonitors();
            patientList = setupObj.getAllPatiens();

            foreach (ECGMonitorModel monitor in monitorList)
            {
                string item = $"Måler {monitor.ID}";
                EcgCB.Items.Add(item);
            }

            foreach (PatientModel patient in patientList)
            {
                string item = $"PatientCPR {patient.CPR}";
                PatientIDCB.Items.Add(item);
            }
            PatientIDCB.IsEnabled = false;
            SetupTB.IsEnabled = false;
        }

        public void UpdateCB()
        {
            int ecgMonitorID = 0;
            string ecgMonitorString = EcgCB.SelectedItem.ToString().Remove(0, 6);
            //ecgMonitorID = ecgMonitorString.Remove(0, 5).Remove(6);
            ecgMonitorID = Convert.ToInt32(ecgMonitorString);

            if (setupObj.monitorInUse(ecgMonitorID.ToString()) == false)
            {
                SetupTB.Visibility = Visibility.Visible;
                SetupTB.Text = "EKG-måler er ikke i brug.";
                PatientIDCB.IsEnabled = true;
                

            }
            else if (setupObj.monitorInUse(ecgMonitorID.ToString()) == true)
            {
                LinkECGB.IsEnabled = false;
                ResetECGB.IsEnabled = true;
                PatientIDCB.IsEnabled = false;
                SetupTB.Visibility = Visibility.Visible;
                SetupTB.Text = "EKG-måler er i brug.";
            }
        }

        //Trykker på Tilknyt knappen:
        private void LinkECGB_Click(object sender, RoutedEventArgs e)
        {

            string patientIDString = PatientIDCB.SelectedItem.ToString().Remove(0,11);
            
            int ecgMonitorID = 0;
            string ecgMonitorString = EcgCB.SelectedItem.ToString().Remove(0, 6);
            //ecgMonitorID = ecgMonitorString.Remove(0, 5).Remove(6);
            ecgMonitorID = Convert.ToInt32(ecgMonitorString);

            if (EcgCB.SelectedItem != null)
            {
                setupObj.LinkECGToPatient(patientIDString, ecgMonitorID.ToString());
            }           

            SetupTB.Visibility = Visibility.Visible;
            SetupTB.Text = "Tilknytning gennemført.";
            UpdateCB();

        }

        private void ResetECGB_Click(object sender, RoutedEventArgs e)
        {
            //strin ecgMonitorID = 0;
            string ecgMonitorString = EcgCB.SelectedItem.ToString().Remove(0, 6).Trim();
            //ecgMonitorID = Convert.ToInt32(ecgMonitorString.Remove(0, 5));

            setupObj.ResetECGMonitor(ecgMonitorString);
            SetupTB.Visibility = Visibility.Visible;
            SetupTB.Text = "Nulstilling gennemført.";
            UpdateCB();
        }

        // Tilknyt patient eller nulstil EKG-måler:
        private void EcgCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateCB();
           

        }

        private void PatientIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetECGB.IsEnabled = false;
            LinkECGB.IsEnabled = true;
        }
    }
}
