using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.ADC;

namespace RPi_EKG_program
{
    class ADC1015
    {
        private ADC1015 adConverter;

        public ADC1015(byte deviceAddress = 72,ushort gain = 0)
        {
            adConverter = new ADC1015();
        }
        public short readADC_Differential_0_1()
        {
            return 1;

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
            
            return 22;
        }
        public byte checkBattery()
        {



            if(checkBattery >8000)
            return 4;
            if (checkBattery > 7800)
                return 3;
            if (checkBattery > 7600)
                return 2;
            else return 1;
        }


    }
}
