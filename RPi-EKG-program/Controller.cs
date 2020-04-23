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
            LCD_Display = new Display();
            Console.WriteLine("Hello World!");
            
        }
    }
}
