using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository_Layer.Entity;
using RepositoryLayer.Entity;

namespace Repository_Layer.Interface
{
    public interface IGreetingRL
    {
        public UserEntity GetUserByEmail(string email);
        public void AddUser(UserEntity user);
        void SaveGreeting(GreetingEntity greeting);
        GreetingEntity GetGreetingById(int id);
        List<GreetingEntity> GetAllGreetings();
        GreetingEntity UpdateGreeting(int id, string newMessage);
        bool DeleteGreeting(int id);
    }
}
