using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RPi_EKG_program
{
    class SDStorage
    {
        private FileStream input;
        private StreamReader reader;
        private StreamWriter writer;
        private BinaryFormatter Formatter;
        public int Count { get; private set; }

        public SDStorage()
        {

        }
        public bool checkUnSentData()
        {

            input = new FileStream(@"EKGMaster.txt", FileMode.Open, FileAccess.Read);
            reader = new StreamReader(input);

            string inputRecord;
            string[] inputFields;

            while (reader.ReadLine() != null)
            {
                inputRecord = reader.ReadLine();
                inputFields = inputRecord.Split(";");

                if(inputFields[1].Contains("0"))
                {
                    return true;
                }
            }
            input.Close();

            return false;

        }

        public Measurement FindUnSentData()
        {
            input = new FileStream(@"EKGMaster.txt", FileMode.Open, FileAccess.ReadWrite);
            reader = new StreamReader(input); 
            writer = new StreamWriter(input);

            Measurement NewMeasurement = new Measurement();
            string inputRecord;
            string[] inputFields;

            while (reader.ReadLine() != null)
            {
                inputRecord = reader.ReadLine();
                inputFields = inputRecord.Split(";");

                if (inputFields[1].Contains("0"))
                {
                    writer.Write(inputFields[0]+",1");
                    input.Close();
                    input = new FileStream(@""+inputFields[0]+".txt", FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(input);
                    Formatter = new BinaryFormatter();

                    try
                    {
                        NewMeasurement = (Measurement)Formatter.Deserialize(input);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    input.Close();

                    break;
                }
            }


            return NewMeasurement;
        }

        public int ReadCountMaster()
        {
            Count = 1;

            input = new FileStream(@"EKGMaster.txt", FileMode.Open, FileAccess.Read);
            reader = new StreamReader(input);


           while(reader.ReadLine() != null)
            {
                Count++;
            }
            input.Close();
            return Count;
           
        }

        public void AddToMasterDataFile()
        {
            input = new FileStream(@"EKGMaster.txt", FileMode.OpenOrCreate, FileAccess.Write);

            writer = new StreamWriter(input);

            writer.WriteLine("EKGdata"+(ReadCountMaster())+",0");
            input.Close();
        }

        public void StoreDataLocal(Measurement Data)
        {
            input = new FileStream(@"EKGdata"+ReadCountMaster()+".txt", FileMode.OpenOrCreate, FileAccess.Write);
            Formatter = new BinaryFormatter();
            writer = new StreamWriter(input);

            try
            {
                Formatter.Serialize(input, Data);
            }
            catch (Exception)
            {

                throw;
            }

            input.Close();

        }


    }
}
