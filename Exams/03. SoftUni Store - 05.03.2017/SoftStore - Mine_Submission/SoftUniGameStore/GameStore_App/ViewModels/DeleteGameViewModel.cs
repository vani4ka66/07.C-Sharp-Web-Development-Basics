using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_App.ViewModels
{
    public class DeleteGameViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            string result = $"<form method=\"post\" id=\"new-game-form\">\r\n                    <input type=\"type\" hidden=\"hidden\" value=\"{this.Id}\" />\r\n\r\n                    <div class=\"form-group row\">\r\n                        <label for=\"name\" class=\"form-control-label\">Title</label>\r\n                        <input type=\"text\" maxlength=\"100\" minlength=\"4\" id=\"name\" class=\"form-control\"\r\n                               placeholder=\"Enter Game Name\" value=\"{this.Title}\" disabled/>\r\n                    </div>\r\n\r\n                    <input type=\"submit\" id=\"btn-edit-game\" class=\"btn btn-outline-danger btn-lg btn-block\"\r\n                           value=\"Delete Game\"/>\r\n                </form>";
            return result;
        }
    }
}
