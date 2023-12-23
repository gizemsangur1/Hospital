// WebApplication7.Utility klasöründeki UserRolesManager.cs

using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace WebApplication7.Utility
{
    public class UserRolesManager
    {
        private readonly ApplicationDbContext _context;

        public UserRolesManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddRolesToDatabase()
        {
            foreach (var roleName in UserRoles.AllRoles)
            {
                if (!_context.Roles.Any(r => r.Name == roleName))
                {
                    _context.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName.ToUpper() });
                }
            }

            _context.SaveChanges();
        }
    }
}
