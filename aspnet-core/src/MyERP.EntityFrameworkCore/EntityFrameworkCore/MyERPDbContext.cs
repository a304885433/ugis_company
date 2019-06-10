using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyERP.Authorization.Roles;
using MyERP.Authorization.Users;
using MyERP.MultiTenancy;
using MyERP.UGIS;

namespace MyERP.EntityFrameworkCore
{
    public class MyERPDbContext : AbpZeroDbContext<Tenant, Role, User, MyERPDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public MyERPDbContext(DbContextOptions<MyERPDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 检查结果
        /// </summary>
        public DbSet<ChkResult> ChkResult { get; set; }
        /// <summary>
        /// 企业信息
        /// </summary>
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
        /// <summary>
        /// 企业药品信息
        /// </summary>
        public DbSet<CompanyMedcineType> CompanyMedcineType { get; set; }
        /// <summary>
        /// 企业排放因子信息
        /// </summary>
        public DbSet<CompanyPoluType> CompanyPoluType { get; set; }
        /// <summary>
        /// 企业污染物排放信息
        /// </summary>
        public DbSet<CompanyContaminants> CompanyContaminants { get; set; }

        /// <summary>
        /// 字典
        /// </summary>
        public DbSet<Dic> Dic { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public DbSet<DicType> DicType { get; set; }
        /// <summary>
        /// 测量方法
        /// </summary>
        public DbSet<MeaureMethod> MeaureMethod { get; set; }
        /// <summary>
        /// 试剂耗材
        /// </summary>
        public DbSet<Reagent> Reagent { get; set; }

    }
}
