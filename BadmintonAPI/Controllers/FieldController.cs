using Badminton.Contract;
using Badminton.Contract.DTO.Field;
using Badminton.Model.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BadmintonAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private IFieldService _fieldService;
        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }
        [HttpGet]
        public QueryResultPackage<FieldCollectionDTO> GetList()
        {
            return _fieldService.GetList();
        }
        [HttpPost]
        public int Create(FieldCreateDTO param)
        {
            return _fieldService.Create(param);
        }
        [HttpPost]
        public void Update(FieldUpdateDTO param)
        {
            _fieldService.Update(param);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            _fieldService.Delete(id);
        }
    }
}
