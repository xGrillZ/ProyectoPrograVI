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
    
    public partial class clasificacionSP
    {
        public clasificacionSP()
        {
            this.TipoServicioProducto = new HashSet<TipoServicioProducto>();
            this.ServiciosCliente = new HashSet<ServiciosCliente>();
        }
    
        public int id_clasificacionSP { get; set; }
        public string nombreClasificacion { get; set; }
    
        public virtual ICollection<TipoServicioProducto> TipoServicioProducto { get; set; }
        public virtual ICollection<ServiciosCliente> ServiciosCliente { get; set; }
    }
}
