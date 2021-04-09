$(function () {
    validacionRegistro();
    validacionModifica();
});

///crea las validaciones para el formulario
function validacionRegistro() {
    $("#frmNuevoVehiculoCliente").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            idCliente: {
                required: true
            },
            idVehiculo: {
                required: true
            },
        }
    });
}

///crea las validaciones para el formulario
function validacionModifica() {
    $("#frmModificaVehiculo").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            idCliente: {
                required: true
            },
            idVehiculo: {
                required: true
            },
        }
    });
}