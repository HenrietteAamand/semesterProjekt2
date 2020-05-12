using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using DataTier.Databaser;
using DataTier.Interfaces;
using System.Linq;

namespace LogicTier
{
    public class AnalyzeECG
    {
        private List<IllnessModel> illnessList;
        private PatientModel patientRef;
        private List<ECGModel> ecgList;
        List<List<double>> listOfListOfIntervals = new List<List<double>>();
        public List<AnalyzedECGModel> NewAECGModelsList = new List<AnalyzedECGModel>();
        List<ECGModel> newECGList;
        private const int intervalHistogram = 10;

        private ILocalDatabase lDBRef;
        private MainWindowLogic mainLogic;

        public TimeSpan IntervalIR { get; private set; }
        public double Baseline { get; private set; }

        private int aECGIDS = 1;

        private double rTakThreshold;

        public double RTakThreshhold
        {
            get { return rTakThreshold; }
            set { rTakThreshold = value; }
        }



        public List<double> STSegmentList { get; private set; }
        public List<int> STSegmentIndexList { get; private set; }
        public double STSegmentTreshhold { get; private set; }
        public bool STSegmentElevated { get; private set; }
        public bool STSegmentDepressed { get; private set; }
        public List<double> AECGCollection { get; private set; }






        public AnalyzeECG()
        {
            illnessList = new List<IllnessModel>();
            
            patientRef = new PatientModel();
            lDBRef = new TestDB();
            
            illnessList = lDBRef.GetAllIllnesses();
            ecgList = new List<ECGModel>();
            ecgList = lDBRef.GetAllECGs();
            newECGList = new List<ECGModel>();
            foreach (ECGModel ecg in ecgList)
            {
                if (!ecg.IsAnalyzed)
                {
                    newECGList.Add(ecg);
                }
            }

            //Der oprettes nye aECG for alle nye ECG'er
            foreach (ECGModel ecg in newECGList)
            {
                NewAECGModelsList.Add(new AnalyzedECGModel(ecg.CPR, ecg.ECGID, aECGIDS, ecg.Date, ecg.SampleRate, ecg.Values, ecg.MonitorId));
            }
        }

        //public List<ECGModel> LoadNewECGs(PatientModel patient) { throw new NotImplementedException(); }

        public void CreateAnalyzedECGs()
        {

            //Der kommer en liste med alle ECG'er
            //Der laves en liste med ECG'er som er nye.


            //Beregner og sætter baseline for alle nyoprettede aECG'er
            CalculateBaseline();

            ////Beregner og sætter ST for alle nye målinger. Laver også lister for ST index og values, til at lave graf
            CalculateST();

            ////Beregner og sætter puls for alle nye målinger
            //CalculatePuls();

            ////Tilføjer illnesses til alle nye målinger
            AddIllnes();

            foreach (AnalyzedECGModel aECG in NewAECGModelsList)
            {
                UploadAnalyzedECG(aECG);

            }


        }

        ////public void CalculateIntervalR() { //Skal bruges til at lave puls, hvis det skal implementeres }

        public void CalculateBaseline()
        {
            //Opdel ECG værdier i intervaller
            //Placer alle mælte værdier i et af intervallerne
            //Tæller hvilket interval, der har flest målte værdier
            //Sæt baseline til, at være i midten af intervallet.
            //Evt. kan laves histogram inde i intervallet

            //Laver histogram
             foreach (ECGModel ecg in newECGList)
            {
                //Sætter max og min og laver et valuespan
                double min = ecg.Values.Min();
                double max = ecg.Values.Max();


                double valueSpan = Math.Sqrt(Math.Pow(max, 2)) - min;

                //ValueSpan opdeles i x-antal dele med hvert et interval på 100/x% af det samlede valueSpan
                //Hver del oprettes som liste automatisk
                //For hver del(i) startende fra 0 og til der er x dele skal der oprettes en liste med værdier fra i*x til (((i+1)*(100/x%))/100)
                //Listen inddeler alle værdier i en af intervallerne


                for (int i = 0; i < intervalHistogram; i++)
                {
                    listOfListOfIntervals.Add(new List<double>());
                    //Tage alle værdier fra newECGList som er mellem i*x og (((i+1)*(100/x%))/100) og putte ind den ny liste
                    foreach (double value in ecg.Values)
                    {
                        if (value >= (min + (i * valueSpan /intervalHistogram)) && 
                            value < (min + (i * valueSpan / intervalHistogram)) + valueSpan/intervalHistogram)  
                        {
                            listOfListOfIntervals[i].Add(value);
                        }
                        else if(i == 9 && value > max - valueSpan/intervalHistogram)
                        {
                            listOfListOfIntervals[9].Add(value);
                        }
                    }


                }

                //Laver baseline
                List<double> intervalList = new List<double>();
                int intervalListCount = listOfListOfIntervals[0].Count;
                for (int i = 0; i < listOfListOfIntervals.Count; i++)
                {
                    if (listOfListOfIntervals[i].Count > intervalListCount)
                    {
                        intervalListCount = listOfListOfIntervals[i].Count;
                        intervalList = listOfListOfIntervals[i];
                    }

                }

                //Sætter Baseline for aECG
                int j = 0;
                NewAECGModelsList[j].Baseline = intervalList.Average();
                RTakThreshhold = NewAECGModelsList[j].Baseline+1.5;
                j++;

            }


        }

