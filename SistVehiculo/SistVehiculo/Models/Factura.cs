//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Factura
    {
        public string id_factura { get; set; }
        public System.DateTime fecha { get; set; }
        public int idCliente { get; set; }
        public int idVehiculo { get; set; }
        public string numFactura { get; set; }
        public int id_estado { get; set; }
        public double montoTotal { get; set; }
        public int id_detalleFactura { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual detalle_Factura detalle_Factura { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual VehiculosCliente VehiculosCliente { get; set; }
    }
}
