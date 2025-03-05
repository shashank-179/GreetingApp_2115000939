using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Interface;
using Model_Layer.Model;
using Repository_Layer.Entity;
using Repository_Layer.Interface;

namespace Business_Layer.Service
{

    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;

        public GreetingBL(IGreetingRL _greetingRL)
        {
            this._greetingRL = _greetingRL;
        }
        public List<GreetingEntity> GetAllGreetings()
        {
            return _greetingRL.GetAllGreetings();
        }
        public GreetingEntity GetGreetingById(int id)
        {
            return _greetingRL.GetGreetingById(id);
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
