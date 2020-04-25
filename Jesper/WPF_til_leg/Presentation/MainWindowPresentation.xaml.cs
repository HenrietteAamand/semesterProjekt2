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
using WPF_til_leg.Models;

namespace WPF_til_leg.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowPresentation : MetroWindow
    {
        public int updates = 10;
        public List<PatientModel> Patients { get; set; }

        public MainWindowPresentation()
        {
            InitializeComponent();
            ShowDialog();
            updateBadge.Badge = updates;
            PatientModel patient1 = new PatientModel {Name = "jens", PatientCPR = "123456"  };
            Patients = new List<PatientModel>();
            Patients.Add(patient1);
            this.DataContext = Patients;
      
            

        }

        async Task ShowDialog()
        {
            var result = await this.ShowMessageAsync("Welcome", $"There are {updates} new ECG's. Do you wanna update?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                await this.ShowMessageAsync($" {updates} ECG's have been updated","");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            settingFlyOut.IsOpen = true;
        }
    }
}
