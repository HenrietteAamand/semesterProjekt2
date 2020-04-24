using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_til_leg
{
    public class PatientModel
    {

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
