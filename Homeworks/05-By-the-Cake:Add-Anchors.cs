using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace 05-By-The-Cake-Add-Anchors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Content-type: text/html\r\n");
            string htmlContent = File.ReadAllText("C:/xampp/htdocs/02.  HTTP-Protocol/01. ByTheCake.html");
            Console.WriteLine(htmlContent);
        }
    }
}
