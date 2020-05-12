using LogicTier;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using LiveCharts;
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
        


        public ChartECG()
        {
            InitializeComponent();
            analyzeLogic.CreateAnalyzedECGs();         


            //MakeChart2(ecg);
            //MakeST(ecg, length, startIndex);
            //MakeChart2(analyzeLogic.NewAECGModelsList[0].Values);
            //MakeST(analyzeLogic.NewAECGModelsList[0].Values, analyzeLogic.NewAECGModelsList[0].STValues.Count, analyzeLogic.NewAECGModelsList[0].STStartIndex);
        }

        public void MakeCharts(List<double> ecg, int length, int startIndex)
        {
            series = new SeriesCollection();
            ECGSeriesVisibility = true;
            BaseLineSeriesVisibility = true;
            STSeriesVisibility = true;
            MakeECGChart(ecg);
            MakeST(ecg, length, startIndex);
            OnPropertyChanged("series");
        }


        public void MakeST(List<double> ecg, int length, int startIndex)
        {   ECGSeriesVisibility = true;
            BaseLineSeriesVisibility = true;
            STSeriesVisibility = false;
            LineSeries line = new LineSeries();
            List<double >ecg1 = new List<double>();
            ecg1 = ecg;
            for (int i = 0; i < ecg1.Count; i++)
            {
                if (i < startIndex || i > startIndex + length)
                {
                    ecg1[i] = double.NaN;
                }
            }
            line.Values = new ChartValues<double>();
            for (int i = 0; i < ecg1.Count; i++)
            {
                line.Values.Add(ecg1[i]);
            }
            series.Add(line);
            DataContext = this;
        }
        public void MakeECGChart(List<double> ecg)
        {
            LineSeries line = new LineSeries();
            List<double> ecg2 = new List<double>();
            ecg2 = ecg;
            line.Values = new ChartValues<double>();
            for (int i = 0; i < ecg2.Count; i++)
            {
                line.Values.Add(ecg2[i]);
            }
            series.Add(line);
            DataContext = this;
        }
        public void MakeBaseLineChart()
        {
            //Lave en streg fra første index af ecgValues til sidste index af ecgValues som har en værdi på baseline.

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