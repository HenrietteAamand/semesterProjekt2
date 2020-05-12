using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class AnalyzedECGModel : ECGModel
    {
        #region Attributes

        #endregion

        #region Properties
        private string monitorId;
        public string MonitorID
        {
            get { return monitorId; }
            private set { monitorId = value; }
        }


        private int aECGID;

        public int AECGID
        {
            get { return aECGID; }
            private set
            {
                aECGID = value;
            }
        }



        private IllnessModel illness;

        public IllnessModel Illnes
        {
            get { return illness; }
            set { illness = value; }
        }

        private List<double> stValues;

        public List<double> STValues
        {
            get { return stValues; }
            set { stValues = value; }
        }


        private int pulse;

        public int Pulse
        {
            get { return pulse; }
            set { pulse = value; }
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


        private bool isRead;

        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; }
        }

        private int stStartIndex;

        public int STStartIndex
        {
            get { return stStartIndex; }
            set { stStartIndex = value; }
        }



        #endregion

        #region Constructors
        public AnalyzedECGModel(string cpr, int ecgID, int aECGID, DateTime date, double samplerate, List<double> values, string monitorID)
        {
            CPR = cpr;
            ECGID = ecgID;
            AECGID = aECGID;
            Date = date;
            SampleRate = samplerate;
            Values = values;
            MonitorID = monitorID;
            IsRead = false;
        }


        public AnalyzedECGModel() { }

        //public AnalyzedECGModel(string cpr, int ecgID, DateTime date, int sampleRate, List<double> values, int monitorId, bool isAnalyzed) 
        //    : base(cpr, ecgID, date, sampleRate, values, monitorId, isAnalyzed)
        //{
        //}
        #endregion

        #region Methods

        #endregion
    }
}
