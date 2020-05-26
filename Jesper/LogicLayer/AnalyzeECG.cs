using DataTier.Models;
using System;
using System.Collections.Generic;
using DataTier.Databaser;
using DataTier.Interfaces;
using System.Linq;

namespace LogicTier
{
    public class AnalyzeECG
    {
        #region Attributes
        private ILocalDatabase lDBRef;
        private const int intervalHistogram = 5;

        private List<IllnessModel> illnessList;
        private List<ECGModel> ecgList;
        private List<List<double>> listOfListOfIntervals; 
        #endregion

        #region Properties
        private List<AnalyzedECGModel> newAECGModelsList;

        public List<AnalyzedECGModel> NewAECGModelsList
        {
            get { return newAECGModelsList; }
            set { newAECGModelsList = value; }
        }

        private List<ECGModel> newECGList;

        public List<ECGModel> NewECGList
        {
            get { return newECGList; }
            set { newECGList = value; }
        }

        private List<int> aECGIDS;

        public List<int> AECGIDS
        {
            get { return aECGIDS; }
            set { aECGIDS = value; }
        }

        private int nextID;

        public int NextID
        {
            get { return nextID; }
            set { nextID = value; }
        }

        private double rTakThreshold;

        public double RTakThreshhold
        {
            get { return rTakThreshold; }
            set { rTakThreshold = value; }
        }

        public List<double> STSegmentList { get; private set; }
        public List<int> STSegmentIndexList { get; private set; }
        public List<AnalyzedECGModel> aECGList { get; private set; }
        #endregion

        #region Ctors
        public AnalyzeECG()
        {
            illnessList = new List<IllnessModel>();
            lDBRef = new Database();
            aECGList = new List<AnalyzedECGModel>();
            ecgList = new List<ECGModel>();
            NewECGList = new List<ECGModel>();
            NewAECGModelsList = new List<AnalyzedECGModel>();

            illnessList = lDBRef.GetAllIllnesses();
            ecgList = lDBRef.GetAllECGs();
            aECGList = lDBRef.GetAllAnalyzedECGs();
        } 
        #endregion

        #region Methods
        public void GetNewECG()
        {
            ecgList = lDBRef.GetAllECGs();
            NewECGList.Clear();
            foreach (ECGModel ecg in ecgList)
            {
                if (!ecg.IsAnalyzed)
                {
                    NewECGList.Add(ecg);
                }
            }
        }
        public void CreateAnalyzedECGs()
        {
            GetNewECG();

            //Næste ID findes
            FindNextID();
            //Der oprettes nye aECG for alle nye ECG'er
            foreach (ECGModel ecg in NewECGList)
            {

                NewAECGModelsList.Add(new AnalyzedECGModel(ecg.CPR, ecg.ECGID, NextID, ecg.Date, ecg.SampleRate, ecg.Values, ecg.MonitorID));
                NextID++;
            }

            //Der kommer en liste med alle ECG'er
            //Der laves en liste med ECG'er som er nye.

            if (NewAECGModelsList.Count != 0)
            {


                //Beregner og sætter baseline for alle nyoprettede aECG'er
                CalculateBaseline();

                //Beregner og sætter ST for alle nye målinger. Laver også lister for ST index og values, til at lave graf
                CalculateST();

                //Tilføjer illnesses til alle nye målinger
                AddIllnes();

                foreach (AnalyzedECGModel aECG in NewAECGModelsList)
                {
                    UploadAnalyzedECG(aECG);
                }
                foreach (ECGModel ecg in NewECGList)
                {
                    ecg.IsAnalyzed = true;
                    lDBRef.UpdateIsAnalyzed(ecg);

                }
            }

        }

        public void CalculateBaseline()
        {

            //Laver histogram
            foreach (AnalyzedECGModel aECG in NewAECGModelsList)
            {
                //Sætter max og min og laver et valuespan
                double min = aECG.Values.Min();
                double max = aECG.Values.Max();
                listOfListOfIntervals = new List<List<double>>();


                double valueSpan = Math.Sqrt(Math.Pow(max, 2)) - min;

                //ValueSpan opdeles i x-antal dele med hvert et interval på 100/x% af det samlede valueSpan
                //Listen inddeler alle værdier i en af intervallerne
                for (int i = 0; i < intervalHistogram; i++)
                {

                    listOfListOfIntervals.Add(new List<double>());
                    foreach (double value in aECG.Values)
                    {
                        if (value >= (min + (i * valueSpan / intervalHistogram)) &&
                            value < (min + (i * valueSpan / intervalHistogram)) + valueSpan / intervalHistogram)
                        {
                            listOfListOfIntervals[i].Add(value);
                        }
                        else if (i == 9 && value > max - valueSpan / intervalHistogram)
                        {
                            listOfListOfIntervals[9].Add(value);
                        }
                    }


                }

                //Laver baseline
                List<double> intervalList = new List<double>();
                int intervalListCount = listOfListOfIntervals[0].Count;
                intervalList = listOfListOfIntervals[0];

                for (int i = 0; i < listOfListOfIntervals.Count; i++)
                {
                    if (listOfListOfIntervals[i].Count > intervalListCount)
                    {
                        intervalListCount = listOfListOfIntervals[i].Count;
                        intervalList = listOfListOfIntervals[i];
                    }

                }

                //Sætter Baseline for aECG
                double avr = intervalList.Average();
                aECG.Baseline = avr;

                RTakThreshhold = aECG.Baseline + 1.5;
            }


        }

