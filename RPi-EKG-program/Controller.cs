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
           
        private const string MonitorID = "Måler1";
        private static int sampleRate = 10;

        
        static void Main(string[] args)
        {

            Display displayController = new Display();
            DatabaseIF LocalDB = new DatabaseIF();
            SDStorage LocalStorage = new SDStorage();
            ADC ADconverter = new ADC();
            Start_Button StartB = new Start_Button();

         
            LocalDB.isConnected();




            while (LocalStorage.getCPRLocal() == null)
            {
                displayController.ScreenShow(7, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                
                if (LocalDB.isConnected())
                {
                    LocalStorage.StoreInfoLocal(LocalDB.RecieveData(MonitorID));

                }

            }

            displayController.ShowGreeting(LocalStorage.getInfoLocal(), LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());

            Thread.Sleep(5000);


            while (true)
            {
                if (LocalDB.isConnected() && LocalStorage.checkUnSentData()!=0)
                {
                    displayController.ScreenShow(9, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                    Thread.Sleep(1000);

                    foreach (var item in LocalStorage.FindUnSentData())
                    {
                        LocalDB.sendData(item);
                    }
                    Thread.Sleep(5000);

                }

                while (ADconverter.checkBattery() == 1)
                {
                    displayController.ScreenShow(8, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                    Thread.Sleep(3000);
                }

                displayController.ScreenShow(3, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());

                if (StartB.isPressed())
                {
                    if (ADconverter.isCableConnected() == true)
                    {

                        bool connection = LocalDB.isConnected();
                       
                        Byte lokalUnSent = LocalStorage.checkUnSentData();
                        byte battery = ADconverter.checkBattery();

                        DateTime StartTime = DateTime.Now;
                        DateTime EndTime = DateTime.Now;
                        TimeSpan MeasureTime = EndTime - StartTime;

                        

                        Measurement NewMeasurement = new Measurement(LocalStorage.getCPRLocal(), new List<double>(), DateTime.Now, (sampleRate/1000),MonitorID );

                        while (MeasureTime.TotalSeconds < 40)
                        {
                            NewMeasurement.addToList(ADconverter.measureSignal());
                            EndTime = DateTime.Now;
                            MeasureTime = EndTime - StartTime;
                            displayController.StatusUpdateMeasurment(MeasureTime.TotalSeconds, connection, lokalUnSent, battery);
                          
                     
                            Thread.Sleep(sampleRate);

                        }

                        LocalStorage.StoreDataLocal(NewMeasurement);

                        if (LocalDB.isConnected())
                        {
                            displayController.ScreenShow(6, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());

                            LocalDB.sendData(NewMeasurement);

                        }
                        else
                        {

                            displayController.ScreenShow(5, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                            Thread.Sleep(10000);

                        }

                        /*displayController.ScreenShow(3);*///Denne behøves ikke, da den alligevel breaker While og viser skærm 3.



                    }

                    else
                    {
                        displayController.ScreenShow(2, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                        Thread.Sleep(5000);
                    }

                }


            }

        }

    }
}
