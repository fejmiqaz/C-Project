using System;
using System.Collections.Generic;

namespace ASP.NET_Core_6._0_API.Entities
{
    public partial class Receta
    {
        public Receta()
        {
            Perberesit = new HashSet<Perberesi>();
        }

        public int Id { get; set; }
        public string? Emri { get; set; }
        public string? Udhezimet { get; set; }
        public int? Kohezgjatja { get; set; }
        public int? Kalorite { get; set; }
        public int? KategoriaId { get; set; }

        public virtual Kategoria? Kategoria { get; set; }
        public virtual ICollection<Perberesi> Perberesit { get; set; }
    }
}
