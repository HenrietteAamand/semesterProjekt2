using System;
using System.Collections.Generic;
using System.Text;
using RaspberryPiCore.LCD;



namespace RPi_EKG_program
{
    class Display
    {

        private static SerLCD displayController;
        private bool ClearScreen;

        

        public void updateInfoBar(bool connection,byte StorageStatus, byte batteryStatus)
        {
            
            string Connect = "";
            string Storage = "";

            if (connection == true)
            {
                Connect = "O";

            }
            else { Connect = "X"; }

            if (StorageStatus != 0)
            {
                Storage = "Data:"+ StorageStatus;
            }
            else { Storage = "Data:0"; }

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

            displayController.lcdGotoXY(0, 0);
            //displayController.lcdHome();
            displayController.lcdPrint(Convert.ToString(DateTime.Now.TimeOfDay));
            displayController.lcdGotoXY(5, 0);

            displayController.lcdPrint(" "+Connect + "  " + Storage + "  " + batteryniveau+"%");
            
        }


        public Display()
        {
            displayController = new SerLCD();
            displayController.lcdDisplay();
            displayController.lcdSetBackLight(255,255,0);
            ClearScreen = true;

            


        }
        public void ShowGreeting(string CPRNAVN, bool connection,byte storagestatus, byte batteryStatus)
        {

           

            string CPR = "";
            string Navn = "";
            displayController.lcdDisplay();
            displayController.lcdClear();
            displayController.lcdHome();

            for (int i = 0; i < 6; i++)
            {
                CPR += CPRNAVN[i];
            }
            for (int i = 6; i < CPRNAVN.Length; i++)
            {
                Navn += CPRNAVN[i];
            }

            

            this.updateInfoBar(connection, storagestatus, batteryStatus);

            
            /* + Navn + "  " + CPR*/

            displayController.lcdGotoXY(5, 1);
            displayController.lcdPrint("Velkommen");
            displayController.lcdGotoXY(0, 2);
            displayController.lcdPrint("Du er logget ind som");
            displayController.lcdGotoXY(0, 3);
            displayController.lcdPrint(Navn);
            displayController.lcdGotoXY(14, 3);
            displayController.lcdPrint(CPR);

        }

        public void StatusUpdateMeasurment(double time, bool connection, byte storagestatus, byte batteryStatus)
        {
            
            if(ClearScreen)
            {
                ClearScreen = false;
                displayController.lcdClear();

                
                this.updateInfoBar(connection, storagestatus, batteryStatus);
              
                displayController.lcdGotoXY(3, 1);

                displayController.lcdPrint("Maaling i gang");

                displayController.lcdGotoXY(2, 2);
                displayController.lcdPrint("__________");

                displayController.lcdGotoXY(2, 3);

                displayController.lcdPrint("Forhold dig i ro");

            }

            //if (Convert.ToInt32(time) < 2 || Convert.ToInt32(time)==10|| Convert.ToInt32(time) ==20|| Convert.ToInt32(time) == 30|| Convert.ToInt32(time) == 39)
            //{



            //}

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
        public void ScreenShow(short ScreenNb, bool connection,byte storagestatus,byte batteryStatus)
        {

            switch (ScreenNb)
            {
                case 1:
                    {
                        displayController.lcdClear();
                        this.updateInfoBar(connection, storagestatus, batteryStatus);

                        displayController.lcdGotoXY(3, 2);
                        displayController.lcdPrint("Sender gamle");
                        displayController.lcdGotoXY(4, 3);
                        displayController.lcdPrint("maalinger");

                    }
                break;

                case 2:
                    {
                        displayController.lcdClear();
                        this.updateInfoBar(connection, storagestatus, batteryStatus);

                        displayController.lcdGotoXY(2, 2);
                        displayController.lcdPrint("Forbind venligst");
                        displayController.lcdGotoXY(5, 3);
                        displayController.lcdPrint("Elektroder");
                    }
                    

                    break;
                case 3:
                    {
                        displayController.lcdClear();
                        this.updateInfoBar(connection, storagestatus, batteryStatus);

                        displayController.lcdGotoXY(3, 2);

                        displayController.lcdPrint("Tryk paa start");

                        ClearScreen = true;
                                               

                    }
                   
                    break;
                case 5:
                    displayController.lcdClear();
                    this.updateInfoBar(connection, storagestatus, batteryStatus);

                    displayController.lcdGotoXY(1, 2);

                    displayController.lcdPrint("Maaling foretaget");
                    displayController.lcdGotoXY(0, 3);

                    displayController.lcdPrint("Ingen netforbindelse");



                    break;
                case 6:
                    displayController.lcdClear();
                    this.updateInfoBar(connection, storagestatus, batteryStatus);

                    displayController.lcdGotoXY(1, 2);

                    displayController.lcdPrint("Maaling foretaget");
                    displayController.lcdGotoXY(4, 3);

                    displayController.lcdPrint("Maaling sendt");

                    break;
                case 7:
                    displayController.lcdClear();
                    this.updateInfoBar(connection, storagestatus, batteryStatus);

                    displayController.lcdGotoXY(0, 2);

                    displayController.lcdPrint("Ingen id tilknyttet");
                    
                    break;
                case 8:
                    displayController.lcdClear();
                    this.updateInfoBar(connection, storagestatus, batteryStatus);

                    displayController.lcdGotoXY(1, 2);

                    displayController.lcdPrint("Batteriniveau lavt");
                    displayController.lcdGotoXY(2, 3);

                    displayController.lcdPrint("Tilslut oplader");

                    break;
            }



        }



    }

}
