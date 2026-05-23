using NPOI.SS.Formula.Functions;
using Badminton.Contract;
using Badminton.Model.CustomException;

namespace BadmintonAPI.Middleware
{
    public class HttpHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private IApiAccessLogService _apiAccessLogService;

        public HttpHandleMiddleware(RequestDelegate next, IApiAccessLogService apiAccessLogService)
        {
            _next = next;
            _apiAccessLogService = apiAccessLogService;
        }

        public async Task InvokeAsync(HttpContext context)
        {            
            var id = await _apiAccessLogService.SaveHttpRequest();

            if (!id.HasValue) throw new LogingException("對Http請求進行紀錄時發生錯誤");

            // Record response
            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            // 將請求傳遞到下一個中介軟體
            await _next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
            responseBodyStream.Seek(0, SeekOrigin.Begin);

            await _apiAccessLogService.SaveHttpResponse(id.Value, responseBody);
            await responseBodyStream.CopyToAsync(originalBodyStream);

        }
    }
}
