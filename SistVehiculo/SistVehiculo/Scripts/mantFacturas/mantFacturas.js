$(function () {
    ///Llamada al método para seleccionar Clientes
    estableceEventosChangeCliente();
    cargaDropdownListCliente();
    cargaDropdownListEstado();
    datePickerRegistroFactura();
    validacionRegistro();
    creaEventoFormularioEncabezado();
    creaEventoDialogDetalle();
    cargaDropdownListServicioProducto();
    validacionIngresaDetalle();
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
        cargaDropdownListMarcaVehiculo(cliente);
        cargaDropdownListDatosCliente(cliente);
    });

    ///Evento change de la lista de PlacaVehiculo
    $("#placaVehiculo").change(function () {
        ///Obtener el ID del canton seleccionado
        var placaVehiculo = $("#placaVehiculo").val();
        ///Funcion que permite cargar todos los distritos asociados al cantón seleccionado
        /*cargaDropdownListMarcaVehiculo(placaVehiculo);*/
        cargaDropdownListTipoVehiculo(placaVehiculo);
    });

    ///Evento change de la lista de MarcaVehiculo
    $("#marcaVehiculo").change(function () {
        ///Obtener el ID del canton seleccionado
        var marcaVehiculo = $("#marcaVehiculo").val();
        ///Funcion que permite cargar todos los distritos asociados al cantón seleccionado
        /*cargaDropdownListTipoVehiculo(marcaVehiculo);*/
        cargaDropdownListPlacaVehiculo(marcaVehiculo);

    });

    ///Evento change de la lista de ServiciosProductos
    $("#servicioProducto").change(function () {
        ///Obtener el ID del canton seleccionado
        var servicioProducto = $("#servicioProducto").val();
        ///Funcion que permite cargar todos los distritos asociados al cantón seleccionado
        cargaDropdownListDatosServiciosProductos(servicioProducto);
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

    ///Obtiene el valor del hidden
    var hiddenCliente = $("#hdIdCliente").val();

    if (hiddenCliente != undefined) {
        ddlCliente.val(hiddenCliente);
        cargaDropdownListPlacaVehiculo(hiddenCliente);
    }
}

///carga los registros de las marcas de vehículos
function cargaDropdownListMarcaVehiculo(pIdCliente) {

    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaMarcaVehiculo'; /*Controlador/Metodo*/
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
        nuevaOpcion = "<option value='" + marcaVehiculoActual.idVehiculo + "'>" + marcaVehiculoActual.marca + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlMarcaVehiculo.append(nuevaOpcion);
    });

    ///Obtiene el valor del hidden
    var hiddenMarca = $("#hdMarca").val();

    if (hiddenMarca != undefined) {
        ddlMarcaVehiculo.val(hiddenMarca);
        cargaDropdownListTipoVehiculo(hiddenMarca);
    }
}

///carga los registros de los vehículos
function cargaDropdownListPlacaVehiculo(pIdVehiculo) {

    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaPlacaVehiculo'; /*Controlador/Metodo*/
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
            nuevaOpcion = "<option value='" + placaVehiculoActual.tipoVehiculo + "'>" + placaVehiculoActual.placa + "</option>";
            ///Agregamos la opción al dropdownlist
            ddlVehiculo.append(nuevaOpcion);
        });

    }
    ///Obtiene el valor del hidden
    var hiddenPlaca = $("#hdPlaca").val();

    if (hiddenPlaca != undefined) {
        ddlVehiculo.val(hiddenPlaca);
        cargaDropdownListMarcaVehiculo(hiddenPlaca);
    }
}

///carga los registros de los tipos de vehículo
function cargaDropdownListTipoVehiculo(pIdTipoVehiculo) {

    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaTipoVehiculo'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        idTipoVehiculo: pIdTipoVehiculo
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
        nuevaOpcion = "<option value='" + tipoVehiculoActual.tipoVehiculo + "'>" + tipoVehiculoActual.tipo + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlTipoVehiculo.append(nuevaOpcion);
    });

    ///Obtiene el valor del hidden
    var hiddenTipoVehiculo = $("#hdMarca").val();

    if (hiddenTipoVehiculo != undefined) {
        ddlTipoVehiculo.val(hiddenTipoVehiculo);
    }
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

    ///Recorrido de los registros obtenidosasdas
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

function datePickerRegistroFactura() {
    $("#fecha").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: "c-70:c", ///Modificar la fecha para colocar la actual y fecha hacia atrás c significa año actual
        dateFormat: "yy/mm/dd" ///Formato de fecha para SQL
    });
}

function cargaDropdownListEstado() {
    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaEstadoFactura'; /*Controlador/Metodo*/
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
            procesarResultadoEstadoFactura(data);
        },
        error: function (jQxhr, textStatus, errorThrown) { /*Ejecución de la funcion si funciona incorrectamente*/
            alert(errorThrown);
        },
    });
}

