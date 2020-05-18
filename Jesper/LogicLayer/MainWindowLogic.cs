using DataTier.Models;
using System;
using System.Collections.Generic;
using DataTier.Databaser;
using DataTier.Interfaces;

namespace LogicTier
{
    public class MainWindowLogic
    {
		public PatientModel patientRef { get; private set; }
		public AnalyzedECGModel aECGRef { get; private set; }
		public List<AnalyzedECGModel> aECGList { get; private set; }
		public List<PatientModel> patientList { get; private set; }
		public ILocalDatabase DB;
		private IDOEDB DOEDB;

		public MainWindowLogic()
		{
			DB = new Database();
			patientList = new List<PatientModel>();
			aECGList = new List<AnalyzedECGModel>();
			
			DOEDB = new DOEDB();
			patientList = DB.GetAllPatients();
			//GetAge("010156-7890");
			//IsAMan("010156-7890");
		}

		public List<AnalyzedECGModel> GetAECGListForPatient(string cpr)
		{
			List<AnalyzedECGModel> analyzedECGList = new List<AnalyzedECGModel>();
			//Viser alle analyserede ECG'er på listen for valgt patient
			aECGList = DB.GetAllAnalyzedECGs();
			foreach (AnalyzedECGModel aECG in aECGList)
			{
				if (aECG.CPR == cpr)
				{
					analyzedECGList.Add(aECG);
				}
			}
			//Sker når en patient vælges på listen
				//TEST DATA
					//AnalyzedECGModel aECG1 = new AnalyzedECGModel("010156-7890", 0, 0, (new DateTime(2011, 2, 1)), 50,
					//	(new List<double> { 1, 1, 1, 2, 1, 2.5, 8, 0, 1, 3, 3, 3.5, 3, 3 }),1);
					//AnalyzedECGModel aECG2 = new AnalyzedECGModel("010156-7890", 1, 1, (new DateTime(2011, 1, 1)), 50,
					//	(new List<double> { 4, 4, 4, 5, 4, 3.5, 9, 1, 2, 4, 4, 4.5, 4, 4 }), 1);
					//aECG1.STValues = new List<double>{4,4.5,3,2 };
					//aECG2.STValues = new List<double> { 3, 3.5, 2, 1 };
					//aECG1.STStartIndex = 5;
					//aECG2.STStartIndex = 7;
					//aECG1.Baseline = 3;
					//aECG1.Illnes = new IllnessModel(1, "st", "not good", 2, 4, false, false);
					//aECG1.IsAnalyzed = true;
					//aECG1.IsRead = true;
					//aECG1.STElevated = true;
					//aECG1.STDepressed = true;
					//aECG1.Pulse = 50;
					//aECG2.Baseline = 3;
					//aECG2.Illnes = new IllnessModel(1, "st", "not good", 2, 4, false, false);
					//aECG2.IsAnalyzed = true;
					//aECG2.IsRead = true;
					//aECG2.STElevated = true;
					//aECG2.STDepressed = true;
					//aECG2.Pulse = 50;
					//analyzedECGList.Add(aECG1);
					//analyzedECGList.Add(aECG2);
					//aECGList = analyzedECGList;
			return analyzedECGList;
		}

		public void UpdatePatientList()
		{
			//Sætter attributter, som fortæller at der er nye målinger og/eller nye syge målinger for alle patienter.
			foreach (PatientModel patient in patientList)
			{
				foreach (AnalyzedECGModel aECG in aECGList)
				{
					if (aECG.CPR == patient.CPR)
					{
						if (!aECG.IsRead)
						{
							if (aECG.Illness != null)
							{
								//Sæt skriftfarven til rød

							}
							//Sæt farven til blå

						}

						if (aECG.Illness != null)
						{
							//Sæt skriftfarven til rød
						}
					}
				}
			}


			//Sker når der opdateres
		}

		//public void UpdateAECGList()
		//{
		//	//Sætter alle nye målinger ind på patientlisten
		//	//Sker når der opdateres
		//}

		public void UploadData(string id, string note, AnalyzedECGModel aECG, PatientModel patient)
		{
			//Hvis der er indtastet id uploader den
			if (id != null && note != null)
			{				
				DOEDB.UploadMaeling(patient, id, note, aECG.Date);
				DOEDB.UploadData(aECG);
			}

			//Uploader data til DOEDB
			//Sker når der trykkes på "Upload Til Offentlig Database"
			//Kan kun gøres, hvis der er udfyldt et ID
		}

		public AnalyzedECGModel GetAnalyzedECG(int aECGID)
		{
			aECGList = DB.GetAllAnalyzedECGs();
			AnalyzedECGModel result = new AnalyzedECGModel();
			foreach (AnalyzedECGModel aECG in aECGList)
			{
				if (aECG.ECGID == aECGID)
				{
					result = aECG;
				}
			}

			//Sætter også isRead = true
			//Sker når der vælges en ECG på listen
			return result;
		}

		public List<double> GetECGValues(int AECGID)
		{
			List<double> ecgValuesList = new List<double>();

			foreach (AnalyzedECGModel aECG in aECGList)
			{
				if (aECG.AECGID == AECGID)
				{
					ecgValuesList = aECG.Values;
					aECG.IsRead = true;
					DB.UpdateAnalyzedECG(aECG);
				}
			}

			//Sætter også isRead = true
			//Sker når der vælges en ECG på listen
			return ecgValuesList;
		}


		//Metode til ST-values
		public List<double> GetSTValues(int ecgID)
		{
			List<double> stValuesList = new List<double>();

			foreach (AnalyzedECGModel aECG in aECGList)
			{
				if (aECG.ECGID == ecgID)
				{
					stValuesList = aECG.STValues;
				}
			}

			//Sætter også isRead = true
			//Sker når der vælges en ECG på listen
			return stValuesList;
		}

		public int GetSTStartIndex(int ecgID)
		{
			int stStartIndex = 0;

			foreach (AnalyzedECGModel aECG in aECGList)
			{

				if (aECG.ECGID == ecgID)
				{
					stStartIndex = aECG.STStartIndex;
				}
			}
			return stStartIndex;
		}



		public PatientModel GetPatient(string cpr)
		{
			PatientModel patient = new PatientModel();

			foreach (PatientModel pa in patientList)
			{
				if (pa.CPR == cpr)
				{
					patient = pa;
				}

			}

			return patient;
		}

		public int GetAge(string cpr)
		{
			cpr = cpr.Replace("-", "");
			cpr= cpr.Remove(6);
			DateTime birthday = DateTime.ParseExact(cpr, "ddmmyy", null);
			int result = new DateTime(DateTime.Now.Subtract(birthday).Ticks).Year - 1;
			if (result>110)
			{
				result =- 100;
			}
			return result;
		}

		public bool IsAMan(string cpr)
		{
			bool result = false;

			int tal = Convert.ToInt32(cpr.Remove(0, 10));
			if (tal%2!=0)
			{
				result = true;
			}
			return result;
		}

		public List<PatientModel> getAllPatiens()
		{
			//Henter alle Patient til liste
			List<PatientModel> patientList = new List<PatientModel>();
			patientList = DB.GetAllPatients();
			return patientList;
		}

	}
}
