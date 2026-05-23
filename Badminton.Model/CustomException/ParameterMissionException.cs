using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.CustomException
{
    public class ParameterMissionException : HttpCustomException
    {
        public ParameterMissionException( string? message = "", Exception? innerException = null)
            : base(httpStatusCode: HttpStatusCode.BadRequest, apiStatusCode: ApiStatusCode.ParameterMission, message: $"傳入參數錯誤({message})", innerException: innerException)
        {
        }

    }
}
