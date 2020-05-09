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
        private SetupWindowLogic setupObj;

        public SetupPatientUC()
        {
            InitializeComponent();
            setupObj = new SetupWindowLogic();
            PatientTB.Visibility = Visibility.Hidden;
        }

        private void OpretB_Click(object sender, RoutedEventArgs e)
        {
            //TextRange CPRTR = new TextRange(CPRTB.Document.ContentStart,CPRTB.Document.ContentEnd);
            //TextRange FnavnTR = new TextRange(FnavnTB.Document.ContentStart, FnavnTB.Document.ContentEnd);
            //TextRange EnavnTR = new TextRange(EnavnTB.Document.ContentStart, EnavnTB.Document.ContentEnd);
            

            //if (CPRTR.Text != null && FnavnTR.Text != null && EnavnTR.Text != null)
            //{

            //    if (setupObj.IsPatientAlreadyCreated(CPRTR.Text) == false) 
            //    {
            //        setupObj.newPatient(CPRTR.Text, FnavnTR.Text, EnavnTR.Text);
            //        PatientTB.Visibility = Visibility.Visible;
            //        PatientTB.Text = "Patient oprettet.";
            //    }
            //    else 
            //    {
            //        PatientTB.Text = "Patient allerede oprettet";
            //    }
            //}
            //else
            //{
            //    PatientTB.Visibility = Visibility.Visible;
            //    PatientTB.Text = "En eller flere oplysninger er ikke udfyldt.";
            //} 

                
        }
    }
}
