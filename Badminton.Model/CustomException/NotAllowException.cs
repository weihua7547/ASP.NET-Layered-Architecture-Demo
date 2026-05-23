using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public class NotAllowException : HttpCustomException
    {
        public NotAllowException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.BadRequest, apiStatusCode: ApiStatusCode.UnknowError, message: $"不允許的操作({message})", innerException: innerException) { }

    }
}
