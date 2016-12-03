using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using DataAccess.UnitOfWork;
using GawayzAdmin.SecurityClasses;
using GawayzAdmin.ViewModels;

namespace GawayzAdmin.SecurityClasses
{
    public class LoginRepository
    {
        public String LoginUser(UserLoginViewModel usrLogin)
        {
            Object result;

            try
            {

                var login = AuthenticationHandler.IsAuthenticated(usrLogin.UserName, usrLogin.PassWord);
                if (login != null)
                {


                    if (login.Isauthenticated)
                    {

                        var secUser = new SecuredUser { User = login };
                        string dataS =
                            secUser.UserId + "," + //user Obj  (0-2)
                            secUser.Username + ","; //user Obj

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usrLogin.UserName, DateTime.Now, DateTime.Now.AddDays(364), usrLogin.RememberMe, dataS, FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                        if (ticket.IsPersistent)
                        {
                            cookie.Expires = ticket.Expiration;
                        }
                        else
                        {
                            cookie.Expires = DateTime.Today.AddDays(1);
                        }
                        HttpContext.Current.Response.Cookies.Add(cookie);

                        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                        if (authCookie != null)
                        {
                            FormsAuthenticationTicket ticketD = FormsAuthentication.Decrypt(authCookie.Value);
                            if (ticketD != null)
                            {
                                string[] values = ticketD.UserData.Split(',').Select(sValue => sValue.Trim()).ToArray();


                                var cuser = new UserLogin
                                {
                                    UserId = Convert.ToInt32(values[0]),
                                    Username = values[1],
                                };
                                var abcUser = new SecuredUser();

                                abcUser.User = cuser;
                            }
                        }


                        SessionManager.CurrentUser = secUser;
                        var redirectUrl = "Company/Index";
                        result = new
                        {
                            urlData = redirectUrl,
                            success = true,
                            message = "Login succeed"
                        };


                    }
                    else
                    {
                        result = new
                        {
                            LoginResponse = "",
                            success = false,
                            message = "Login Failed"
                        };
                    }

                }
                else
                {
                    result = new
                    {
                        LoginResponse = "",
                        success = false,
                        message = "Login Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new
                {
                    success = false,
                    message = ex.Message
                };
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
    }
}

public static class AuthenticationHandler
{
    public static UserLogin IsAuthenticated(string username, string password)
    {
        var uow = new UnitOfWork();
        var usrLogin = new UserLogin();
        var login = uow.Users.List().FirstOrDefault(x => x.UserName == username && x.IsActive);

        if (login != null)
        {
            string proDecPassString = StringCipher.Decrypt(login.Password, WebConfigurationManager.AppSettings["EncDecKey"]);
            if (proDecPassString == password)
            {
                usrLogin.Isauthenticated = true;
                usrLogin.UserId = login.UserId;
                usrLogin.Username = login.UserName;
                return usrLogin;
            }
        }
        return null;
    }
}