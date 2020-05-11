using System;

namespace Models.Models
{
    public class IllnessModel
    {
        #region Attributes
        
        private string name;
        private string about;
        private bool stSegmentElevated;
        private bool stSegmentDepressed;
       

        #endregion

        #region Properties
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private double stMax;

        public double STMax
        {
            get { return stMax; }
            private set { stMax = value; }
        }

        private double srMax;

        public double SRMax
        {
            get { return srMax; }
            private set { srMax = value; }
        }



        #endregion

        #region Constructor
        public IllnessModel(int id, string name, string about, double stMax, double srMax, bool stSegmentElevated, bool stSegmentDepressed)
        {
            Id = id;
            this.name = name;
            this.about = about;
            this.stMax = stMax;
            this.srMax = srMax;
            this.stSegmentElevated = stSegmentElevated;
            this.stSegmentDepressed = stSegmentDepressed;


        }



        #endregion

        #region Methods

        #endregion
    }
}
