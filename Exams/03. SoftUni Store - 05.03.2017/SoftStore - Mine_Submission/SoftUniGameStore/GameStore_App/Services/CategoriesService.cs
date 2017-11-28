using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_App.BindingModels;
using GameStore_App.Models;
using GameStore_App.ViewModels;
using GameStore_App.Views.Categories;

namespace GameStore_App.Services
{
    public class CategoriesService : Service
    {
        public AllViewModel GetAllViewModel(User activeUser)
        {
            AllViewModel view = new AllViewModel();

            IEnumerable<AllGameViewModel> games = this.Context.Games
                .Select(g => new AllGameViewModel()
                {
                    Id = g.Id,
                    Name = g.Title,
                    Price = g.Price,
                    Size = g.Size
                });

            view.Games = games;
            return view;
        }

        public bool IsAddGameValid(AddGameBindingModel model)
        {
            if (model.Title.Length <3 || model.Title.Length > 100 || !char.IsUpper(model.Title[0]))
            {
                return false;
            }

            if (model.Price <= 0)
            {
                return false;
            }

            if (model.Size <= 0)
            {
                return false;
            }

            if (!model.VideoUrl.StartsWith("is https://www.youtube.com/watch?v="))
            {
                return false;
            }
            if (!model.Thumbnail.StartsWith("http://"))
            {
                return false;
            }

            if (model.Description.Length < 20)
            {
                return false;
            }
            return true;
        }

        public void AddNewGame(AddGameBindingModel model)
        {
            this.Context.Games.Add(new Game()
            {
                Description = model.Description,
                ImageThumbnail = model.Thumbnail,
                Price = model.Price,
                Size = model.Size,
                Title = model.Title,
                ReleaseDate = model.ReleaseDate,
                VideoUrl = model.VideoUrl.Substring(32, 11)
                
            });

            this.Context.SaveChanges();
        }

        public void EditGame(EditGameBindingModel model)
        {
            Game game = this.Context.Games.Find(model.Id);

            if (game != null)
            {
                game.Title = model.Title;
                game.Price = model.Price;
                game.Size = model.Size;
                game.Description = model.Description;
                game.VideoUrl = model.VideoUrl;
                game.ImageThumbnail = model.Thumbnail;
            }

            this.Context.SaveChanges();
        }

        public void DeleteGame(int id)
        {
            this.Context.Games.Remove(this.Context.Games.Find(id));
            this.Context.SaveChanges();
        }

        public EditGameViewModel GetEditGameViewModel(int id)
        {
            Game game = this.Context.Games.Find(id);

            return new EditGameViewModel()
            {
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                Size = game.Size,
                Thumbnail = game.ImageThumbnail,
                VideoUrl = game.VideoUrl
            };
        }

        public DeleteGameViewModel GetDeleteGameViewModel(int id)
        {
            Game game = this.Context.Games.Find(id);

            return new DeleteGameViewModel()
            {
                Id = game.Id,
                Title = game.Title
            };
        }
    }
}
