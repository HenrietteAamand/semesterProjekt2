using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class ECGModel
    {
        #region Attributes

        #endregion

        #region Properties
        private string cpr;
        public string CPR
        {
            get { return cpr; }
            private set { cpr = value; }
        }

        private int ecgID;

        public int ECGID
        {
            get { return ecgID; }
            private set { ecgID = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            private set { date = value; }
        }

        private int sampleRate;

        public int SampleRate
        {
            get { return sampleRate; }
            private set { sampleRate = value; }
        }

        private List<double> values;

        public List<double> Values
        {
            get { return values; }
            private set { values = value; }
        }

        private bool isRead;

        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; }
        }

        private bool isAnalyzed;

        public bool IsAnalyzed
        {
            get { return isAnalyzed; }
            set { isAnalyzed = value; }
        }

        private string monitorId;

        public string MonitorId
        {
            get { return monitorId; }
            set { monitorId = value; }
        }



        #endregion

        #region Constructor
        public ECGModel(string cpr, int ecgID, DateTime date, int sampleRate, List<double> values, string monitorId)
        {
            CPR = cpr;
            ECGID = ecgID;
            Date = date;
            SampleRate = sampleRate;
            Values = values;
            MonitorId = monitorId;
        }

        #endregion

        #region Methods

        #endregion
    }
}
