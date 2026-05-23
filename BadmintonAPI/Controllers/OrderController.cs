using Badminton.Contract;
using Badminton.Contract.DTO.Order;
using Badminton.Model.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadmintonAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public QueryResultPackage<OrderCollectionDTO> GetList()
        {
            return _orderService.GetList();
        }
        [HttpPost]
        public int Create(OrderCreateDTO param)
        {
            return _orderService.Create(param);
        }
        [HttpPost]
        public void Update(OrderUpdateDTO param)
        {
            _orderService.Update(param);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            _orderService.Delete(id);
        }
    }
}
