using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuTracker_App.BindingModels
{
    public class RegisterUserBindingModel
    {
        public string Username { get; set; }

        public string Fullname { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

    }
}
