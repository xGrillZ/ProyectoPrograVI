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
    
    public partial class Genero
    {
        public Genero()
        {
            this.Cliente = new HashSet<Cliente>();
        }
    
        public int id_Genero { get; set; }
        public string nombreGenero { get; set; }
    
        public virtual ICollection<Cliente> Cliente { get; set; }
    }
}
