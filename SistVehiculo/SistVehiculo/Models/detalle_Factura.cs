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
    
    public partial class detalle_Factura
    {
        public int num_detalle { get; set; }
        public int num_factura { get; set; }
        public int idTipoServicioProducto { get; set; }
        public int cantidadServicioProducto { get; set; }
        public double Precio { get; set; }
    
        public virtual TipoServicioProducto TipoServicioProducto { get; set; }
        public virtual Factura Factura { get; set; }
    }
}
