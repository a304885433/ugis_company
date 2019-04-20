using System.Threading.Tasks;
using MyERP.Configuration.Dto;

namespace MyERP.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
