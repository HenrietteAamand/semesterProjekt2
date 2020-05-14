using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RaspberryPiCore.TWIST;


namespace RPi_EKG_program
{
    class Start_Button
    {
        private static TWIST twistButton;

        public Start_Button()
        {
            twistButton = new TWIST();
        }

        public bool isPressed()
        {
            //return twistButton.isPressed(); //Dette er sådan det skulle være lavet, men siden vi ikke har en knap
            //så bliver det således i stedet for:
            Thread.Sleep(7000);
            return true;

        }

    }
}
