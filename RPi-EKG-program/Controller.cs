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
                displayController.screenShow(7, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                
                if (LocalDB.isConnected())
                {
                    LocalStorage.storeInfoLocal(LocalDB.recieveData(MonitorID));

                }

            }

            displayController.showGreeting(LocalStorage.getInfoLocal(), LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());

            Thread.Sleep(5000);


            while (true)
            {
                if (LocalDB.isConnected() && LocalStorage.checkUnSentData()!=0)
                {
                    displayController.screenShow(9, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                    Thread.Sleep(1000);

                    foreach (var item in LocalStorage.findUnSentData())
                    {
                        LocalDB.sendData(item);
                    }
                    Thread.Sleep(5000);

                }

                while (ADconverter.checkBattery() == 1)
                {
                    displayController.screenShow(8, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                    Thread.Sleep(3000);
                }

                displayController.screenShow(3, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());

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
                        displayController.statusUpdateMeasurment(MeasureTime.TotalSeconds, connection, lokalUnSent, battery);

                        while (MeasureTime.TotalSeconds < 40)
                        {
                            NewMeasurement.addToList(ADconverter.measureSignal());


                            EndTime = DateTime.Now;
                            MeasureTime = EndTime - StartTime;

                            //Det var her vi skulle have startet en ny thread, og fordi vi ikke har lært trådprogrammering og fordi
                            //den ikke kan nå at følge med sampleRate, har vi valgt at fjerne den for denne omgang.

                            //displayController.StatusUpdateMeasurment(MeasureTime.TotalSeconds, connection, lokalUnSent, battery);

                            Thread.Sleep(sampleRate-4);

                        }
                        NewMeasurement.SampleRate = (40 / NewMeasurement.Measurements.Count);

                        //Vi viser den nu før og efter en måling.
                        displayController.statusUpdateMeasurment(MeasureTime.TotalSeconds, connection, lokalUnSent, battery);
                        //Thread.Sleep(5000);

                        LocalStorage.storeDataLocal(NewMeasurement);

                        if (LocalDB.isConnected())
                        {
                            displayController.screenShow(6, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());

                            LocalDB.sendData(NewMeasurement);

                        }
                        else
                        {

                            displayController.screenShow(5, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                            Thread.Sleep(10000);

                        }

                        /*displayController.ScreenShow(3);*///Denne behøves ikke, da den alligevel breaker While og viser skærm 3.



                    }

                    else
                    {
                        displayController.screenShow(2, LocalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());
                        Thread.Sleep(5000);
                    }

                }


            }

        }

    }
}
