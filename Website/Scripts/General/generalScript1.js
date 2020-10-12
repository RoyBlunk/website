
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
                let result = JSON.parse(e.d);

                if (!result.Success) {
                    event.preventDefault();
                }

                console.log(result.Message);
            },
            error: () => {
                console.log("Beim Registrieren ist ein Fehler aufgetreten.");
                clearFields(true);
                event.preventDefault();
            }
        });
    } else {
        console.log("Bitte füllen Sie alle Felder korrekt aus.");
        event.preventDefault();
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
                let result = JSON.parse(e.d);

                if (!result.Success) {
                    event.preventDefault();
                }

                console.log(result.Message);
            },
            error: () => {
                console.log("Es ist ein Fehler beim Einloggen aufgetreten.");
                clearFields(false);
                event.preventDefault();
            }
        });
    } else {
        console.log("Bitte füllen Sie alle Felder korrekt aus.");
        event.preventDefault();
    }
});

function clearFields(register) {
    if (register != null) {
        switch (register) {
            case false:
                $("#kennwortLoginInput").val("");
                $("#passwortLoginInput").val("");
                break;
            case true:
            default:
                $("#kennwortRegisterInput").val("");
                $("#passwortRegisterInput").val("");
                break;
        }
    }
}