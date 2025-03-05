using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository_Layer.Entity;

namespace Repository_Layer.Interface
{
    public interface IGreetingRL
    {
        void SaveGreeting(GreetingEntity greeting);
        GreetingEntity GetGreetingById(int id);
    }
}
