using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Interface;

namespace Business_Layer.Service
{
    public class GreetingBL: IGreetingBL
    {
        public GreetingBL()
        {
           
        }
        public string GetGreeting()
        {
            return "Hello World";
        }
    }
}
