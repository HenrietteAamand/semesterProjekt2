using LogicTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Wpf;

namespace WPF_til_leg.Presentation
{
    public partial class ChartECG : UserControl, INotifyPropertyChanged
    {
        private bool _ECGSeriesVisibility;
        private bool _baseLineSeriesVisibility;
        private bool _stSeriesVisibility;
        AnalyzeECG analyzeLogic = new AnalyzeECG();
        MainWindowLogic mainLogic = new MainWindowLogic();
        public SeriesCollection series { get; set; }
        //SeriesCollection series = new SeriesCollection();
        LineSeries line = new LineSeries();

        


        public ChartECG()
        {
            InitializeComponent();
            
            
        }

        public void MakeChart(List<double> ecg)
        {
            series = new SeriesCollection();
            ECGSeriesVisibility = true;
            BaseLineSeriesVisibility = true;
            STSeriesVisibility = false;
            List<double >ecg1 = new List<double>();
            ecg1 = ecg;
            line.Values = new ChartValues<double>();
            for (int i = 0; i < ecg1.Count; i++)
            {
                line.Values.Add(ecg1[i]);
            }
            series.Add(line);
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