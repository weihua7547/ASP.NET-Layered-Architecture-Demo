using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public class LoginException : HttpCustomException
    {
        public LoginException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.Unauthorized, apiStatusCode: ApiStatusCode.UserValidError, message: $"登入失敗({message})", innerException: innerException) { }
    }
}
