using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using LogicTier;
using Models.Models;


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
            //SetupTB.Visibility = Visibility.Hidden;

            //setupObj = new SetupWindowLogic();
            //monitorList = new List<ECGMonitorModel>();
            //patientList = new List<PatientModel>();

            //monitorList= setupObj.getAllMonitors();
            //patientList = setupObj.getAllPatiens();

            //foreach (ECGMonitorModel monitor in monitorList)
            //{
            //    string item = $"Måler {monitor.ID}";
            //    EcgCB.Items.Add(item);
            //}

            //foreach (PatientModel patient in patientList)
            //{
            //    string item = $"PatientID {patient.ID}";
            //    PatientIDCB.Items.Add(item);
            //}

        }

        //Trykker på Tilknyt knappen:
        private void LinkECGB_Click(object sender, RoutedEventArgs e)
        {
            //string patientIDString = EcgCB.SelectedItem.ToString().Remove(0,9);
            
            //int ecgMonitorID = 0;
            //string ecgMonitorString = EcgCB.SelectedItem.ToString();
            //ecgMonitorID = Convert.ToInt32(ecgMonitorString.Remove(0, 5));

            //setupObj.LinkECGToPatient(patientIDString, ecgMonitorID);

            //SetupTB.Visibility = Visibility.Visible;
            //SetupTB.Text = "Tilknytning gennemført.";
        }

        private void ResetECGB_Click(object sender, RoutedEventArgs e)
        {
            //int ecgMonitorID = 0;
            //string ecgMonitorString = EcgCB.SelectedItem.ToString();
            //ecgMonitorID = Convert.ToInt32(ecgMonitorString.Remove(0, 5));

            //setupObj.resetECGMonitor(ecgMonitorID);
            //SetupTB.Visibility = Visibility.Visible;
            //SetupTB.Text = "Nulstilling gennemført.";
        }

        // Tilknyt patient eller nulstil EKG-måler:
        private void EcgCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //int ecgMonitorID = 0;
            //string ecgMonitorString = EcgCB.SelectedItem.ToString();
            //ecgMonitorID = Convert.ToInt32(ecgMonitorString.Remove(0, 5));

            //if (setupObj.monitorInUse(ecgMonitorID) == false)
            //{
            //    ResetECGB.IsEnabled = false;
                
            //    SetupTB.Visibility = Visibility.Visible;
            //    SetupTB.Text = "EKG-måler er ikke i brug.";
            //}
            //else
            //{
            //    LinkECGB.IsEnabled = true;
            //    SetupTB.Visibility = Visibility.Visible;
            //    SetupTB.Text = "EKG-måler er i brug.";
            //}
           

        }
    }
}
