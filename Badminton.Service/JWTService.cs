using Microsoft.IdentityModel.Tokens;
using Badminton.Contract.JWT;
using Badminton.Model.JWT;
using Badminton.Model.CustomException;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Service
{
    public class JWTService : IJWTService
    {
        public string GetJWT(JWTCliam jWTCliam, string secretKey, string issuer, int expireMinutes = 30)
        {
            try
            {
                #region Step 1. 取得資訊聲明(claims)集合

                List<Claim> claims = GenCliams(jWTCliam);

                #endregion

                #region  Step 2. 建置資訊聲明(claims)物件實體，依據上面步驟產生Data來做

                ClaimsIdentity userClaimsIdentity = new ClaimsIdentity(claims);

                #endregion

                #region Step 3. 建立Token加密用金鑰

                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                #endregion

                #region Step 4. 建立簽章，依據金鑰

                // 使用HmacSha256進行加密
                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                #endregion

                #region  Step 5. 建立Token內容實體

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = issuer, // 設置發行者資訊
                    Audience = issuer, // 設置驗證發行者對象，如果需要驗證Token發行者，需要設定此項目
                    NotBefore = DateTime.Now, // 設置可用時間， 預設值就是 DateTime.Now
                    IssuedAt = DateTime.Now, // 設置發行時間，預設值就是 DateTime.Now
                    Subject = userClaimsIdentity, // Token 針對User資訊內容物件
                    Expires = DateTime.Now.AddMinutes(expireMinutes), // 建立Token有效期限
                    SigningCredentials = signingCredentials // Token簽章
                };

                #endregion

                #region Step 6. 產生JWT Token並轉換成字串

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler(); // 建立一個JWT Token處理容器
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);  // 將Token內容實體放入JWT Token處理容器
                string serializeToken = tokenHandler.WriteToken(securityToken); // 最後將JWT Token處理容器序列化，這一個就是最後會需要的Token 字串

                #endregion

                return serializeToken;
            }
            catch (Exception ex)
            {
                throw new GetJWTException(innerException: ex);
            }
        }

        public string? JWTValid(string token, string secretKey, string issuer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey); // secretKey 應該從配置或安全存儲中獲取

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = false, // 根據您的需求設置
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // 可以設置時間偏差容忍度
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value; // 替換為實際的 claim 類型

                // 將 UserId 附加到 HttpContext，以便在後續的處理流程中使用
                return userId;
            }
            catch
            {
                return null;
            }
        }

        private List<Claim> GenCliams(JWTCliam jWTCliam)
        {
            List<Claim> claims = new List<Claim>();

            // (audience)
            // 設定Token接受者，用在驗證接收者驗證是否相符
            if (!string.IsNullOrEmpty(jWTCliam.Audience))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, jWTCliam.Audience));
            }

            // (expiration time)
            // Token過期時間，一但超過這時間此Token就失效
            if (!string.IsNullOrEmpty(jWTCliam.ExpirationTime))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Exp, jWTCliam.ExpirationTime));
            }

            //  (issued at time)
            // Token發行時間，用在後面檢查Token發行多久
            if (!string.IsNullOrEmpty(jWTCliam.IssuedAt))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Iat, jWTCliam.IssuedAt));
            }

            // (issuer)
            // 發行者資訊
            if (!string.IsNullOrEmpty(jWTCliam.Issuer))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Iss, jWTCliam.Issuer));
            }

            // (JWT ID)
            // Token ID，避免Token重複在被套用
            if (!string.IsNullOrEmpty(jWTCliam.JwtId))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, jWTCliam.JwtId));
            }

            // (not before time)
            // Token有效起始時間，用來驗證Token可用時間
            if (!string.IsNullOrEmpty(jWTCliam.NotBefore))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, jWTCliam.NotBefore));
            }

            // (subject)
            // Token 主題，放置該User內容
            if (!string.IsNullOrEmpty(jWTCliam.Subject))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, jWTCliam.Subject));
            }

            return claims;
        }
    }
}
