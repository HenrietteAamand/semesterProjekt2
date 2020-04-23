using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.LCD;


namespace RPi_EKG_program
{
    class Display
    {
        private string Linjeskift = "                   ";
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
                    string Navn = "Lars"; //Navnet hentes fra lokalDB
                    string CPR = "123456"; // CPR hentes også fra LokalDB - Måske kun de første 6

                    if(Navn.Length>12)
                    {
                        string NyNavn = "";
                        for (int i = 0; i < 12; i++)
                        {
                            NyNavn += Navn[i];
                        }
                        Navn = NyNavn;
                    }
                    while(Navn.Length<11)
                    {
                        Navn += " ";
                    }


                    displayController.lcdPrint(Linjeskift + "     Velkommen      " + "Du er logget ind som" + Navn + "  " + CPR ); ;
                    break;
                case 2:
                    displayController.lcdClear();
                    displayController.lcdPrint(Linjeskift+ "  Forbind venligst  " + "     Elektroder     ");

                    break;
                case 3:
                    displayController.lcdClear();
                    displayController.lcdPrint(Linjeskift + Linjeskift+"    Tryk på start   ");
                    break;
                case 4:
                    displayController.lcdClear();
                    displayController.lcdPrint("█_"); //alt code 219
                    break;
                    
            }


        }


    }

}
