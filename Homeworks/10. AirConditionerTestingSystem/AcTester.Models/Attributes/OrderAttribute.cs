using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcTester.Models.Attributes
{
    public class OrderAttribute : Attribute
    {
        public int Order { get; set; }
    }
}
