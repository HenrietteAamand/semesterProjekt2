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
using LogicTier;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for SetupUC.xaml
    /// </summary>

    public partial class SetupPatientUC : UserControl
    {
        #region Attributes
        private MainWindowPresentation parentWindow;
        private SetupWindowLogic setupObj;
        #endregion

        #region Ctor
        public SetupPatientUC()
        {
            InitializeComponent();
            setupObj = new SetupWindowLogic();

            PatientTB.Visibility = Visibility.Hidden;
            PatientTB.IsEnabled = false;
            CPRTB.MaxLength = 11;
            parentWindow = (MainWindowPresentation)App.Current.MainWindow;
        }
        #endregion

        #region Methods
        private void OpretB_Click(object sender, RoutedEventArgs e)
        {
            if (CPRTB.Text != "" && FnavnTB.Text != "" && EnavnTB.Text != "")
            {
                string day = CPRTB.Text.Substring(0, 2);
                string month = CPRTB.Text.Substring(2, 2);
                if (CPRTB.Text.Length == 11)
                {
                    if (0 < Convert.ToInt32(day) && Convert.ToInt32(day) < 32 && 0 < Convert.ToInt32(month) && Convert.ToInt32(month) < 13)
                    {
                        if (setupObj.IsPatientAlreadyCreated(CPRTB.Text) == false)
                        {
                            setupObj.newPatient(CPRTB.Text, FnavnTB.Text, EnavnTB.Text);
                            PatientTB.Visibility = Visibility.Visible;
                            PatientTB.Text = "Patient oprettet.";
                            CPRTB.Text = "";
                            FnavnTB.Text = "";
                            EnavnTB.Text = "";
                            parentWindow.UpdateView();
                        }
                        else
                        {
                            PatientTB.Visibility = Visibility.Visible;
                            PatientTB.Text = "Patient allerede oprettet";
                        }
                    }
                    else
                    {
                        PatientTB.Visibility = Visibility.Visible;
                        PatientTB.Text = "Ugyldigt CPR";
                        CPRTB.Focus();
                    }

                }
                else
                {
                    PatientTB.Visibility = Visibility.Visible;
                    PatientTB.Text = "Ugyldigt CPR";
                }
            }
            else
            {
                PatientTB.Visibility = Visibility.Visible;
                PatientTB.Text = "En eller flere oplysninger er ikke udfyldt.";
            }
        } 
        #endregion
    }
}
