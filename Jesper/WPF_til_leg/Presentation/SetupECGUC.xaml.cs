using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicTier;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for SetupECGUC.xaml
    /// </summary>
    public partial class SetupECGUC : UserControl
    {
        public SetupECGUC()
        {
            InitializeComponent();
            

    }

        //UC2 + UC4
        private void LinkECGB_Click(object sender, RoutedEventArgs e)
        {
            //LinkECGToPatient();
            System.Windows.MessageBox.Show("Tilknytning gennemført.");
        }

        private void ResetECGB_Click(object sender, RoutedEventArgs e)
        {
            //LinkECGToPatient();
            System.Windows.MessageBox.Show("Nulstilling gennemført.");
        }

        private void EcgCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (EcgCB.SelectedItem())
            //{
            //    ResetECGB.IsEnabled = false;
            //    System.Windows.MessageBox.Show("EKG-måler er ikke i brug");
            //}
            //else
            //{
            //    LinkECGB.IsEnabled = false;
            //    System.Windows.MessageBox.Show("EKG-måler er i brug");
            //}
        }
    }
}
