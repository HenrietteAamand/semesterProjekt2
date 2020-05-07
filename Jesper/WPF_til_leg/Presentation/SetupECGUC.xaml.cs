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
            SetupTB.Visibility = Visibility.Hidden;

            setupObj = new SetupWindowLogic();
            monitorList = new List<ECGMonitorModel>();
            patientList = new List<PatientModel>();

            monitorList= setupObj.getAllMonitors();

            foreach (ECGMonitorModel monitor in monitorList)
            {
                string item = $"Måler {monitor.ID}";
                EcgCB.Items.Add(item);
            }

            foreach (PatientModel patient in patientList)
            {
                string item = $"PatientID {patient.ID}";
                PatientIDCB.Items.Add(item);
            }

            //Foregå i main
            //Der skal være et item for hver måler i DB
            //Items'ne skal indeholde ID'erne for målerne i DB -> (Måler 1, 2, 3)
            //GetAllMonitors -> giver os en liste med monitors
            //List<ECGMOnitors> monitorList = new ...... -> monitorList[0] = monitor med ID 1 i DB
            //monitorList[1] = monitor med ID 2 i DB
            //monitorList[2] = monitor med ID 3 i DB
        }


        private void LinkECGB_Click(object sender, RoutedEventArgs e)
        {
            //setupObj.LinkECGToPatient(ecgID, cpr);
            //System.Windows.MessageBox.Show("Tilknytning gennemført.");
        }

        private void ResetECGB_Click(object sender, RoutedEventArgs e)
        {
            //setupObj.resetECGMonitor(ecgID);
            //System.Windows.MessageBox.Show("Nulstilling gennemført.");
        }

        // Tilknyt patient eller nulstil EKG-måler:
        private void EcgCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int ecgMonitorID = 0;
            string ecgMonitorString = EcgCB.SelectedItem.ToString();
            ecgMonitorID = Convert.ToInt32(ecgMonitorString.Remove(0, 5));

            if (setupObj.monitorInUse(ecgMonitorID) == false)
            {
                ResetECGB.IsEnabled = false;
                
                SetupTB.Visibility = Visibility.Visible;
                SetupTB.Text = "EKG-måler er ikke i brug.";
            }
            else
            {
                LinkECGB.IsEnabled = true;
                SetupTB.Visibility = Visibility.Visible;
                SetupTB.Text = "EKG-måler er i brug.";
            }
           

            //Foregår i eventhandler -> slection_changed
            //Vi vælger måler nummer 5 i combobox
            //Den har ID 5 i DB.
            //monitorList[4] = Måler nr. 5 = ID 5 i DB
            //monitorList[4].InUse()
            //if(monitorList[4].InUse())
            //Gøre det med knapperne

        }
    }
}
