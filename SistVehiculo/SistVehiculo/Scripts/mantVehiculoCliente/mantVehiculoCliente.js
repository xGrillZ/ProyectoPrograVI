$(function () {
    obtenerRegistrosVehiculosClientesKendo();
});


function obtenerRegistrosVehiculosClientesKendo() {
    /////construir la dirección del método del servidor
    var urlMetodo = '/MantVehiculosCliente/RetornaVehiculosClienteLista'
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
                field: "placa",
                title: "Placa Vehículo"
            },
            {
                field: "marca",
                title: "Marca Vehículo"
            },
            {
                field: "tipo",
                title: "Tipo Vehículo"
            },
            {
                field: "numeroPuerta",
                title: "Número de puertas"
            },
            {
                field: "numeroRueda",
                title: "Número de ruedas"
            }
        ],
        filterable: true, ///Permite al usurio filtrar la información
        toolbar: ["excel", "pdf"], ///Permite exporar la información
        excel: { ///Modificar información del archivo excel
            fileName: "Lista de Vehiculos Cliente.xlsx", ///Nombre del archivo
            filterable: true, ///Permite filtrar dentro del excel
            allPages: true ///Mostrará todos los datos dentro del excel
        },
        pdf: {
            fileName: "Lista de Vehiculos Cliente.pdf", ///Nombre del archivo
            author: "UMCA", ///Nombre del Autor
            creator: "Steven Vargas Corrales", ///Nombre del creador
            date: new Date(), ///Fecha del archivo
        }
    });
}