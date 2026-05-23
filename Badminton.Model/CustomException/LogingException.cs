using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public class LogingException : HttpCustomException
    {
        public LogingException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.InternalServerError, apiStatusCode: ApiStatusCode.UnknowError, message: $"紀錄LOG時發生錯誤({message})", innerException: innerException) { }

    }
}
