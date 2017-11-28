using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_App.BindingModels;
using GameStore_App.Models;
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
    public class CategoriesController : Controller
    {
        private CategoriesService service;

        public CategoriesController()
        {
            this.service = new CategoriesService();
        }

        [HttpGet]
        public IActionResult Details(HttpSession session, HttpResponse responce)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(responce, "/users/register");
                return null;
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult<AllViewModel> All(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (!activeUser.IsAdmin)
            {
                this.Redirect(response, "/home/index");
                return null;
            }

            AllViewModel viewModel = this.service.GetAllViewModel(activeUser);
            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Add(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (!activeUser.IsAdmin)
            {
                this.Redirect(response, "/home/index");
                return null;
            }
            return this.View();
        }

        [HttpPost]
        public void Add(HttpResponse response, HttpSession session, AddGameBindingModel model)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");
                return;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (!activeUser.IsAdmin)
            {
                this.Redirect(response, "/home/index");
                return;
            }

            if (!this.service.IsAddGameValid(model))
            {
                this.Redirect(response, "/categories/add");
                return;
            }

            this.service.AddNewGame(model);
            this.Redirect(response, "/categories/all");

        }

        [HttpGet]
        public IActionResult<EditGameViewModel> Edit(HttpSession session, HttpResponse response, int id)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (!activeUser.IsAdmin)
            {
                this.Redirect(response, "/home/index");
                return null;
            }

            EditGameViewModel viewModel = this.service.GetEditGameViewModel(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public void Edit(HttpSession session, HttpResponse response, EditGameBindingModel model)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");
                return ;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (!activeUser.IsAdmin)
            {
                this.Redirect(response, "/home/index");
                return ;
            }

            this.service.EditGame(model);
            this.Redirect(response, "categories/all");
        }

        [HttpGet]
        public IActionResult Delete(HttpSession session, HttpResponse response)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (!activeUser.IsAdmin)
            {
                this.Redirect(response, "/home/index");
                return null;
            }
            return this.View();
        }

        [HttpGet]
        public IActionResult<DeleteGameViewModel> Delete(HttpSession session, HttpResponse response, int id)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");
                return null;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (!activeUser.IsAdmin)
            {
                this.Redirect(response, "/home/index");
                return null;
            }

            DeleteGameViewModel viewModel = this.service.GetDeleteGameViewModel(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public void Delete(HttpSession session, HttpResponse response, DeleteGameBindingModel model)
        {
            if (!AuthenticationManager.IsAuthenticated(session.Id))
            {
                this.Redirect(response, "/users/register");
                return;
            }

            User activeUser = AuthenticationManager.GetAuthenticatedUser(session.Id);

            if (!activeUser.IsAdmin)
            {
                this.Redirect(response, "/home/index");
                return ;
            }

            this.service.DeleteGame(model.Id);
            this.Redirect(response, "/categories/all");
        }
    }
}
