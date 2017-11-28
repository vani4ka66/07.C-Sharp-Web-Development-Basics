using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore_App.ViewModels;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace GameStore_App.Views.Home
{
    public class Index : IRenderable<IndexViewModel>
    {
        public IndexViewModel Model { get; set; }


        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            string header = File.ReadAllText(@"../../Content/header.html");
            string navigation = File.ReadAllText(@"../../Content/nav-logged.html");
            string home = File.ReadAllText(@"../../Content/home.html");
            home = string.Format(home, this.Model);
            string footer = File.ReadAllText(@"../../Content/footer.html");

            sb.AppendLine(header);
            sb.AppendLine(navigation);
            sb.AppendLine(home);
            sb.AppendLine(footer);

            return sb.ToString();
        }

    }
}
