using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_App.BindingModels
{
    public class EditGameBindingModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public string Thumbnail { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }




    }
}
