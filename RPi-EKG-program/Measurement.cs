using System;
using System.Collections.Generic;
using System.Text;

namespace RPi_EKG_program
{
    class Measurement
    {
        public string CPRNr { get; private set; }
        public List<double> Measurements { get; private set; }
        public DateTime Dato { get; private set; }
        public double samplerate { get; private set; }
        public string MeasurerID { get; private set; }


        public Measurement(string CPR, List<double> Measurements, DateTime Dato, double Samplerate, string MeasurerID)
        {
            CPRNr = CPR;
            this.Measurements = Measurements;
            this.Dato = Dato;
            samplerate = Samplerate;
            this.MeasurerID = MeasurerID; 

        }


        public void addToList(double measurement)
        {
            Measurements.Add(measurement);
        }
        


    }

}
