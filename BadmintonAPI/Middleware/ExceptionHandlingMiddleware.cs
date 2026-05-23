using Newtonsoft.Json;
using Badminton.Contract;
using BadmintonAPI.Handler;
using System;

namespace BadmintonApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IErrorLogService _errorLogService;

        public ExceptionHandlingMiddleware(RequestDelegate next, IErrorLogService errorLogService)
        {
            _next = next;
            _errorLogService = errorLogService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Error(ex, JsonConvert.SerializeObject(ex));

                var apiResponse = ApiResultHandler.CreateApiResponse(ex);
                var result = JsonConvert.SerializeObject(apiResponse.Item2);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = apiResponse.Item1.GetHashCode();

                await context.Response.WriteAsync(result);

                // 在這裡添加錯誤記錄邏輯
                await _errorLogService.SaveLog(ex);
            }
        }
    }
}
