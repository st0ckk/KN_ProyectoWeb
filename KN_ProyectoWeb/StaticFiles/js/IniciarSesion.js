$(function () {

    $("#FormIniciarSesion").validate({
        rules: {
            CorreoElectronico: {
                required: true,
                email: true
            },
            Contrasenia: {
                required: true
            }
        },
        messages: {
            CorreoElectronico: {
                required: "* Requerido",
                email: "* Formato",
            },
            Contrasenia: {
                required: "* Requerido",
            }
        }
    });

});