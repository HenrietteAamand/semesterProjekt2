using System;

namespace Models.Models
{
    public class IllnessModel
    {
        #region Attributes
        private int id;
        private string name;
        private string about;
        private bool stSegmentElevated;
        private bool stSegmentDepressed;
        private double stMax;
        private double srMax;

        #endregion

        #region Properties

        #endregion

        #region Constructor
        public IllnessModel(int id, string name, string about, double stMax, double srMax, bool stSegmentElevated, bool stSegmentDepressed)
        {
            this.id = id;
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
