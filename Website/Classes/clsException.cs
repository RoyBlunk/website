using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Classes
{
     public class KennwortToShortException : Exception
    {
        public KennwortToShortException() : base("Das Kennwort ist zu kurz.") { }
    }

    public class KennwortToLongException : Exception
    {
        public KennwortToLongException() : base("Das Kennwort ist zu lang.") { }
    }

    public class KennwortInvalidCharsException : Exception
    {
        public KennwortInvalidCharsException() : base("Das Kennwort enthält unzulässige Zeichen.") { }
    }

    public class KennwortExistException : Exception
    {
        public KennwortExistException() : base("Das Kennwort wird bereits verwendet.") { }
    }

    public class KennwortExistedException : Exception
    {
        public KennwortExistedException() : base("Das Kennwort wurde bereits von jemand anderem verwendet.") { }
    }

    public class KennwortAlreadyInUseException : Exception
    {
        public KennwortAlreadyInUseException() : base("Du benutzt dieses Kennwort schon.") { }
    }

    public class PasswordToShortException : Exception
    {
        public PasswordToShortException() : base("Das Password ist zu kurz.") { }
    }

    public class PasswordToLongException : Exception
    {
        public PasswordToLongException() : base("Das Password ist zu lang.") { }
    }

    public class PasswordInvalidCharsException : Exception
    {
        public PasswordInvalidCharsException() : base("Das Passwort enthält unzulässige Zeichen.") { }
    }

    public class NoAccountFoundException : Exception
    {
        public NoAccountFoundException() : base("Es existiert kein Account mit diesem Kennwort.") { }
    }

    public class OldPasswordException : Exception
    {
        public OldPasswordException() : base("Es existiert ein neueres Passwort.") { }
    }
}