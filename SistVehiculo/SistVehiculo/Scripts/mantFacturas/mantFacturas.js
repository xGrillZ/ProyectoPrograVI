$(function () {
    ///Llamada al método para seleccionar Clientes
    estableceEventosChangeCliente();
    cargaDropdownListCliente();
});

//función que registrará los eventos necesarios para "monitorear"
//cuando se ejecute el método change de las respectivas listas
function estableceEventosChangeCliente() {
    ///Evento change de la lista de clientes
    $("#nomCliente").change(function () {
        ///Obtener el ID del cliente seleccionado
        var cliente = $("#nomCliente").val();
        ///Funcion que permitie cargar todos los vehículos
        ///del cliente seleccionado
        cargaDropdownListPlacaVehiculo(cliente);
        cargaDropdownListDatosCliente(cliente);
    });

    ///Evento change de la lista de PlacaVehiculo
    $("#placaVehiculo").change(function () {
        ///Obtener el ID del canton seleccionado
        var placaVehiculo = $("#placaVehiculo").val();
        ///Funcion que permite cargar todos los distritos asociados al cantón seleccionado
        cargaDropdownListMarcaVehiculo(placaVehiculo);
    });

    ///Evento change de la lista de MarcaVehiculo
    $("#marcaVehiculo").change(function () {
        ///Obtener el ID del canton seleccionado
        var marcaVehiculo = $("#marcaVehiculo").val();
        ///Funcion que permite cargar todos los distritos asociados al cantón seleccionado
        cargaDropdownListTipoVehiculo(marcaVehiculo);
    });
}

///carga los registros de los Clientes
function cargaDropdownListCliente() {
    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaClientes'; /*Controlador/Metodo*/
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

///Toma el resultado del método RetornaClientes
///y lo procesa, recorriendo cada posición
function procesarResultadoClientes(data) {
    ///Mediante un selector nos posicionamos sobre la lista de provincias
    var ddlCliente = $("#nomCliente");

    ///Limpiamos todas las opcionesd e la lista de provincias
    ddlCliente.empty();

    ///Creación de la primera opcion de la lista, con un valor vacio y texto de selección
    var nuevaOpcion = "<option value=''>Selecciona un Cliente</option>";
    ///Asignacion de la opcion al dropdownlist
    ddlCliente.append(nuevaOpcion);
    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Provincia haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///provinciaActual.nombre nos retorna el nombre de la provincia
        var clienteActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + clienteActual.idCliente + "'>" + clienteActual.nomCliente + " " + clienteActual.ape1Cliente + " " + clienteActual.ape2Cliente + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlCliente.append(nuevaOpcion);
    });
}

///carga los registros de los vehículos
function cargaDropdownListPlacaVehiculo(pIdCliente) {

    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaPlacaVehiculo'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        idCliente: pIdCliente
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoPlacaVehiculos(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

function procesarResultadoPlacaVehiculos(data) {
    ///Declarar un selector donde nos posicionamos sobre la lista de cantones
    var ddlVehiculo = $("#placaVehiculo");
    ///Limpiamos todas las opciones de la lista de cantones
    ddlVehiculo.empty();
    ///Creación de la primera opción de la lista, con un valor vacio
    var nuevaOpcion = "<option value=''>Seleccione una placa vehículo</option>";
    ///Agregar la opción al dropdownlist
    ddlVehiculo.append(nuevaOpcion);

    if (Object.entries(data).length === 0) {
        ddlVehiculo.empty();
        var nuevaOpcion = "<option value=''>No tiene número de placa</option>";
        ddlVehiculo.append(nuevaOpcion);
    } else {
        ///Recorrido de los registros obtenidos
        $(data).each(function () {
            ///Obtenemos el objeto de tipo Canton haciendo uso de la claúsula "this"
            ///ahora podemos acceder a todas las propiedades por ejemplo
            ///cantonActual.nombre nos retorna el nombre del canton
            var placaVehiculoActual = this;
            ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
            nuevaOpcion = "<option value='" + placaVehiculoActual.placa + "'>" + placaVehiculoActual.placa + "</option>";
            ///Agregamos la opción al dropdownlist
            ddlVehiculo.append(nuevaOpcion);
        });
    }
}

///carga los registros de las marcas de vehículos
function cargaDropdownListMarcaVehiculo(pPlacaVehiculo) {

    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaMarcaVehiculo'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        placa: pPlacaVehiculo
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoMarcaVehiculo(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

function procesarResultadoMarcaVehiculo(data) {
    ///Declarar un selector donde nos posicionamos sobre la lista de cantones
    var ddlMarcaVehiculo = $("#marcaVehiculo");
    ///Limpiamos todas las opciones de la lista de cantones
    ddlMarcaVehiculo.empty();
    ///Creación de la primera opción de la lista, con un valor vacio
    var nuevaOpcion = "<option value=''>Seleccione una marca de vehículo</option>";
    ///Agregar la opción al dropdownlist
    ddlMarcaVehiculo.append(nuevaOpcion);

    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Canton haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///cantonActual.nombre nos retorna el nombre del canton
        var marcaVehiculoActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + marcaVehiculoActual.marca + "'>" + marcaVehiculoActual.marca + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlMarcaVehiculo.append(nuevaOpcion);
    });
}

///carga los registros de los tipos de vehículo
function cargaDropdownListTipoVehiculo(pMarcaVehiculo) {

    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaTipoVehiculo'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        marcaVehiculo: pMarcaVehiculo
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
    ///Declarar un selector donde nos posicionamos sobre la lista de cantones
    var ddlTipoVehiculo = $("#tipoVehiculo");
    ///Limpiamos todas las opciones de la lista de cantones
    ddlTipoVehiculo.empty();
    ///Creación de la primera opción de la lista, con un valor vacio
    var nuevaOpcion = "<option value=''>Seleccione un tipo de vehículo</option>";
    ///Agregar la opción al dropdownlist
    ddlTipoVehiculo.append(nuevaOpcion);

    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Canton haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///cantonActual.nombre nos retorna el nombre del canton
        var tipoVehiculoActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + tipoVehiculoActual.idTipoVehiculo + "'>" + tipoVehiculoActual.tipo + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlTipoVehiculo.append(nuevaOpcion);
    });
}

///carga los registros de los tipos de vehículo
function cargaDropdownListDatosCliente(pIdCliente) {

    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaClienteID'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        idCliente: pIdCliente
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoDatosCliente(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

function procesarResultadoDatosCliente(data) {
    ///Declarar un selector donde nos posicionamos sobre la lista de cantones
    var lblCorreo = $("#correo");
    var lblCedula = $("#numCedula");
    var lblTelefono = $("#pTelefono");

    ///Limpiamos todas las opciones de la lista de cantones
    lblCorreo.empty();
    lblCedula.empty();
    lblTelefono.empty();

    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Canton haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///cantonActual.nombre nos retorna el nombre del canton
        var tipoDatosClienteActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = tipoDatosClienteActual.email;
        lblCorreo.val(nuevaOpcion);

        nuevaOpcionDos = tipoDatosClienteActual.numCedula;
        lblCedula.val(nuevaOpcionDos);

        nuevaOpcionTres = tipoDatosClienteActual.pTelefono;
        lblTelefono.val(nuevaOpcionTres);
    });
}