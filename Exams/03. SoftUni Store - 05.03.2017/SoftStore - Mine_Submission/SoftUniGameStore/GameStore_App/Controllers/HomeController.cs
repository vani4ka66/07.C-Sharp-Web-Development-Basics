using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_App.Services;
using GameStore_App.Utilities;
using GameStore_App.ViewModels;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace GameStore_App.Controllers
{
    public class HomeController : Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }

        [HttpGet]
        public IActionResult<IndexViewModel> Index(HttpSession session, HttpResponse responce)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
               this.Redirect(responce, "/users/register");
                return null;
            }

            IndexViewModel viewModel = this.service.GetAllViewModel();

            return this.View(viewModel);
        }

       


    }
}
