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
        EmailService _emailService;

        public GreetingBL(IGreetingRL _greetingRL, EmailService _emailService)
        {
            this._greetingRL = _greetingRL;
            this._emailService = _emailService;
        }

        public bool ForgotPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            var user = _greetingRL.GetUserByEmail(forgetPasswordDTO.Email);
            if (user == null) return false;

            string resetToken = ResetTokenGenerator.GenerateResetToken();
            _greetingRL.SavePasswordResetToken(user.Id, resetToken);

            return _emailService.SendResetPasswordEmail(user.Email, resetToken);
        }

        public bool ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = _greetingRL.GetUserByResetToken(resetPasswordDTO.Token);
            if (user == null)
            {
                return false;
            }

            _greetingRL.UpdatePassword(user.Id, resetPasswordDTO.NewPassword);
            return true;
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

        public UserEntity LoginUserBL(LoginDTO loginDTO)
        {
            var user = _greetingRL.GetUserByEmail(loginDTO.Email);
            if (user == null || !PasswordHashing.VerifyPassword(loginDTO.Password, user.PasswordHash))
            {
                return null; // Invalid login
            }
            return user; // Return UserEntity on success
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
