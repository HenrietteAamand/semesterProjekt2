using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.LCD;



namespace RPi_EKG_program
{
    class Display
    {
        private string Linjeskift = "                   ";
        private static SerLCD displayController;
        

        public void updateMenuBar(bool connection,byte StorageStatus, byte batteryStatus)
        {
            displayController.lcdGotoXY(0, 0);
            string Connect = "";
            string Storage = "";

            if (connection == true)
            {
                Connect = "O";

            }
            else { Connect = "X"; }

            if (StorageStatus != 0)
            {
                Storage = "Data: "+ StorageStatus;
            }
            else { Storage = "Data: 0"; }

            string batteryniveau = "";

            switch (batteryStatus)
            {
                case 1:
                    {
                        // Her ville der være en "custom" karatker for at vise batteristatus
                        //Da vi ikke kan lave specielle custom karakter, bruger vi et system med procent 0- 100.
                        batteryniveau = "25";
                        //SKAL vise screen 8


                    }
                    break;
                case 2:
                    batteryniveau = "50";

                    break;
                case 3:
                    {

                        batteryniveau = "75";

                    }
                    break;
                case 4:
                    batteryniveau = "99";

                    break;
            }
            displayController.lcdDisplay();
            displayController.lcdClear();
            displayController.lcdPrint("HEEEEEEEEEj");
            displayController.lcdClear();
            displayController.lcdPrint(Convert.ToString(DateTime.Now.Hour)+":"+ Convert.ToString(DateTime.Now.Minute)+ "|" + Connect + "|" + Storage + "|" + batteryniveau);
                

        }


        public Display()
        {
            displayController = new SerLCD();
            displayController.lcdDisplay();
            displayController.lcdSetBackLight(255,255,0);

        }
        public void ShowGreeting(string CPRNAVN)
        {

            string CPR = "";
            string Navn = "";
            //displayController.lcdDisplay();
            displayController.lcdClear();

            for (int i = 0; i < 6; i++)
            {
                CPR += CPRNAVN[i];
            }
            for (int i = 6; i < CPRNAVN.Length; i++)
            {
                Navn += CPRNAVN[i];
            }

            if (Navn.Length > 12)
            {
                string NyNavn = "";
                for (int i = 0; i < 12; i++)
                {
                    NyNavn += Navn[i];
                }
                Navn = NyNavn;
            }
            while (Navn.Length < 11)
            {
                Navn += " ";
            }
            displayController.lcdPrint("     Velkommen      Du er logget ind som"/* + Navn + "  " + CPR*/);



        }

        public void StatusUpdateMeasurment()
        {

            // skal opdatere skærm 4. YEs
        }
        public void ScreenShow(short ScreenNb)
        {

            switch (ScreenNb)
            {
                case 1:
                    displayController.lcdDisplay();
                    displayController.lcdClear();
                    string Navn = "Lars"; //Navnet hentes fra lokalDB
                    /*string CPR = "123456";*/ // CPR hentes også fra LokalDB - Måske kun de første 6

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


                    displayController.lcdPrint("     Velkommen      Du er logget ind som" /*+ Navn + "  " + CPR */);
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
                    displayController.lcdDisplay();
                    displayController.lcdClear();
                    
                    displayController.lcdPrint("      ###_  | | "); //alt code 219
                    break;
                    
            }



        }



    }

}
