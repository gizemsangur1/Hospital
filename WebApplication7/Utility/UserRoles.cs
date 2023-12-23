

namespace WebApplication7.Utility
{
    public class UserRoles
    {
        public const string Role_Admin = "Admin";
        public const string Role_Patient = "Patient";

        public static string[] AllRoles => new string[] { Role_Admin, Role_Patient };
    }
}
