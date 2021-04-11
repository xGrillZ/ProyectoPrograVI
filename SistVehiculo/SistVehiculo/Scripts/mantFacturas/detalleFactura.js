$(function () {
	eventosPrincipales();
	creaEventoDetalleFactura();
});

function eventosPrincipales() {
    ///Al hacer click en agregar, se ingresa una nueva fila en la tabla
    $("#btnAgregarDetalle").on("click", agregarNuevafila);

	$("loans_table").on("click", ".fa-trash-alt", borrarProducto);

	$("#divDialogDetalleFactura").on("click", ".fa-trash-alt", borrarProducto);
}

function borrarProducto() {
	///Almacenamiento del objeto seleccionado
	var _this = this;
	///Obtener en formato de filas los datos de la fila a eliminar
	var array_fila = obtenerFila(_this);

	///Resta de los datos obtenidos con el total
	///calcularDatos(precio, cantidad, total, variable)
	calcularDatos(array_fila[4], array_fila[3], array_fila[5], 2)

	$(this).parent().parent().fadeOut("slow", function () { $(this).remove(); });
}

function obtenerFila(objectPressed) {
	//Guardar información de la linea seleccionada
	var pFila = objectPressed.parentNode.parentNode;

	///Variables que almacenan los datos de la columna con su posición, también almacena los elementos de tipo parrafo con su posición
	var codigo = pFila.getElementsByTagName("td")[0].getElementsByTagName("p")[0].innerHTML;
	var servicio = pFila.getElementsByTagName("td")[1].getElementsByTagName("p")[0].innerHTML;
	var tipo = pFila.getElementsByTagName("td")[2].getElementsByTagName("p")[0].innerHTML;
	var precio = pFila.getElementsByTagName("td")[3].getElementsByTagName("p")[0].innerHTML;
	var cantidad = pFila.getElementsByTagName("td")[4].getElementsByTagName("p")[0].innerHTML;
	var total = pFila.getElementsByTagName("td")[5].getElementsByTagName("p")[0].innerHTML;

	///Guardar todos los datos en un arraylist
	var array_fila = [codigo, servicio, tipo, precio, cantidad, total];
	///Enviar el dato guardado
	return array_fila;
}

function agregarNuevafila() {
	var codigo = $("#codigo").val();
	var servicioProducto = $("#servicioProducto").val();
	var tipo = $("#tipo").val();
	var precio = $("#precio").val();
	var cantidad = $("#cantidad").val();
	var total = parseFloat(cantidad) * parseFloat(precio);

	///Variable que contiene la referencia de la tabla
	var tablaDetalle = document.getElementById("tablaFactura");

	///Inserción de una nueva fila en la posición 1
	var row = tablaDetalle.insertRow(0 + 1);
	///Creación de nuevas columnas
	var columna1 = row.insertCell(0);
	var columna2 = row.insertCell(1);
	var columna3 = row.insertCell(2);
	var columna4 = row.insertCell(3);
	var columna5 = row.insertCell(4);
	var columna6 = row.insertCell(5);
	var columna7 = row.insertCell(6);
	///Valores de cada columna
	columna1.innerHTML = '<p>' + codigo +'</p>';
	columna2.innerHTML = '<p>' + servicioProducto +'</p>';
	columna3.innerHTML = '<p>' + tipo +'</p';
	columna4.innerHTML = '<p>' + precio +'</p>';
	columna5.innerHTML = '<p>' + cantidad +'</p>';
	columna6.innerHTML = '<p>' + total +'</p>';
	columna7.innerHTML = '<span class="fas fa-trash-alt"></span>';

	///Se llama a la funcion calcularDatos para mostrar los datos calculados cuando se ingresan
	calcularDatos(cantidad, precio, total, 1);
}

