﻿using System;
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
        public GreetingEntity UpdateGreeting(int id, string newMessage)
        {
            var greeting = _context.Users.Find(id);
            if (greeting != null)
            {
                greeting.Message = newMessage;
                _context.SaveChanges();
            }
            return greeting;
        }

        public void SaveGreeting(GreetingEntity greeting)
        {
            _context.Users.Add(greeting);

            _context.SaveChanges();
        }
        public GreetingEntity GetGreetingById(int id)
        {
            return _context.Users.Find(id);
        }
        public List<GreetingEntity> GetAllGreetings()
        {
            return _context.Users.ToList();
        }
        public bool DeleteGreeting(int id)
        {
            var greeting = _context.Users.Find(id);
            if (greeting != null)
            {
                _context.Users.Remove(greeting);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
