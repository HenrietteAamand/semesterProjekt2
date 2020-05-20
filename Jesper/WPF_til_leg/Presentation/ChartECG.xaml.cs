using System.Collections.Generic;
using System.ComponentModel;
using LiveCharts;
using System.Windows;
using UserControl = System.Windows.Controls.UserControl;

namespace WPF_til_leg.Presentation
{
    public partial class ChartECG : UserControl, INotifyPropertyChanged
    {
        #region Attributes
        private bool _ECGSeriesVisibility;
        private bool _baseLineSeriesVisibility;
        private bool _stSeriesVisibility; 
        #endregion

        #region Properties
        public SeriesCollection series { get; set; }
        public ChartValues<double> STList { get; set; }
        public ChartValues<double> ECGList { get; set; }
        public ChartValues<double> BaseList { get; set; }
        public double ECGSampleRate { get; set; }
        public double gridStep { get; set; }
        private double from;
        public double From
        {
            get { return from; }
            set
            {
                from = value;
                OnPropertyChanged("From");
            }
        }
        private double to;
        public double To
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged("To");
            }
        }
        private double xAxis;
        public double XAxis
        {
            get { return xAxis; }
            set
            {
                xAxis = value;
                OnPropertyChanged("XAxis");
            }
        }
        #endregion

        #region Ctor
        public ChartECG()
        {
            InitializeComponent();
            PrevB.IsEnabled = false;
        }
        #endregion

        #region Methods
        public void MakeCharts(List<double> ecg, int length, int startIndex, double baseline, double sampleRate)
        {
            STList = new ChartValues<double>();
            ECGList = new ChartValues<double>();
            BaseList = new ChartValues<double>();

            ECGSeriesVisibility = true;
            BaseLineSeriesVisibility = true;
            STSeriesVisibility = true;

            ECGSampleRate = sampleRate;
            gridStep = 0.04 / sampleRate;
            OnPropertyChanged("gridStep");

            MakeECGList(ecg);
            MakeSTList(ecg, length, startIndex);
            MakeBaseList(ecg, baseline);
            OnPropertyChanged("ECGList");
            OnPropertyChanged("STList");
            OnPropertyChanged("BaseList");

            DataContext = this;
        }

        public void MakeSTList(List<double> ecg, int length, int startIndex)
        {
            List<double> ecg12 = new List<double>();
            for (int i = 0; i < ecg.Count; i++)
            {
                if (i < startIndex || i > startIndex + length)
                {
                    ecg12.Add(double.NaN);
                }
                else
                    ecg12.Add(ecg[i]);
            }


            ChartValues<double> stValues = new ChartValues<double>();
            for (int i = 0; i < ecg12.Count; i++)
            {
                stValues.Add(ecg12[i]);
            }
            STList = stValues;
            DataContext = this;
            OnPropertyChanged("STList");
        }

        public void MakeECGList(List<double> ecg)
        {
            List<double> ecg2 = new List<double>();
            ecg2 = ecg;
            ChartValues<double> ecgValues = new ChartValues<double>();
            for (int i = 0; i < ecg2.Count; i++)
            {
                ecgValues.Add(ecg2[i]);
            }

            ECGList = ecgValues;
            DataContext = this;
        }
        public void MakeBaseList(List<double> ecg, double baseline)
        {
            List<double> ecg2 = new List<double>();
            ecg2 = ecg;
            ChartValues<double> baseValues = new ChartValues<double>();
            for (int i = 0; i < ecg2.Count; i++)
            {
                baseValues.Add(baseline);
            }
            BaseList = baseValues;
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


        private void NextOnClick(object sender, RoutedEventArgs e)
        {
            if (From - (1 / ECGSampleRate) >= 0)
            {
                PrevB.IsEnabled = true;
            }
            From += 1 / ECGSampleRate;
            To += 1 / ECGSampleRate;
        }

        private void PrevOnClick(object sender, RoutedEventArgs e)
        {
            if (From - (1 / ECGSampleRate) <= 0)
            {
                PrevB.IsEnabled = false;
            }
            From -= 1 / ECGSampleRate;
            To -= 1 / ECGSampleRate;

        } 
        #endregion
    }
}