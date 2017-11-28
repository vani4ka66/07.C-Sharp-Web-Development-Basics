using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IssuTracker_App.BindingModels;
using IssuTracker_App.Models;
using IssuTracker_App.Services;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using AuthenticationManager = IssuTracker_App.Utilities.AuthenticationManager;

namespace IssuTracker_App.Controllers
{
    public class UsersController : Controller
    {
        private UsersService service;

        public UsersController()
        {
            this.service =  new UsersService();
        }

        [HttpGet]
        public IActionResult Register(HttpSession session, HttpResponse responce)
        {
            if (AuthenticationManager.IsAuthenticated(session.Id))
            {
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Register(HttpResponse responce, RegisterUserBindingModel model)
        {
            if (!this.service.IsRegisterModelValid(model))
            {
                this.Redirect(responce, "/users/register");
                return null;
            }

            User user = this.service.GetUserFromRegisterBind(model);
            this.service.RegisterUser(user);

            this.Redirect(responce, "/users/login");
            return null;

        }

        [HttpGet]
        public IActionResult Login(HttpSession session, HttpResponse responce)
        {
            if (AuthenticationManager.IsAuthenticated(session.Id))
            {
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Login(HttpResponse response, HttpSession session, LoginUserBindingModel model)
        {
            if (!this.service.IsLoginModelValid(model))
            {
                this.Redirect(response, "/users/login");
                return null;
            }

            User user = this.service.GetUserFromLoginBind(model);
            this.service.LoginUser(user, session.Id);
            ViewBag.Bag.Add("username", user.Username);

            this.Redirect(response, "/home/index");
            return null;
        }

        [HttpGet]
        public void Logout(HttpResponse response, HttpSession session)
        {
            AuthenticationManager.Logout(response, session.Id);
            this.Redirect(response, "/home/index");
        }
    }
}