function calcularDatos(cantidad, precio, total, accion) {
	///Creación de variables con los datos de las etiquetas a utilizar
	var total_cantidad = parseFloat(document.getElementById("total_cantidad").innerHTML);
	var total_precio = parseFloat(document.getElementById("total_precio").innerHTML);
	var total_total = parseFloat(document.getElementById("total_total").innerHTML);

	///accion=1		Sumar valores
	///accion=2		Restar valores
	if (accion == 1) {
		document.getElementById("total_cantidad").innerHTML = parseFloat(total_cantidad) + parseFloat(cantidad);
		document.getElementById("total_precio").innerHTML = parseFloat(total_precio) + parseFloat(precio);
		document.getElementById("total_total").innerHTML = parseFloat(total_total) + parseFloat(total);
	} else if (accion == 2) {
		document.getElementById("total_cantidad").innerHTML = parseFloat(total_cantidad) - parseFloat(cantidad);
		document.getElementById("total_precio").innerHTML = parseFloat(total_precio) - parseFloat(precio);
		document.getElementById("total_total").innerHTML = parseFloat(total_total) - parseFloat(total);
	} else {
		alert('Ha ocurrido un error, revisar valores');
	}
}

/* Permite realizar una acción con el evento click */
function creaEventoDetalleFactura() {
	$("#btnGenerarDetalle").on("click", function () {
		/*enviarDatosJson();*/
		/*test1();*/
		/*enviarDatosJSONFacturaGlobal();
	
		enviarDatosJSONFacturaServiciosCliente();

		enviarDatosJSONFacturaServiciosVehiculo();*/
		enviarDatosJson();
		enviarDatosJSONFacturaGlobal();
	});
	$("#btnEnviarServicioCliente").on("click", function () {
		enviarDatosJSONFacturaServiciosCliente();
	});
	$("#btnEnviarServicioVehiculo").on("click", function () {
		enviarDatosJSONFacturaServiciosVehiculo();
	});
}

///Funciona de manera general
/*function enviarDatosJson() {
	var datosDetalleFactura = new Array();

	var table = document.getElementById("tablaFactura");
	var idCliente = $("#hdIdCliente").val();
	var idVehiculo = $("#hdPlaca").val();

    for (var i = 1; i < table.rows.length; i++) {
		var row = table.rows[i];

		var datos = {};
		datos.Cliente = idCliente;
		datos.Vehiculo = idVehiculo;
		datos.Codigo = row.cells[0].innerHTML;
		datos.Servicio = row.cells[1].innerHTML;
		datos.Tipo = row.cells[2].innerHTML;
		datos.Precio = row.cells[3].innerHTML;
		datos.Cantidad = row.cells[4].innerHTML;
		datos.Total = row.cells[5].innerHTML;

		datosDetalleFactura.push(datos);

		console.log(datosDetalleFactura);
    }
}*/

