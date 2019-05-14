using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using System.Collections.Generic;

namespace MyERP.Authorization
{
    public class MyERPAuthorizationProvider : AuthorizationProvider
    {
        private string[] curdAction = new[] { "create", "update", "read", "delete" };
        private string[] reportAction = new[] { "read", "export" };

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            // 默认权限
            //context.CreatePermission(PermissionNames.Users, L("Users"));
            //context.CreatePermission(PermissionNames.Roles, L("Roles"));
            //context.CreatePermission(PermissionNames.Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            CreatePermission(context, PermissionNames.UserManager, curdAction);
            CreatePermission(context, PermissionNames.RoleManager, curdAction);
            CreatePermission(context, PermissionNames.CompanyManager, curdAction);
            CreatePermission(context, PermissionNames.CheckManager, curdAction);
            CreatePermission(context, PermissionNames.Report1, reportAction);
            CreatePermission(context, PermissionNames.Report2, reportAction);
            CreatePermission(context, PermissionNames.Report3, reportAction);
            CreatePermission(context, PermissionNames.Report4, reportAction);
            CreatePermission(context, PermissionNames.Report5, reportAction);
            CreatePermission(context, PermissionNames.BaseDataManager, curdAction);
        }

        private void CreatePermission(IPermissionDefinitionContext context, string name, string[] actions = null, string[] extraActions = null)
        {
            context.CreatePermission(name, L(name));
            if (actions != null && actions.Length > 0)
            {
                foreach (var item in actions)
                {
                    context.CreatePermission($"{name}.{item}", L(item));
                }
            }
            if (extraActions != null && extraActions.Length > 0)
            {
                foreach (var item in extraActions)
                {
                    context.CreatePermission($"{name}.{item}", L(item));
                }
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MyERPConsts.LocalizationSourceName);
        }
    }
}
