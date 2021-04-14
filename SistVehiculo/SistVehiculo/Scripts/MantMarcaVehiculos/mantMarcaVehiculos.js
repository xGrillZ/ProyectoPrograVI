$(function () {
    validacionRegistro();
    validacionModifica();

});

///crea las validaciones para el formulario
function validacionRegistro() {
    $("#frmNuevoMarcaVehiculo").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            codigo: {
                required: true,
                maxlength: 50
            },
            idPaisFabricante: {
                required: true,
                maxlength: 50
            },
            marca: {
                required: true,
                maxlength: 50
            },
        },
    });
}



///crea las validaciones para el formulario
function validacionModifica() {
    $("#frmModificaMarcaVehiculo").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            codigo: {
                required: true,
                maxlength: 50
            },
            idPaisFabricante: {
                required: true,
                maxlength: 50
            },
            marca: {
                required: true,
                maxlength: 50
            },
        },
        });
}
