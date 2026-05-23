using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model
{
    public enum ApiStatusCode
    {
        Success = 0,
        UnknowError = 1,
        SqlError = 2,
        UserValidError = 3,
        UserAuthorizeError = 4,
        ParameterMission = 5
    }
}
