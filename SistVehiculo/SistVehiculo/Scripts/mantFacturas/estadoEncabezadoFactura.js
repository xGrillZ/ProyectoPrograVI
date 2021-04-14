$(function () {
    validacionEstadoEncabezado();
});

/*
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('/');
}*/

/*function validacionEstadoEncabezado() {
    const cantDias = 15;

    var fechaEncabezado = $("#fecha").val();

    var fechaActualEdit = new Date();
    fechaActualEdit.setDate(fechaActualEdit.getDate()+cantDias);
    var fechaEdit = formatDate(fechaActualEdit);

    var fechaActual = new Date();
    var fecha = formatDate(fechaActual);

    if (fechaEncabezado == fecha) {
        alert("Encabezado No Vencido");
    } else if (fechaEncabezado <= fechaEdit) {
        alert("Encabezado Vencido");
    }

}*/

function validacionEstadoEncabezado() {
    var fechaEncabezado = $("#fecha").val();
    var fechaEditEncabezado = new Date(fechaEncabezado);

    var estado = $("#hdEstado").val();

    var fechaActual = new Date();

    var tiempo = fechaActual.getTime() - fechaEditEncabezado.getTime();
    var dias = Math.floor(tiempo / (1000 * 60 * 60 * 24));

    if (dias >= 15) {
        invocarMetodoInhabilitarEncabezadoFactura();

        /*alert("Encabezado Vencido");*/

        var ddlCliente = $("#numFactura");
        ddlCliente.attr("readonly", "readonly");

        var ddlCliente = $("#nomCliente");
        ddlCliente.attr("readonly", "readonly");

        var ddlPlacaVehiculo = $("#placaVehiculo");
        ddlPlacaVehiculo.attr("readonly", "readonly");

        var ddlMarcaVehiculo = $("#marcaVehiculo");
        ddlMarcaVehiculo.attr("readonly", "readonly");

        var ddlTipoVehiculo = $("#tipoVehiculo");
        ddlTipoVehiculo.attr("readonly", "readonly");

        var ddlEstado = $("#estado");
        ddlEstado.attr("readonly", "readonly");

        var botonModificar = $("#btnModificar");
        botonModificar.attr("disabled", "disabled");

        var botonDetalle = $("#btnDetalle");
        botonDetalle.attr("disabled", "disabled");

        var botonDetalleModifica = $("#btnMostrarDetalle");
        botonDetalleModifica.attr("disabled", "disabled");

        var botonElimina = $("#btnEliminar");
        botonElimina.attr("disabled", "disabled");

    } else if (estado == 1) {
        invocarMetodoInhabilitarEncabezadoFactura();

        /*alert("Encabezado Vencido");*/

        var ddlCliente = $("#numFactura");
        ddlCliente.attr("readonly", "readonly");

        var ddlCliente = $("#nomCliente");
        ddlCliente.attr("readonly", "readonly");

        var ddlPlacaVehiculo = $("#placaVehiculo");
        ddlPlacaVehiculo.attr("readonly", "readonly");

        var ddlMarcaVehiculo = $("#marcaVehiculo");
        ddlMarcaVehiculo.attr("readonly", "readonly");

        var ddlTipoVehiculo = $("#tipoVehiculo");
        ddlTipoVehiculo.attr("readonly", "readonly");

        var ddlEstado = $("#estado");
        ddlEstado.attr("readonly", "readonly");

        var botonModificar = $("#btnModificar");
        botonModificar.attr("disabled", "disabled");

        var botonDetalle = $("#btnDetalle");
        botonDetalle.attr("disabled", "disabled");

        var botonElimina = $("#btnEliminar");
        botonElimina.attr("disabled", "disabled");

        var botonDetalleModifica = $("#btnMostrarDetalle");
        botonDetalleModifica.attr("disabled", "disabled");
    }
    else {
        alert("Encabezado No Vencido");
    }

}

///se encarga de llamar al método del controlador y procesar el resultado
function invocarMetodoInhabilitarEncabezadoFactura() {
    /*Dirección a donde se enviarán los datos */
    var url = '/MantFacturas/ModificaEstadoOffEncabezadoFactura';
    /*Parámetros del método*/
    var parametros = {
        pId_factura: $("#hdIdFactura").val()
    };
    /*Invocación del método*/
    ///Este método puede ser reciclado AVERIGUAR COMO
    $.ajax({
        ///Dirección del método
        url: url,
        dataType: 'json', ///Formato en el que se envían y reciben los datos
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros), ///Parámetros convertidos en formato JSON
        ///Función que se ejecuta cuando ela respuesta fue satisfactoria
        ///data: contiene el valor retornado por el método del servidor
        success: function (data, textStatus, jQxhr) {
            procesarResultadoMetodoInhabilitarEncabezado(data);
        },
        ///Función que se ejecuta cuando la respuesta tuvo errores
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function procesarResultadoMetodoInhabilitarEncabezado(data) {
    ///Es .resultado porque la función devuelve
    ///un objeto JSON que posee una propiedad
    ///llamada resultado 
    var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
    alert("Información: " + resultadoFuncion);
}