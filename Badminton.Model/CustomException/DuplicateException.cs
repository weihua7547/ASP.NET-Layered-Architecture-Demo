using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace Badminton.Model.CustomException
{
    public class DuplicateException : HttpCustomException
    {
        public DuplicateException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.InternalServerError, apiStatusCode: ApiStatusCode.UnknowError, message: $"以下內容重覆({message})", innerException: innerException) { }
    }
}
