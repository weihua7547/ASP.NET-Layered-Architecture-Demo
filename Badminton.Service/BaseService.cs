using Badminton.Model.Abstract;
using Badminton.Contract;
using Badminton.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Service
{
    public abstract class BaseService<T> where T : Entity
    {
        protected readonly BadmintonDbContext _dbContext;
        protected readonly IUserContext _userContext;
        protected readonly IUserService _userInfoService;
        protected readonly IConfiguration _configuration;
        protected readonly IDataGenericService<T> _baseDataChange;
        protected BaseService(IDataGenericService<T> baseDataChange, BadmintonDbContext dbContext, IUserContext userContext, IUserService userInfoService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _userInfoService = userInfoService;
            _configuration = configuration;
            _baseDataChange = baseDataChange;
        }
    }
}
