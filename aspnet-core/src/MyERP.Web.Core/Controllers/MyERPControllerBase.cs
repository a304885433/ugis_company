using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MyERP.Controllers
{
    public abstract class MyERPControllerBase: AbpController
    {
        protected MyERPControllerBase()
        {
            LocalizationSourceName = MyERPConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
