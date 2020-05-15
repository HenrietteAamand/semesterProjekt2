using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier.Interfaces;
using DataTier;
using System.Data.SqlClient;
using DataTier.Models;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace DataTier.Interfaces
{
    public interface IDOEDB
    {
        void UploadData(AnalyzedECGModel analyzedEcg);

        void UploadMaeling(PatientModel patient, string workerID, string note,DateTime date);

    }
}
