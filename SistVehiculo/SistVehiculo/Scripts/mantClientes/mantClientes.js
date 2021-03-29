$(function () {
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
    validacionModificaContrasena();
    creaEventoFormularioContrasena();
    obtenerRegistrosServiciosClientesKendo();
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

    ///Obtiene el valor del hidden
    var hiddenProvincia = $("#hdProvincia").val();

    if (hiddenProvincia != undefined) {
        ddlProvincias.val(hiddenProvincia);
        cargaDropdownListCantones(hiddenProvincia);
    }
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

    ///Obtiene el valor del hidden
    var hiddenCanton = $("#hdCanton").val();

    if (hiddenCanton != undefined) {
        ddlCantones.val(hiddenCanton);
        cargaDropdownListDistritos(hiddenCanton);
    }
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

    ///Obtiene el valor del hidden
    var hiddenDistrito = $("#hdDistrito").val();

    if (hiddenDistrito != undefined) {
        ddlDistritos.val(hiddenDistrito);
    }
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
        }
    });
}

///Funcion que permite mostrar el dialogo al hacer click
function creaElementosJqueryUI() {
    ///creamos el div divDialog como elemento de tipo Dialog
    crearDialog();
    ///evento click del botón btMostrarDialog          
    $("#btMostrarDialogPassword").click(function () {
        $("#divDialogPassword").dialog("open");
    });
    //evento click del botón btCerrar   
    $("#btCerrarPassword").click(function () {
        $("#divDialogPassword").dialog("close");
    });
}

///Funcion que crea un dialog
function crearDialog() {
    $("#divDialogPassword").dialog({
        autoOpen: false,
        height: 450,
        width: 500,
        modal: true,
        title: "Cambiar contraseña",
        resizable: false
    });
}

///crea las validaciones para el formulario
function validacionModificaContrasena() {
    $("#frmModificaContrasena").validate({
        ///objeto que contiene "las condiciones" que el formulario
        ///debe cumplir para ser considerado válido
        rules: {
            contrasena: {
                required: true,
                maxlength: 200
            },
            contrasenaRepite: {
                required: true,
                maxlength: 200,
                equalTo: "#contrasena"
            },
        }
    });
}

/* Permite realizar una acción con el evento click */
function creaEventoFormularioContrasena() {
    $("#btnAceptarPassword").on("click", function () {
        /*Asignar a la variable formulario
          el resultado del selector*/
        var formulario = $("#frmModificaContrasena");
        /*Ejecutar el método de validación*/
        formulario.validate();
        /*Si el formulario es valido, proceder a
         ejecutar la función invocarMetodoPost*/
        if (formulario.valid()) {
            invocarMetodoPostPassword();
        }
    });
}

///se encarga de llamar al método del controlador y procesar el resultado
function invocarMetodoPostPassword() {
    /*Dirección a donde se enviarán los datos */
    var url = '/MantClientes/ModificaContrasena';
    /*Parámetros del método*/
    var parametros = {
        pIdCliente: $("#hdIdCliente").val(),
        pContrasena: $("#contrasena").val()
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
            procesarResultadoMetodoPassword(data);
        },
        ///Función que se ejecuta cuando la respuesta tuvo errores
        error: function (jQxhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}

function procesarResultadoMetodoPassword(data) {
    ///Es .resultado porque la función devuelve
    ///un objeto JSON que posee una propiedad
    ///llamada resultado 
    var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
    alert("Información: " + resultadoFuncion);
    $("#divDialogPassword").dialog("close");
}

function obtenerRegistrosServiciosClientesKendo() {
    /////construir la dirección del método del servidor
    var urlMetodo = '/MantClientes/RetornaServiciosClienteLista'
    var parametros = {};
    var funcion = creaGridKendo;
    ///ejecuta la función $.ajax utilizando un método genérico
    //para no declarar toda la instrucción siempre
    ejecutaAjax(urlMetodo, parametros, funcion);
}

function creaGridKendo(data) {
    ///Selector por ID
    $("#divKendoGrid").kendoGrid({
        ///Asignar la fuente de datos al objeto KendoGrid
        dataSource: {
            data: data.resultado, ///Se obtiene los datos pero con propiedad de resultado indicado en el controlador Personas
            pageSize: 5, ///Mostrar los registros en pantalla
        },
        pageable: true, ///Permite crear un menu de páginas
        columns: [ ///Se muestra el nombre de las columnas por un array
            ///Cada columna se agrega por llaves
            {
                ///Propiedad de la fuenta de datos a mostrar
                field: "nomCliente",
                ///Texto del encabezado
                title: "Nombre del Cliente"
            },
            {
                field: "ape1Cliente",
                title: "Primer Apellido"
            },
            {
                field: "ape2Cliente",
                title: "Segundo Apellido"
            },
            {
                field: "email",
                title: "Correo Electrónico",
            },
            {
                field: "pTelefono",
                title: "Teléfono"
            },
            {
                field: "codigoServicioProducto",
                title: "Código Servicio"
            },
            {
                field: "descripcion",
                title: "Descripción"
            },
            {
                field: "precio",
                title: "Precio"
            },
            {
                field: "nombreClasificacion",
                title: "Tipo Servicio"
            }
        ],
        filterable: true, ///Permite al usurio filtrar la información
        toolbar: ["excel", "pdf"], ///Permite exporar la información
        excel: { ///Modificar información del archivo excel
            fileName: "Lista de Servicios Cliente.xlsx", ///Nombre del archivo
            filterable: true, ///Permite filtrar dentro del excel
            allPages: true ///Mostrará todos los datos dentro del excel
        },
        pdf: {
            fileName: "Lista de Servicios Cliente.pdf", ///Nombre del archivo
            author: "UMCA", ///Nombre del Autor
            creator: "Steven Vargas Corrales", ///Nombre del creador
            date: new Date(), ///Fecha del archivo
        }
    });
}