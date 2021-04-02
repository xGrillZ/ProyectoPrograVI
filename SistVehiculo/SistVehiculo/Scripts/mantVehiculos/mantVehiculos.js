$(function () {
    validacionRegistro();
    validacionModifica();
    //obtenerRegistrosServiciosVehiculoKendo();
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

/*function obtenerRegistrosServiciosVehiculoKendo() {
    /////construir la dirección del método del servidor
    var urlMetodo = '/MantVehiculos/RetornaServiciosVehiculoLista'
    var parametros = {};
    var funcion = creaGridKendo;
    ///ejecuta la función $.ajax utilizando un método genérico
    //para no declarar toda la instrucción siempre
    ejecutaAjax(urlMetodo, parametros, funcion);
}*/

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
                field: "marca",
                ///Texto del encabezado
                title: "Marca Vehículo"
            },
            {
                field: "tipoDeVehiculo",
                title: "Tipo Vehículo"
            },
            {
                field: "numeroPuerta",
                title: "Número de Puertas"
            },
            {
                field: "numeroRueda",
                title: "Número de Ruedas",
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