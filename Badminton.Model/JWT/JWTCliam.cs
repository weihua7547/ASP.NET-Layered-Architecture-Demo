using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.JWT
{
    public class JWTCliam
    {
        /// <summary>
        /// 聲明資訊-發行者
        /// </summary>
        /// <value></value>
        public string Issuer { set; get; } = string.Empty;

        /// <summary>
        /// 聲明資訊-User內容
        /// </summary>
        /// <value></value>
        public string Subject { set; get; } = string.Empty;

        /// <summary>
        /// 聲明資訊-接收者
        /// </summary>
        /// <value></value>
        public string Audience { set; get; } = string.Empty;

        /// <summary>
        /// 聲明資訊-有效期限
        /// </summary>
        /// <value></value>
        public string ExpirationTime { set; get; } = string.Empty;

        /// <summary>
        /// 聲明資訊-起始時間
        /// </summary>
        /// <value></value>
        public string NotBefore { set; get; } = string.Empty;

        /// <summary>
        /// 聲明資訊-發行時間
        /// </summary>
        /// <value></value>
        public string IssuedAt { set; get; } = string.Empty;

        /// <summary>
        /// 聲明資訊-獨立識別ID
        /// </summary>
        /// <value></value>
        public string JwtId { set; get; } = string.Empty;
    }
}
