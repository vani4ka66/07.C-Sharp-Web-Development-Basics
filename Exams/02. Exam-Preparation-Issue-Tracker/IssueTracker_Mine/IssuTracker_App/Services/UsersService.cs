using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IssuTracker_App.BindingModels;
using IssuTracker_App.Models;
using IssuTracker_App.Models.Enums;

namespace IssuTracker_App.Services
{
    public class UsersService : Service
    {
        public bool IsRegisterModelValid(RegisterUserBindingModel model)
        {
            if (model.Username.Length < 5 || model.Username.Length > 30)
            {
                return false;
            }

            if (model.Fullname.Length < 5)
            {
                return false;
            }

            //Regex regex = new Regex(@"^[!@#$%^&*,.]$");
            //if (!regex.IsMatch(model.Password))
            //{
            //    return false;
            //}

            if (model.Password.Length < 8 || !model.Password.Any(char.IsUpper) || !model.Password.Any(char.IsDigit))
            {
                return false;
            }

            if (model.Password != model.ConfirmPassword)
            {
                return false;
            }

            return true;
        }

        public User GetUserFromRegisterBind(RegisterUserBindingModel model)
        {
            User user = new User()
            {
                Username =  model.Username,
                FullName = model.Fullname,
                Password = model.Password
                
            };

            return user;
        }

        public void RegisterUser(User user)
        {
            if (!this.Context.Users.Any())
            {
                User userEntity = new User()
                {
                    Username = user.Username,
                    FullName = user.FullName,
                    Password = user.Password,
                    Role = Role.Admin
                };

                this.Context.Users.Add(userEntity);
                this.Context.SaveChanges();
            }
        }

        public bool IsLoginModelValid(LoginUserBindingModel model)
        {
            return this.Context.Users.Any(
                user => user.Username == model.Username && user.Password == model.Password);
        }

        public User GetUserFromLoginBind(LoginUserBindingModel model)
        {
            return this.Context.Users.FirstOrDefault(
                user => user.Username == model.Username && user.Password == model.Password);
        }

        public void LoginUser(User user, string id)
        {
            this.Context.Logins.Add(new Login()
            {
                SessionId = id,
                User = user,
                IsActive = true
            });

            this.Context.SaveChanges();
        }

       
    }
}
