using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyERP.Authorization;

namespace MyERP
{
    [DependsOn(
        typeof(MyERPCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MyERPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MyERPAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MyERPApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
