using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier.Models;

namespace DataTier.Interfaces
{
    public interface IDOEDB
    {
        void UploadData(AnalyzedECGModel analyzedEcg);
    }
}
