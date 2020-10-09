using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Classes
{
    public abstract class Cookie
    {
        public static bool CreateUserLogin(string kw, string pw)
        {
            if (!String.IsNullOrEmpty(kw) && !String.IsNullOrEmpty(pw))
            {

                
                return true; 
                
            }
            return false;
        }
    }
}