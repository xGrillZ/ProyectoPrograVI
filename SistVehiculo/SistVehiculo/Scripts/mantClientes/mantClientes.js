﻿$(function () {
    ///llamamos a la función que se encargará de crear los eventos
    //que nos permitirán controlar cuando se haga una selección en las respectivas listas
    estableceEventosChange();
    ///Carga inicialmente la lista der provincias, ya que es 
    //la lista con la que iniciaremos.
    cargaDropdownListProvincias();
    datePickerRegistro();
    validacionRegistro();
    validacionModifica();
    creaElementosJqueryUI();
});

//función que registrará los eventos necesarios para "monitorear"
//cuando se ejecute el método change de las respectivas listas
function estableceEventosChange() {
    ///Evento change de la lista de provincias
    $("#provincia").change(function () {
        ///Obtener el ID de la provincia seleccionada
        var provincia = $("#provincia").val();
        ///Funcion que permitie cargar todos los cantones asociados
        ///a la provincia seleccionada
        cargaDropdownListCantones(provincia);
    });

    ///Evento change de la lista de Cantones
    $("#canton").change(function () {
        ///Obtener el ID del canton seleccionado
        var canton = $("#canton").val();
        ///Funcion que permite cargar todos los distritos asociados al cantón seleccionado
        cargaDropdownListDistritos(canton);
    });
}

///carga los registros de las provincias
function cargaDropdownListProvincias() {
    ///dirección a donde se enviarán los datos
    var url = '/MantClientes/RetornaProvincias'; /*Controlador/Metodo*/
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
            procesarResultadoProvincias(data);
        },
        error: function (jQxhr, textStatus, errorThrown) { /*Ejecución de la funcion si funciona incorrectamente*/
            alert(errorThrown);
        },
    });
}

///Toma el resultado del método RetornaProvincias
///y lo procesa, recorriendo cada posición
function procesarResultadoProvincias(data) {
    ///Mediante un selector nos posicionamos sobre la lista de provincias
    var ddlProvincias = $("#provincia");

    ///Limpiamos todas las opcionesd e la lista de provincias
    ddlProvincias.empty();

    ///Creación de la primera opcion de la lista, con un valor vacio y texto de selección
    var nuevaOpcion = "<option value=''>Selecciona una Provincia</option>";
    ///Asignacion de la opcion al dropdownlist
    ddlProvincias.append(nuevaOpcion);
    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Provincia haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///provinciaActual.nombre nos retorna el nombre de la provincia
        var provinciaActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + provinciaActual.id_Provincia + "'>" + provinciaActual.nombre + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlProvincias.append(nuevaOpcion);
    });
}

///carga los registros de los cantones
function cargaDropdownListCantones(pIdProvincia) {

    ///dirección a donde se enviarán los datos
    var url = '/MantClientes/RetornaCantones'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        id_Provincia: pIdProvincia
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoCantones(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

function procesarResultadoCantones(data) {
    ///Declarar un selector donde nos posicionamos sobre la lista de cantones
    var ddlCantones = $("#canton");
    ///Limpiamos todas las opciones de la lista de cantones
    ddlCantones.empty();
    ///Creación de la primera opción de la lista, con un valor vacio
    var nuevaOpcion = "<option value=''>Seleccione un cantón</option>";
    ///Agregar la opción al dropdownlist
    ddlCantones.append(nuevaOpcion);

    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Canton haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///cantonActual.nombre nos retorna el nombre del canton
        var cantonActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + cantonActual.id_Canton + "'>" + cantonActual.nombre + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlCantones.append(nuevaOpcion);
    });
}

function cargaDropdownListDistritos(pIdCanton) {
    ///dirección a donde se enviarán los datos
    var url = '/MantClientes/RetornaDistritos'; /*Controlador/Metodo*/
    ///parámetros del método, es CASE-SENSITIVE
    var parametros = {
        id_Canton: pIdCanton
    };
    ///invocar el método
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(parametros),
        success: function (data, textStatus, jQxhr) {
            procesarResultadoDistritos(data);
        },
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        },
    });
}

