using System;
using DataTier.Models;

namespace DataTier.Interfaces
{
    public interface IDOEDB
    {
        void UploadData(AnalyzedECGModel analyzedEcg);

        void UploadMaeling(PatientModel patient, string workerID, string note,DateTime date);

    }
}
