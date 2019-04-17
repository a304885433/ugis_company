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

        public DbSet<MedType> MedType { get; set; }

    }
}
