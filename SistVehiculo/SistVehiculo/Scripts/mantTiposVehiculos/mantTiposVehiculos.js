$(function () {
    validacionRegistro();
    validacionModifica();
});

///crea las validaciones para el formulario
function validacionRegistro() {
    $("#frmNuevoTipoVehiculo").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            codigo: {
                required: true,
                maxlength: 50
            },
            tipo: {
                required: true,
                maxlength: 50
            },
        }
    });
}

///crea las validaciones para el formulario
function validacionModifica() {
    $("#frmModificaServicioProducto").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            codigo: {
                required: true,
                maxlength: 50
            },
            descripcion: {
                required: true,
                maxlength: 50
            },
            precio: {
                required: true,
                number: true,
                max: 5000000,
                min: 500
            },
            tipo: {
                required: true
            },
        }
    });
}