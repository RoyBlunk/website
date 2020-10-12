using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Website.Classes
{
    public abstract class Cookie
    {
        private const int UserLoginCookieExpireTimeDays = 30;
        public static ResultState CreateUserLogin(string kw, string pw)
        {
            ResultState state = new ResultState(false, 18);
            if (!String.IsNullOrEmpty(kw))
            {
                if (!String.IsNullOrEmpty(pw))
                {
                    string cookieName = Generator.CreateCookieName();
                    string cookieValue = Generator.CreateCookieValue();
                    if (cookieName != null && cookieName.Length > 0 && cookieValue != null && cookieValue.Length > 0)
                    {
                        TimeSpan expiresIn = new TimeSpan(UserLoginCookieExpireTimeDays, 0, 0, 0, 0);
                        if (CreateCookie(cookieName, cookieValue, expiresIn))
                        {
                            state.SetState(true, 6);
                        }
                    }
                    
                }
            }
            return state;
        }

        public static bool CreateCookie(string name, dynamic value, TimeSpan expiresFromNow)
        {
            if (name != null)
            {
                HttpCookie cookie = new HttpCookie(name);
                cookie.Value = value;
                cookie.Expires = DateTime.Now.Add(expiresFromNow);

                var response = HttpContext.Current.Response;
                response.Cookies.Add(cookie);

                return true;
            }
            return false;
        }
    }
}