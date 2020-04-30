using System;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using System.Collections.Generic;

namespace RPi_EKG_program
{
    class Program
    {

        private USB_stick LocalStorage;
        private Display LCD_Display;
        
       
        static void Main(string[] args)
        {
            Display displayController = new Display();


            //displayController.lcdPrint("test");

            ADC ADconverter = new ADC();


            ADconverter.isCableConnected();


            displayController.ScreenShow(4);



            DateTime Start = DateTime.Now;
            List<double> Test = new List<double>();
            Measurement measurement = new Measurement("123456-7890", Test, Start, 0.02, "54321");
                 
            for (int i = 0; i < /*40sek*/; i++)
            {
                measurement.addToList(ADconverter.measureSignal());
                

            }


        }
       
        public void startIsPressed()
        {

        }
        
    }
}
