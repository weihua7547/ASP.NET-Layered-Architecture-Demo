using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Contract
{
    public interface ICryptographyService
    {
        public string ComputeSha256Hash(string rawData);
    }
}
