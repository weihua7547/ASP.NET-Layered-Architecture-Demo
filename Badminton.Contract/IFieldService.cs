using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Badminton.Contract.DTO.Field;
using Badminton.Model.Global;
namespace Badminton.Contract
{
    public interface IFieldService
    {
        public int Create(FieldCreateDTO param);
        public void Update(FieldUpdateDTO param);
        public QueryResultPackage<FieldCollectionDTO> GetList();
        public void Delete(int id);
    }
}
