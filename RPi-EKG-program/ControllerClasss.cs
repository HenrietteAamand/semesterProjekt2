//using System;
//using RaspberryPiCore.ADC;
//using RaspberryPiCore.TWIST;
//using RaspberryPiCore.LCD;
//using System.Collections.Generic;
//using System.Threading;

//namespace RPi_EKG_program
//{
//    class Program
//    {
//        //    >DebugAdapterHost.Launch /LaunchJson:"C:\Users\emils\Source\Repos\semesterProjekt2\RPi-EKG-program\launch.json" /EngineGuid:541B8A8A-6081-4506-9F0A-1CE771DEBC04
       

//        static void Main(string[] args)
//        {

//            Display displayController = new Display();
//            DatabaseIF LocalDB = new DatabaseIF();
//            SDStorage LocalStorage = new SDStorage();
//            ADC ADconverter = new ADC();
//            Start_Button StartB = new Start_Button();


//            //bool test = true;
//            //test = LokalDB.isConnected();
//            //ADconverter.isCableConnected();

//            displayController.updateMenuBar(LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
//            string CPRNAVN = LocalStorage.getInfoLocal();

//            displayController.ShowGreeting(CPRNAVN);

//            Thread.Sleep(5000);


//            while(true)
//            {

//                while (ADconverter.checkBattery() == 1)
//                {
//                    displayController.ScreenShow(8);
//                }

//                displayController.ScreenShow(3);

//                if (StartB.isPressed())
//                {
//                    if (ADconverter.isCableConnected() == true)
//                    {
//                        displayController.ScreenShow(4);

//                        DateTime StartTime = DateTime.Now;
//                        DateTime EndTime = DateTime.Now;
//                        TimeSpan MeasureTime = EndTime - StartTime;

//                        Measurement NewMeasurement = new Measurement("OnTheMoveCPRnumberYes", new List<double>(), DateTime.Now, 1.2, "Måler1");

//                        while ( MeasureTime.TotalSeconds < 40)
//                        {
//                           NewMeasurement.addToList( ADconverter.measureSignal());
//                            EndTime = DateTime.Now;
//                            MeasureTime = EndTime - StartTime;
//                            displayController.StatusUpdateMeasurment();

//                        }

//                        LocalStorage.StoreDataLocal(NewMeasurement);

//                        if (LocalDB.isConnected())
//                        {
//                            displayController.ScreenShow(6);

//                            LocalDB.sendData(NewMeasurement);

//                        }

//                        displayController.ScreenShow(5);
//                        Thread.Sleep(10000);

//                        displayController.ScreenShow(3);//Denne behøves ikke, da den alligevel breaker While og viser skærm 3.



//                    }

//                    else
//                    {
//                        displayController.ScreenShow(2);
//                        Thread.Sleep(5000);
//                    }

//                }


//            }
      
//        }

//    }
//}
