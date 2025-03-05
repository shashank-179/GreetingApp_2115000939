using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository_Layer.Interface;
using Repository_Layer.Context;
using Repository_Layer.Entity;

namespace Repository_Layer.Service
{
    public class GreetingRL: IGreetingRL
    {
        private readonly GreetingDbContext _context;

        public GreetingRL(GreetingDbContext context)
        {
            _context = context;
        }

        public void SaveGreeting(GreetingEntity greeting)
        {
            _context.Users.Add(greeting);
            
                _context.SaveChanges();

            
                
            
        }
    }
}
