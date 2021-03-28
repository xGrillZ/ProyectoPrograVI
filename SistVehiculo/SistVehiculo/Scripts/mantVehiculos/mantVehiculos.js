$(function () {
    validacionRegistro();
    validacionModifica();
});

///crea las validaciones para el formulario
function validacionRegistro() {
    $("#frmNuevoVehiculo").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            placa: {
                required: true,
                maxlength: 50
            },
            numeroPuerta: {
                required: true,
                maxlength: 50
            },
            numeroRuedas: {
                required: true,
            },
            tipoVehiculo: {
                required: true,
            },
            marcaVehiculo: {
                required: true,
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
            marca: {
                required: true,
                maxlength: 100
            },
            tipo: {
                required: true,
                
            },
            tipoVeh: {
                required: true,
                
            },
            numeroPuerta: {
                required: true,
                number: true,
            },
            numeroRueda: {
                required: true,
                number: true,
            }, 

        }
    });
}