using System;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;


namespace Raspberry_Pi_Dot_Net_Core_Console_Application3
{
    class Program
    {
        static void Main(string[] args)
        {
            TWIST Button = new TWIST();
            int knaptryk = 1;

            while(true)
            {
                if(Button.isPressed())
                {
                    Console.WriteLine(knaptryk + " " + DateTime.Now +":"+  DateTime.Now.Millisecond);
                    knaptryk++;
                }


            }
        }
    }
}
