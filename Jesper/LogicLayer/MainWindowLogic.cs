using Models.Models;
using System;
using System.Collections.Generic;
using DataTier.Databaser;
using DataTier.Interfaces;

namespace LogicTier
{
    public class MainWindowLogic
    {
		private AnalyzeECG analyzeECG;
		public PatientModel patientRef { get; private set; }
		public AnalyzedECGModel aECGRef { get; private set; }
		public List<AnalyzedECGModel> aECGList { get; private set; }
		public List<PatientModel> patientList { get; private set; }
		private ILocalDatabase DB;
		private IDOEDB DOEDB;

		public MainWindowLogic()
		{
			analyzeECG = new AnalyzeECG();
			patientList = new List<PatientModel>();
			aECGList = new List<AnalyzedECGModel>();
			DB = new TestDB();
			DOEDB = new DOEDB();
			patientList = DB.GetAllPatients();
			aECGList = DB.GetAllAnalyzedECGs();
		}

		public List<AnalyzedECGModel> GetAECGListForPatient(string cpr)
		{
			List<AnalyzedECGModel> analyzedECGList = new List<AnalyzedECGModel>();
			//Viser alle analyserede ECG'er på listen for valgt patient

			foreach (AnalyzedECGModel aECG in aECGList)
			{
				if (aECG.CPR == cpr)
				{
					analyzedECGList.Add(aECG);
				}
			}

			//Sker når en patient vælges på listen
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
							if (aECG.Illnes != null)
							{
								//Sæt skriftfarven til rød

							}
							//Sæt farven til blå

						}

						if (aECG.Illnes != null)
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

		public void UploadData(string id)
		{
			//Hvis der er indtastet id uploader den
			if (id != null)
			{
				DOEDB.UploadData();
			}

			//Uploader data til DOEDB
			//Sker når der trykkes på "Upload Til Offentlig Database"
			//Kan kun gøres, hvis der er udfyldt et ID
		}

		public AnalyzedECGModel GetAnalyzedECG(int aECGID)
		{
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

		public List<double> GetECGValues(int ecgID)
		{
			List<double> ecgValuesList = new List<double>();

			foreach (AnalyzedECGModel aECG in aECGList)
			{
				if (aECG.ECGID == ecgID)
				{
					ecgValuesList = aECG.Values;
					aECG.IsRead = true;
					//DB.UpdateIsRead(aECG.AECGID);
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
			throw new NotImplementedException();
		}

		public bool IsAMan(string cpr)
		{
			throw new NotImplementedException();
			//bool result;
			//cpr.ToString()
			//if(cpr.ToString())
		}

	}
}
