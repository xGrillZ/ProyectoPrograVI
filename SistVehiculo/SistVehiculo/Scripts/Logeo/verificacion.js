///document on ready del view Registro de Personas
$(function () {
    creaValidaciones();
});

///crea las validaciones para el formulario
function creaValidaciones() {

    $("#frmClienteLogin").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            correoElectronico: {
                required: true,
                email: true
            },
            password: {
                required: true,
                maxlength: 500,
                minlength: 1
            },
        }
    });
}

