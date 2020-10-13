using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Classes
{
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