using Badminton.Contract;
using Badminton.Contract.DTO.User;
using Badminton.DataAccess;
using Badminton.Model;
using Badminton.Model.Global;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IDataGenericService<User> baseDataChange, BadmintonDbContext dbContext, IUserContext userContext, IUserService userInfoService, IConfiguration configuration) : base(baseDataChange, dbContext, userContext, userInfoService, configuration)
        {
        }

        public int Create(UserCreateDTO param)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public QueryResultPackage<UserCollectionDTO> GetList()
        {
            throw new NotImplementedException();
        }

        public void Update(UserUpdateDTO param)
        {
            throw new NotImplementedException();
        }
    }
}
