using System;
using System.Collections.Generic;
using System.Text;

namespace RPi_EKG_program
{
    [Serializable]
    class Measurement
    {
       
        public string CPRNr { get; private set; }
        public List<double> Measurements { get; private set; }
        public DateTime Date { get; private set; }
        public double SampleRate { get; set; }
        public string MeasurerID { get; private set; }


        public Measurement(string cpr, List<double> measurements, DateTime date, double sampleRate, string measurerID)
        {
            CPRNr = cpr;
            Measurements = measurements;
            Date = date;
            SampleRate = sampleRate;
            MeasurerID = measurerID;

        }

        public Measurement()
        {

        }


        public void addToList(double measurement)
        {
            Measurements.Add(measurement);
        }
        


    }

}
