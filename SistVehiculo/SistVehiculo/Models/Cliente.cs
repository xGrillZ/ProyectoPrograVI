//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistVehiculo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cliente
    {
        public Cliente()
        {
            this.Factura = new HashSet<Factura>();
            this.ServiciosCliente = new HashSet<ServiciosCliente>();
            this.VehiculosCliente = new HashSet<VehiculosCliente>();
        }
    
        public int idCliente { get; set; }
        public string nomCliente { get; set; }
        public string ape1Cliente { get; set; }
        public string ape2Cliente { get; set; }
        public string numCedula { get; set; }
        public int genero { get; set; }
        public int provincia { get; set; }
        public System.DateTime fechNacimiento { get; set; }
        public int canton { get; set; }
        public int distrito { get; set; }
        public string email { get; set; }
        public string pTelefono { get; set; }
        public int tipoCliente { get; set; }
        public System.DateTime ultimoIngreso { get; set; }
        public string contrasena { get; set; }
    
        public virtual Canton Canton1 { get; set; }
        public virtual Distrito Distrito1 { get; set; }
        public virtual Genero Genero1 { get; set; }
        public virtual Provincia Provincia1 { get; set; }
        public virtual TipoCliente TipoCliente1 { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<ServiciosCliente> ServiciosCliente { get; set; }
        public virtual ICollection<VehiculosCliente> VehiculosCliente { get; set; }
    }
}
