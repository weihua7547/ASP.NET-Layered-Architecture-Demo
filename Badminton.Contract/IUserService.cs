using Badminton.Contract.DTO.User;
using Badminton.Model.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Contract
{
    public interface IUserService
    {
        public int Create(UserCreateDTO param);
        public void Update(UserUpdateDTO param);
        public QueryResultPackage<UserCollectionDTO> GetList();
        public void Delete(int id);

    }
}
