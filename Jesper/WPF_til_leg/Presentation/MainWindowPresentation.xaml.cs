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
using DataTier;
using DataTier.Interfaces;
using DataTier.Databaser;
using Models.Models;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowPresentation : MetroWindow
    {
        private ILocalDatabase DB;

        public MainWindowPresentation()
        {
            InitializeComponent();
            ShowDialog();

            DB = new Database();
      
            

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            settingsFlyOut.IsOpen = true;
        }

        private void UploadB_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = new DateTime(2019, 05, 05);
            IllnessModel illness = new IllnessModel(1, "prove", "Nothing", 2, 3, false, false);
            List<double> values = new List<double>() { 1.3, 2.3, 3.3 };
            List<double> stvalues = new List<double>() { 5.4, 6.4, 7.4 };
            ECGModel ecg = new ECGModel("121212-1212", 11, date, 0.002, values, "Måler2",true);
            ecg.IsAnalyzed = true;
            

            DB.UpdateIsAnalyzed(ecg);

            //if (idT.Text != null)
            //{
                
            //}
        }
    }
}
