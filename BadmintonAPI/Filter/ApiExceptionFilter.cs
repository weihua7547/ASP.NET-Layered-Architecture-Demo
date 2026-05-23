using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Badminton.Contract;
using BadmintonAPI.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BadmintonAPI.Filter
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private IErrorLogService _errorLogService;

        public ApiExceptionFilter(IErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        public void OnException(ExceptionContext context)
        {
            var result = ApiResultHandler.CreateApiResponse(context.Exception);

            context.Result = new ObjectResult(result.Item2)
            {
                StatusCode = result.Item1.GetHashCode() // 或者其他適合的狀態碼
            };

            context.ExceptionHandled = true; // 標記異常已被處理

            _errorLogService.SaveLog(context.Exception);
        }
    }
}
