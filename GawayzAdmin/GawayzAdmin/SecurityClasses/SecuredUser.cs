using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GawayzAdmin.SecurityClasses
{
    public class SecuredUser
    {
        public UserLogin User { get; set; }

        public String Username
        {
            get { return User.Username; }
        }

        public int UserId
        {
             get { return User.UserId; }
        }
    }
    public class UserLogin
    {
        public bool Isauthenticated { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}