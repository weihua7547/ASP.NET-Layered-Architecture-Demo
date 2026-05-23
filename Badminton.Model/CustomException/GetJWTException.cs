using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Badminton.Model.CustomException
{
    public class GetJWTException : HttpCustomException
    {
        public GetJWTException(string? message = "", Exception? innerException = null) 
            : base(httpStatusCode: HttpStatusCode.InternalServerError, apiStatusCode: ApiStatusCode.UnknowError, message: $"產生Token 過程發生錯誤({message})", innerException: innerException) { }

    }
}
