using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using System.Collections.Generic;

namespace MyERP.Authorization
{
    public class MyERPAuthorizationProvider : AuthorizationProvider
    {
        private string[] curAction = new[] { "create", "update", "read" };
        private string[] curdAction = new[] { "create", "update", "read", "delete" };
        private string[] formAction = new[] { "save" };
        private string[] reportAction = new[] { "export" };

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            // 默认权限
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            CreatePermission(context, PermissionNames.Pages_Company, curdAction);
            CreatePermission(context, PermissionNames.Pages_CompanyEdit, formAction);
            CreatePermission(context, PermissionNames.Pages_Check, curAction);
            CreatePermission(context, PermissionNames.Pages_CheckEdit, formAction);
            CreatePermission(context, PermissionNames.Pages_Report1);
            CreatePermission(context, PermissionNames.Pages_Report2);
            CreatePermission(context, PermissionNames.Pages_Report4);
            CreatePermission(context, PermissionNames.Pages_Report5);
            CreatePermission(context, PermissionNames.Pages_BaseData, curdAction);
        }

        private void CreatePermission(IPermissionDefinitionContext context, string name, string[] actions = null, string[] extraActions = null )
        {
            context.CreatePermission(name, L(name));
            //if (actions != null && actions.Length > 0)
            //{
            //    foreach (var item in actions)
            //    {
            //        context.CreatePermission(item, L(item));
            //    }
            //}
            //if (extraActions != null && extraActions.Length > 0)
            //{
            //    foreach (var item in extraActions)
            //    {
            //        context.CreatePermission(item, L(item));
            //    }
            //}
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MyERPConsts.LocalizationSourceName);
        }
    }
}
