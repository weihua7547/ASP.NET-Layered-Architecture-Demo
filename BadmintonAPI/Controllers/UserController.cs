using Badminton.Contract;
using Badminton.Contract.DTO.User;
using Badminton.Model.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadmintonAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public QueryResultPackage<UserCollectionDTO> GetList()
        {
            return _userService.GetList();
        }
        [HttpPost]
        public int Create(UserCreateDTO param)
        {
            var id = _userService.Create(param);
            return id;
        }
        [HttpPost]
        public void Update(UserUpdateDTO param)
        {
            _userService.Update(param);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            _userService.Delete(id);
        }
    }
}
