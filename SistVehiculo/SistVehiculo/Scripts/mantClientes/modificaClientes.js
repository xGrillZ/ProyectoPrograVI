$(function () {
    ///llamamos a la función que se encargará de crear los eventos
    //que nos permitirán controlar cuando se haga una selección en las respectivas listas
    estableceEventosChange();
    ///Carga inicialmente la lista der provincias, ya que es 
    //la lista con la que iniciaremos.
    cargaDropdownListProvincias();
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
        success: function (data, test,textStatus, jQxhr) { /*Ejecución de la funcion si funciona correctamente*/
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