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
        private BinaryFormatter formatter;
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
            byte counter = 0;
            while (inputRecord != null && inputRecord !="")
            {

                inputFields = inputRecord.Split(Convert.ToChar(";"));

                if (!inputFields[1].Contains("1"))
                {
                    counter++;

                }
                inputRecord = reader.ReadLine();
            }
            input.Close();

            return counter;

        }

        public List<Measurement> findUnSentData()
        {
            input = new FileStream(@"EKGMaster.txt", FileMode.Open, FileAccess.Read);
            reader = new StreamReader(input);

            List<Measurement> measurementList = new List<Measurement>();
            Measurement newMeasurement = new Measurement();
            string inputRecord;
            string[] inputFields = new string[3];
            List<string> textAlreadyInFile = new List<string>();

            inputRecord = reader.ReadLine();

            while (inputRecord != null)
            {
                textAlreadyInFile.Add(inputRecord);
                inputRecord = reader.ReadLine();
            }

            input.Close();

            for (int i = 0; i < textAlreadyInFile.Count; i++)
            {
                inputFields = textAlreadyInFile[i].Split(Convert.ToChar(";"));
                if (!inputFields[1].Contains("1"))
                {

                    textAlreadyInFile[i] += 1;


                    input = new FileStream(@"" + inputFields[0] + ".txt", FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(input);
                    formatter = new BinaryFormatter();

                    try
                    {
                        newMeasurement = (Measurement)formatter.Deserialize(input);
                        measurementList.Add(newMeasurement);
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
                    foreach (string line in textAlreadyInFile)
                    {
                        file.WriteLine(line);
                    }
            }
            catch (Exception)
            { throw; }
            return measurementList;
        }

        public int readCountMaster()
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


        public void storeDataLocal(Measurement data)
        {
            int count = readCountMaster();
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
            formatter = new BinaryFormatter();
            writer = new StreamWriter(input);

            try
            {
                formatter.Serialize(input, data);
            }
            catch (Exception)
            { throw; }
            input.Close();
        }

        public void storeInfoLocal(string cprName)
        {
            string cpr;
            string firstName;

            cpr = cprName.Split(Convert.ToChar(";"))[0];
            firstName = cprName.Split(Convert.ToChar(";"))[1];

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"EKGPersonID.txt"))
            {
                file.WriteLine(cpr + ";" + firstName + ";" + DateTime.Now.ToString());
            }

        }

        public string getInfoLocal()
        {

            input = new FileStream(@"EKGPersonID.txt", FileMode.Open, FileAccess.Read);
            reader = new StreamReader(input);

            string cprName;
            string inputRecord = reader.ReadLine();
            string[] inputFields = new string[3];
            inputFields = inputRecord.Split(Convert.ToChar(";"));
            cprName = inputFields[0].Split(Convert.ToChar("-"))[0] + inputFields[1];

            return cprName;
        }

        public string getCPRLocal()
        {
            input = new FileStream(@"EKGPersonID.txt", FileMode.Open, FileAccess.Read);
            reader = new StreamReader(input);

            string cpr = "";
            string inputRecord = reader.ReadLine();
            string[] inputFields = new string[3];
            inputFields = inputRecord.Split(Convert.ToChar(";"));

            cpr = inputFields[0];
            return cpr;
        }


    }
}
