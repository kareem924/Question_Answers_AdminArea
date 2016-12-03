using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GawayzAdmin.ViewModels
{
    public class UserLoginViewModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}