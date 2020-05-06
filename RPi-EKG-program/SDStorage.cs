using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Runtime.InteropServices.ComTypes;

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
        public byte checkUnSentData()
        {
            input = new FileStream(@"EKGMaster.txt", FileMode.OpenOrCreate, FileAccess.Read);
            reader = new StreamReader(input);

            string inputRecord;
            string[] inputFields = new string[2];
            inputRecord = reader.ReadLine();
            byte Counter = 0;
            while (inputRecord != null)
            {

                inputFields = inputRecord.Split(Convert.ToChar(";"));

                if (!inputFields[1].Contains("1"))
                {
                    Counter++;
                   
                }
                inputRecord = reader.ReadLine();
            }
            input.Close();

            return Counter;

        }

        public List<Measurement> FindUnSentData()
        {
            input = new FileStream(@"EKGMaster.txt", FileMode.Open, FileAccess.Read);
            reader = new StreamReader(input);

            List<Measurement> MeasurementList = new List<Measurement>();
            Measurement NewMeasurement = new Measurement();
            string inputRecord;
            string[] inputFields = new string[3];
            List<string> TextAlreadyInFile = new List<string>();

            inputRecord = reader.ReadLine();

            while (inputRecord != null)
            {
                TextAlreadyInFile.Add(inputRecord);
                inputRecord = reader.ReadLine();
            }

            input.Close();

            for (int i = 0; i < TextAlreadyInFile.Count; i++)
            {
                inputFields = TextAlreadyInFile[i].Split(Convert.ToChar(";"));
                if (!inputFields[1].Contains("1"))
                {
                    TextAlreadyInFile[i] += 1;


                    input = new FileStream(@"" + inputFields[0] + ".txt", FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(input);
                    Formatter = new BinaryFormatter();

                    try
                    {
                        NewMeasurement = (Measurement)Formatter.Deserialize(input);
                        MeasurementList.Add(NewMeasurement);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    input.Close();
                }
            }
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"EKGMaster.txt"))
                    foreach (string line in TextAlreadyInFile)
                    {
                        file.WriteLine(line);
                    }
            }
            catch (Exception)
            { throw; }
            return MeasurementList;
        }

        public int ReadCountMaster()
        {
            Count = 1;

            input = new FileStream(@"EKGMaster.txt", FileMode.OpenOrCreate, FileAccess.Read);
            reader = new StreamReader(input);

            while (reader.ReadLine() != null)
            {
                Count++;
            }
            input.Close();
            return Count;
        }


        public void StoreDataLocal(Measurement Data)
        {
            int count = ReadCountMaster();
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"EKGMaster.txt", true))
                {
                    file.WriteLine("EKGdata" + count + ";0");
                }
            }
            catch (Exception)
            { throw; }

            input = new FileStream(@"EKGdata" + count + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            Formatter = new BinaryFormatter();
            writer = new StreamWriter(input);

            try
            {
                Formatter.Serialize(input, Data);
            }
            catch (Exception)
            { throw; }
            input.Close();
        }

        public void StoreInfoLocal(string CPR, string FirstName)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"EKGPersonID.txt"))
            {
                file.WriteLine(CPR + ";" + FirstName + ";" + DateTime.Now.ToString());
            }

        }

        public string getInfoLocal()
        {

            input = new FileStream(@"EKGPersonID.txt", FileMode.Open, FileAccess.Read);
            reader = new StreamReader(input);

            string CPRNavn;
            string inputRecord = reader.ReadLine();
            string[] inputFields = new string[3];
            inputFields = inputRecord.Split(Convert.ToChar(";"));
            CPRNavn = inputFields[0] + inputFields[1];
            


            return CPRNavn;
        }

    }
}
