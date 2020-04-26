using LiveCharts;
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

        private SeriesCollection aECGChart;

        public SeriesCollection AECGCHART
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

        #endregion

        #region Constructors
        public AnalyzedECGModel(int aECGID, int ecgID, IllnessModel illness, SeriesCollection aECGCHart,
    int pulse)
        {
            AECGID = aECGID;
            ECGID = ecgID;
            IllnesList.Add(illness);
            AECGCHART = aECGCHart;
            Pulse = pulse;
        }
        #endregion

        #region Methods

            #endregion
    }
}
