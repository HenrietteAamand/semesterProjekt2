using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Text;
using LiveCharts;
using LiveCharts.Definitions.Charts;

namespace LogicTier
{
    class AnalyzeECG
    {
        private List<IllnessModel> illnessList;
        private PatientModel patientRef;

        public TimeSpan IntervalIR { get; private set; }
        public double Baseline { get; private set; }
        public List<double> STSegmentList { get; private set;}
        public List<int> STSegmentIndexList { get; private set; }
        public double STSegmentTreshhold { get; private set; }
        public bool STSegmentElevated { get; private set; }
        public bool STSegmentDepressed { get; private set; }
        public SeriesCollection AECGCollection { get; private set; }
        


        public AnalyzeECG()
        {
            illnessList = new List<IllnessModel>();
            patientRef = new PatientModel();
        }

        public List<ECGModel> LoadNewECGs(PatientModel patient) { }

        public void CreateAnalyzedECG(int ECGID, List<IllnessModel> illnes, CartesianChart<Sereiscollection> AECGChart, int pulse) { }

        public void CalculateIntervalIR() { }

        public void CalculateBaseline() { }

        public void CalculateST() { }

        public void AddIllnes() { }

        public void CreateAECGChart() { }

        public void CreateMark(List<double> STSegmentList, List<int> STSegmentIndexList) { }

        public void addMark() { }

        public void UploadAnalyzedECG() { }
    }
}
