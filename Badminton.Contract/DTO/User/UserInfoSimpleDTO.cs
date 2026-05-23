using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Contract.DTO.User
{
    public class UserInfoSimpleDTO
    {
        public required int Id { get; set; }
        public required string UserName { get; set; }

        public string? PersonnelName { get; set; }
    }
}
