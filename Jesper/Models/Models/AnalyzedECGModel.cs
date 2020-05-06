using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class AnalyzedECGModel
    {
        #region Attributes
        private int monitorId;
        #endregion

        #region Properties
        private string cpr;
        public string CPR
        {
            get { return cpr; }
            private set { cpr = value; }
        }

        private int aECGID;

        public int AECGID
        {
            get { return aECGID; }
            private set { aECGID = value; }
        }

        private int ecgID;

        public int ECGID
        {
            get { return ecgID; }
            private set { ecgID = value; }
        }

        private List<IllnessModel> illnessList;

        public List<IllnessModel> IllnesList
        {
            get { return illnessList; }
            private set { illnessList = value; }
        }

        private List<double> aECGChart;

        public List<double> AECGCHART
        {
            get { return aECGChart; }
            private set { aECGChart = value; }
        }

        private int pulse;

        public int Pulse
        {
            get { return pulse; }
            set { pulse = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            private set { date = value; }
        }

        private double baseline;

        public double Baseline
        {
            get { return baseline; }
            set { baseline = value; }
        }

        private bool stElevated;

        public bool STElevated
        {
            get { return stElevated; }
            set { stElevated = value; }
        }

        private bool stDepressed;

        public bool STDepressed
        {
            get { return stDepressed; }
            set { stDepressed = value; }
        }

        private int sampleRate;

        public int SampleRate
        {
            get { return sampleRate; }
            private set { sampleRate = value; }
        }

        private bool isRead;

        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; }
        }



        #endregion

        #region Constructors
        public AnalyzedECGModel(string cpr, int ecgID, int aECGID, DateTime date, IllnessModel illness, List<double> aECGCHart, int samplerate, int monitorID, bool isRead)
        {
            CPR = cpr;
            AECGID = aECGID;
            ECGID = ecgID;
            IllnesList.Add(illness);
            AECGCHART = aECGCHart;
            //Pulse = pulse;
            Date = date;
            SampleRate = samplerate;
            monitorId = monitorID;
            IsRead = isRead;
        }

        public AnalyzedECGModel() { }
        #endregion

        #region Methods

            #endregion
    }
}
