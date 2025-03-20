﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository_Layer.Interface;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace Repository_Layer.Service
{
    public class GreetingRL: IGreetingRL
    {
        private readonly GreetingDbContext _context;
        private readonly UserContext userContext;
        public GreetingRL(GreetingDbContext context, UserContext userContext)    
        {
            _context = context;
            this.userContext = userContext;
        }
        public void SavePasswordResetToken(int userId, string token)
        {
            var user = userContext.UserDetails.Find(userId);
            if (user != null)
            {
                user.ResetToken = token;
                user.TokenExpiry = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour
                userContext.SaveChanges();
            }
        }

        public UserEntity GetUserByResetToken(string token)
        {
            return userContext.UserDetails.FirstOrDefault(u => u.ResetToken == token && u.TokenExpiry > DateTime.UtcNow);
        }

        public void UpdatePassword(int userId, string newPassword)
        {
            var user = userContext.UserDetails.Find(userId);
            if (user != null)
            {
                user.PasswordHash = PasswordHashing.HashPassword(newPassword);
                user.ResetToken = null; // Invalidate token after reset
                user.TokenExpiry = null;
                userContext.SaveChanges();
            }
        }

        public UserEntity GetUserByEmail(string email)
        {
            return userContext.Set<UserEntity>().FirstOrDefault(user => user.Email == email);
        }

        public void AddUser(UserEntity user)
        {
            userContext.Set<UserEntity>().Add(user);
            userContext.SaveChanges();
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
