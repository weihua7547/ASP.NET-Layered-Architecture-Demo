using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Badminton.Model.CustomException
{
    public class NotSupportFileException : HttpCustomException
    {
        public NotSupportFileException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.BadRequest, apiStatusCode: ApiStatusCode.UnknowError, message: $"不允許的檔案類型({message})", innerException: innerException) { }

    }
}
