using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.Common
{
    public class LargeFileDataChunk
    {
        public string Id { get; set; }
        public required string fileName { get; set; }
        public required IFormFile Video { get; set; }
    }
}
