using System;
using System.Collections.Generic;
using System.Text;

namespace DataTier.Models
{
    public class PatientModel
    {

        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        #endregion

        private string _patientCPR;
        private string _name;

        public string PatientCPR
        {
            get => this._patientCPR;
            set
            {
                this._patientCPR = value;
            }
        }
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
            }
        }

    }
}
