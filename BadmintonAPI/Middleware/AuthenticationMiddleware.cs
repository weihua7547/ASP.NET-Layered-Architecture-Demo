using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Badminton.Contract.DTO.User;
using Badminton.Contract.JWT;
using Badminton.Model.JWT;
using Badminton.Model.CustomException;

namespace BadmintonAPI.Middleware
{
    /// <summary>
    /// 使用者驗證(是否可操作系統)
    /// </summary>
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private IJWTService _jWTService;
        private JWTConfig _jWTConfig;

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
            "/api/File/StreamVideo",
            "/favicon.ico"
        };

        public AuthenticationMiddleware(RequestDelegate next, IJWTService jWTService, IOptions<JWTConfig> jwtOptions)
        {
            _next = next;
            _jWTService = jWTService;
            _jWTConfig = jwtOptions.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!_whitelist.Contains(context.Request.Path)&& !context.Request.Path.ToString().Contains("/api/File/StreamVideo"))
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token == null)
                {
                    throw new UserVerifyException();
                }

                AttachUserToContext(context, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            // 在這裡解析 JWT 並提取 UserId
            // 這裡的實現取決於您的 JWT 結構和用於解析 JWT 的方法
            var userInfo = GetUserIdFromToken(token);

            // 將 UserInfo 附加到 HttpContext，以便在後續的處理流程中使用
            context.Items["UserInfo"] = userInfo;
        }

        private string GetUserIdFromToken(string token)
        {
            var user = _jWTService.JWTValid(token, _jWTConfig.SignKey, _jWTConfig.Issuer);

            if (string.IsNullOrEmpty(user))
            {
                throw new UserVerifyException();
            }

            return user;
        }
    }
}
