using System;
using RaspberryPiCore.ADC;
using RaspberryPiCore.TWIST;
using RaspberryPiCore.LCD;
using System.Collections.Generic;

namespace RPi_EKG_program
{
    class Program
    {
        //    >DebugAdapterHost.Launch /LaunchJson:"C:\Users\emils\Source\Repos\semesterProjekt2\RPi-EKG-program\launch.json" /EngineGuid:541B8A8A-6081-4506-9F0A-1CE771DEBC04
        private SDStorage LocalStorage;
        
        
       
        static void Main(string[] args)
        {
            Display displayController = new Display();
            DatabaseIF LokalDB = new DatabaseIF();

            SDStorage LocalStorage = new SDStorage();


            ADC ADconverter = new ADC();
            bool test = true;
            test = LokalDB.isConnected();



            ADconverter.isCableConnected();
            //displayController.ScreenShow(4);

            LocalStorage.StoreInfoLocal("080596-1234", "Emil");

            string CPRNAVN = LocalStorage.getInfoLocal();

            displayController.ShowGreeting(CPRNAVN);

            displayController.updateMenuBar(LokalDB.isConnected(), LocalStorage.checkUnSentData(), ADconverter.checkBattery());


            //Hvis batteri er over 5.



            DateTime Start = DateTime.Now;
            List<double> Test = new List<double>();
            Measurement measurement = new Measurement("123456-7890", Test, Start, 0.02, "54321");
                 
            //for (int i = 0; i < /*40sek*/; i++)
            //{
            //    measurement.addToList(ADconverter.measureSignal());
                

            //}


        }
       
        public void startIsPressed()
        {

        }
        
    }
}
