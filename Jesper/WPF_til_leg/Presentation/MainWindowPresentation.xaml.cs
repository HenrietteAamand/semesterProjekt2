using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using WPF_til_leg;
using LogicTier;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowPresentation : MetroWindow
    {
        private MainWindowLogic mainObj;

        public MainWindowPresentation()
        {
            InitializeComponent();
            ShowDialog();

            while (idT.Text == null)
            {
                UploadB.IsEnabled = false;
            }

            mainObj = new MainWindowLogic();

        }

        async Task ShowDialog()
        {
            var result = await this.ShowMessageAsync("Velkommen", $"Der er {0} nye EKG målinger. Vil du opdatere?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                await this.ShowMessageAsync($" {0} EKG målinger er blevet opdateret","");
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

            String cpr = patientsLV.SelectedItem.ToString();

            CPRTB.Text = cpr;
            NavnTB.Text = mainObj.GetPatient(cpr).FirstName + " " + mainObj.GetPatient(cpr).LastName;
            //AlderTB.Text = mainObj.GetAge(cpr).Age;
            //KonTB.Text = mainObj.GetIsAMan(cpr).Gender;
            ecgLV.ItemsSource = mainObj.GetAECGListForPatient(cpr);

        }

        private void ecgLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Vis graf for valgte måling
        }
    }
}
