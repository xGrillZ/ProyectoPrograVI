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
    
    public partial class Provincia
    {
        public Provincia()
        {
            this.Canton = new HashSet<Canton>();
            this.Cliente = new HashSet<Cliente>();
        }
    
        public int id_Provincia { get; set; }
        public string nombre { get; set; }
        public string usuarioCrea { get; set; }
        public Nullable<System.DateTime> fechaCrea { get; set; }
        public string usuarioModifica { get; set; }
        public Nullable<System.DateTime> fechaModifica { get; set; }
        public string vc_Estado { get; set; }
    
        public virtual ICollection<Canton> Canton { get; set; }
        public virtual ICollection<Cliente> Cliente { get; set; }
    }
}
