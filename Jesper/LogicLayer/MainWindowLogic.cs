using Models.Models;
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

		//public void LoadPatient(String cpr)
		//{
		//	//Henter alt data fra listen af patienter, for den valgte patient.
		//	//Sker når der trykkes på en patient
			
		//}

		public void SelectPatient(string cpr)
		{
			//Viser alle analyserede ECG'er på listen for valgt patient
			//Sker når en patient vælges på listen
		}

		public void UpdatePatientList() 
		{
			//Sætter attributter, som fortæller at der er nye målinger og/eller nye syge målinger for alle patienter.
			//Sker når der opdateres
		}

		public void UpdateAECGList()
		{
			//Sætter alle nye målinger ind på patientlisten
			//Sker når der opdateres
		}

		public void UploadData()
		{ 
			//Uploader data til DOEDB
			//Sker når der trykkes på "Upload Til Offentlig Database"
			//Kan kun gøres, hvis der er udfyldt et ID
		}

		public void SelectECG()
		{
			//Viser graf for det valgte ECG
			//Sætter også isRead = true
			//Sker når der vælges en ECG på listen
		}
	}
}
