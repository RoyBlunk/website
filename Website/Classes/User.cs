using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Website.Classes
{
    public class User
    {
        private const int KennwortMinLength = 8;
        private const int KennwortMaxLength = 64;
        private static Regex KennwortValidChars = new Regex("^[a-zA-Z0-9]*$");

        private const int PasswortMinLength = 8;
        private const int PasswortMaxLength = 64;
        private static Regex PasswortValidChars = new Regex("^[A-Za-z0-9\"!§$%&/()[\\]{}+\\-*_<>|#;.~]*$");


        public static ResultState Login(string kw, string pw)
        {
            ResultState state = new ResultState();
            try
            {
                if (!String.IsNullOrEmpty(kw) && !String.IsNullOrWhiteSpace(kw))
                {       
                    if (!String.IsNullOrEmpty(pw) && !String.IsNullOrWhiteSpace(pw))
                    {
                        using (Models.SecondNamespace.TestSiteEntities2 database = new Models.SecondNamespace.TestSiteEntities2())
                        {
                            int? userID = database.Kennwort
                                .AsEnumerable()
                                .Where(i => i.Kennwort1 == kw && i.Deleted == false)
                                .Select(i => i.UserID)
                                .FirstOrDefault();
                                
                            if (userID != null && userID != 0)
                            {
                                string dbSalt = database.Salt
                                    .Where(i => i.UserID == userID && i.Deleted == false)
                                    .Select(i => i.Salt1)
                                    .FirstOrDefault();

                                List<string> dbPasswords = database.Password
                                    .Where(i => i.UserID == userID)
                                    .OrderBy(i => i.Deleted)
                                    .Select(i => i.Passwort)
                                    .ToList();

                                if (!String.IsNullOrEmpty(dbSalt) && !String.IsNullOrWhiteSpace(dbSalt) && dbPasswords.Count > 0)
                                {
                                    byte[] salt = Convert.FromBase64String(dbSalt);
                                    byte[] bytePW = Generator.CreateHash(pw, salt);

                                    if (bytePW != null && bytePW.Length > 0)
                                    {
                                        string hashPW = Convert.ToBase64String(bytePW);

                                        if (!String.IsNullOrEmpty(hashPW) && !String.IsNullOrWhiteSpace(hashPW))
                                        {
                                            foreach (string curPW in dbPasswords)
                                            {
                                                if (curPW == hashPW)
                                                {
                                                    if (dbPasswords.IndexOf(curPW) == 0)
                                                    {
                                                        return Cookie.CreateUserLogin(kw, pw);
                                                    }
                                                    else
                                                    {
                                                        state.SetState(false, 4);
                                                        break;
                                                    }
                                                }
                                            }
                                            state.SetState(false, 17);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                state.SetState(false, 3);
                            }
                        }
                    }
                    else
                    {
                        state.SetState(false, 2);
                    }
                }
                else
                {
                    state.SetState(false, 1);
                }
            }
            catch
            {
                state.SetState(false, 5);
            }
            return state;
        }

        public static ResultState Register(string kw, string pw)
        {
            ResultState state = new ResultState();
            try
            {
                if (!String.IsNullOrEmpty(kw) && !String.IsNullOrWhiteSpace(kw))
                {
                    if (kw.Length >= KennwortMinLength)
                    {
                        if (kw.Length <= KennwortMaxLength)
                        {
                            if (KennwortValidChars.IsMatch(kw))
                            {
                                using (Models.SecondNamespace.TestSiteEntities2 database = new Models.SecondNamespace.TestSiteEntities2())
                                {
                                    int? userID = database.Kennwort
                                        .AsEnumerable()
                                        .Where(i => i.Kennwort1 == kw)
                                        .Select(i => i.UserID)
                                        .FirstOrDefault();

                                    if (userID == null || userID == 0)
                                    {
                                        if (!String.IsNullOrEmpty(pw) && !String.IsNullOrWhiteSpace(pw))
                                        {
                                            if (pw.Length >= PasswortMinLength)
                                            {
                                                if (pw.Length <= PasswortMaxLength)
                                                {
                                                    if (PasswortValidChars.IsMatch(pw))
                                                    {
                                                        byte[] salt = Generator.CreateSalt();

                                                        if (salt != null && salt.Length > 0)
                                                        {
                                                            byte[] bytePW = Generator.CreateHash(pw, salt);

                                                            if (bytePW != null && bytePW.Length > 0)
                                                            {
                                                                string newPW = Convert.ToBase64String(bytePW);
                                                                string newSalt = Convert.ToBase64String(salt);

                                                                if (!String.IsNullOrEmpty(newPW) && !String.IsNullOrWhiteSpace(newPW) && !String.IsNullOrEmpty(newSalt) && !String.IsNullOrWhiteSpace(newSalt))
                                                                {
                                                                    Models.SecondNamespace.User _User = new Models.SecondNamespace.User();
                                                                    Models.SecondNamespace.Salt _Salt = new Models.SecondNamespace.Salt();
                                                                    Models.SecondNamespace.Password _Password = new Models.SecondNamespace.Password();
                                                                    Models.SecondNamespace.Kennwort _Kennwort = new Models.SecondNamespace.Kennwort();

                                                                    _Salt.UserID = _User.ID;
                                                                    _Salt.Salt1 = newSalt;

                                                                    _Password.UserID = _User.ID;
                                                                    _Password.Passwort = newPW;

                                                                    _Kennwort.UserID = _User.ID;
                                                                    _Kennwort.Kennwort1 = kw;

                                                                    database.User.Add(_User);
                                                                    database.Salt.Add(_Salt);
                                                                    database.Password.Add(_Password);
                                                                    database.Kennwort.Add(_Kennwort);

                                                                    database.SaveChanges();

                                                                    state.SetState(true, 15);
                                                                }
                                                            }
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
            }
            catch
            {
                state.SetState(false, 14);
            }
            return state;
        }
    }
}