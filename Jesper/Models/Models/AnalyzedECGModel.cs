using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class AnalyzedECGModel
    {
        #region Attributes

        #endregion

        #region Properties
        private int monitorId;
        public int MonitorID
        {
            get { return monitorId; }
            private set { monitorId = value; }
        }

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
            private set { stValues = value; }
        }

        private List<double> values;

        public List<double> Values
        {
            get { return values; }
            set { values = value; }
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

        private int stStartIndex;

        public int STStartIndex
        {
            get { return stStartIndex; }
            set { stStartIndex = value; }
        }



        #endregion

        #region Constructors
        public AnalyzedECGModel(string cpr, int ecgID, int aECGID, DateTime date, IllnessModel illness, List<double> aECGCHart, int samplerate, int monitorID, bool isRead)
        {
            CPR = cpr;
            AECGID = aECGID;
            ECGID = ecgID;
            Illnes = illness;
            STValues = aECGCHart;
            //Pulse = pulse;
            Date = date;
            SampleRate = samplerate;
            MonitorID = monitorID;
            IsRead = isRead;
        }

        public AnalyzedECGModel() { }
        #endregion

        #region Methods

            #endregion
    }
}
