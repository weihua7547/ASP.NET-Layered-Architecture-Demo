using System;
using System.Collections.Generic;
using System.Text;
using Badminton.Contract;
using Badminton.Contract.DTO.Field;
using Badminton.DataAccess;
using Badminton.Model;
using Badminton.Model.Global;
using Microsoft.Extensions.Configuration;
namespace Badminton.Service
{
    public class FieldService : BaseService<Field>, IFieldService
    {
        public FieldService(IDataGenericService<Field> baseDataChange, BadmintonDbContext dbContext, IUserContext userContext, IUserService userInfoService, IConfiguration configuration) : base(baseDataChange, dbContext, userContext, userInfoService, configuration)
        {
        }

        public int Create(FieldCreateDTO param)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public QueryResultPackage<FieldCollectionDTO> GetList()
        {
            throw new NotImplementedException();
        }

        void IFieldService.Update(FieldUpdateDTO param)
        {
            throw new NotImplementedException();
        }
    }
}
