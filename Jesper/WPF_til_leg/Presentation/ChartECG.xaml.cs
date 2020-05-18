using LogicTier;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using LiveCharts;
using System.Windows;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;

namespace WPF_til_leg.Presentation
{
    public partial class ChartECG : UserControl, INotifyPropertyChanged
    {
        private bool _ECGSeriesVisibility;
        private bool _baseLineSeriesVisibility;
        private bool _stSeriesVisibility;
        public SeriesCollection series { get; set; }
        public ChartValues<double> STList { get; set; }
        public ChartValues<double> ECGList { get; set; }
        public ChartValues<double> BaseList { get; set; }
        public double ECGSampleRate { get; set; }

        private double _to;
        private double _from;
        private double xAxis;


        public ChartECG()
        {
            
            InitializeComponent();


            //MakeChart2(ecg);
            //MakeST(ecg, length, startIndex);
            //MakeChart2(analyzeLogic.NewAECGModelsList[0].Values);
            //MakeST(analyzeLogic.NewAECGModelsList[0].Values, analyzeLogic.NewAECGModelsList[0].STValues.Count, analyzeLogic.NewAECGModelsList[0].STStartIndex);
            PrevB.IsEnabled = false;
            
        }

        public void MakeCharts(List<double> ecg, int length, int startIndex, double baseline, double sampleRate)
        {
            
            STList = new ChartValues<double>();
            ECGList = new ChartValues<double>();
            BaseList = new ChartValues<double>();
            ECGSeriesVisibility = true;
            BaseLineSeriesVisibility = true;
            STSeriesVisibility = true;
            ECGSampleRate = sampleRate;
            MakeECGList(ecg);
            //MakeECGLine(ecg);
            MakeSTList(ecg, length, startIndex);
            //MakeSTLine(ecg, length, startIndex);
            MakeBaseList(ecg, baseline);
            //MakeBaseLineChart(ecg, baseline);

            OnPropertyChanged("ECGList");

            OnPropertyChanged("BaseList");
            DataContext = this;
        }


        //public void MakeSTLine(List<double> ecg, int length, int startIndex)
        //{   
        //    List<double >ecg1 = new List<double>();
        //    ecg1 = ecg;
        //    for (int i = 0; i < ecg1.Count; i++)
        //    {
        //        if (i < startIndex || i > startIndex + length)
        //        {
        //            ecg1[i] = double.NaN;
        //        }
        //    }
        //    stLine.Values = new ChartValues<double>();
        //    for (int i = 0; i < ecg1.Count; i++)
        //    {
        //        stLine.Values.Add(ecg1[i]);
        //    }
        //    series.Add(stLine);
        //    DataContext = this;
        //}
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
        //public void MakeECGLine(List<double> ecg)
        //{
        //    List<double> ecg2 = new List<double>();
        //    ecg2 = ecg;
        //    ecgLine.Values = new ChartValues<double>();
        //    for (int i = 0; i < ecg2.Count; i++)
        //    {
        //        ecgLine.Values.Add(ecg2[i]);
        //    }
        //    series.Add(ecgLine);
        //    DataContext = this;
        //}
        public void MakeECGList(List<double> ecg)
        {
            List<double> ecg2 = new List<double>();
            ecg2 = ecg;
            ChartValues<double> ecgValues= new ChartValues<double>();
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
        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public double XAxis
        {
            get { return xAxis; }
            set
            {
                xAxis = value;
                OnPropertyChanged("XAxis");
            }
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
            To -= 1 /ECGSampleRate;
            
        }

       
    }
}