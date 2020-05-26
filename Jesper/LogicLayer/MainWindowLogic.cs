using DataTier.Models;
using System;
using System.Collections.Generic;
using DataTier.Databaser;
using DataTier.Interfaces;

namespace LogicTier
{
    public class MainWindowLogic
    {
        #region Attributes
        public ILocalDatabase DB;
        private IDOEDB DOEDB;
        #endregion

        #region Properties
        public PatientModel patientRef { get; private set; }
        public AnalyzedECGModel aECGRef { get; private set; }
        public List<AnalyzedECGModel> aECGList { get; private set; }
        public List<PatientModel> patientList { get; private set; }
        #endregion

        #region Ctors
        public MainWindowLogic()
        {
            DB = new Database();
            patientList = new List<PatientModel>();
            aECGList = new List<AnalyzedECGModel>();

            DOEDB = new DOEDB();
            patientList = DB.GetAllPatients();
        }
        #endregion

        #region Methods
        public List<AnalyzedECGModel> GetAECGListForPatient(string cpr)
        {
            //Viser alle analyserede ECG'er på listen for valgt patient
            List<AnalyzedECGModel> analyzedECGList = new List<AnalyzedECGModel>();
            aECGList = DB.GetAllAnalyzedECGs();
            foreach (AnalyzedECGModel aECG in aECGList)
            {
                if (aECG.CPR == cpr)
                {
                    analyzedECGList.Add(aECG);
                }
            }
            return analyzedECGList;
        }


        public void UploadData(string id, string note, AnalyzedECGModel aECG, PatientModel patient)
        {
            //Hvis der er indtastet id uploader den
            if (id != null && note != null)
            {
                DOEDB.UploadMaeling(patient, id, note, aECG.Date);
                DOEDB.UploadData(aECG);
            }
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
            return ecgValuesList;
        }

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
            cpr = cpr.Remove(6);
            DateTime birthday = DateTime.ParseExact(cpr, "ddmmyy", null);
            int result = new DateTime(DateTime.Now.Subtract(birthday).Ticks).Year - 1;
            if (result > 110)
            {
                result = -100;
            }
            return result;
        }

        public bool IsAMan(string cpr)
        {
            bool result = false;

            int tal = Convert.ToInt32(cpr.Remove(0, 10));
            if (tal % 2 != 0)
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
        #endregion

    }
}
