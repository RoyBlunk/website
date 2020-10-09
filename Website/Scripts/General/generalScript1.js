
$("form#registerForm").on("submit", (event) => {
    let kennwort = $("#kennwortRegisterInput").val();
    let passwort = $("#passwortRegisterInput").val();

    if (kennwort != null && kennwort.length > 0 && passwort != null && passwort.length > 0) {
        $.ajax({
            url: "/Services/General/RegisterLogin.svc/RegisterUser",
            method: "GET", contentType: "application/json; charset=utf-8", dataType: "json",
            async: false,
            data: {
                kw: kennwort,
                pw: passwort
            },
            success: (e) => {
                if (e.d == "true") {
                    alert("Erfolgreich registiert...");
                } else if (e.d == "false") {
                    alert("Es konnte kein Account angelegt werden.")
                } else {
                    alert(e.d)
                    clearFields("Register", event);
                }
            },
            error: () => {
                alert("Beim Registrieren ist ein Fehler aufgetreten.");
                clearFields("Register", event);
            }
        });
    } else {
        alert("KW und PW überprüfen");
        clearFields(null, event);
    }
});

$("form#loginForm").submit((event) => {
    let kennwort = $("#kennwortLoginInput").val();
    let passwort = $("#passwortLoginInput").val();

    if (kennwort != null && kennwort.length > 0 && passwort != null && passwort.length > 0) {
        $.ajax({
            url: "/Services/General/RegisterLogin.svc/LoginUser",
            method: "GET", contentType: "application/json; charset=utf-8", dataType: "json",
            async: false,
            data: {
                kw: kennwort,
                pw : passwort
            },
            success: (e) => {
                if (e.d) {
                    alert("Erfolgreich eingeloggt...");
                } else {
                    alert("Es wurde kein Account mit diesen Daten gefunden.");
                    clearFields("Login", event);
                }
            },
            error: () => {
                alert("Es ist ein Fehler beim Einloggen aufgetreten.");
                clearFields("Login", event);
            }
        });
    } else {
        alert("KW und PW überprüfen");
        clearFields(null, event);
    }
});

function clearFields(type, event) {
    if (type != null) {
        switch (type) {
            case "Login":
                $("#kennwortLoginInput").val("");
                $("#passwortLoginInput").val("");
                break;
            case "Register":
            default:
                $("#kennwortRegisterInput").val("");
                $("#passwortRegisterInput").val("");
                break;
        }
    }

    if (event != null) {
        event.preventDefault();
    }
}