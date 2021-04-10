$(function () {
	eventosPrincipales();
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
	columna1.innerHTML = '<p name="codigo_servicio[]" class="non-margin">'+codigo+'</p>';
	columna2.innerHTML = '<p name="nombre_servicio[]" class="non-margin">' + servicioProducto+'</p>';
	columna3.innerHTML = '<p name="tipo_servicio[]" class="non-margin">' + tipo+'</p>';
	columna4.innerHTML = '<p name="precio_servicio[]" class="non-margin">' + precio+'</p>';
	columna5.innerHTML = '<p name="cantidad_servicio[]" class="non-margin">' + cantidad+'</p>';
	columna6.innerHTML = '<p name="total_servicio[]" class="non-margin">' + total+'</p>';
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