using System;
using System.Collections.Generic;
using System.Text;

namespace DataTier.Models
{
    public class ECGMonitorModel
    {
        #region Properties
        private bool inUse;

        public bool InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }

        private string id;

        public string ID
        {
            get { return id; }
            private set { id = value; }
        }



        #endregion

        #region Constructor
        public ECGMonitorModel(string id, bool inUse)
        {
            ID = id;
            InUse = inUse;
        }

        #endregion

    }
}
