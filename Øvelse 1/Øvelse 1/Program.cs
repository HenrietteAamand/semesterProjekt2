using System;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using System.Threading;
using System.Collections.Generic;


namespace Raspberry_Pi
{
    class Program
    {
        private SerLCD displayController;
        private ADC1015 adConverter;
        private TWIST twist;
        private Temp temp;
        static void Main(string[] args)
        {
            //Hej
            SerLCD displayController = new SerLCD();
            ADC1015 adConverter = new ADC1015();
            TWIST twist = new TWIST();
            string temp = Convert.ToString(adConverter.readADC_SingleEnded(0));

            double Celcius = Convert.ToInt32(temp) / 10;

            double Fahrenheit = Celcius * 2 + 31;
            double Kelvin = Celcius + 273 + "Kelvin";

            List<double> items = new List<double> { Celcius, Fahrenheit, Kelvin };


            displayController.lcdDisplay();

            displayController.lcdClear();

            displayController.lcdPrint("tempteratur: " + temp);

            byte x = 0;
            byte y = 0;
            for (byte i = 1; i < items.Count + 1; i++)
            {
                displayController.lcdGotoXY(x, i);
                displayController.lcdPrint(Convert.ToString((items[i - 1]) + " grader"));

            }


            //displayController.lcdGotoXY(x,y);

            //displayController.lcdPrint("tempteratur: " + temp + " Celcius: " + Celcius + " Fahrenheit: " + Fahrenheit + " Kelvin: " + Kelvin);






            //Console.WriteLine("Hello World!");

            //Console.WriteLine("Temperatur:\n\n");
            //Console.WriteLine("Celcius:         "+Celcius);
            //Console.WriteLine();
            //Console.WriteLine("Celcius:         "+ Fahrenheit);
            //Console.WriteLine();
            //Console.WriteLine("Celcius:         "+ Kelvin);

        }

    }
}

