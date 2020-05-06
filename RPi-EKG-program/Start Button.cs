using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.TWIST;


namespace RPi_EKG_program
{
    class Start_Button
    {
        private static TWIST TwistKnap;
        public Start_Button()
        {
            TwistKnap = new TWIST();
        }

        public bool isPressed()
        {
            //return TwistKnap.isPressed(); //Dette er sådan det skulle være lavet, men siden vi ikke har en knap
            //så bliver det således i stedet for:
            return true;

        }

    }
}