        public void CalculateST()
        {
            
            //Måle R-tak
            //R-tak threshhold er sat til Baseline +1,5 mV
            //Den finder ud af, hvornår value bliver højere end threshold
            //Så tager den, den højeste værdi efter threshold og gemmer indexet for spidsen på r-takken
            foreach (ECGModel ecg in newECGList)
            {
                int j = 0;
                int rSpidsIndex = 0;
                STSegmentList = new List<double>();
                STSegmentIndexList = new List<int>();
                //Løber alle values igennem
                for (int i = 0; i < ecg.Values.Count; i++)
                {
                    //Hvis en value er større end threshold og større end den tidligere største værdi bliver rSpidsIndex = i
                    if (ecg.Values[i] > RTakThreshhold && ecg.Values[i] > ecg.Values[rSpidsIndex])
                    {
                        rSpidsIndex = i;
                    }
                }

                int sSpidsIndex = 0;
                for (int i = 0; i < ecg.Values.Count; i++)
                {
                    //Hvis en value er mindre end baseline og mindre end den tidligere mindste værdi og index er større end index for rSpidsIndex(dvs efter den)
                    //bliver sSpidsIndex = i
                    if (ecg.Values[i] < NewAECGModelsList[j].Baseline && ecg.Values[i] < ecg.Values[sSpidsIndex] && i > rSpidsIndex)
                    {
                        sSpidsIndex = i;
                    }
                }

                int tSpidsIndex = 0;
                for (int i = 0; i < ecg.Values.Count; i++)
                {
                    //Hvis en value er større end baseline og større end den tidligere største værdi og det ligger efter rSpidsIndex bliver tSpidsIndex = i
                    if (ecg.Values[i] > NewAECGModelsList[j].Baseline && ecg.Values[i] > ecg.Values[tSpidsIndex] && i > sSpidsIndex)
                    {
                        tSpidsIndex = i;
                    }
                }


                //Alle values løbes igennem
                for (int i = 0; i < ecg.Values.Count; i++)
                {
                    //Hvis indexet for en value, ligger ml. sSpidsIndex og tSpidsIndex tilføjes de til stSegment
                    if (i >= sSpidsIndex && i <= tSpidsIndex)
                    {
                        STSegmentList.Add(ecg.Values[i]);
                        STSegmentIndexList.Add(i);
                    }
                    
                }
                //Startindexet gemmes i alle aECG's så man ved hvor grafen skal starte for ST-segmentet
                NewAECGModelsList[j].STStartIndex = STSegmentIndexList[0];

                NewAECGModelsList[j].STValues = STSegmentList;


                //STSegmentList's længde sammenlignes med Illnesses reference værdier
                //Hvis STSegmentList er for lang, er ST - segmentet deprimeret
                if (STSegmentList.Count > illnessList[0].STMax)
                    {
                        STSegmentDepressed = true;
                    }

                //Hvis længden ml.sSpindsIndex og rSpindsIndex er for lang, we ST - segmentet eleveret

                if ((sSpidsIndex - rSpidsIndex) > illnessList[0].SRMax)
                    {
                        STSegmentElevated = true;
                    }
                j++;
            }


            //Måle tiden fra R-tak til første målte værdi under baseline

            //Hvis den tid er for høj/lang er der STEMI
            //Kalder AddIllness()
            //Måler tiden fra første værdi under baseline, til første værdi over baseline igen
            //Hvis den tid er for lang så er der NonStemi.
            //Kalder AddIllnes
            //Kig på powerpoint for intro slide 19.

            //laver liste med værdier for ST-segment
            //Listen indeholder værdier fra den laveste værdi efter R-takken til den rammer en værdi der er højere end baseline.

        }

        public void AddIllnes()
        {
            //Tilføjer Illness til aECG-måling
            //Kaldes af CalculateST()
            foreach (AnalyzedECGModel aECG in NewAECGModelsList)
            {
                if (STSegmentDepressed)
                {
                    aECG.Illnes = illnessList[0];
                }

                if (STSegmentElevated)
                {
                    //aECG.Illnes = illnessList[1];
                }
            }
        }

        ////public void CreateAECGChart()
        ////{
        ////    //Laver lister med værdier for ECG
        ////}

        ////public void CreateMark(List<double> STSegmentList, List<int> STSegmentIndexList) { }

        ////public void addMark()
        ////{

        ////}

        public void UploadAnalyzedECG(AnalyzedECGModel aECG)
            {
                lDBRef.UploadAnalyzedECGs(aECG);
            }


        //public void CalculatePuls()
        //{

                //int Rtak_old = 0;
                //int Rtak_new = 0;
                //double sample;
                //double diff;
                //private double threshold = 2.2;
                //bool belowThreshold = true;
                //List<double> RRList = new List<double>();
                //private int Puls;
                //int aECGIDS = 1;
        //    //For hver ecg skal den tage det første objekt i aECG og sætte pulsen for det. Så skal både ecg og aECG gå én op.
        //    throw new NotImplementedException();
        //    foreach (ECGModel ecg in newECGList)
        //    {
        //        for (int i = 0; i < ecg.Values.Count; i++)
        //        {
        //            if (ecg.Values[i] > threshold && belowThreshold == true)
        //            {
        //                Rtak_new = i;
        //                diff = (Rtak_new - Rtak_old) / Convert.ToDouble(ecg.SampleRate);
        //                RRList.Add(diff);
        //                Rtak_old = Rtak_new;
        //            }
        //            if (ecg.Values[i] < threshold)
        //            {
        //                belowThreshold = true;
        //            }
        //            else
        //            {
        //                belowThreshold = false;
        //            }
        //        }

        //        int Puls = 0;
        //        if (RRList.Count > 0)
        //        {
        //            Puls = (int)(60 / RRList.Average());
        //            RRList.RemoveAt(0);
        //        }

        //        int j = 0;
        //        NewAECGModelsList[j].Pulse = Puls;
        //        j++;


        //    }
        //}

    }
}
