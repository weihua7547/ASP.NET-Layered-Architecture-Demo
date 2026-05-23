using Badminton.Contract;
using Badminton.Contract.DTO.Order;
using Badminton.DataAccess;
using Badminton.Model;
using Badminton.Model.Global;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Service
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService(IDataGenericService<Order> baseDataChange, BadmintonDbContext dbContext, IUserContext userContext, IUserService userInfoService, IConfiguration configuration) : base(baseDataChange, dbContext, userContext, userInfoService, configuration)
        {
        }

        public int Create(OrderCreateDTO param)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public QueryResultPackage<OrderCollectionDTO> GetList()
        {
            throw new NotImplementedException();
        }

        public void Update(OrderUpdateDTO param)
        {
            throw new NotImplementedException();
        }
    }
}
