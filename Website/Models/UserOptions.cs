//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Website.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserOptions
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public bool BlackTheme { get; set; }
        public bool ShowMass { get; set; }
        public bool NoNames { get; set; }
        public bool NoSkins { get; set; }
    
        public virtual User User { get; set; }
    }
}
