using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public abstract class HttpCustomException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public ApiStatusCode ApiStatusCode { get; private set; }

        public HttpCustomException(HttpStatusCode httpStatusCode, ApiStatusCode apiStatusCode, string message, Exception? innerException)
            : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
            ApiStatusCode = apiStatusCode;
        }
    }
}
