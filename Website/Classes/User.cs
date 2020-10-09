using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Website.Classes
{
    public class User
    {
        public User()
        {

        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="kw">kennword</param>
        /// <param name="pw">password</param>
        /// <returns>True if successfull logged in, false if failed somehow or method trigger exceptions</returns>
        public static bool Login(string kw, string pw)
        {
            if (!String.IsNullOrEmpty(kw) && !String.IsNullOrEmpty(pw))
            {
                if (!Data.AccountExist(kw))
                {
                    if (!Data.IsOldPassword(kw, pw))
                    {
                        return Data.LoginUser(kw, pw);
                    } else
                    {
                        throw new OldPasswordException();
                    }
                } else
                {
                    throw new NoAccountFoundException();
                }
            }
            return false;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="kw">kennwort</param>
        /// <param name="pw">password</param>
        /// <returns>True if data is successfull saved, false if failed somehow or methods trigger exception</returns>
        public static bool Register(string kw, string pw)
        {
            if (!String.IsNullOrEmpty(kw) && !String.IsNullOrEmpty(pw))
            {
                if (!Data.KennwortToShort(kw))
                {
                    if (!Data.KennwortToLong(kw))
                    {
                        if (!Data.KennwortInvalidChars(kw))
                        {
                            if (!Data.KennwortExist(kw))
                            {
                                if (!Data.PasswortToShort(pw))
                                {
                                    if (!Data.PasswortToLong(pw))
                                    {
                                        if (!Data.PasswordInvalidChars(pw))
                                        {
                                            return Data.RegisterUser(kw, pw);
                                        } else
                                        {
                                            throw new PasswordInvalidCharsException();
                                        }
                                    } else
                                    {
                                        throw new PasswordToLongException();
                                    }
                                } else
                                {
                                    throw new PasswordToShortException();
                                }
                            } else
                            {
                                throw new KennwortExistException();
                            }
                        } else
                        {
                            throw new KennwortInvalidCharsException();
                        }
                    } else
                    {
                        throw new KennwortToLongException();
                    }
                } else
                {
                    throw new KennwortToShortException();
                }
            }
            return false;
        }
    }
}