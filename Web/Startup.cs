using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Web.Startup))]

namespace Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			//services.AddDbContext<ApplicationDbContext>(options =>
			//	options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			//services.AddIdentity<ApplicationUser, IdentityRole>()
			//	.AddEntityFrameworkStores<ApplicationDbContext>()
			//	.AddDefaultTokenProviders();

			//services.AddMvc();

			//// Add application services.
			//services.AddTransient<IEmailSender, AuthMessageSender>();
			//services.AddTransient<ISmsSender, AuthMessageSender>();
		}
	}
}
