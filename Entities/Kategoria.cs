using System;
using System.Collections.Generic;

namespace ASP.NET_Core_6._0_API.Entities
{
    public partial class Kategoria
    {
        public Kategoria()
        {
            //Receta = new HashSet<Receta>();
        }

        public int Id { get; set; }
        public string? Emri { get; set; }

        //public virtual ICollection<Receta> Receta { get; set; }
    }
}
