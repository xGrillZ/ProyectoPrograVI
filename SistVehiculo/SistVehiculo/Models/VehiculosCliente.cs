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
    
    public partial class VehiculosCliente
    {
        public VehiculosCliente()
        {
            this.Factura = new HashSet<Factura>();
        }
    
        public int idVehiculosCliente { get; set; }
        public int idVehiculo { get; set; }
        public int idCliente { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Vehiculos Vehiculos { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
