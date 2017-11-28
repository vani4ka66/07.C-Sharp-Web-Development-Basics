using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_App.ViewModels;

namespace GameStore_App.Services
{
    public class HomeService : Service
    {
        public IndexViewModel GetAllViewModel()
        {
            IndexViewModel view = new IndexViewModel();

            IEnumerable<IndexGameViewModel> games = this.Context.Games
                .Select(g => new IndexGameViewModel()
                {
                    Title = g.Title,
                    Description = g.Description,
                    ImageUrl = g.ImageThumbnail,
                    Price = g.Price,
                    Size = g.Size
                });

            view.Games = games;
            return view;
        }
    }
}
