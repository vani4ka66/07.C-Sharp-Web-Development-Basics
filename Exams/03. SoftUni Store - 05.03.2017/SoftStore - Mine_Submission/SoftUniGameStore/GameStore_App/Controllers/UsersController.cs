using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_App.BindingModels;
using GameStore_App.Models;
using GameStore_App.Services;
using GameStore_App.Utilities;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;

namespace GameStore_App.Controllers
{
    public class UsersController : Controller
    {
        private UsersService service;

        public UsersController()
        {
            this.service = new UsersService();
        }

        [HttpGet]
        public IActionResult Register(HttpSession session, HttpResponse response)
        {
            if (AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/index");
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Register(HttpResponse response, RegisterUserBindingModel model)
        {
            if (!this.service.IsRegisterModelValid(model))
            {
                this.Redirect(response, "/users/register");
                return null;
            }

            User user = this.service.GetUserFromRegisterBind(model);
            this.service.RegisterUser(user);

            this.Redirect(response, "/users/login");
            return null;
        }

        [HttpGet]
        public IActionResult Login(HttpSession session, HttpResponse response)
        {
            if (AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/home/index");
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(HttpResponse response, HttpSession session, LoginUserBindingModel lubm)
        {
            if (!this.service.IsLoginModelValid(lubm))
            {
                this.Redirect(response, "/users/login");
                return null;
            }

            User user = this.service.GetUserFromLoginBind(lubm);

            this.service.LoginUser(user, session.Id);
            ViewBag.Bag.Add("email", user.Email);

            this.Redirect(response, "/home/index");
            return null;
        }

        [HttpGet]
        public void Logout(HttpResponse response, HttpSession session)
        {
            AuthenticationManager.Logout(response, session.Id);
            this.Redirect(response, "/home/topics");
        }

        private bool IsAuthenticated(string sessionId, HttpResponse response)
        {
            if (AuthenticationManager.IsAuthenticated(sessionId))
            {
                this.Redirect(response, "/home/index");
                return true;
            }

            return false;
        }

    }
}
