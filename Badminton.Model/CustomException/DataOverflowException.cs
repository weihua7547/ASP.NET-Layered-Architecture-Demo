using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public class DataOverflowException : HttpCustomException
    {
        public DataOverflowException(string message, Exception? innerException=null)
            : base(httpStatusCode: HttpStatusCode.InternalServerError, apiStatusCode: ApiStatusCode.UnknowError, message: $"目標實體資料溢位({message})", innerException: innerException) { }
    }
}
