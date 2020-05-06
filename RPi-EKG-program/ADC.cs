using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.ADC;

namespace RPi_EKG_program
{
    class ADC
    {
        private static ADC1015 adConverter;
        private Measurement measurement;

        public ADC()
        {
            adConverter = new ADC1015();

        }
        
        public bool isCableConnected()
        {
            if(adConverter.readADC_SingleEnded(0)==0)
            { 
                return false; 
            }
            else
            return true;
        }

        public double measureSignal()
        {
            double sample = (adConverter.readADC_SingleEnded(0) / 2048.0) * 6.144;
            return sample;

        }
        public byte checkBattery()
        {

            //Vi har ikke et batteri at teste på, vi laver test således at vores batteriniveau ligger et sted imellem 9.6V og 10V

            //double batteryStatus = (adConverter.readADC_SingleEnded(3) / 2048.0) * 6.144;
            //if (batteryStatus >= 10)
            //    return 4;
            //if (batteryStatus >= 9.6)
            //    return 3;
            //if (batteryStatus >= 8.96)
            //    return 2;

            //else return 1;
            
            return 3;


        }
    }
}
