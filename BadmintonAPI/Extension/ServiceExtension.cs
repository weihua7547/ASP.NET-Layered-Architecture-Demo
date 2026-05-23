using Badminton.Contract;
using Badminton.Contract.JWT;
using Badminton.Model;
using Badminton.Model.CustomException;
using Badminton.Model.JWT;
using Badminton.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using System.Reflection;
using System.Text;

namespace BadmintonAPI.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IDataGenericService<>), typeof(DataGenericService<>));
            services.Configure<SMTPConfig>(configuration.GetSection("SMTPConfig"));
            services.AddSingleton<IPasswordService, PasswordService>();
            return services;

        }
        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJWTService, JWTService>();

            services.Configure<JWTConfig>(configuration.GetSection("JWTConfig"));

            string? iss = configuration.GetSection("JWTConfig").GetValue<string>("Issuer");
            string? key = configuration.GetSection("JWTConfig").GetValue<string>("SignKey");

            if (string.IsNullOrEmpty(iss) || string.IsNullOrEmpty(key))
            {
                throw new GetJWTException();
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        #region  配置驗證發行者

                        ValidateIssuer = true, // 是否要啟用驗證發行者
                        ValidIssuer = iss,

                        #endregion

                        #region 配置驗證接收方

                        ValidateAudience = false, // 是否要啟用驗證接收者
                        // ValidAudience = "" // 如果不需要驗證接收者可以註解

                        #endregion

                        #region 配置驗證Token有效期間

                        ValidateLifetime = true, // 是否要啟用驗證有效時間

                        #endregion

                        #region 配置驗證金鑰

                        ValidateIssuerSigningKey = false, // 是否要啟用驗證金鑰，一般不需要去驗證，因為通常Token內只會有簽章

                        #endregion

                        #region 配置簽章驗證用金鑰

                        // 這裡配置是用來解Http Request內Token加密
                        // 如果Secret Key跟當初建立Token所使用的Secret Key不一樣的話會導致驗證失敗
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

                        #endregion
                    };
                });

            return services;
        }
    }
}