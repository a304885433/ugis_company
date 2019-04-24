using System.Collections.Generic;
using Abp.Configuration;
using Abp.Localization;

namespace MyERP.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme,"red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true
                ),

             new SettingDefinition(LocalizationSettingNames.DefaultLanguage,AppSettingNames.DefaultLanguageName),

            };
        }
    }
}
