using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.ADC;
using System.Threading;
namespace RPi_EKG_program
{
    class ADC
    {
        private static ADC1015 adConverter;
        

        public ADC()
        {
            adConverter = new ADC1015();

        }
        
        public bool isCableConnected()
        {
            ushort test = adConverter.readADC_SingleEnded(0);
            Thread.Sleep(500);
            ushort test1 = adConverter.readADC_SingleEnded(0);
            Thread.Sleep(500);
            ushort test2 = adConverter.readADC_SingleEnded(0);
            Thread.Sleep(500);
            short test4 = adConverter.readADC_Differential_0_1();
            Thread.Sleep(500);
            // Vi har målt 222 228 414 427
            // 1079 x4 på alle når jack er i % klemmer
            //

            if (adConverter.readADC_SingleEnded(0)==0)
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
