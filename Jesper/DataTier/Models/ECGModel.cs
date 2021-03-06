﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataTier.Models
{
    public class ECGModel
    {
        #region Attributes

        #endregion

        #region Properties
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


        #endregion

        #region Constructor
        public ECGModel(int ecgID, DateTime date, int sampleRate, List<double> values)
        {
            ECGID = ecgID;
            Date = date;
            SampleRate = sampleRate;
            Values = values;
        }

        #endregion

        #region Methods

        #endregion
    }
}
