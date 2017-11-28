using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces;

namespace IssuTracker_App.Views.Home
{
    public class Index : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            string navigation;

            string currentUser = ViewBag.GetUserName();


            if (currentUser != null)
            {
                navigation = File.ReadAllText(@"../../Content/menu-logged.html");
                navigation = string.Format(navigation, currentUser);
            }
            else
            {
                navigation = File.ReadAllText(@"../../Content/menu.html");


            }

            string hello = "<h2>Welcome to Issue Tracker</h2>,";
            
            string footer = File.ReadAllText(@"../../Content/footer.html");
            sb.AppendLine(navigation);
            sb.AppendLine(hello);
            //sb.AppendLine(currentUser + "+++++++++++++");

            sb.AppendLine(footer);

            return sb.ToString();
        }
    }
}
