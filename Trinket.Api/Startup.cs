using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace Trinket.Api
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(owin => owin.UseNancy());
        }
    }
}
