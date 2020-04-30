using System;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;


namespace RPi_EKG_program
{
    class Program
    {

        private USB_stick LocalStorage;
        private static Display LCD_Display;
        private static ADC1015 ADC;
        static void Main(string[] args)
        {
            SerLCD displayController = new SerLCD();
            displayController = new SerLCD();
            displayController.lcdDisplay();

            //displayController.lcdPrint("test");

            ADC = new ADC();


            ADC.readADC_Differential_0_1();


            LCD_Display = new Display();
            displayController.lcdDisplay();
            LCD_Display.ScreenShow(4);

        }
       
        public void startIsPressed()
        {

        }
     
    }
}
