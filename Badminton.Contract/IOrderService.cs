using Badminton.Contract.DTO.Order;
using Badminton.Model.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Contract
{
    public interface IOrderService
    {
        public int Create(OrderCreateDTO param);
        public void Update(OrderUpdateDTO param);
        public QueryResultPackage<OrderCollectionDTO> GetList();
        public void Delete(int id);
    }
}
