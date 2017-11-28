using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces;

namespace GameStore_App.Views.Categories
{
    public class Add : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            string header = File.ReadAllText(@"../../Content/header.html");
            string navigation = File.ReadAllText(@"../../Content/nav-logged.html");
            string addGame = File.ReadAllText(@"../../Content/add-game.html");
            string footer = File.ReadAllText(@"../../Content/footer.html");

            sb.AppendLine(header);
            sb.AppendLine(navigation);
            sb.AppendLine(addGame);
            sb.AppendLine(footer);

            return sb.ToString();
        }
    }
}
