using System;
using System.Collections.Generic;
using System.Text;

namespace DataTier.Models
{
    public class AnalyzedECGModel
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

        //private int aECGID;

        //public int AECGID
        //{
        //    get { return aECGID; }
        //    private set { aECGID = value; }
        //}

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
            private set { pulse = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            private set { date = value; }
        }

        #endregion

        #region Constructors
        public AnalyzedECGModel(string cpr, int ecgID, DateTime date, IllnessModel illness, List<double> aECGCHart,
    int pulse)
        {
            CPR = cpr;
            //AECGID = aECGID;
            ECGID = ecgID;
            IllnesList.Add(illness);
            AECGCHART = aECGCHart;
            Pulse = pulse;
            Date = date;
        }
        #endregion

        #region Methods

            #endregion
    }
}
