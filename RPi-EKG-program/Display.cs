using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.LCD;



namespace RPi_EKG_program
{
    class Display
    {

        private static SerLCD displayController;
        private bool clearScreen;

        

        public void updateInfoBar(bool connection,byte storageStatus, byte batteryStatus)
        {
            
            string connectionIcon = "";
            string storageStatusIcon = "";

            if (connection == true)
            {
                connectionIcon = "O";

            }
            else { connectionIcon = "X"; }

            if (storageStatus != 0)
            {
                storageStatusIcon = "Data:"+ storageStatus;
            }
            else { storageStatusIcon = "Data:0"; }

            string batteryStatusIcon = "";

            switch (batteryStatus)
            {
                case 1:
                    {
                        // Her ville der være en "custom" karatker for at vise batteristatus
                        //Da vi ikke kan lave specielle custom karakter, bruger vi et system med procent 0- 100.
                        batteryStatusIcon = "5";
                        //SKAL vise screen 8


                    }
                    break;
                case 2:
                    batteryStatusIcon = "50";

                    break;
                case 3:
                    {

                        batteryStatusIcon = "75";

                    }
                    break;
                case 4:
                    batteryStatusIcon = "99";

                    break;
            }
            displayController.lcdDisplay();
            displayController.lcdGotoXY(0, 0);
            displayController.lcdPrint(Convert.ToString(DateTime.Now.TimeOfDay));
            displayController.lcdGotoXY(5, 0);
            displayController.lcdPrint(" "+connectionIcon + "  " + storageStatusIcon + "  " + batteryStatusIcon+"%");
            
        }


        public Display()
        {
            displayController = new SerLCD();
            displayController.lcdDisplay();
            displayController.lcdSetBackLight(0,0,255);
            clearScreen = true;

            


        }
        public void showGreeting(string cprNavn, bool connection,byte storageStatus, byte batteryStatus)
        {

           

            string cprIcon = "";
            string navnIcon = "";
            displayController.lcdDisplay();
            displayController.lcdClear();
            displayController.lcdHome();

            for (int i = 0; i < 6; i++)
            {
                cprIcon += cprNavn[i];
            }
            for (int i = 6; i < cprNavn.Length; i++)
            {
                navnIcon += cprNavn[i];
            }

            this.updateInfoBar(connection, storageStatus, batteryStatus);

            displayController.lcdGotoXY(5, 1);
            displayController.lcdPrint("Velkommen");
            displayController.lcdGotoXY(0, 2);
            displayController.lcdPrint("Du er logget ind som");
            displayController.lcdGotoXY(0, 3);
            displayController.lcdPrint(navnIcon);
            displayController.lcdGotoXY(14, 3);
            displayController.lcdPrint(cprIcon);
           
        }

        public void statusUpdateMeasurment(double time, bool connection, byte storageStatus, byte batteryStatus)
        {
            
            if(clearScreen)
            {
                clearScreen = false;
                displayController.lcdClear();

                
                this.updateInfoBar(connection, storageStatus, batteryStatus);
              
                displayController.lcdGotoXY(3, 1);

                displayController.lcdPrint("Maaling i gang");

                displayController.lcdGotoXY(2, 2);
                displayController.lcdPrint("__________");

                displayController.lcdGotoXY(2, 3);

                displayController.lcdPrint("Forhold dig i ro");

            }

            displayController.lcdGotoXY(2, 2);
            displayController.lcdGotoXY(2, 2);
            for (int i = 0; i < time/4; i++)
            {
                
                displayController.lcdPrint("#");
            }

            double procent = (time / 40) * 100;
            if (procent > 100)
            {
                procent = 100;
            }
            displayController.lcdGotoXY(15, 2);
            displayController.lcdGotoXY(15, 2);

            displayController.lcdPrint(Convert.ToString(Convert.ToInt32(procent)) + "%");


       
        }
        public void screenShow(short screenNb, bool connection,byte storageStatus,byte batteryStatus)
        {

            switch (screenNb)
            {
                

                case 2:
                    {
                        displayController.lcdClear();
                        this.updateInfoBar(connection, storageStatus, batteryStatus);

                        displayController.lcdGotoXY(2, 2);
                        displayController.lcdPrint("Forbind venligst");
                        displayController.lcdGotoXY(5, 3);
                        displayController.lcdPrint("Elektroder");
                    }
                    

                    break;
                case 3:
                    {
                        displayController.lcdClear();

                        this.updateInfoBar(connection, storageStatus, batteryStatus);

                        displayController.lcdGotoXY(3, 2);

                        displayController.lcdPrint("Tryk paa start");

                        clearScreen = true;
                                               

                    }
                   
                    break;
                case 5:
                    displayController.lcdClear();
                    this.updateInfoBar(connection, storageStatus, batteryStatus);

                    displayController.lcdGotoXY(1, 2);

                    displayController.lcdPrint("Maaling foretaget");
                    displayController.lcdGotoXY(0, 3);

                    displayController.lcdPrint("Ingen netforbindelse");



                    break;
                case 6:
                    displayController.lcdClear();
                    this.updateInfoBar(connection, storageStatus, batteryStatus);

                    displayController.lcdGotoXY(1, 2);

                    displayController.lcdPrint("Maaling foretaget");
                    displayController.lcdGotoXY(4, 3);

                    displayController.lcdPrint("Maaling sendt");

                    break;
                case 7:
                    displayController.lcdClear();
                    this.updateInfoBar(connection, storageStatus, batteryStatus);

                    displayController.lcdGotoXY(0, 2);

                    displayController.lcdPrint("Ingen id tilknyttet");
                    
                    break;
                case 8:
                    displayController.lcdClear();
                    this.updateInfoBar(connection, storageStatus, batteryStatus);

                    displayController.lcdGotoXY(1, 2);

                    displayController.lcdPrint("Batteriniveau lavt");
                    displayController.lcdGotoXY(2, 3);

                    displayController.lcdPrint("Tilslut oplader");

                    break;
                case 9:
                    {
                        displayController.lcdClear();
                        this.updateInfoBar(connection, storageStatus, batteryStatus);

                        displayController.lcdGotoXY(3, 2);
                        displayController.lcdPrint("Sender gamle");
                        displayController.lcdGotoXY(4, 3);
                        displayController.lcdPrint("maalinger");

                    }
                    break;
            }



        }



    }

}
