using System.Threading.Tasks;
using Abp.Application.Services;
using MyERP.Sessions.Dto;

namespace MyERP.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