///Logica de separación
function enviarDatosJson() {
	///Creación de array
	var datosDetalleFacturaServicio = new Array();
	var datosDetalleFacturaProducto = new Array();
	var datosDetalleFacturaGeneral = new Array();

	///Obtención de datos por medio de variables
	var table = document.getElementById("tablaFactura");
	var idCliente = $("#hdIdCliente").val();
	var idVehiculo = $("#hdPlaca").val();
	var totalGeneral = parseFloat(document.getElementById("total_total").innerHTML)
	var numFactura = $("#numFactura").val();
	var idFactura = $("#hdIdFactura").val();

	///Recorrido de la tabla para obtener los datos
	for (var i = 1; i < table.rows.length; i++) {
		var row = table.rows[i];

		///Arreglos que almacenan los datos del recorrido temporalmente
		var datosServicio = {};
		var datosProducto = {};
		var datosGenerales = {};

		///Recorrido para rellenar el Arreglo datosDetalleFacturaGeneral
		///InnerText permite solo obtener el valor del texto a buscar
		datosGenerales.Cliente = idCliente;
		datosGenerales.Vehiculo = idVehiculo;
		datosGenerales.Codigo = row.cells[0].innerText;
		datosGenerales.Servicio = row.cells[1].innerText;
		datosGenerales.Tipo = row.cells[2].innerText;
		datosGenerales.Precio = row.cells[3].innerText;
		datosGenerales.Cantidad = row.cells[4].innerText;
		datosGenerales.Total = row.cells[5].innerText;

		datosDetalleFacturaGeneral.push(datosGenerales);

		///Verificador si el tipo es Servicio
		///Y Almacenamiento en el arreglo correspondiente
		if (row.cells[2].innerHTML == 1) {
			datosServicio.Cliente = idCliente;
			datosServicio.Vehiculo = idVehiculo;
			datosServicio.Codigo = row.cells[0].innerText;
			datosServicio.Servicio = row.cells[1].innerText;
			datosServicio.Tipo = row.cells[2].innerText;
			datosServicio.Precio = row.cells[3].innerText;
			datosServicio.Cantidad = row.cells[4].innerText;
			datosServicio.Total = row.cells[5].innerText;

			datosDetalleFacturaServicio.push(datosServicio);

			/*console.log(datosDetalleFacturaServicio);*/
		}

		if (row.cells[2].innerHTML == 2){
			datosProducto.Cliente = idCliente;
			datosProducto.Vehiculo = idVehiculo;
			datosProducto.Codigo = row.cells[0].innerText;
			datosProducto.Servicio = row.cells[1].innerText;
			datosProducto.Tipo = row.cells[2].innerText;
			datosProducto.Precio = row.cells[3].innerText;
			datosProducto.Cantidad = row.cells[4].innerText;
			datosProducto.Total = row.cells[5].innerText;

			datosDetalleFacturaProducto.push(datosProducto);

			/*console.log(datosDetalleFacturaProducto);*/
		}

		///Enviar datos al procedimiento almacenado InsertaDetalleFactura
		recorridoJsonDetalleFacturaGlobal(datosDetalleFacturaGeneral);
		recorridoJsonDetalleFacturaCliente(datosDetalleFacturaProducto);
		recorridoJsonDetalleFacturaVehiculo(datosDetalleFacturaServicio);
		invocarMetodoModificaTotalFactura(idFactura, totalGeneral);
	}
}

function enviarDatosJSONFacturaGlobal() {

	var datosDetalleFacturaGeneral = new Array();

	var table = document.getElementById("tablaFactura");

	for (var i = 1; i < table.rows.length; i++) {
		var row = table.rows[i];

		///Arreglos que almacenan los datos del recorrido temporalmente
		var datosGenerales = {};

		///Recorrido para rellenar el Arreglo datosDetalleFacturaGeneral
		///InnerText permite solo obtener el valor del texto a buscar
		datosGenerales.Codigo = row.cells[0].innerText;
		datosGenerales.Servicio = row.cells[1].innerText;
		datosGenerales.Tipo = row.cells[2].innerText;
		datosGenerales.Precio = row.cells[3].innerText;
		datosGenerales.Cantidad = row.cells[4].innerText;
		datosGenerales.Total = row.cells[5].innerText;

		datosDetalleFacturaGeneral.push(datosGenerales);

		recorridoJsonDetalleFacturaGlobal(datosDetalleFacturaGeneral);
	}

}

function enviarDatosJSONFacturaServiciosCliente() {

	var datosDetalleFacturaServicio = new Array();

	var table = document.getElementById("tablaFactura");

	for (var i = 1; i < table.rows.length; i++) {
		var row = table.rows[i];

		///Arreglos que almacenan los datos del recorrido temporalmente
		var datosServicio = {};

		///Recorrido para rellenar el Arreglo datosDetalleFacturaGeneral
		///InnerText permite solo obtener el valor del texto a buscar
		if (row.cells[2].innerText === 1) {
			datosServicio.Codigo = row.cells[0].innerText;
			datosServicio.Servicio = row.cells[1].innerText;
			datosServicio.Tipo = row.cells[2].innerText;
			datosServicio.Precio = row.cells[3].innerText;
			datosServicio.Cantidad = row.cells[4].innerText;
			datosServicio.Total = row.cells[5].innerText;

			datosDetalleFacturaServicio.push(datosServicio);

			recorridoJsonDetalleFacturaVehiculo(datosDetalleFacturaServicio);
		}
	}

}