///Toma el resultado del método RetornaClientes
///y lo procesa, recorriendo cada posición
function procesarResultadoEstadoFactura(data) {
    ///Mediante un selector nos posicionamos sobre la lista de provincias
    var ddlEstado = $("#estado");

    ///Limpiamos todas las opcionesd e la lista de provincias
    ddlEstado.empty();

    ///Creación de la primera opcion de la lista, con un valor vacio y texto de selección
    var nuevaOpcion = "<option value=''>Selecciona el Estado</option>";
    ///Asignacion de la opcion al dropdownlist
    ddlEstado.append(nuevaOpcion);
    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Provincia haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///provinciaActual.nombre nos retorna el nombre de la provincia
        var estadoActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + estadoActual.id_estado + "'>" + estadoActual.nomEstado + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlEstado.append(nuevaOpcion);
    });

    ///Obtiene el valor del hidden
    var hiddenEstado = $("#hdEstado").val();

    if (hiddenEstado != undefined) {
        ddlEstado.val(hiddenEstado);
    }
}

///crea las validaciones para el formulario
function validacionRegistro() {
    $("#frmNuevoEncabezadoFactura").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            numFactura: {
                required: true,
                maxlength: 150
            },
            nomCliente: {
                required: true
            },
            correo: {
                required: true
            },
            numCedula: {
                required: true
            },
            pTelefono: {
                required: true
            },
            placaVehiculo: {
                required: true
            },
            marcaVehiculo: {
                required: true
            },
            tipoVehiculo: {
                required: true
            },
            fecha: {
                required: true
            },
            estado: {
                required: true
            }
        }
    });
}

///crea las validaciones para el formulario
function validacionModificaEncabezado() {
    $("#frmModificarEncabezadoFactura").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            numFactura: {
                required: true,
                maxlength: 150
            },
            nomCliente: {
                required: true
            },
            correo: {
                required: true
            },
            numCedula: {
                required: true
            },
            pTelefono: {
                required: true
            },
            placaVehiculo: {
                required: true
            },
            marcaVehiculo: {
                required: true
            },
            tipoVehiculo: {
                required: true
            },
            fecha: {
                required: true
            },
            estado: {
                required: true
            },
        }
    });
}

function validacionIngresaDetalle() {
    $("#frmAgregaDetalleFactura").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            codigo: {
                required: true,
                maxlength: 150
            },
            servicioProducto: {
                required: true
            },
            tipo: {
                required: true
            },
            precio: {
                required: true,
                number: true
            },
            cantidad: {
                required: true,
                number: true
            },
        }
    });
}

/* Permite realizar una acción con el evento click */
function creaEventoFormularioEncabezado() {
    $("#btnRegistrar").on("click", function () {
        /*Asignar a la variable formulario
          el resultado del selector*/
        var formulario = $("#frmNuevoEncabezadoFactura");
        /*Ejecutar el método de validación*/
        formulario.validate();
        /*Si el formulario es valido, proceder a
         ejecutar la función invocarMetodoPost*/
        if (formulario.valid()) {
            invocarMetodoPostEncabezadoFactura();
        }
    });

    $("#btnModificar").on("click", function () {
        /*Asignar a la variable formulario
          el resultado del selector*/
        var formulario = $("#frmModificarEncabezadoFactura");
        /*Ejecutar el método de validación*/
        formulario.validate();
        /*Si el formulario es valido, proceder a
         ejecutar la función invocarMetodoPost*/
        if (formulario.valid()) {
            invocarMetodoPostModificaEncabezadoFactura();
        }
    });
}

