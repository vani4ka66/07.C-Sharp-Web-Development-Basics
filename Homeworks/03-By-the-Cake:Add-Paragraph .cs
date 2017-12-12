using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace 03-By-The-Cake-Add-Paragraph
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Content-type: text/html\r\n");
            Console.WriteLine("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n    <head>\r\n        <meta charset=\"UTF-8\">\r\n        <title>By The Cake</title>\r\n    </head>\r\n   <body>\r\n       <h1> By The Cake</h1>\r\n       <h2>Enjoy our awesome cakes</h2>\r\n       <hr>\r\n       <ul>\r\n           <li>Home\r\n               <ol>\r\n                   <li>Our Cakes</li>\r\n                   <li>Our Stores</li>\r\n               </ol>\r\n           </li>\r\n           <li>Add Cake</li>\r\n           <li>Browse Cakes</li>\r\n           <li>About Us</li>\r\n       <h2>Home</h2>\r\n       <section>\r\n           <h3>Our Cakes</h3>\r\n           <p>Cake is a form of sweet dessert that is typically baked. In its oldest forms, cakes were modifications of breads, but cakes now cover a wide range of preparations that can be simple or elaborate, and that share features with other desserts such as pastries, meringues, custards, and pies.</p>\r\n           <img src=\"http://blog.yululate.com/wp-content/uploads/2016/06/cake-images-21.jpg\" alt=\"one\" width=\"20%\" height=\"20%\">\r\n        </section>\r\n        <section>\r\n           <h3>Our Stores</h3>\r\n           <p>Our stores are located in 21 cities all over the world. Come and see what we have for you.</p>\r\n            <img src=\"https://c2.staticflickr.com/4/3205/2697382625_67ea7d94ef_z.jpg?zz=1\" alt=\"two\" width=\"20%\" height=\"20%\">\r\n       </section>\r\n       </ul>\r\n   </body>\r\n</html>\r\n");
        }
    }
}
