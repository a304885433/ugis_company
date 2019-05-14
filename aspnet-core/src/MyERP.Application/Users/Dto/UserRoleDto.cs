using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyERP.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Users.Dto
{
    /// <summary>
    /// 登录用户拥有的角色信息
    /// </summary>
    [AutoMapFrom(typeof(Role))]
    public class UserRoleDto : EntityDto<long>
    {
        /// <summary>
        /// 角色代码
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
