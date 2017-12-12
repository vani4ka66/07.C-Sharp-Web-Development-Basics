using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace 02-By-The-Cake-Add-Menu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Content-type: text/html\r\n");
            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n    <head>\r\n        <meta charset=\"UTF-8\">\r\n        <title>By The Cake</title>\r\n    </head>\r\n   <body>\r\n       <h1> By The Cake</h1>\r\n       <h2>Enjoy our awesome cakes</h2>\r\n       <hr>\r\n       <ul>\r\n           <li>Home\r\n               <ol>\r\n                   <li>Our Cakes</li>\r\n                   <li>Our Stores</li>\r\n               </ol>\r\n           </li>\r\n           <li>Add Cake</li>\r\n           <li>Browse Cakes</li>\r\n           <li>About Us</li>\r\n       </ul>\r\n   </body>\r\n</html>");
        }
    }
}