function enviarDatosJSONFacturaServiciosVehiculo() {

	var datosDetalleFacturaProducto = new Array();

	var table = document.getElementById("tablaFactura");

	for (var i = 1; i < table.rows.length; i++) {
		var row = table.rows[i];

		///Arreglos que almacenan los datos del recorrido temporalmente
		var datosProducto = {};

		///Recorrido para rellenar el Arreglo datosDetalleFacturaGeneral
		///InnerText permite solo obtener el valor del texto a buscar
		if (row.cells[2].innerText === "2") {
			datosProducto.Codigo = row.cells[0].innerText;
			datosProducto.Servicio = row.cells[1].innerText;
			datosProducto.Tipo = row.cells[2].innerText;
			datosProducto.Precio = row.cells[3].innerText;
			datosProducto.Cantidad = row.cells[4].innerText;
			datosProducto.Total = row.cells[5].innerText;

			datosDetalleFacturaProducto.push(datosProducto);

			recorridoJsonDetalleFacturaCliente(datosDetalleFacturaProducto);
		}
	}

}

function test1() {

	var totalGeneral = parseFloat(document.getElementById("total_total").innerHTML)
	var idFactura = $("#hdIdFactura").val();

	invocarMetodoModificaTotalFactura(idFactura, totalGeneral);
}

/*function test(objetoJson) {
	var jsonobject = objetoJson;

    for (var i = 0; i < jsonobject.length; i++) {
		var resultado = jsonobject[i].Codigo;
		var resultadoDos = jsonobject[i].Precio;
		///console.log(resultado, resultadoDos);
	}

	invocarMetodoJsonPrueba(resultado, resultadoDos);
}*/

function recorridoJsonDetalleFacturaGlobal(objetoJson) {
	var jsonobject = objetoJson;

	var numFactura = $("#numFactura").val();

	for (var i = 0; i < jsonobject.length; i++) {
		var TipoServicio = jsonobject[i].Servicio;
		var Cantidad = jsonobject[i].Cantidad;
		var Precio = jsonobject[i].Precio;
		/*console.log(resultado, resultadoDos);*/
	}

	invocarMetodoInsertaDetalleFactura(numFactura, TipoServicio, Cantidad, Precio);
}

function recorridoJsonDetalleFacturaCliente(objetoJson) {
	var jsonobject = objetoJson;

	var idCliente = $("#hdIdCliente").val();

	for (var i = 0; i < jsonobject.length; i++) {
		var TipoServicio = jsonobject[i].Servicio;
		var Clasificacion = jsonobject[i].Tipo;
		var Cantidad = jsonobject[i].Cantidad;
		var Precio = jsonobject[i].Precio;
		var PrecioTotal = jsonobject[i].Total;
		/*console.log(resultado, resultadoDos);*/
	}

	invocarMetodoInsertaDetalleFacturaCliente(TipoServicio, idCliente, Clasificacion, Cantidad, Precio, PrecioTotal);
}

function recorridoJsonDetalleFacturaVehiculo(objetoJson) {
	var jsonobject = objetoJson;

	var idVehiculo = $("#hdPlaca").val();

	for (var i = 0; i < jsonobject.length; i++) {
		var TipoServicio = jsonobject[i].Servicio;
		var Clasificacion = jsonobject[i].Tipo;
		var Cantidad = jsonobject[i].Cantidad;
		var Precio = jsonobject[i].Precio;
		var PrecioTotal = jsonobject[i].Total;
		/*console.log(resultado, resultadoDos);*/
	}

	invocarMetodoInsertaDetalleFacturaVehiculo(TipoServicio, idVehiculo, Clasificacion, Cantidad, Precio, PrecioTotal);
}

