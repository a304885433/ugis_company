using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using MyERP.Sessions.Dto;
using System.Linq;
using Abp.AutoMapper;
using MyERP.Users.Dto;

namespace MyERP.Sessions
{
    public class SessionAppService : MyERPAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                var user = await GetCurrentUserAsync();

                output.User = ObjectMapper.Map<UserLoginInfoDto>(user);
            }

            return output;
        }
    }
}
