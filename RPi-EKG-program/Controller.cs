using System;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;


namespace RPi_EKG_program
{
    class Program
    {


        private static Display LCD_Display;
        static void Main(string[] args)
        {
            SerLCD displayController = new SerLCD();
            displayController = new SerLCD();
            displayController.lcdDisplay();

            //displayController.lcdPrint("test");



            LCD_Display = new Display();
            displayController.lcdDisplay();
            LCD_Display.ScreenShow(4);

        }
    }
}
