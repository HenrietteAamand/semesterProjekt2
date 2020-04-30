using System;
using System.Collections.Generic;
using System.Text;

namespace RPi_EKG_program
{
    class Measurement
    {
        public string CPRNr { get; private set; }
        private List<double> Measurements { get; }
        private DateTime Dato;
        private double samplerate;
        private string MeasurerID;


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
