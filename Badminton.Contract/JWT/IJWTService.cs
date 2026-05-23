using Badminton.Model.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Contract.JWT
{
    public interface IJWTService
    {
        /// <summary>
        ///  產生JWT Token
        /// </summary>
        /// <param name="jWTCliam">Token 資訊聲明內容物件</param>
        /// <param name="secretKey">加密金鑰，用來做加密簽章用</param>
        /// <param name="issur">Token 發行者資訊</param>
        /// <param name="expireMinutes">Token 有效期限(分鐘)</param>
        /// <returns>回應內容物件，內容屬性jwt放置Token字串</returns>
        public string GetJWT(JWTCliam jWTCliam, string secretKey, string issuer, int expireMinutes = 30);

        public string? JWTValid(string token, string secretKey, string issuer);
    }
}
