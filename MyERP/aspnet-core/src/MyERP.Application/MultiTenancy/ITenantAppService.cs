using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyERP.MultiTenancy.Dto;

namespace MyERP.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

