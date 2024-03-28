using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_6._0_API.Entities
{
    public class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetUser User { get; set; }
        public virtual AspNetRole Role { get; set; }
    }
}
