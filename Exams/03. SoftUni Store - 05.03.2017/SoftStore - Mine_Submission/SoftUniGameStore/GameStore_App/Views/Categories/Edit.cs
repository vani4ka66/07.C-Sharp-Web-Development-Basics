using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_App.ViewModels;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace GameStore_App.Views.Categories
{
    public class Edit : IRenderable<EditGameViewModel>
    {
        public EditGameViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            string header = File.ReadAllText(@"../../Content/header.html");
            string navigation = File.ReadAllText(@"../../Content/nav-logged.html");

            string edit = File.ReadAllText(@"../../Content/edit-game.html");
            edit = string.Format(edit, this.Model);

            string footer = File.ReadAllText(@"../../Content/footer.html");

            sb.AppendLine(header);
            sb.AppendLine(navigation);
            sb.AppendLine(edit);
            sb.AppendLine(footer);

            return sb.ToString();
        }

    }
}
