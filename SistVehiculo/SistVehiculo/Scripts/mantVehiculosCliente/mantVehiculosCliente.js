$(function () {
    validacionRegistro();
    validacionModifica();
    cargaDropdownListClientes();
    cargaDropdownListVehiculos();
    estableceEventosChange();
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
            idTipoVehiculo: {
                required: true
            },
        }
    });
}

///crea las validaciones para el formulario
function validacionModifica() {
    $("#frmModificaVehiculoCliente").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            idCliente: {
                required: true
            },
            idVehiculo: {
                required: true
            },
            tipoVehiculo: {
                required: true
            },
        }
    });
}

function estableceEventosChange() {
    ///Evento change de la lista de provincias
    $("#idVehiculo").change(function () {
        ///Obtener el ID de la provincia seleccionada
        var vehiculo = $("#idVehiculo").val();
        ///Funcion que permitie cargar todos los cantones asociados
        ///a la provincia seleccionada
        cargaDropdownListTipoVehiculo(vehiculo);
    });
}

///carga los registros de los clientes
function cargaDropdownListClientes() {
    ///dirección a donde se enviarán los datos
    var url = '/MantVehiculosCliente/RetornaClientes'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {};
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros), /*Envio de parametros por medio de una conversion*/
        success: function (data, textStatus, jQxhr) { /*Ejecución de la funcion si funciona correctamente*/
            procesarResultadoClientes(data);
        },
        error: function (jQxhr, textStatus, errorThrown) { /*Ejecución de la funcion si funciona incorrectamente*/
            alert(errorThrown);
        },
    });
}

function procesarResultadoClientes(data) {
    ///Mediante un selector nos posicionamos sobre la lista de provincias
    var ddlCliente = $("#idCliente");

    ///Limpiamos todas las opcionesd e la lista de provincias
    ddlCliente.empty();

    ///Creación de la primera opcion de la lista, con un valor vacio y texto de selección
    var nuevaOpcion = "<option value=''>Seleccione un Cliente</option>";
    ///Asignacion de la opcion al dropdownlist
    ddlCliente.append(nuevaOpcion);
    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Provincia haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///provinciaActual.nombre nos retorna el nombre de la provincia
        var clienteActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + clienteActual.idCliente + "'>" + clienteActual.nomCliente + " " + clienteActual.ape1Cliente + " " + clienteActual.ape2Cliente +"</option>";
        ///Agregamos la opción al dropdownlist
        ddlCliente.append(nuevaOpcion);
    });

    ///Obtiene el valor del hidden
    var hiddenCliente = $("#hdIdCliente").val();

    if (hiddenCliente != undefined) {
        ddlCliente.val(hiddenCliente);
    }
}

function cargaDropdownListVehiculos() {
    ///dirección a donde se enviarán los datos
    var url = '/MantVehiculosCliente/RetornaVehiculo'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {};
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros), /*Envio de parametros por medio de una conversion*/
        success: function (data, textStatus, jQxhr) { /*Ejecución de la funcion si funciona correctamente*/
            procesarResultadoVehiculo(data);
        },
        error: function (jQxhr, textStatus, errorThrown) { /*Ejecución de la funcion si funciona incorrectamente*/
            alert(errorThrown);
        },
    });
}

function procesarResultadoVehiculo(data) {
    ///Mediante un selector nos posicionamos sobre la lista de provincias
    var ddlVehiculo = $("#idVehiculo");

    ///Limpiamos todas las opcionesd e la lista de provincias
    ddlVehiculo.empty();

    ///Creación de la primera opcion de la lista, con un valor vacio y texto de selección
    var nuevaOpcion = "<option value=''>Seleccione un Vehículo</option>";
    ///Asignacion de la opcion al dropdownlist
    ddlVehiculo.append(nuevaOpcion);
    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Provincia haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///provinciaActual.nombre nos retorna el nombre de la provincia
        var vehiculoActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + vehiculoActual.idVehiculos + "'>" + vehiculoActual.marca + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlVehiculo.append(nuevaOpcion);
    });

    ///Obtiene el valor del hidden
    var hiddenVehiculos = $("#hdIdVehiculo").val();

    if (hiddenVehiculos != undefined) {
        ddlVehiculo.val(hiddenVehiculos);
        cargaDropdownListTipoVehiculo(hiddenVehiculos);
    }
}

function cargaDropdownListTipoVehiculo(pIdVehiculo){
    var url = '/MantVehiculosCliente/RetornaTipoVehiculo'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        idVehiculo: pIdVehiculo
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoTipoVehiculo(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

function procesarResultadoTipoVehiculo(data) {
    ///Mediante un selector nos posicionamos sobre la lista de provincias
    var ddlTipoVehiculo = $("#tipoVehiculo");
    ///Limpiamos todas las opcionesd e la lista de provincias
    ddlTipoVehiculo.empty();

    ///Creación de la primera opcion de la lista, con un valor vacio y texto de selección
    var nuevaOpcion = "<option value=''>Seleccione un Tipo de Vehículo</option>";
    ///Asignacion de la opcion al dropdownlist
    ddlTipoVehiculo.append(nuevaOpcion);
    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Provincia haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///provinciaActual.nombre nos retorna el nombre de la provincia
        var tipoVehiculoActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + tipoVehiculoActual.idTipoVehiculo + "'>" + tipoVehiculoActual.tipo + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlTipoVehiculo.append(nuevaOpcion);
    });

    ///Obtiene el valor del hidden
    var hdTipoVehiculo = $("#hdIdTipoVehiculo").val();

    if (hdTipoVehiculo != undefined) {
        ddlTipoVehiculo.val(hdTipoVehiculo);
    }
}

function creaEventoModificaVehiculoCliente() {
    $("#btnModificar").on("click", function () {
        /*Asignar a la variable formulario
          el resultado del selector*/
        var formulario = $("#frmModificaVehiculoCliente");
        /*Ejecutar el método de validación*/
        formulario.validate();
        /*Si el formulario es valido, proceder a
         ejecutar la función invocarMetodoPost*/
        if (formulario.valid()) {
            invocarMetodoPostModificar();
        }
    });
}

function invocarMetodoPostModificar() {
    /*Dirección a donde se enviarán los datos */
    var url = '/MantVehiculosCliente/ModificarVehiculosCliente';
    /*Parámetros del método*/
    var parametros = {
        idVehiculosCliente: $("#idVehiculosCliente").val(),
        idVehiculo: $("#idVehiculo").val(),
        idCliente: $("#idCliente").val(),
        idTipoVehiculo: $("#idTipoVehiculo").val()
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
            procesarResultadoMetodoModificarVehiculo(data);
        },
        ///Función que se ejecuta cuando la respuesta tuvo errores
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function procesarResultadoMetodoModificarVehiculo(data) {
    ///Es .resultado porque la función devuelve
    ///un objeto JSON que posee una propiedad
    ///llamada resultado 
    var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
    alert("Información: " + resultadoFuncion);
}