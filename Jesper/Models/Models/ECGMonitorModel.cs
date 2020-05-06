﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class ECGMonitorModel
    {
        #region Attributes
        private int id;
        #endregion

        #region Properties
        private bool inUse;

        public bool InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }


        #endregion

        #region Constructor
        public ECGMonitorModel(int id, bool inUse)
        {
            this.id = id;
            InUse = inUse;
        }

        #endregion

        #region Methods

        #endregion
    }
}