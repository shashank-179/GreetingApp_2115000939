using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Business_Layer.Interface;
using Model_Layer.Model;
using ModelLayer.Model;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using RepositoryLayer.Entity;

namespace Business_Layer.Service
{

    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;

        public GreetingBL(IGreetingRL _greetingRL)
        {
            this._greetingRL = _greetingRL;
        }

        public UserEntity RegisterUserBL(RegisterDTO registerDTO)
        {
            if (string.IsNullOrWhiteSpace(registerDTO.Name) || string.IsNullOrWhiteSpace(registerDTO.Email) || string.IsNullOrWhiteSpace(registerDTO.Password))
            {
                throw new ArgumentException("Name, email, and password are required.");
            }

            if (_greetingRL.GetUserByEmail(registerDTO.Email) != null)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            string hashedPassword = PasswordHashing.HashPassword(registerDTO.Password);

            UserEntity newUser = new UserEntity
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                PasswordHash = hashedPassword,
                
            };

            _greetingRL.AddUser(newUser);
            return newUser;
        }

        public string LoginUserBL(LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                return "Email and Password are required";
            }

            var user = _greetingRL.GetUserByEmail(loginDTO.Email);
            if (user == null)
            {
                return "Invalid email or password";
            }

            bool isPasswordValid = PasswordHashing.VerifyPassword(loginDTO.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return "Invalid email or password";
            }

            return "Login successful";
        }

        public GreetingEntity UpdateGreeting(int id, string newMessage)
        {
            return _greetingRL.UpdateGreeting(id, newMessage);
        }
        public bool DeleteGreeting(int id)
        {
            return _greetingRL.DeleteGreeting(id);
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
