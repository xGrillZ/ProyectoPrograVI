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
    
    public partial class Estado
    {
        public Estado()
        {
            this.Factura = new HashSet<Factura>();
        }
    
        public int id_estado { get; set; }
        public string nomEstado { get; set; }
    
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
