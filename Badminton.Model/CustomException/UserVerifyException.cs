using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public class UserVerifyException : HttpCustomException
    {
        public UserVerifyException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.Unauthorized, apiStatusCode: ApiStatusCode.UserValidError, message: $"使用者驗證失敗({message})", innerException: innerException) { }

    }
}
