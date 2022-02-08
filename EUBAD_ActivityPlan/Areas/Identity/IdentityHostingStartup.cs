using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EUBAD_ActivityPlan.Areas.Identity.IdentityHostingStartup))]
namespace EUBAD_ActivityPlan.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}