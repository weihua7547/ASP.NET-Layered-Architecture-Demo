using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public class EntityHasBeenCreatedException :HttpCustomException
    {
        public EntityHasBeenCreatedException(string? message = "", Exception? innerException = null)
        : base(httpStatusCode: HttpStatusCode.InternalServerError, apiStatusCode: ApiStatusCode.UnknowError, message: $"資料已存在({message})", innerException: innerException) { }
    }
}
