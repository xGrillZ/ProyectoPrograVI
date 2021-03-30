﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistVehiculo.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class sistvehiculoviEntities : DbContext
    {
        public sistvehiculoviEntities()
            : base("name=sistvehiculoviEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Canton> Canton { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<detalle_Factura> detalle_Factura { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<MarcaVehiculo> MarcaVehiculo { get; set; }
        public DbSet<PaisFabricante> PaisFabricante { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<TipoCliente> TipoCliente { get; set; }
        public DbSet<TiposVehiculo> TiposVehiculo { get; set; }
        public DbSet<Vehiculos> Vehiculos { get; set; }
        public DbSet<VehiculosCliente> VehiculosCliente { get; set; }
        public DbSet<clasificacionSP> clasificacionSP { get; set; }
        public DbSet<TipoServicioProducto> TipoServicioProducto { get; set; }
        public DbSet<ServiciosCliente> ServiciosCliente { get; set; }
        public DbSet<ServiciosVehiculo> ServiciosVehiculo { get; set; }
    
        public virtual int pa_ModificaUltimaSesionCliente(Nullable<int> idCliente, Nullable<System.DateTime> ultimoIngreso)
        {
            var idClienteParameter = idCliente.HasValue ?
                new ObjectParameter("idCliente", idCliente) :
                new ObjectParameter("idCliente", typeof(int));
    
            var ultimoIngresoParameter = ultimoIngreso.HasValue ?
                new ObjectParameter("ultimoIngreso", ultimoIngreso) :
                new ObjectParameter("ultimoIngreso", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_ModificaUltimaSesionCliente", idClienteParameter, ultimoIngresoParameter);
        }
    
        public virtual ObjectResult<pa_RetornaCliente_Result> pa_RetornaCliente(string numCedula, string nomCliente, string ape1Cliente, string ape2Cliente)
        {
            var numCedulaParameter = numCedula != null ?
                new ObjectParameter("numCedula", numCedula) :
                new ObjectParameter("numCedula", typeof(string));
    
            var nomClienteParameter = nomCliente != null ?
                new ObjectParameter("nomCliente", nomCliente) :
                new ObjectParameter("nomCliente", typeof(string));
    
            var ape1ClienteParameter = ape1Cliente != null ?
                new ObjectParameter("ape1Cliente", ape1Cliente) :
                new ObjectParameter("ape1Cliente", typeof(string));
    
            var ape2ClienteParameter = ape2Cliente != null ?
                new ObjectParameter("ape2Cliente", ape2Cliente) :
                new ObjectParameter("ape2Cliente", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaCliente_Result>("pa_RetornaCliente", numCedulaParameter, nomClienteParameter, ape1ClienteParameter, ape2ClienteParameter);
        }
    
        public virtual ObjectResult<pa_RetornaClienteCorreoPwd_Result> pa_RetornaClienteCorreoPwd(string correoElectronico, string password)
        {
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("correoElectronico", correoElectronico) :
                new ObjectParameter("correoElectronico", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaClienteCorreoPwd_Result>("pa_RetornaClienteCorreoPwd", correoElectronicoParameter, passwordParameter);
        }
    
        public virtual ObjectResult<pa_RetornaClienteID_Result> pa_RetornaClienteID(Nullable<int> idCliente)
        {
            var idClienteParameter = idCliente.HasValue ?
                new ObjectParameter("idCliente", idCliente) :
                new ObjectParameter("idCliente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaClienteID_Result>("pa_RetornaClienteID", idClienteParameter);
        }
    
        public virtual ObjectResult<RetornaCantones_Result> RetornaCantones(string nombre, Nullable<int> id_Provincia)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var id_ProvinciaParameter = id_Provincia.HasValue ?
                new ObjectParameter("id_Provincia", id_Provincia) :
                new ObjectParameter("id_Provincia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RetornaCantones_Result>("RetornaCantones", nombreParameter, id_ProvinciaParameter);
        }
    
        public virtual ObjectResult<RetornaCantonesID_Result> RetornaCantonesID(Nullable<int> id_Canton)
        {
            var id_CantonParameter = id_Canton.HasValue ?
                new ObjectParameter("id_Canton", id_Canton) :
                new ObjectParameter("id_Canton", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RetornaCantonesID_Result>("RetornaCantonesID", id_CantonParameter);
        }
    
        public virtual ObjectResult<RetornaProvincias_Result> RetornaProvincias(string nombre)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RetornaProvincias_Result>("RetornaProvincias", nombreParameter);
        }
    
        public virtual int sp_EliminaCanton(Nullable<int> id_Canton)
        {
            var id_CantonParameter = id_Canton.HasValue ?
                new ObjectParameter("id_Canton", id_Canton) :
                new ObjectParameter("id_Canton", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminaCanton", id_CantonParameter);
        }
    
        public virtual int sp_InsertaCanton(Nullable<int> id_Provincia, string nombre, Nullable<int> id_CantonInec)
        {
            var id_ProvinciaParameter = id_Provincia.HasValue ?
                new ObjectParameter("id_Provincia", id_Provincia) :
                new ObjectParameter("id_Provincia", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var id_CantonInecParameter = id_CantonInec.HasValue ?
                new ObjectParameter("id_CantonInec", id_CantonInec) :
                new ObjectParameter("id_CantonInec", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertaCanton", id_ProvinciaParameter, nombreParameter, id_CantonInecParameter);
        }
    
        public virtual int sp_ModificaCanton(Nullable<int> id_Canton, Nullable<int> id_Provincia, string nombre, Nullable<int> id_CantonInec)
        {
            var id_CantonParameter = id_Canton.HasValue ?
                new ObjectParameter("id_Canton", id_Canton) :
                new ObjectParameter("id_Canton", typeof(int));
    
            var id_ProvinciaParameter = id_Provincia.HasValue ?
                new ObjectParameter("id_Provincia", id_Provincia) :
                new ObjectParameter("id_Provincia", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var id_CantonInecParameter = id_CantonInec.HasValue ?
                new ObjectParameter("id_CantonInec", id_CantonInec) :
                new ObjectParameter("id_CantonInec", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ModificaCanton", id_CantonParameter, id_ProvinciaParameter, nombreParameter, id_CantonInecParameter);
        }
    
        public virtual int pa_EliminaCliente(Nullable<int> idCliente)
        {
            var idClienteParameter = idCliente.HasValue ?
                new ObjectParameter("idCliente", idCliente) :
                new ObjectParameter("idCliente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_EliminaCliente", idClienteParameter);
        }
    
        public virtual int pa_EliminaTipoServicioProducto(Nullable<int> idTipoServicioProducto)
        {
            var idTipoServicioProductoParameter = idTipoServicioProducto.HasValue ?
                new ObjectParameter("idTipoServicioProducto", idTipoServicioProducto) :
                new ObjectParameter("idTipoServicioProducto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_EliminaTipoServicioProducto", idTipoServicioProductoParameter);
        }
    
        public virtual int pa_InsertaCliente(string nomCliente, string ape1Cliente, string ape2Cliente, string numCedula, Nullable<int> genero, Nullable<int> provincia, Nullable<System.DateTime> fechNacimiento, Nullable<int> canton, Nullable<int> distrito, string email, string pTelefono, Nullable<int> tipoCliente, Nullable<System.DateTime> ultimoIngreso, string contrasena)
        {
            var nomClienteParameter = nomCliente != null ?
                new ObjectParameter("nomCliente", nomCliente) :
                new ObjectParameter("nomCliente", typeof(string));
    
            var ape1ClienteParameter = ape1Cliente != null ?
                new ObjectParameter("ape1Cliente", ape1Cliente) :
                new ObjectParameter("ape1Cliente", typeof(string));
    
            var ape2ClienteParameter = ape2Cliente != null ?
                new ObjectParameter("ape2Cliente", ape2Cliente) :
                new ObjectParameter("ape2Cliente", typeof(string));
    
            var numCedulaParameter = numCedula != null ?
                new ObjectParameter("numCedula", numCedula) :
                new ObjectParameter("numCedula", typeof(string));
    
            var generoParameter = genero.HasValue ?
                new ObjectParameter("genero", genero) :
                new ObjectParameter("genero", typeof(int));
    
            var provinciaParameter = provincia.HasValue ?
                new ObjectParameter("provincia", provincia) :
                new ObjectParameter("provincia", typeof(int));
    
            var fechNacimientoParameter = fechNacimiento.HasValue ?
                new ObjectParameter("fechNacimiento", fechNacimiento) :
                new ObjectParameter("fechNacimiento", typeof(System.DateTime));
    
            var cantonParameter = canton.HasValue ?
                new ObjectParameter("canton", canton) :
                new ObjectParameter("canton", typeof(int));
    
            var distritoParameter = distrito.HasValue ?
                new ObjectParameter("distrito", distrito) :
                new ObjectParameter("distrito", typeof(int));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var pTelefonoParameter = pTelefono != null ?
                new ObjectParameter("pTelefono", pTelefono) :
                new ObjectParameter("pTelefono", typeof(string));
    
            var tipoClienteParameter = tipoCliente.HasValue ?
                new ObjectParameter("tipoCliente", tipoCliente) :
                new ObjectParameter("tipoCliente", typeof(int));
    
            var ultimoIngresoParameter = ultimoIngreso.HasValue ?
                new ObjectParameter("ultimoIngreso", ultimoIngreso) :
                new ObjectParameter("ultimoIngreso", typeof(System.DateTime));
    
            var contrasenaParameter = contrasena != null ?
                new ObjectParameter("contrasena", contrasena) :
                new ObjectParameter("contrasena", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_InsertaCliente", nomClienteParameter, ape1ClienteParameter, ape2ClienteParameter, numCedulaParameter, generoParameter, provinciaParameter, fechNacimientoParameter, cantonParameter, distritoParameter, emailParameter, pTelefonoParameter, tipoClienteParameter, ultimoIngresoParameter, contrasenaParameter);
        }
    
        public virtual int pa_InsertaVehiculos(string placa, Nullable<int> numeroPuerta, Nullable<int> numeroRuedas, Nullable<int> tipoVehiculo, Nullable<int> marcaVehiculo)
        {
            var placaParameter = placa != null ?
                new ObjectParameter("placa", placa) :
                new ObjectParameter("placa", typeof(string));
    
            var numeroPuertaParameter = numeroPuerta.HasValue ?
                new ObjectParameter("numeroPuerta", numeroPuerta) :
                new ObjectParameter("numeroPuerta", typeof(int));
    
            var numeroRuedasParameter = numeroRuedas.HasValue ?
                new ObjectParameter("numeroRuedas", numeroRuedas) :
                new ObjectParameter("numeroRuedas", typeof(int));
    
            var tipoVehiculoParameter = tipoVehiculo.HasValue ?
                new ObjectParameter("tipoVehiculo", tipoVehiculo) :
                new ObjectParameter("tipoVehiculo", typeof(int));
    
            var marcaVehiculoParameter = marcaVehiculo.HasValue ?
                new ObjectParameter("marcaVehiculo", marcaVehiculo) :
                new ObjectParameter("marcaVehiculo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_InsertaVehiculos", placaParameter, numeroPuertaParameter, numeroRuedasParameter, tipoVehiculoParameter, marcaVehiculoParameter);
        }
    
        public virtual int pa_ModificaPaisFabricante(Nullable<int> idFabricante, string codigo, string pais)
        {
            var idFabricanteParameter = idFabricante.HasValue ?
                new ObjectParameter("idFabricante", idFabricante) :
                new ObjectParameter("idFabricante", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var paisParameter = pais != null ?
                new ObjectParameter("pais", pais) :
                new ObjectParameter("pais", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_ModificaPaisFabricante", idFabricanteParameter, codigoParameter, paisParameter);
        }
    
        public virtual ObjectResult<pa_RetornaVehiculos_Result> pa_RetornaVehiculos(string placa)
        {
            var placaParameter = placa != null ?
                new ObjectParameter("placa", placa) :
                new ObjectParameter("placa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaVehiculos_Result>("pa_RetornaVehiculos", placaParameter);
        }
    
        public virtual ObjectResult<pa_RetornaGenero_Result> pa_RetornaGenero(string nombre)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaGenero_Result>("pa_RetornaGenero", nombreParameter);
        }
    
        public virtual ObjectResult<pa_RetornaTipoCliente_Result> pa_RetornaTipoCliente(string nombre)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaTipoCliente_Result>("pa_RetornaTipoCliente", nombreParameter);
        }
    
        public virtual ObjectResult<RetornaDistritos_Result> RetornaDistritos(string nombre, Nullable<int> id_Canton)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var id_CantonParameter = id_Canton.HasValue ?
                new ObjectParameter("id_Canton", id_Canton) :
                new ObjectParameter("id_Canton", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RetornaDistritos_Result>("RetornaDistritos", nombreParameter, id_CantonParameter);
        }
    
        public virtual int pa_InsertaTipoServicioProducto(string codigo, string descripcion, Nullable<double> precio, Nullable<int> tipo)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("descripcion", descripcion) :
                new ObjectParameter("descripcion", typeof(string));
    
            var precioParameter = precio.HasValue ?
                new ObjectParameter("precio", precio) :
                new ObjectParameter("precio", typeof(double));
    
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_InsertaTipoServicioProducto", codigoParameter, descripcionParameter, precioParameter, tipoParameter);
        }
    
        public virtual int pa_ModificaTipoServicioProducto(Nullable<int> idTipoServicioProducto, string codigo, string descripcion, Nullable<double> precio, Nullable<int> tipo)
        {
            var idTipoServicioProductoParameter = idTipoServicioProducto.HasValue ?
                new ObjectParameter("idTipoServicioProducto", idTipoServicioProducto) :
                new ObjectParameter("idTipoServicioProducto", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("descripcion", descripcion) :
                new ObjectParameter("descripcion", typeof(string));
    
            var precioParameter = precio.HasValue ?
                new ObjectParameter("precio", precio) :
                new ObjectParameter("precio", typeof(double));
    
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_ModificaTipoServicioProducto", idTipoServicioProductoParameter, codigoParameter, descripcionParameter, precioParameter, tipoParameter);
        }
    
        public virtual ObjectResult<pa_RetornaTipoServicioProductoID_Result> pa_RetornaTipoServicioProductoID(Nullable<int> idTipoServicioProducto)
        {
            var idTipoServicioProductoParameter = idTipoServicioProducto.HasValue ?
                new ObjectParameter("idTipoServicioProducto", idTipoServicioProducto) :
                new ObjectParameter("idTipoServicioProducto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaTipoServicioProductoID_Result>("pa_RetornaTipoServicioProductoID", idTipoServicioProductoParameter);
        }
    
        public virtual ObjectResult<pa_RetornaClasificacionSP_Result> pa_RetornaClasificacionSP(string nombre)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaClasificacionSP_Result>("pa_RetornaClasificacionSP", nombreParameter);
        }
    
        public virtual int pa_EliminaTiposVehiculo(Nullable<int> idTiposVehiculo)
        {
            var idTiposVehiculoParameter = idTiposVehiculo.HasValue ?
                new ObjectParameter("idTiposVehiculo", idTiposVehiculo) :
                new ObjectParameter("idTiposVehiculo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_EliminaTiposVehiculo", idTiposVehiculoParameter);
        }
    
        public virtual int pa_InsertaTiposVehiculo(string codigo, string tipo)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_InsertaTiposVehiculo", codigoParameter, tipoParameter);
        }
    
        public virtual int pa_ModificaTiposVehiculo(Nullable<int> idTipoVehiculo, string codigo, string tipo)
        {
            var idTipoVehiculoParameter = idTipoVehiculo.HasValue ?
                new ObjectParameter("idTipoVehiculo", idTipoVehiculo) :
                new ObjectParameter("idTipoVehiculo", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_ModificaTiposVehiculo", idTipoVehiculoParameter, codigoParameter, tipoParameter);
        }
    
        public virtual ObjectResult<pa_RetornaTiposVehiculo_Result> pa_RetornaTiposVehiculo(string codigo, string tipo)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaTiposVehiculo_Result>("pa_RetornaTiposVehiculo", codigoParameter, tipoParameter);
        }
    
        public virtual ObjectResult<pa_RetornaTiposVehiculoID_Result> pa_RetornaTiposVehiculoID(Nullable<int> idTipoVehiculo)
        {
            var idTipoVehiculoParameter = idTipoVehiculo.HasValue ?
                new ObjectParameter("idTipoVehiculo", idTipoVehiculo) :
                new ObjectParameter("idTipoVehiculo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaTiposVehiculoID_Result>("pa_RetornaTiposVehiculoID", idTipoVehiculoParameter);
        }
    
        public virtual int pa_EliminaMarcaVehiculo(Nullable<int> idMarcaVehiculo)
        {
            var idMarcaVehiculoParameter = idMarcaVehiculo.HasValue ?
                new ObjectParameter("idMarcaVehiculo", idMarcaVehiculo) :
                new ObjectParameter("idMarcaVehiculo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_EliminaMarcaVehiculo", idMarcaVehiculoParameter);
        }
    
        public virtual int pa_InsertaMarcaVehiculo(string codigo, Nullable<int> idPaisFabricante, string marca)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var idPaisFabricanteParameter = idPaisFabricante.HasValue ?
                new ObjectParameter("idPaisFabricante", idPaisFabricante) :
                new ObjectParameter("idPaisFabricante", typeof(int));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("marca", marca) :
                new ObjectParameter("marca", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_InsertaMarcaVehiculo", codigoParameter, idPaisFabricanteParameter, marcaParameter);
        }
    
        public virtual int pa_ModificaMarcaVehiculo(Nullable<int> idMarcaVehiculo, string codigo, Nullable<int> idPaisFabricante, string marca)
        {
            var idMarcaVehiculoParameter = idMarcaVehiculo.HasValue ?
                new ObjectParameter("idMarcaVehiculo", idMarcaVehiculo) :
                new ObjectParameter("idMarcaVehiculo", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var idPaisFabricanteParameter = idPaisFabricante.HasValue ?
                new ObjectParameter("idPaisFabricante", idPaisFabricante) :
                new ObjectParameter("idPaisFabricante", typeof(int));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("marca", marca) :
                new ObjectParameter("marca", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_ModificaMarcaVehiculo", idMarcaVehiculoParameter, codigoParameter, idPaisFabricanteParameter, marcaParameter);
        }
    
        public virtual ObjectResult<pa_RetornaMarcaVehiculoID_Result> pa_RetornaMarcaVehiculoID(Nullable<int> idMarcaVehiculo)
        {
            var idMarcaVehiculoParameter = idMarcaVehiculo.HasValue ?
                new ObjectParameter("idMarcaVehiculo", idMarcaVehiculo) :
                new ObjectParameter("idMarcaVehiculo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaMarcaVehiculoID_Result>("pa_RetornaMarcaVehiculoID", idMarcaVehiculoParameter);
        }
    
        public virtual ObjectResult<pa_RetornaPaisFabricante_Result> pa_RetornaPaisFabricante(string pais)
        {
            var paisParameter = pais != null ?
                new ObjectParameter("pais", pais) :
                new ObjectParameter("pais", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaPaisFabricante_Result>("pa_RetornaPaisFabricante", paisParameter);
        }
    
        public virtual int pa_ModificaPasswordCliente(Nullable<int> idCliente, string contrasena)
        {
            var idClienteParameter = idCliente.HasValue ?
                new ObjectParameter("idCliente", idCliente) :
                new ObjectParameter("idCliente", typeof(int));
    
            var contrasenaParameter = contrasena != null ?
                new ObjectParameter("contrasena", contrasena) :
                new ObjectParameter("contrasena", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_ModificaPasswordCliente", idClienteParameter, contrasenaParameter);
        }
    
        public virtual int pa_ModificaCliente(Nullable<int> idCliente, string nomCliente, string ape1Cliente, string ape2Cliente, string numCedula, Nullable<int> genero, Nullable<int> provincia, Nullable<System.DateTime> fechNacimiento, Nullable<int> canton, Nullable<int> distrito, string email, string pTelefono, Nullable<int> tipoCliente)
        {
            var idClienteParameter = idCliente.HasValue ?
                new ObjectParameter("idCliente", idCliente) :
                new ObjectParameter("idCliente", typeof(int));
    
            var nomClienteParameter = nomCliente != null ?
                new ObjectParameter("nomCliente", nomCliente) :
                new ObjectParameter("nomCliente", typeof(string));
    
            var ape1ClienteParameter = ape1Cliente != null ?
                new ObjectParameter("ape1Cliente", ape1Cliente) :
                new ObjectParameter("ape1Cliente", typeof(string));
    
            var ape2ClienteParameter = ape2Cliente != null ?
                new ObjectParameter("ape2Cliente", ape2Cliente) :
                new ObjectParameter("ape2Cliente", typeof(string));
    
            var numCedulaParameter = numCedula != null ?
                new ObjectParameter("numCedula", numCedula) :
                new ObjectParameter("numCedula", typeof(string));
    
            var generoParameter = genero.HasValue ?
                new ObjectParameter("genero", genero) :
                new ObjectParameter("genero", typeof(int));
    
            var provinciaParameter = provincia.HasValue ?
                new ObjectParameter("provincia", provincia) :
                new ObjectParameter("provincia", typeof(int));
    
            var fechNacimientoParameter = fechNacimiento.HasValue ?
                new ObjectParameter("fechNacimiento", fechNacimiento) :
                new ObjectParameter("fechNacimiento", typeof(System.DateTime));
    
            var cantonParameter = canton.HasValue ?
                new ObjectParameter("canton", canton) :
                new ObjectParameter("canton", typeof(int));
    
            var distritoParameter = distrito.HasValue ?
                new ObjectParameter("distrito", distrito) :
                new ObjectParameter("distrito", typeof(int));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var pTelefonoParameter = pTelefono != null ?
                new ObjectParameter("pTelefono", pTelefono) :
                new ObjectParameter("pTelefono", typeof(string));
    
            var tipoClienteParameter = tipoCliente.HasValue ?
                new ObjectParameter("tipoCliente", tipoCliente) :
                new ObjectParameter("tipoCliente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_ModificaCliente", idClienteParameter, nomClienteParameter, ape1ClienteParameter, ape2ClienteParameter, numCedulaParameter, generoParameter, provinciaParameter, fechNacimientoParameter, cantonParameter, distritoParameter, emailParameter, pTelefonoParameter, tipoClienteParameter);
        }
    
        public virtual int pa_EliminaVehiculo(string placa)
        {
            var placaParameter = placa != null ?
                new ObjectParameter("placa", placa) :
                new ObjectParameter("placa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_EliminaVehiculo", placaParameter);
        }
    
        public virtual ObjectResult<pa_RetornaVehiculosCliente_Result> pa_RetornaVehiculosCliente(string placa, string nombreCliente, string marca, string numCedula)
        {
            var placaParameter = placa != null ?
                new ObjectParameter("placa", placa) :
                new ObjectParameter("placa", typeof(string));
    
            var nombreClienteParameter = nombreCliente != null ?
                new ObjectParameter("nombreCliente", nombreCliente) :
                new ObjectParameter("nombreCliente", typeof(string));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("marca", marca) :
                new ObjectParameter("marca", typeof(string));
    
            var numCedulaParameter = numCedula != null ?
                new ObjectParameter("numCedula", numCedula) :
                new ObjectParameter("numCedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaVehiculosCliente_Result>("pa_RetornaVehiculosCliente", placaParameter, nombreClienteParameter, marcaParameter, numCedulaParameter);
        }
    
        public virtual ObjectResult<pa_RetornaVehiculosClienteID_Result> pa_RetornaVehiculosClienteID(Nullable<int> idVehiculosCliente)
        {
            var idVehiculosClienteParameter = idVehiculosCliente.HasValue ?
                new ObjectParameter("idVehiculosCliente", idVehiculosCliente) :
                new ObjectParameter("idVehiculosCliente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaVehiculosClienteID_Result>("pa_RetornaVehiculosClienteID", idVehiculosClienteParameter);
        }
    
        public virtual ObjectResult<pa_RetornaServiciosCliente_Result> pa_RetornaServiciosCliente(string tipo, string nombreCliente, string numCedula)
        {
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            var nombreClienteParameter = nombreCliente != null ?
                new ObjectParameter("nombreCliente", nombreCliente) :
                new ObjectParameter("nombreCliente", typeof(string));
    
            var numCedulaParameter = numCedula != null ?
                new ObjectParameter("numCedula", numCedula) :
                new ObjectParameter("numCedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaServiciosCliente_Result>("pa_RetornaServiciosCliente", tipoParameter, nombreClienteParameter, numCedulaParameter);
        }
    
        public virtual ObjectResult<pa_RetornaServiciosVehiculo_Result> pa_RetornaServiciosVehiculo(string tipo, string marca, string tipoVehiculo)
        {
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("marca", marca) :
                new ObjectParameter("marca", typeof(string));
    
            var tipoVehiculoParameter = tipoVehiculo != null ?
                new ObjectParameter("tipoVehiculo", tipoVehiculo) :
                new ObjectParameter("tipoVehiculo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaServiciosVehiculo_Result>("pa_RetornaServiciosVehiculo", tipoParameter, marcaParameter, tipoVehiculoParameter);
        }
    
        public virtual ObjectResult<pa_RetornaTipoServicioProducto_Result> pa_RetornaTipoServicioProducto(string descripcion, string tipo)
        {
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("descripcion", descripcion) :
                new ObjectParameter("descripcion", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaTipoServicioProducto_Result>("pa_RetornaTipoServicioProducto", descripcionParameter, tipoParameter);
        }
    
        public virtual ObjectResult<pa_RetornaMarcaVehiculo_Result> pa_RetornaMarcaVehiculo(string codigo, string marca, string pais)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var marcaParameter = marca != null ?
                new ObjectParameter("marca", marca) :
                new ObjectParameter("marca", typeof(string));
    
            var paisParameter = pais != null ?
                new ObjectParameter("pais", pais) :
                new ObjectParameter("pais", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_RetornaMarcaVehiculo_Result>("pa_RetornaMarcaVehiculo", codigoParameter, marcaParameter, paisParameter);
        }
    }
}
