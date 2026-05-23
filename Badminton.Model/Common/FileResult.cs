using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.Common
{
    public class FileResult
    {
        public required byte[] File { get; set; }
        public required string ContentType { get; set; }
        public string? OutputFileName { get; set; }
    }
}