function invocarMetodoInsertaDetalleFactura(pNumFactura, pTipoServicio, pCantidad, pPrecio) {
	/*Dirección a donde se enviarán los datos */
	var url = '/MantFacturas/InsertaDetalleFactura';
	/*Parámetros del método*/
	var parametros = {
		NumFactura: pNumFactura,
		TipoServicio: pTipoServicio,
		Cantidad: pCantidad,
		Precio: pPrecio
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
			procesarResultadoMetodoDetalleFactura(data);
		},
		///Función que se ejecuta cuando la respuesta tuvo errores
		error: function (jQxhr, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}

function procesarResultadoMetodoDetalleFactura(data) {
	///Es .resultado porque la función devuelve
	///un objeto JSON que posee una propiedad
	///llamada resultado 
	var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
	alert("Información: " + resultadoFuncion);
	/*$("#divDialogPassword").dialog("close");*/
}

function invocarMetodoJsonPrueba(pCodigo, pPrecio) {
	/*Dirección a donde se enviarán los datos */
	var url = '/MantFacturas/InsertaJsonPrueba';
	/*Parámetros del método*/
	var parametros = {
		pCodigo: pCodigo,
		pPrecio: pPrecio
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
			procesarResultadoMetodoJsonPrueba(data);
		},
		///Función que se ejecuta cuando la respuesta tuvo errores
		error: function (jQxhr, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}

function procesarResultadoMetodoJsonPrueba(data) {
	var resultadoFuncion = data.resultado;
	alert("Información: " + resultadoFuncion);
}

function invocarMetodoInsertaDetalleFacturaCliente(pTipoServicio, pIdCliente, pIdClasificacion, pCantidad, pPrecio, pPrecioTotal) {
	/*Dirección a donde se enviarán los datos */
	var url = '/MantFacturas/InsertaDetalleFacturaCliente';
	/*Parámetros del método*/
	var parametros = {
		TipoServicio: pTipoServicio,
		IdCliente: pIdCliente,
		IdClasificacion: pIdClasificacion,
		Cantidad: pCantidad,
		Precio: pPrecio,
		PrecioTotal: pPrecioTotal
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
			procesarResultadoMetodoDetalleFacturaCliente(data);
		},
		///Función que se ejecuta cuando la respuesta tuvo errores
		error: function (jQxhr, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}

function procesarResultadoMetodoDetalleFacturaCliente(data) {
	///Es .resultado porque la función devuelve
	///un objeto JSON que posee una propiedad
	///llamada resultado 
	var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
	alert("Información: " + resultadoFuncion);
	/*$("#divDialogPassword").dialog("close");*/
}

function invocarMetodoInsertaDetalleFacturaVehiculo(pTipoServicio, pIdVehiculo, pIdClasificacion, pCantidad, pPrecio, pPrecioTotal) {
	/*Dirección a donde se enviarán los datos */
	var url = '/MantFacturas/InsertaDetalleFacturaVehiculo';
	/*Parámetros del método*/
	var parametros = {
		TipoServicio: pTipoServicio,
		IdVehiculo: pIdVehiculo,
		IdClasificacion: pIdClasificacion,
		Cantidad: pCantidad,
		Precio: pPrecio,
		PrecioTotal: pPrecioTotal
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
			procesarResultadoMetodoDetalleFacturaVehiculo(data);
		},
		///Función que se ejecuta cuando la respuesta tuvo errores
		error: function (jQxhr, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}

function procesarResultadoMetodoDetalleFacturaVehiculo(data) {
	///Es .resultado porque la función devuelve
	///un objeto JSON que posee una propiedad
	///llamada resultado 
	var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
	alert("Información: " + resultadoFuncion);
	/*$("#divDialogPassword").dialog("close");*/
}

function invocarMetodoModificaTotalFactura(pIdFactura, pMontoTotal) {
	/*Dirección a donde se enviarán los datos */
	var url = '/MantFacturas/ModificaFacturaMontos';
	/*Parámetros del método*/
	var parametros = {
		idFactura: pIdFactura,
		montoTotal: pMontoTotal
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
			procesarResultadoMetodoModificaTotalFactura(data);
		},
		///Función que se ejecuta cuando la respuesta tuvo errores
		error: function (jQxhr, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}

function procesarResultadoMetodoModificaTotalFactura(data) {
	///Es .resultado porque la función devuelve
	///un objeto JSON que posee una propiedad
	///llamada resultado 
	var resultadoFuncion = data.resultado; /*.resultado es la propiedad del objeto que retorno el controlador*/
	alert("Información: " + resultadoFuncion);
	/*$("#divDialogPassword").dialog("close");*/
}