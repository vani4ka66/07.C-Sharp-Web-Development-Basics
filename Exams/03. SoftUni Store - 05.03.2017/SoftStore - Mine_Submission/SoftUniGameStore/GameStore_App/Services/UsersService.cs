using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GameStore_App.BindingModels;
using GameStore_App.Models;

namespace GameStore_App.Services
{
    public class UsersService : Service
    {
        public bool IsRegisterModelValid(RegisterUserBindingModel model)
        {
            if (!model.Email.Contains('@') || !model.Email.Contains('.'))
            {
                return false;
            }

            if (model.Password.Length < 6 || !model.Password.Any(char.IsUpper) ||
                !model.Password.Any(char.IsLower)|| !model.Password.Any(char.IsDigit))
            {
                return false;
            }

            if (model.Password != model.ConfirmPassword)
            {
                return false;
            }

            if (string.IsNullOrEmpty(model.FullName))
            {
                return false;
            }

            return true;
        }

        public User GetUserFromRegisterBind(RegisterUserBindingModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                FullName = model.FullName,
                Password = model.Password
            };

            return user;
        }

        public void RegisterUser(User user)
        {
            if (!this.Context.Users.Any())
            {
                user.IsAdmin = true;
            }

            this.Context.Users.Add(user);
            this.Context.SaveChanges();
        }

        public bool IsLoginModelValid(LoginUserBindingModel model)
        {
            return this.Context.Users.Any(
                user => user.Email == model.Email && user.Password == model.Password);
        }

        public User GetUserFromLoginBind(LoginUserBindingModel model)
        {
            return this.Context.Users.FirstOrDefault(
                user => user.Email == model.Email && user.Password == model.Password);
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

