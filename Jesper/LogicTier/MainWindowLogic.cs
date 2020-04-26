using DataTier.Models;
using System;
using System.Collections.Generic;

namespace LogicTier
{
    public class MainWindowLogic
    {
		public PatientModel patientRef { get; private set; }
		public AnalyzedECGModel aECGRef { get; private set; }
		public List<AnalyzedECGModel> aECGList { get; private set; }

		public MainWindowLogic()
		{

		}

	}
}
