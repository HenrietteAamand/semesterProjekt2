using System.Threading;
using System.Windows;
using System.Windows.Controls;
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
        private ECGMonitorModel ecgModelObj;


        public SetupECGUC()
        {
            InitializeComponent();

            setupObj = new SetupWindowLogic();
            
        }

        
        private void LinkECGB_Click(object sender, RoutedEventArgs e)
        {
            setupObj.LinkECGToPatient(ecgID, cpr);
            System.Windows.MessageBox.Show("Tilknytning gennemført.");
        }

        private void ResetECGB_Click(object sender, RoutedEventArgs e)
        {
            setupObj.resetECGMonitor(ecgID);
            System.Windows.MessageBox.Show("Nulstilling gennemført.");
        }

        // Tilknyt patient eller nulstil EKG-måler:
        private void EcgCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setupObj.getAllMonitors(monitor) = new monitorList<>;

            foreach (object monitor in monitorList)
            {
                EcgCB.Items.Add(ecgModelObj.ID);
            }

            if (setupObj.monitorInUse(monitor) == false)
            {
                ResetECGB.IsEnabled = false;
                System.Windows.MessageBox.Show("EKG-måler er ikke i brug");
            }
            else
            {
                LinkECGB.IsEnabled = true;
                System.Windows.MessageBox.Show("EKG-måler er i brug");
            }
            //Der skal være et item for hver måler i DB
            //Items'ne skal indeholde ID'erne for målerne i DB -> (Måler 1, 2, 3)
            //GetAllMonitors -> giver os en liste med monitors
            //List<ECGMOnitors> monitorList = new ...... -> monitorList[0] = monitor med ID 1 i DB
            //monitorList[1] = monitor med ID 2 i DB
            //monitorList[2] = monitor med ID 3 i DB
            //Vi vælger måler nummer 5 i combobox
            //Den har ID 5 i DB.
            //monitorList[4] = Måler nr. 5 = ID 5 i DB
            //monitorList[4].InUse()
            //if(monitorList[4].InUse())
            //Gøre det med knapperne

        }
    }
}
