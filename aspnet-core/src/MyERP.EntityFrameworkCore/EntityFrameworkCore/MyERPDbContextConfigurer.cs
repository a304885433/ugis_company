using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyERP.EntityFrameworkCore
{
    public static class MyERPDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyERPDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString, t => t.UseRowNumberForPaging());
        }

        public static void Configure(DbContextOptionsBuilder<MyERPDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection, t => t.UseRowNumberForPaging() );
        }
    }
}
