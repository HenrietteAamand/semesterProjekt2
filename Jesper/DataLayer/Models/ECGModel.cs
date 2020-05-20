using System;
using System.Collections.Generic;
using System.Text;

namespace DataTier.Models
{
    public class ECGModel
    {
        #region Properties
        private string cpr;
        public string CPR
        {
            get { return cpr; }
            protected set { cpr = value; }
        }

        private int ecgID;

        public int ECGID
        {
            get { return ecgID; }
            protected set { ecgID = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            protected set { date = value; }
        }

        private double sampleRate;

        public double SampleRate
        {
            get { return sampleRate; }
            protected set { sampleRate = value; }
        }

        private List<double> values;

        public List<double> Values
        {
            get { return values; }
            protected set { values = value; }
        }

        private bool isAnalyzed;

        public bool IsAnalyzed
        {
            get { return isAnalyzed; }
            set { isAnalyzed = value; }
        }

        private string monitorId;

        public string MonitorID
        {
            get { return monitorId; }
            set { monitorId = value; }
        }



        #endregion

        #region Constructor
        public ECGModel(string cpr, int ecgID, DateTime date, double sampleRate, List<double> values, string monitorId, bool isAnalyzed)
        {
            CPR = cpr;
            ECGID = ecgID;
            Date = date;
            SampleRate = sampleRate;
            Values = values;
            MonitorID = monitorId;
            IsAnalyzed = isAnalyzed;
        }

        public ECGModel()
        {
        }

        #endregion
    }
}
