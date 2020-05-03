using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Text;
using DataTier.Databaser;
using DataTier.Interfaces;

namespace LogicTier
{
    class AnalyzeECG
    {
        private List<IllnessModel> illnessList;
        private PatientModel patientRef;
        private List<ECGModel> ecgList;

        private ILocalDatabase lDBRef;

        public TimeSpan IntervalIR { get; private set; }
        public double Baseline { get; private set; }
        public List<double> STSegmentList { get; private set;}
        public List<int> STSegmentIndexList { get; private set; }
        public double STSegmentTreshhold { get; private set; }
        public bool STSegmentElevated { get; private set; }
        public bool STSegmentDepressed { get; private set; }
        public List<double> AECGCollection { get; private set; }
        


        public AnalyzeECG()
        {
            illnessList = new List<IllnessModel>();
            patientRef = new PatientModel();
            lDBRef = new Database();
            ecgList = new List<ECGModel>();
        }

        public List<ECGModel> LoadNewECGs(PatientModel patient) { throw new NotImplementedException(); }

        public void CreateAnalyzedECG(int ecgID, List<IllnessModel> illnes, List<double> aECGChart, int pulse)
        {
            //Der kommer en liste med ECG'er som er nye.
            //Hvis der vælges et ECG har den et tilknyttet cpr.
            //Det CPR
            //Henter ikke analyseret ecg
            ecgList = lDBRef.GetAllECGs(ecgList[ecgID].CPR);
            //Beregner udvalgte parametre
            //Sammenligner parametre med Illnesses
            // ST-segment og Baseline til charts



        }

        //public void CalculateIntervalR() { //Skal bruges til at lave puls, hvis det skal implementeres }

        public void CalculateBaseline()
        {
            //Opdel ECG værdier i intervaller
            //Placer alle mælte værdier i et af intervallerne
            //Tæller hvilket interval, der har flest målte værdier
            //Sæt baseline til, at være i midten af intervallet.
            //Evt. kan laves histogram inde i intervallet
        }

        public List<double> CalculateST()
        {
            //Måle R-tak
            //Måle tiden fra R-tak til første målte værdi under baseline
            //Hvis den tid er for høj/lang er der STEMI
                //Kalder AddIllness()
            //Måler tiden fra første værdi under baseline, til første værdi over baseline igen
            //Hvis den tid er for lang så er der NonStemi.
                //Kalder AddIllnes
            //Kig på powerpoint for intro slide 19.

            //laver liste med værdier for ST-segment
            //Listen indeholder værdier fra den laveste værdi efter R-takken til den rammer en værdi der er højere end baseline.
            throw new NotImplementedException();
        }

        public void AddIllnes()
        {
            //Tilføjer Illness til aECG-måling
            //Kaldes af CalculateST()
        }

        public void CreateAECGChart()
        {
            //Laver lister med værdier for ECG
        }

        //public void CreateMark(List<double> STSegmentList, List<int> STSegmentIndexList) { }

        //public void addMark()
        //{
            
        //}

        public void UploadAnalyzedECG()
        {
            lDBRef.UpdateAnalyzedECGs();
        }
    }
}
