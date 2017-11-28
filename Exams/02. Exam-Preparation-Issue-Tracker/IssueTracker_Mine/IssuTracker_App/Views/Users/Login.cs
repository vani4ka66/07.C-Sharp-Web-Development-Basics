using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces;

namespace IssuTracker_App.Views.Users
{
    public class Login : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            string menu = File.ReadAllText(@"../../Content/menu.html");
            string login = File.ReadAllText(@"../../Content/login.html");
            string footer = File.ReadAllText(@"../../Content/footer.html");
            sb.AppendLine(menu);
            sb.AppendLine(login);
            sb.AppendLine(footer);

            return sb.ToString();
        }
    }
}
