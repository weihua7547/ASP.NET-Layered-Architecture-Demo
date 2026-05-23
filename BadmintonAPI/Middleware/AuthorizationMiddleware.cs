using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Badminton.Contract;
using Badminton.Contract.DTO.User;
using Badminton.Contract.JWT;
using Badminton.DataAccess;
using Badminton.Model.JWT;
using Badminton.Model.CustomException;
namespace BadmintonAPI.Middleware
{
    /// <summary>
    /// 使用者授權API權限
    /// </summary>
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HashSet<string> _whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "/api/UserInfo/Login",
            "/api/UserInfo/CreateFamily",
            "/api/UserInfo/SendResetPasswordLink",
            "/api/UserInfo/VerifyResetPasswordToken",
            "/api/UserInfo/ResetPassword",
            "/api/UserInfo/ForgetAccount",
            "/api/UserInfo/SendPhoneSMS",
            "/api/UserInfo/VerifyPhone",
            "/api/UserInfo/RegisterVerify",
            "/api/NurseAides/DownloadDocument",
            "/api/City/GetAll",
            "/api/City/GetDistsByCityId",
            "/api/CashFlow/ECPayResult",
            "/api/Bank/List",
            "/api/Bank/ListBranch",
            "/swagger/index.html",
            "/swagger/v1/swagger.json",
            "/favicon.ico",
            "/api/File/StreamVideo",
            "/api/CareCasePlanQA/Create",
            "/api/CareCasePlanQA/GetAllMessages"
        };

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            if (!_whitelist.Contains(context.Request.Path)&& !context.Request.Path.ToString().Contains("/api/File/StreamVideo"))
            {
                var userInfo = context.Items["UserInfo"] ?? throw new AuthorizeException();

                var userStr = userInfo.ToString();

                if (string.IsNullOrEmpty(userStr)) throw new AuthorizeException();

                var userView = JsonConvert.DeserializeObject<UserInfoSimpleDTO>(userStr) ?? throw new AuthorizeException();

                //var userPermissions = userService.GetAllPermissions(userView.Id);

                //if (userPermissions == null || userPermissions.Count == 0) throw new AuthorizeException();

                //if (!userPermissions.ToList().Exists(x => x == "*" || x == context.Request.Path.ToString())) throw new AuthorizeException();
            }


            await _next(context);
        }
    }
}
