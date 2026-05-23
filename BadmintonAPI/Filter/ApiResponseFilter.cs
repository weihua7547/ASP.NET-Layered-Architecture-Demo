using Badminton.Model;
using Badminton.Model.CustomException;
using Badminton.Model.Global;
using BadmintonAPI.Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BadmintonAPI.Filter
{
    public class ApiResponseFilter:IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is Microsoft.AspNetCore.Mvc.FileResult)
            {
                await next();
                return;
            }

            var objectResult = context.Result as ObjectResult;

            if (objectResult?.Value is IApiResult)
            {
                await next();
                return;
            }


            if (objectResult != null)
            {
                if (objectResult.StatusCode == 400)
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .Select(x => new { Field = x.Key, Error = x.Value?.Errors.First().ErrorMessage })
                        .ToList();

                    var error = ApiResultHandler.CreateApiResponse(new ParameterMissionException(string.Join(",", errors.Select(x => $"{x.Field}-{x.Error}"))));

                    var apiResult = new ApiResult<object> { Code = error.Item2.Code, Message = error.Item2.Message };

                    context.Result = new ObjectResult(apiResult)
                    {
                        StatusCode = error.Item1.GetHashCode()
                    };

                    await next();

                    return;
                }
            }

            {
                var apiResult = ApiResultHandler.CreateApiResponse(objectResult?.Value);

                context.Result = new ObjectResult(apiResult)
                {
                    StatusCode = objectResult?.StatusCode
                };

                await next();
            }


        }
    }
}
