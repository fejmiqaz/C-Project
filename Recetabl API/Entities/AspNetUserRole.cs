using System;
using System.Collections.Generic;

namespace ASP.NET_Core_6._0_API.Entities
{
    public partial class AspNetUserRole
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string RoleId { get; set; } = null!;

        public virtual AspNetRole Role { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
