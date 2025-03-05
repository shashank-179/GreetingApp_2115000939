using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_Layer.Model;

namespace Business_Layer.Interface
{
    public interface IGreetingBL
    {
        public string GetGreeting();
        public string PersonalizedGreeting(UserModel userModel);
    }
}
