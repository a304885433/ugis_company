using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace MyERP.Web.Host.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // BuildWebHost(args).Run();
            Console.WriteLine("server run");
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