///se encarga de llamar al método del controlador y procesar el resultado
function invocarMetodoPostEncabezadoFactura() {
    /*Dirección a donde se enviarán los datos */
    var url = '/MantFacturas/InsertaEncabezadoFactura';
    /*Parámetros del método*/
    var parametro = {
        Num_factura: $("#numFactura").val(),
        Fecha: $("#fecha").val(),
        MontoTotal: $("#montoTotal").val(),
        Estado: $("#estado").val(),
        IdCliente: $("#nomCliente").val(),
        IdVehiculo: $("#marcaVehiculo").val(),
        IdTipoVehiculo: $("#tipoVehiculo").val()
    };
    /*Invocación del método*/
    ///Este método puede ser reciclado AVERIGUAR COMO
    $.ajax({
        ///Dirección del método
        url: url,
        dataType: 'json', ///Formato en el que se envían y reciben los datos
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametro), ///Parámetros convertidos en formato JSON
        ///Función que se ejecuta cuando ela respuesta fue satisfactoria
        ///data: contiene el valor retornado por el método del servidor
        success: function (data, textStatus, jQxhr) {
            procesarResultadoMetodoEncabezado(data);
        },
        ///Función que se ejecuta cuando la respuesta tuvo errores
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function procesarResultadoMetodoEncabezado(data) {
    ///Es .resultado porque la función devuelve
    ///un objeto JSON que posee una propiedad
    ///llamada resultado 
    var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
    alert("Información: " + resultadoFuncion);
}

///se encarga de llamar al método del controlador y procesar el resultado
function invocarMetodoPostModificaEncabezadoFactura() {
    /*Dirección a donde se enviarán los datos */
    var url = '/MantFacturas/ModificaFactura';
    /*Parámetros del método*/
    var parametros = {
        Id_factura: $("#hdIdFactura").val(),
        Num_factura: $("#numFactura").val(),
        Fecha: $("#fecha").val(),
        MontoTotal: $("#montoTotal").val(),
        Estado: $("#estado").val(),
        IdCliente: $("#hdIdCliente").val(),
        IdVehiculo: $("#placaVehiculo").val(),
        idTipoVehiculo: $("#tipoVehiculo").val()
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
            procesarResultadoMetodoModificaEncabezado(data);
        },
        ///Función que se ejecuta cuando la respuesta tuvo errores
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function procesarResultadoMetodoModificaEncabezado(data) {
    ///Es .resultado porque la función devuelve
    ///un objeto JSON que posee una propiedad
    ///llamada resultado 
    var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
    alert("Información: " + resultadoFuncion);
}

function creaEventoDialogDetalle() {
    ///creamos el div divDialog como elemento de tipo Dialog
    crearDialog();
    ///evento click del botón btMostrarDialog          
    $("#btnMostrarDetalle").click(function () {
        $("#divDialogDetalleFactura").dialog("open");
    });
    //evento click del botón btCerrar   
    $("#btCerrarDetalle").click(function () {
        $("#divDialogDetalleFactura").dialog("close");
    });
}

///Funcion que crea un dialog
function crearDialog() {
    $("#divDialogDetalleFactura").dialog({
        autoOpen: false,
        height: 610,
        width: 800,
        modal: true,
        title: "Ingresar detalle factura",
        resizable: false
    });
}

///carga los registros de los Clientes
function cargaDropdownListServicioProducto() {
    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaServicioProducto'; /*Controlador/Metodo*/
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
            procesarResultadoServicioProducto(data);
        },
        error: function (jQxhr, textStatus, errorThrown) { /*Ejecución de la funcion si funciona incorrectamente*/
            alert(errorThrown);
        },
    });
}

///Toma el resultado del método RetornaClientes
///y lo procesa, recorriendo cada posición
function procesarResultadoServicioProducto(data) {
    ///Mediante un selector nos posicionamos sobre la lista de provincias
    var ddlServicioProducto = $("#servicioProducto");

    ///Limpiamos todas las opcionesd e la lista de provincias
    ddlServicioProducto.empty();

    ///Creación de la primera opcion de la lista, con un valor vacio y texto de selección
    var nuevaOpcion = "<option value=''>Selecciona un Servicio o Producto</option>";
    ///Asignacion de la opcion al dropdownlist
    ddlServicioProducto.append(nuevaOpcion);
    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Provincia haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///provinciaActual.nombre nos retorna el nombre de la provincia
        var servicioProductoActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + servicioProductoActual.idTipoServicioProducto + "'>" + servicioProductoActual.descripcion + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlServicioProducto.append(nuevaOpcion);
    });
}

///carga los registros de los tipos de vehículo
function cargaDropdownListDatosServiciosProductos(pIdServicioProducto) {

    ///dirección a donde se enviarán los datos
    var url = '/MantFacturas/RetornaServicioProductoID'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        idTipoServicioProducto: pIdServicioProducto
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoDatosServiciosProducto(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

function procesarResultadoDatosServiciosProducto(data) {
    ///Declarar un selector donde nos posicionamos sobre la lista de cantones
    var lblCodigo = $("#codigo");
    var lblTipo = $("#tipo");
    var lblPrecio = $("#precio");

    ///Limpiamos todas las opciones de la lista de cantones
    lblCodigo.empty();
    lblTipo.empty();
    lblPrecio.empty();

    ///Recorrido de los registros obtenidosasdas
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Canton haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///cantonActual.nombre nos retorna el nombre del canton
        var datosServicioProductoActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = datosServicioProductoActual.codigo;
        lblCodigo.val(nuevaOpcion);

        nuevaOpcionDos = datosServicioProductoActual.tipo;
        lblTipo.val(nuevaOpcionDos);

        nuevaOpcionTres = datosServicioProductoActual.precio;
        lblPrecio.val(nuevaOpcionTres);
    });
}