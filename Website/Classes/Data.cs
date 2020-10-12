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

                        if (userID == null || userID == 0)
                        {
                            return false;
                        }
                    }
                }
                catch { }
            }
            return true;
        }

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

                        if (userID != null && userID != 0)
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

        public static bool IsOldPassword(string kw, string pw)
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

                        if (userID != null && userID != 0)
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
                        } 
                    }
                }
                catch { }
            }
            return false;
        }

        private static bool CheckPasswords(string pw1, string pw2)
        {
            if (!String.IsNullOrEmpty(pw1) && !String.IsNullOrEmpty(pw2))
            {

            }
            return false;
        }

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

        public static ResultState LoginUser(string kw, string pw)
        {
            ResultState state = new ResultState();

            string dbSalt;
            string dbPassword;
            try
            {
                using (Models.SecondNamespace.TestSiteEntities2 db = new Models.SecondNamespace.TestSiteEntities2())
                {
                    int? userID = db.Kennwort.Where(i => i.Kennwort1 == kw && i.Deleted == false).Select(i => i.UserID).FirstOrDefault();

                    if (userID != null && userID != 0)
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

                                if (!String.IsNullOrEmpty(hashPW))
                                {
                                    if (!String.IsNullOrEmpty(dbPassword))
                                    {
                                        if (hashPW == dbPassword)
                                        {
                                            return Cookie.CreateUserLogin(kw, pw);
                                        }
                                        else
                                        {
                                            state.SetState(false, 17);
                                        }
                                    }
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
            catch {
                state.SetState(false, 5);
            }
            return state;
        }
    }

    public class ResultState
    {
        private IDictionary<int, string> Messages = new Dictionary<int, string>()
        {
            { 0, "Es wurde keine Fehlermeldung angegeben." },
            { 1, "Das Kennwort enthält keinen Wert." },
            { 2, "Das Passwort enthält keinen Wert." },
            { 3, "Mit diesem Kennwort wurde kein aktiviertes Konto gefunden." },
            { 4, "Sie haben ein altes Passwort benutzt." },
            { 5, "Beim einloggen ist ein Fehler aufgetreten." },
            { 6, "Der Login ist erfolgreich."},
            { 7, "Das Kennwort unterschreitet die minimale Zeichenlänge." },
            { 8, "Das Kennwort überschreitet die maximale Zeichenlänge." },
            { 9, "Das Kennwort enthält unzulässige Zeichen." },
            { 10, "Das Kennwort ist bereits mit einem anderem Konto verknüpft." },
            { 11, "Das Passwort unterschreitet die minimale Zeichenlänge." },
            { 12, "Das Passwort überschreitet die maximale Zeichenlänge." },
            { 13, "Das Passwort enthält unzulässige Zeichen." },
            { 14, "Beim registrieren ist ein Fehler aufgetreten." },
            { 15, "Die Registrierung ist erfolgreich." },
            { 16, "Es ist ein Datenbankfehler aufgetreten." },
            { 17, "Das Passwort ist nicht korrekt." },
            { 18, "Beim Erstellen des Cookie ist ein Fehler aufgetreten." }
        };

        public bool Success { get; set; }
        public string Message { get; set; }

        public ResultState()
        {
            this.Success = false;
            this.Message = Messages[0];
        }

        public ResultState(bool value, int messageIndex)
        {
            SetState(value, messageIndex);
        }

        public void SetState(bool value, int messageIndex)
        {
            this.Success = value;
            this.Message = Messages[messageIndex];
        }
    }
}