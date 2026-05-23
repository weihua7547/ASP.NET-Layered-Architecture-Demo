using Badminton.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Model
{
    public class Member:Role
    {
        public string CreditCardNumber { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
