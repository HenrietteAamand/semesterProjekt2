using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.LCD;


namespace RPi_EKG_program
{
    class Display
    {
        private SerLCD displayController;
        public Display()
        {
            displayController = new SerLCD();
            displayController.lcdDisplay();
        }

        public void ScreenShow(short ScreenNb)
        {
            switch (ScreenNb)
            {
                case 1:
                    displayController.lcdClear();

                    break;
                case 2:
                    displayController.lcdClear();
                    break;
            }


        }


    }

}
