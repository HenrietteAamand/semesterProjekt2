using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using LogicTier;
using DataTier.Models;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for SetupECGUC.xaml
    /// </summary>
    public partial class SetupECGUC : UserControl
    {
        #region Attributes
        private SetupWindowLogic setupObj;
        private List<ECGMonitorModel> monitorList;
        private List<PatientModel> patientList; 
        #endregion

        #region Ctor
        public SetupECGUC()
        {
            InitializeComponent();
            SetupTB.Visibility = Visibility.Hidden;

            setupObj = new SetupWindowLogic();

            LoadCB();

            ResetECGB.IsEnabled = false;
            LinkECGB.IsEnabled = false;
        }
        #endregion

        #region Methods
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
                string item = $"CPR {patient.CPR}: {patient.FullName}";
                PatientIDCB.Items.Add(item);
            }
            PatientIDCB.IsEnabled = false;
            SetupTB.IsEnabled = false;
        }

        public void UpdateCB()
        {
            int ecgMonitorID = 0;
            string ecgMonitorString = EcgCB.SelectedItem.ToString().Remove(0, 6);
            ecgMonitorID = Convert.ToInt32(ecgMonitorString);

            if (setupObj.monitorInUse(ecgMonitorID.ToString()) == false)
            {
                SetupTB.Visibility = Visibility.Visible;
                SetupTB.Text = "EKG-måler er ikke i brug.";
                PatientIDCB.IsEnabled = true;
                LinkECGB.IsEnabled = false;
                ResetECGB.IsEnabled = false;

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

        private void LinkECGB_Click(object sender, RoutedEventArgs e)
        {
            string patientIDString = PatientIDCB.SelectedItem.ToString().Remove(0, 4).Remove(11);

            int ecgMonitorID = 0;
            string ecgMonitorString = EcgCB.SelectedItem.ToString().Remove(0, 6);
            ecgMonitorID = Convert.ToInt32(ecgMonitorString);

            if (EcgCB.SelectedItem != null)
            {
                setupObj.LinkECGToPatient(patientIDString, ecgMonitorID.ToString());
            }

            SetupTB.Visibility = Visibility.Visible;
            PatientIDCB.SelectedValue = null;
            UpdateCB();
            SetupTB.Text = "Tilknytning gennemført.";

            PatientIDCB.Text = "Vælg et PatientID...";
        }

        private void ResetECGB_Click(object sender, RoutedEventArgs e)
        {
            string ecgMonitorString = EcgCB.SelectedItem.ToString().Remove(0, 6).Trim();

            setupObj.ResetECGMonitor(ecgMonitorString);
            SetupTB.Visibility = Visibility.Visible;
            UpdateCB();
            PatientIDCB.SelectedValue = null;
            SetupTB.Text = "Nulstilling gennemført.";

        }

        // Tilknyt patient eller nulstil EKG-måler:
        private void EcgCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateCB();
            PatientIDCB.SelectedValue = null;


        }

        private void PatientIDCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetECGB.IsEnabled = false;
            LinkECGB.IsEnabled = true;
        }
        #endregion
    }
}
