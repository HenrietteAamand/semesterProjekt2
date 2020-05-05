using System;

namespace DataTier.Models
{
    public class IllnessModel
    {
        #region Attributes
        private int id;
        private string name;
        private string about;
        private bool stSegmentElevated;
        private bool stSegmentDepressed;

        #endregion

        #region Properties

        #endregion

        #region Constructor
        public IllnessModel(int id, string name, string about, bool stSegmentElevated, bool stSegmentDepressed)
        {
            this.id = id;
            this.name = name;
            this.about = about;
            this.stSegmentElevated = stSegmentElevated;
            this.stSegmentDepressed = stSegmentDepressed;

        }



        #endregion

        #region Methods

        #endregion
    }
}