function procesarResultadoDistritos(data) {
    ///Selector que nos posicionamos sobre la lista de distritos
    var ddlDistritos = $("#distrito");
    ///Limpieza de las opciones de la lista de distritos
    ddlDistritos.empty();
    ///Creación de la primera opción de la lista, con un valor vacio
    var nuevaOpcion = "<option value=''>Seleccione un Distrito</option>";
    ///Agregar la opción al dropdownlist
    ddlDistritos.append(nuevaOpcion);

    ///Recorrido de los registros obtenidos
    $(data).each(function () {
        ///Obtenemos el objeto de tipo Distrito haciendo uso de la claúsula "this"
        ///ahora podemos acceder a todas las propiedades por ejemplo
        ///distritoActual.nombre nos retorna el nombre del distrito
        var distritoActual = this;
        ///Creación de la nueva opción de la lista, con el valor ID de provincia y Nombre de la provincia
        nuevaOpcion = "<option value='" + distritoActual.id_Distrito + "'>" + distritoActual.nombre + "</option>";
        ///Agregamos la opción al dropdownlist
        ddlDistritos.append(nuevaOpcion);
    });
}

///Función para Fecha de Nacimiento -> Registrar Nuevo Cliente
function datePickerRegistro() {
    $("#fechNacimiento").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: "c-70:c", ///Modificar la fecha para colocar la actual y fecha hacia atrás c significa año actual
        dateFormat: "yy/mm/dd" ///Formato de fecha para SQL
    });
}

///crea las validaciones para el formulario
function validacionRegistro() {
    $("#frmNuevoCliente").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            nomCliente: {
                required: true,
                maxlength: 50
            },
            ape1Cliente: {
                required: true,
                maxlength: 50
            },
            ape2Cliente: {
                required: true,
                maxlength: 50
            },
            numCedula: {
                required: true,
                maxlength: 20
            },
            genero: {
                required: true
            },
            provincia: {
                required: true
            },
            canton: {
                required: true
            },
            distrito: {
                required: true
            },
            fechNacimiento: {
                required: true
            },
            email: {
                required: true,
                email: true,
                maxlength: 50
            },
            pTelefono: {
                required: true,
                maxlength: 50
            },
            tipoCliente: {
                required: true
            },
            contrasena: {
                required: true,
                maxlength: 200
            },
        }
    });
}

///crea las validaciones para el formulario
function validacionModifica() {
    $("#frmModificaCliente").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            nomCliente: {
                required: true,
                maxlength: 50
            },
            ape1Cliente: {
                required: true,
                maxlength: 50
            },
            ape2Cliente: {
                required: true,
                maxlength: 50
            },
            numCedula: {
                required: true,
                maxlength: 20
            },
            genero: {
                required: true
            },
            provincia: {
                required: true
            },
            canton: {
                required: true
            },
            distrito: {
                required: true
            },
            fechNacimiento: {
                required: true
            },
            email: {
                required: true,
                email: true,
                maxlength: 50
            },
            pTelefono: {
                required: true,
                maxlength: 50
            },
            tipoCliente: {
                required: true
            },
            contrasena: {
                required: true,
                maxlength: 200
            },
        }
    });
}

///Funcion que permite mostrar el dialogo al hacer click
function creaElementosJqueryUI() {
    ///creamos el div divDialog como elemento de tipo Dialog
    crearDialog();
    ///evento click del botón btMostrarDialog          
    $("#btMostrarDialog").click(function () {
        $("#divDialog").dialog("open");
    });
    //evento click del botón btCerrar   
    $("#btCerrar").click(function () {
        $("#divDialog").dialog("close");
    });
}

///Funcion que crea un dialog
function crearDialog() {
    $("#divDialog").dialog({
        autoOpen: false,
        height: 650,
        width: 800,
        modal: true,
        title: "Cambiar Contraseña",
        resizable: false,
    });
}