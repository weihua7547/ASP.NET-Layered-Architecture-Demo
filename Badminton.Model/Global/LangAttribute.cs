using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.Global
{
    public class LangAttribute:Attribute
    {
        public string Lang { get; private set; }
        public LangAttribute(string lang)
        {
            Lang = lang;
        }
    }
}