        public void CalculateST()
        {
            foreach (AnalyzedECGModel aECG in NewAECGModelsList)
            {

                int rSpidsIndex = 0;
                int tSpidsIndex = 0;
                bool rSpidsReached = false;
                bool rSpidsTwoReached = false;
                STSegmentList = new List<double>();
                STSegmentIndexList = new List<int>();

                //Løber alle values igennem
                for (int i = 0; i < aECG.Values.Count; i++)
                {
                    if (aECG.Values[i] < RTakThreshhold && rSpidsIndex != 0)
                    {
                        rSpidsReached = true;
                    }

                    //Hvis en value er større end threshold og større end den tidligere største værdi bliver rSpidsIndex = i
                    if (aECG.Values[i] > RTakThreshhold && aECG.Values[i] > aECG.Values[rSpidsIndex] && !rSpidsReached)
                    {
                        rSpidsIndex = i;
                    }
                }

                rSpidsReached = false;
                int sSpidsIndex = 0;

                //Der ledes ml. rtakken og 400ms frem
                //Hvis ikke der er nået en værdi under baseline indenfor 0,1S er ST eleveret.
                int tenMsAfterR = (rSpidsIndex + (int)(0.1 / aECG.SampleRate));
                for (int i = rSpidsIndex; i < tenMsAfterR; i++)
                {
                    aECG.STElevated = true;
                    if (aECG.Values[i] <= aECG.Baseline)
                    {
                        aECG.STElevated = false;
                    }

                }
                //Så skal sSpidsIndex sættes til index for rTakIndex + 0,1S
                if (aECG.STElevated)
                {
                    sSpidsIndex = tenMsAfterR;

                    rSpidsTwoReached = false;
                    tSpidsIndex = 0;
                    for (int i = sSpidsIndex; i < (1.5 / aECG.SampleRate); i++)
                    {

                        //Når aECG.values[i] igen er under baseline, er det eleverede stykke slut
                        if (aECG.Values[i] <= aECG.Baseline && i > sSpidsIndex && !rSpidsTwoReached)
                        {
                            tSpidsIndex = i;
                            rSpidsTwoReached = true;

                        }
                    }
                }
                else
                {
                    for (int i = rSpidsIndex; i < (1.5 / aECG.SampleRate); i++)
                    {
                        if (aECG.Values[i] < RTakThreshhold && rSpidsIndex != 0)
                        {
                            rSpidsReached = true;

                        }
                        if (rSpidsReached && aECG.Values[i] > RTakThreshhold)
                        {
                            rSpidsTwoReached = true;
                        }

                        //Når en value er mindre end baseline og index er større end index for rSpidsIndex(dvs efter den)
                        //bliver sSpidsIndex = i
                        if (aECG.Values[i] < aECG.Baseline && i > rSpidsIndex && !rSpidsTwoReached)
                        {
                            sSpidsIndex = i;
                            rSpidsTwoReached = true;
                        }
                    }
                    rSpidsTwoReached = false;
                    tSpidsIndex = 0;
                    for (int i = sSpidsIndex; i < (1.5 / aECG.SampleRate); i++)
                    {

                        //Hvis en value er større end baseline og større end den tidligere største værdi og
                        //det ligger efter rSpidsIndex bliver tSpidsIndex = i
                        if (aECG.Values[i] > aECG.Baseline && i > sSpidsIndex && !rSpidsTwoReached)
                        {
                            tSpidsIndex = i;
                            rSpidsTwoReached = true;

                        }
                    }
                }



                //Alle values løbes igennem
                for (int i = 0; i < aECG.Values.Count; i++)
                {
                    //Hvis indexet for en value, ligger ml. sSpidsIndex og tSpidsIndex tilføjes de til stSegment
                    if (i >= sSpidsIndex && i <= tSpidsIndex)
                    {
                        STSegmentList.Add(aECG.Values[i]);
                        STSegmentIndexList.Add(i);
                    }

                }
                //Startindexet gemmes i alle aECG's så man ved hvor grafen skal starte for ST-segmentet
                if (STSegmentIndexList.Count != 0)
                {
                    aECG.STStartIndex = STSegmentIndexList[0];

                    aECG.STValues = STSegmentList;
                }
                else
                {
                    aECG.STStartIndex = 0;

                    aECG.STValues = new List<double> { 0, 0 };
                }



                //STSegmentList's længde sammenlignes med Illnesses reference værdier
                //Hvis STSegmentList er for lang, er ST - segmentet deprimeret
                if (STSegmentList.Count > illnessList[2].STMax / aECG.SampleRate)
                {
                    aECG.STDepressed = true;
                }
            }

        }

        public void AddIllnes()
        {
            //Tilføjer Illness til aECG-måling
            //Kaldes af CalculateST()
            foreach (AnalyzedECGModel aECG in NewAECGModelsList)
            {
                if (aECG.STElevated)
                {
                    aECG.Illness = illnessList[1];
                }
                //Hvis ST segmentet er længere end 0,08S er det deprimeret

                else if (aECG.STDepressed)
                {
                    aECG.Illness = illnessList[2];
                }
                else
                    aECG.Illness = illnessList[0];

            }
        }

        public void UploadAnalyzedECG(AnalyzedECGModel aECG)
        {
            lDBRef.UploadAnalyzedECGs(aECG);
            lDBRef.UpdateAnalyzedECG(aECG);

        }

        public void FindNextID()
        {
            AECGIDS = new List<int>();
            aECGList = lDBRef.GetAllAnalyzedECGs();
            NextID = 0;
            //Putter alle ID's ind i aECGIDS
            foreach (AnalyzedECGModel aECG in aECGList)
            {
                AECGIDS.Add(aECG.AECGID);
            }

            //Tager det sidste ID fra DB
            NextID = AECGIDS[AECGIDS.Count - 1];
            NextID++;
        } 
        #endregion
    }
}
