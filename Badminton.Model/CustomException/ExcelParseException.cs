using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace Badminton.Model.CustomException
{
    public class ExcelParseException : HttpCustomException
    {
        public ExcelParseException(string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.InternalServerError, apiStatusCode: ApiStatusCode.UnknowError, message: $"Excel解析失敗({message})", innerException: innerException) { }
    }
}
