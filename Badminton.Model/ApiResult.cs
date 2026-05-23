using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model
{
    public interface IApiResult
    {
        ApiStatusCode Code { get; set; }
        string Message { get; set; }
    }

    public class ApiResult<T> : IApiResult
    {
        public required ApiStatusCode Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
