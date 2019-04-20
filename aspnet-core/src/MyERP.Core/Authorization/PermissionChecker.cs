using Abp.Authorization;
using MyERP.Authorization.Roles;
using MyERP.Authorization.Users;

namespace MyERP.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
