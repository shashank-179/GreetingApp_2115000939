using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Interface;
using Model_Layer.Model;

namespace Business_Layer.Service
{
    public class GreetingBL : IGreetingBL
    {
        public GreetingBL()
        {

        }
        public string GetGreeting()
        {
            return "Hello World";
        }
        public string PersonalizedGreeting(UserModel userModel)
        {
            if (userModel.FirstName != null && userModel.LastName != null)
            {
                return "Hello " + userModel.FirstName + " " + userModel.LastName;
            }
            else if (userModel.FirstName != null && userModel.LastName == null)
            {
                return "Hello " + userModel.FirstName;
            }
            else if (userModel.FirstName == null && userModel.LastName != null)
            {
                return "Hello " + userModel.LastName;
            }
            else
            {
                return "Hello World";
            }
        }
    }
}
