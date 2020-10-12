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
        public static ResultState Login(string kw, string pw)
        {
            ResultState state = new ResultState();
            if (!String.IsNullOrEmpty(kw))
            {
                if (!String.IsNullOrEmpty(pw))
                {
                    if (Data.AccountExist(kw))
                    {
                        if (!Data.IsOldPassword(kw, pw))
                        {
                            return Data.LoginUser(kw, pw);
                        }
                        else
                        {
                            state.SetState(false, 4);
                        }
                    }
                    else
                    {
                        state.SetState(false, 3);
                    }
                }
                else
                {
                    state.SetState(false, 2);
                }
            } else
            {
                state.SetState(false, 1);
            }
            return state;
        }

        public static ResultState Register(string kw, string pw)
        {
            ResultState state = new ResultState();

            if (!String.IsNullOrEmpty(kw))
            {
                if (!Data.KennwortToShort(kw))
                {
                    if (!Data.KennwortToLong(kw))
                    {
                        if (!Data.KennwortInvalidChars(kw))
                        {
                            if (!Data.KennwortExist(kw))
                            {
                                if (!String.IsNullOrEmpty(pw))
                                {
                                    if (!Data.PasswortToShort(pw))
                                    {
                                        if (!Data.PasswortToLong(pw))
                                        {
                                            if (!Data.PasswordInvalidChars(pw))
                                            {
                                                if (Data.RegisterUser(kw, pw))
                                                {
                                                    state.SetState(true, 15);
                                                }
                                                else
                                                {
                                                    state.SetState(false, 14);
                                                }
                                            }
                                            else
                                            {
                                                state.SetState(false, 13);
                                            }
                                        }
                                        else
                                        {
                                            state.SetState(false, 12);
                                        }
                                    }
                                    else
                                    {
                                        state.SetState(false, 11);
                                    }
                                }
                                else
                                {
                                    state.SetState(false, 2);
                                }
                            }
                            else
                            {
                                state.SetState(false, 10);
                            }
                        }
                        else
                        {
                            state.SetState(false, 9);
                        }
                    }
                    else
                    {
                        state.SetState(false, 8);
                    }
                }
                else
                {
                    state.SetState(false, 7);
                }
            }
            else
            {
                state.SetState(false, 1);
            }

            return state;
        }
    }
}