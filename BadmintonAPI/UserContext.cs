using Newtonsoft.Json;
using Badminton.Contract;
using Badminton.Contract.DTO.User;
using System.Security.Claims;

namespace BadmintonAPI
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                var userStr =
                    _httpContextAccessor
                    .HttpContext?
                    .Items["UserInfo"]
                    ?.ToString();

                if (string.IsNullOrWhiteSpace(userStr))
                    return null;

                var user =
                    JsonConvert
                    .DeserializeObject<UserInfoSimpleDTO>(
                        userStr);

                return user?.Id;
            }
        }

        public string? Platform =>
            _httpContextAccessor
                .HttpContext?
                .Request?
                .Headers["Platform"]
                .FirstOrDefault();

        public string? RoleType =>
            _httpContextAccessor
                .HttpContext?
                .Request?
                .Headers["RoleType"]
                .FirstOrDefault();
    }
}
