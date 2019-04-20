using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyERP.Configuration;
using MyERP.Web;

namespace MyERP.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MyERPDbContextFactory : IDesignTimeDbContextFactory<MyERPDbContext>
    {
        public MyERPDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyERPDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MyERPDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MyERPConsts.ConnectionStringName));

            return new MyERPDbContext(builder.Options);
        }
    }
}
