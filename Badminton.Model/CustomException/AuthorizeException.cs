using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Badminton.Model.CustomException
{
    public class AuthorizeException : HttpCustomException
    {
        public AuthorizeException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.Forbidden, apiStatusCode: ApiStatusCode.UserAuthorizeError, message: $"授權失敗({message})", innerException: innerException) { }

    }
}
