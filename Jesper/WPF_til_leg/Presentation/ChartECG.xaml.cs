using LogicTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace WPF_til_leg.Presentation
{
    public partial class ChartECG : UserControl, INotifyPropertyChanged
    {
        private bool _ECGSeriesVisibility;
        private bool _baseLineSeriesVisibility;
        private bool _stSeriesVisibility;
        AnalyzeECG analyzeLogic = new AnalyzeECG();
        MainWindowLogic mainLogic = new MainWindowLogic();
        

        


        public ChartECG()
        {
            InitializeComponent();

            ECGSeriesVisibility = true;
            BaseLineSeriesVisibility = true;
            STSeriesVisibility = false;

            List<double> ecg = new List<double>();
            ecg = mainLogic.GetECGValues(1);
            DataContext = this;
        }

        public bool ECGSeriesVisibility
        {
            get { return _ECGSeriesVisibility; }
            set
            {
                _ECGSeriesVisibility = value;
                OnPropertyChanged("ECGSeriesVisibility");
            }
        }

        public bool BaseLineSeriesVisibility
        {
            get { return _baseLineSeriesVisibility; }
            set
            {
                _baseLineSeriesVisibility = value;
                OnPropertyChanged("BaseLineSeriesVisibility");
            }
        }

        public bool STSeriesVisibility
        {
            get { return _stSeriesVisibility; }
            set
            {
                _stSeriesVisibility = value;
                OnPropertyChanged("STSeriesVisibility");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}