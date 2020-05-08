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

        public MainWindowPresentation()
        {
            InitializeComponent();
            ShowDialog();

      
            

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
            if (idT.Text != null)
            {
                
            }
        }
    }
}
