using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public class EntityNotExistException : HttpCustomException
    {
        public EntityNotExistException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.InternalServerError, apiStatusCode: ApiStatusCode.UnknowError, message: $"{message}", innerException: innerException) { }
    }
}
