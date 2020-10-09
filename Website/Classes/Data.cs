using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Website.Classes
{
    public abstract class Data
    {
        private const int KennwortMinLength = 8;
        private const int KennwortMaxLength = 64;
        private static Regex KennwortValidChars = new Regex("^[a-zA-Z0-9]*$");

        private const int PasswortMinLength = 8;
        private const int PasswortMaxLength = 64;
        private static Regex PasswortValidChars = new Regex("^[A-Za-z0-9\"!§$%&/()[\\]{}+\\-*_<>|#;.~]*$");

        /// <summary>
        /// Checks if Kennwort is to short
        /// </summary>
        /// <param name="kw">Kennwort</param>
        /// <returns>True if Kennwort is null or shorter than const KennwortMinLength</returns>
    public static bool KennwortToShort(string kw)
        {
            if (!String.IsNullOrEmpty(kw))
            {
                if (kw.Length >= KennwortMinLength)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the kennwort is to long
        /// </summary>
        /// <param name="kw">Kennwort</param>
        /// <returns>True if Kennwort is longer than const KennwortMaxLength</returns>
        public static bool KennwortToLong(string kw)
        {
            if (!String.IsNullOrEmpty(kw))
            {
                if (kw.Length > KennwortMaxLength)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the input includes invalid characters
        /// Valid chars: A-Za-z0-9
        /// </summary>
        /// <param name="kw">kennwort</param>
        /// <returns>True if kennwort includes invalid characters (KennwortValidChars)</returns>
        public static bool KennwortInvalidChars(string kw)
        {
            if (!String.IsNullOrEmpty(kw))
            {
                if (KennwortValidChars.IsMatch(kw))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the kennwort already exist somewhere
        /// </summary>
        /// <param name="kw">kennwort</param>
        /// <returns>True if the kennwort got found somewhere n Kennwort-Datatable</returns>
        public static bool KennwortExist(string kw)
        {
            if (!String.IsNullOrEmpty(kw))
            {
                try
                {
                    using (Models.SecondNamespace.TestSiteEntities2 database = new Models.SecondNamespace.TestSiteEntities2())
                    {
                        int? userID = database.Kennwort
                            .Where(i => i.Kennwort1 == kw)
                            .Select(i => i.UserID)
                            .FirstOrDefault();

                        if (userID == null)
                        {
                            return false;
                        }
                    }
                }
                catch { }
            }
            return true;
        }

        /// <summary>
        /// Checks if the password is to short
        /// </summary>
        /// <param name="pw">password</param>
        /// <returns>True if the password is null or shorten than const PasswortMinLength</returns>
        public static bool PasswortToShort(string pw)
        {
            if (!String.IsNullOrEmpty(pw))
            {
                if (pw.Length >= PasswortMinLength)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the passwort is to long
        /// </summary>
        /// <param name="pw">password</param>
        /// <returns>True if the password is longer than const PasswortMaxLength</returns>
        public static bool PasswortToLong(string pw)
        {
            if (!String.IsNullOrEmpty(pw))
            {
                if (pw.Length >= PasswortMaxLength)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the password includes invalid chars
        /// Valid chars: A-Za-z0-9"!§$%&/()[]{}+-*_<>|#;.~]*$
        /// </summary>
        /// <param name="pw">password</param>
        /// <returns>True if the password is null or includes any invalid char</returns>
        public static bool PasswordInvalidChars(string pw)
        {
            if (!String.IsNullOrEmpty(pw))
            {
                if (PasswortValidChars.IsMatch(pw))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if any account exist with this kennwort
        /// </summary>
        /// <param name="kw">kennwort</param>
        /// <returns>True if null or any account got found</returns>
        public static bool AccountExist(string kw)
        {
            if (!String.IsNullOrEmpty(kw))
            {
                try
                {
                    using (Models.SecondNamespace.TestSiteEntities2 database = new Models.SecondNamespace.TestSiteEntities2())
                    {
                        int? userID = database.Kennwort
                            .Where(i => i.Kennwort1 == kw && i.Deleted == false)
                            .Select(i => i.UserID)
                            .FirstOrDefault();

                        if (userID != null)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                catch { }
            }
            return true;
        }

        /// <summary>
        /// Checks if the user entered an old password
        /// </summary>
        /// <param name="kw">kennword</param>
        /// <param name="pw">password</param>
        /// <returns>True if it is an old password of this account, false if any parameter is null or empty or its not a old password</returns>
        public static bool IsOldPassword (string kw, string pw)
        {
            if (!String.IsNullOrEmpty(kw) && !String.IsNullOrEmpty(pw))
            {
                try
                {
                    using (Models.SecondNamespace.TestSiteEntities2 database = new Models.SecondNamespace.TestSiteEntities2())
                    {
                        int? userID = database.Kennwort
                                .Where(i => i.Kennwort1 == kw && i.Deleted == false)
                                .Select(i => i.UserID)
                                .FirstOrDefault();

                        if (userID != null)
                        {
                            string dbSalt = database.Salt.Where(i => i.UserID == userID && i.Deleted == false).Select(i => i.Salt1).FirstOrDefault();
                            List<string> dbPasswords = database.Password.Where(i => i.UserID == userID && i.Deleted == true).Select(i => i.Passwort).ToList();

                            if (!String.IsNullOrEmpty(dbSalt)) {
                                byte[] salt = Convert.FromBase64String(dbSalt);

                                if (salt != null && salt.Length > 0)
                                {
                                    bool foundInactivePassword = false;

                                    dbPasswords.ForEach((item) =>
                                    {
                                        if (!String.IsNullOrEmpty(item))
                                        {
                                            byte[] bpw = Generator.CreateHash(pw, salt);
                                            if (bpw != null && bpw.Length > 0)
                                            {
                                                string hashPW = Convert.ToBase64String(bpw);

                                                if (!String.IsNullOrEmpty(hashPW) && !String.IsNullOrEmpty(item))
                                                {
                                                    if (hashPW == item)
                                                    {
                                                        foundInactivePassword = true;
                                                    }
                                                }
                                            }
                                        }
                                    });
                                    return foundInactivePassword;
                                }
                            }
                        } else
                        {
                            throw new NoAccountFoundException();
                        }
                    }
                }
                catch { }
            }
            return false;
        }

        /// <summary>
        /// Register User Method
        /// </summary>
        /// <param name="kw">kennwort</param>
        /// <param name="pw">password</param>
        /// <returns>True if data is successfull saved, false if failed</returns>
        public static bool RegisterUser(string kw, string pw)
        {
            byte[] salt = Generator.CreateSalt();
            if (salt != null && salt.Length > 0)
            {
                byte[] bpw = Generator.CreateHash(pw, salt);
                if (bpw != null && bpw.Length > 0)
                {
                    string newPW = Convert.ToBase64String(bpw);
                    string newSalt = Convert.ToBase64String(salt);

                    if (!String.IsNullOrEmpty(newPW) && !String.IsNullOrEmpty(newSalt))
                    {
                        try
                        {
                            using (Models.SecondNamespace.TestSiteEntities2 db = new Models.SecondNamespace.TestSiteEntities2())
                            {
                                Models.SecondNamespace.User USER = new Models.SecondNamespace.User();
                                Models.SecondNamespace.Salt SALT = new Models.SecondNamespace.Salt();
                                Models.SecondNamespace.Password PASSWORD = new Models.SecondNamespace.Password();
                                Models.SecondNamespace.Kennwort KENNWORT = new Models.SecondNamespace.Kennwort();

                                PASSWORD.UserID = USER.ID;
                                PASSWORD.Passwort = newPW;

                                SALT.UserID = USER.ID;
                                SALT.Salt1 = newSalt;

                                KENNWORT.UserID = USER.ID;
                                KENNWORT.Kennwort1 = kw;

                                db.User.Add(USER);
                                db.Password.Add(PASSWORD);
                                db.Salt.Add(SALT);
                                db.Kennwort.Add(KENNWORT);

                                db.SaveChanges();
                            }
                            return true;
                        }
                        catch { }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the user can log in
        /// </summary>
        /// <param name="kw">kennwort</param>
        /// <param name="pw">password</param>
        /// <returns>True if successfull logged in, false if failed</returns>
        public static bool LoginUser(string kw, string pw)
        {
            string dbSalt;
            string dbPassword;
            try
            {
                using (Models.SecondNamespace.TestSiteEntities2 db = new Models.SecondNamespace.TestSiteEntities2())
                {
                    int? userID = db.Kennwort.Where(i => i.Kennwort1 == kw && i.Deleted == false).Select(i => i.UserID).FirstOrDefault();

                    if (userID != null)
                    {
                        dbSalt = db.Salt.Where(i => i.UserID == userID && i.Deleted == false).Select(i => i.Salt1).FirstOrDefault();
                        dbPassword = db.Password.Where(i => i.UserID == userID && i.Deleted == false).Select(i => i.Passwort).FirstOrDefault();

                        if (!String.IsNullOrEmpty(dbSalt))
                        {
                            byte[] salt = Convert.FromBase64String(dbSalt);
                            byte[] bpw = Generator.CreateHash(pw, salt);

                            if (bpw != null && bpw.Length > 0)
                            {
                                string hashPW = Convert.ToBase64String(bpw);

                                if (!String.IsNullOrEmpty(hashPW) && !String.IsNullOrEmpty(dbPassword))
                                {
                                    if (hashPW == dbPassword)
                                    {
                                        return Cookie.CreateUserLogin(kw, pw);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }
    }
}