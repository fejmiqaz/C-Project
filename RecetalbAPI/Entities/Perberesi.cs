using System;
using System.Collections.Generic;

namespace ASP.NET_Core_6._0_API.Entities
{
    public partial class Perberesi
    {
        public int Id { get; set; }
        public string? Emri { get; set; }
        public int? RecetaId { get; set; }

        public virtual Receta? Receta { get; set; }
    }
}
