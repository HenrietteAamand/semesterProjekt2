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
           
        private const string monitorID = "1";
        private static int sampleRate = 20;

        static void Main(string[] args)
        {

            Display displayController = new Display();
            DatabaseIF localDB = new DatabaseIF();
            SDStorage localStorage = new SDStorage();
            ADC adConverter = new ADC();
            Start_Button startB = new Start_Button();

         

            if (localDB.isConnected())
            {
                localStorage.storeInfoLocal(localDB.recieveData(monitorID));           
            }


            while (localStorage.getCPRLocal() == null|| localStorage.getCPRLocal() == "")
            {
                displayController.screenShow(7, localDB.isConnected(), localStorage.checkUnSentData(), adConverter.checkBattery());
                

                if (localDB.isConnected())
                {
                    localStorage.storeInfoLocal(localDB.recieveData(monitorID));

                }

            }

            displayController.showGreeting(localStorage.getInfoLocal(), localDB.isConnected(), localStorage.checkUnSentData(), adConverter.checkBattery());
            Thread.Sleep(5000);
         


            while (true)
            {
               
                while (adConverter.checkBattery() == 1)
                {
                    displayController.screenShow(8, localDB.isConnected(), localStorage.checkUnSentData(), adConverter.checkBattery());
                   
                }

                if (localDB.isConnected() && localStorage.checkUnSentData() != 0)
                {
                    displayController.screenShow(9, localDB.isConnected(), localStorage.checkUnSentData(), adConverter.checkBattery());


                    foreach (var item in localStorage.findUnSentData())
                    {
                        localDB.sendData(item);
                    }
                    

                }

                displayController.screenShow(3, localDB.isConnected(), localStorage.checkUnSentData(), adConverter.checkBattery());

                if (startB.isPressed())
                {
                    if (adConverter.isCableConnected() == true)
                    {

                        bool connection = localDB.isConnected();
                       
                        Byte storageStatus = localStorage.checkUnSentData();
                        byte batteryStatus = adConverter.checkBattery();

                        DateTime startTime = DateTime.Now;
                        DateTime endTime = DateTime.Now;
                        TimeSpan measureTime = endTime - startTime;


                        Thread.Sleep(5000);
                        Measurement newMeasurement = new Measurement(localStorage.getCPRLocal(), new List<double>(), DateTime.Now, (sampleRate/1000),monitorID );
                        displayController.statusUpdateMeasurment(measureTime.TotalSeconds, connection, storageStatus, batteryStatus);

                        while (measureTime.TotalSeconds < 40)
                        {
                            newMeasurement.addToList(adConverter.measureSignal());


                            endTime = DateTime.Now;
                            measureTime = endTime - startTime;

                            //Det var her vi skulle have startet en ny thread, og fordi vi ikke har lært trådprogrammering og fordi
                            //den ikke kan nå at følge med sampleRate, har vi valgt at fjerne den for denne omgang.

                            //displayController.statusUpdateMeasurment(measureTime.TotalSeconds, connection, storageStatus, batteryStatus);

                            Thread.Sleep(sampleRate-4);

                        }
                        newMeasurement.SampleRate = (Convert.ToDouble(40 )/ Convert.ToDouble( newMeasurement.Measurements.Count));

                        //Vi viser den nu før og efter en måling.
                        displayController.statusUpdateMeasurment(measureTime.TotalSeconds, connection, storageStatus, batteryStatus);
                        Thread.Sleep(5000);

                        localStorage.storeDataLocal(newMeasurement, localDB.isConnected());

                        if (localDB.isConnected())
                        {
                            displayController.screenShow(6, localDB.isConnected(), localStorage.checkUnSentData(), adConverter.checkBattery());

                            localDB.sendData(newMeasurement);
                            Thread.Sleep(10000);

                        }
                        else
                        {

                            displayController.screenShow(5, localDB.isConnected(), localStorage.checkUnSentData(), adConverter.checkBattery());
                            Thread.Sleep(10000);


                        }

                    }

                    else
                    {
                        displayController.screenShow(2, localDB.isConnected(), localStorage.checkUnSentData(), adConverter.checkBattery());
                        Thread.Sleep(15000);

                    }

                }


            }

        }

    }
}
