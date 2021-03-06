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
    
    public partial class Factura
    {
        public Factura()
        {
            this.detalle_Factura = new HashSet<detalle_Factura>();
        }
    
        public int id_factura { get; set; }
        public string num_factura { get; set; }
        public System.DateTime fecha { get; set; }
        public double montoTotal { get; set; }
        public int estado { get; set; }
        public int idCliente { get; set; }
        public int idVehiculo { get; set; }
        public int idTipoVehiculo { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<detalle_Factura> detalle_Factura { get; set; }
        public virtual Estado Estado1 { get; set; }
        public virtual Vehiculos Vehiculos { get; set; }
        public virtual TiposVehiculo TiposVehiculo { get; set; }
    }
}
