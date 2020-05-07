using System;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using System.Collections.Generic;
using System.Threading;

namespace RPi_EKG_program
{
    class Program
    {
        //    >DebugAdapterHost.Launch /LaunchJson:"C:\Users\emils\Source\Repos\semesterProjekt2\RPi-EKG-program\launch.json" /EngineGuid:541B8A8A-6081-4506-9F0A-1CE771DEBC04
        private SDStorage LocalStorage;
        private static SerLCD displayController1;


        static void Main(string[] args)
        {
            Display displayController = new Display();
            DatabaseIF LokalDB = new DatabaseIF();
            SerLCD display = new SerLCD();
            SDStorage LocalStorage = new SDStorage();

            
            ADC ADconverter = new ADC();
            bool test = true;
            test = LokalDB.isConnected();



            ADconverter.isCableConnected();
            //displayController.ScreenShow(4);

            LocalStorage.StoreInfoLocal("080596-1234;Emil");

            string CPRNAVN = LocalStorage.getInfoLocal();


            displayController.ShowGreeting(CPRNAVN, test, LocalStorage.checkUnSentData(), ADconverter.checkBattery());
            Thread.Sleep(6000);

            DateTime StartTime = DateTime.Now;
            DateTime EndTime = DateTime.Now;
            TimeSpan MeasureTime = EndTime - StartTime;


            displayController.ScreenShow(4);

            while (MeasureTime.TotalSeconds < 40)
            {
             
                EndTime = DateTime.Now;
                MeasureTime = EndTime - StartTime;
                displayController.StatusUpdateMeasurment(MeasureTime.TotalSeconds, test, LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                Thread.Sleep(10);


            }







            //display.lcdDisplay();
            //display.lcdClear();

            //display.lcdHome();

            //display.lcdPrint(Convert.ToString(DateTime.Now.TimeOfDay));
            //display.lcdGotoXY(5, 0);


            //display.lcdPrint(" O  Data:8  50%");

            //display.lcdGotoXY(5, 1);

            //string navn = "Lars";
            //string cpr = "080596";
            //string navn2 = "123456789012345678903";
            //string navn1 = "12345678901234567893";
            //string navn3 = "123456789 0123456789";

            //display.lcdPrint("Velkommen");
            //display.lcdGotoXY(0, 2);
            //display.lcdPrint("Du er loGGet ind som");
            //display.lcdGotoXY(0, 3);
            //display.lcdPrint(navn);
            //display.lcdGotoXY(14, 3);
            //display.lcdPrint(cpr);

            //displayController.ShowGreeting(CPRNAVN, test, LocalStorage.checkUnSentData(), ADconverter.checkBattery());

            //display.lcdGotoXY(0, 3);
            //display.lcdPrint("Emil");
            //display.lcdGotoXY(14, 3);
            //display.lcdPrint("123456");
            //display.lcdGotoXY(0, 3);
            //display.lcdPrint(navn2);
            //display.lcdGotoXY(0, 3);
            //display.lcdPrint(navn1);





            //string velkommen = "     Velkommen      Du er logget ind somLars  080596";

            //display.lcdGotoXY(0, 2);


            //display.lcdPrint(velkommen);

            //display.lcdGotoXY(0, 3);


            //displayController.ShowGreeting(CPRNAVN, test, LocalStorage.checkUnSentData(), ADconverter.checkBattery());

            //displayController.ShowGreeting(CPRNAVN);

            //displayController.updateMenuBar(LokalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());


            //Hvis batteri er over 5.



            DateTime Start = DateTime.Now;
            List<double> Test = new List<double>();
            Measurement measurement = new Measurement("123456-7890", Test, Start, 0.02, "54321");

            //for (int i = 0; i < /*40sek*/; i++)
            //{
            //    measurement.addToList(ADconverter.measureSignal());


            //}


            //}

        
        }
    }
}
