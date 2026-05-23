using Badminton.Model;
using Badminton.Model.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonAPI.Handler
{
    public static class ApiResultHandler
    {
        public static ApiResult<object> CreateApiResponse(object? obj)
        {
            return new ApiResult<object>
            {
                Code = ApiStatusCode.Success,
                Message = "操作成功",
                Data = obj
            };
        }

        public static (HttpStatusCode, ApiResult<string>) CreateApiResponse(Exception ex)
        {
            ApiStatusCode code = ApiStatusCode.UnknowError;
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "操作失敗，未知錯誤";

            if (ex is HttpCustomException customException)
            {
                code = customException.ApiStatusCode;
                status = customException.HttpStatusCode;
                message = customException.Message;
            }

            return new(status, new ApiResult<string> { Code = code, Message = message, Data = null });
        }
    }

}
