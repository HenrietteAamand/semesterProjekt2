using DataTier.Models;
using System;
using System.Collections.Generic;

namespace LogicTier
{
    public class MainWindowLogic
    {
		private AnalyzeECG analyzeECG;
		public PatientModel patientRef { get; private set; }
		public AnalyzedECGModel aECGRef { get; private set; }
		public List<AnalyzedECGModel> aECGList { get; private set; }
		public List<PatientModel> patientList { get; private set; }

		public MainWindowLogic()
		{
			analyzeECG = new AnalyzeECG();
		}

		public void LoadPatient(String cpr) {}

		public void UpdatePatientList() {}

		public void UpdateAECGList() {}

		public void UploadData() { }

		public void SelectECG() { }

		public void SelectPatient() { }

	}
}
