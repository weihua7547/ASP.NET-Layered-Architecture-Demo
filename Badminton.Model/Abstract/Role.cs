using Badminton.Model.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Model.Abstract
{
    public abstract class Role : Entity
    {
        public RoleType RoleType { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
    public enum RoleType
    {
        [Lang("系統管理員")]
        SystemManager = 0,
        [Lang("使用者")]
        User = 1,
    }
}
