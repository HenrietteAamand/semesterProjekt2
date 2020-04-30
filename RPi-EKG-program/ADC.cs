using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.ADC;

namespace RPi_EKG_program
{
    class ADC
    {
        private ADC1015 adConverter;
        private Measurement measurement;

        public ADC()
        {
            adConverter = new ADC1015();
        }
        
        public bool isCableConnected()
        {
            if(adConverter.readADC_Differential_0_1() == 0)
            { 
                return false; 
            }
            else
            return true;
        }

        public double measureSignal()
        {
            double sample = (adConverter.readADC_Differential_0_1() / 2048.0) * 6.144;
            return sample;

        }
        //public byte checkBattery()
        //{



        //    //if(checkBattery >8000)
        //    //return 4;
        //    //if (checkBattery > 7800)
        //    //    return 3;
        //    //if (checkBattery > 7600)
        //    //    return 2;
        //    //else return 1;
        //}


    }
}
