using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebClient.Services;
using Dbh.ServiceLayer.Contracts;
using Dbh.Model.EF.Entities;
using Dbh.ServiceLayer.Services;
using MVCWebClient.Services;
using Microsoft.AspNetCore.Identity;
using NToastNotify;
using Microsoft.AspNetCore.Authentication.Google;

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebClient
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<Dbh.Model.EF.Context.UserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 5;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<Dbh.Model.EF.Context.UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });
            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.BottomRight
            });

            services.AddMvc(config =>
            {
                //config.Filters.Add(new RequireHttpsAttribute());
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<ApplicationSignInManager>();
            services.AddScoped<ApplicationUserManager>();

            services.AddScoped<IQuestionServices>(serviceProvider => {
                return new QuestionServices();
            });
            services.AddScoped<IAnswerServices>(serviceProvider => {
                return new AnswerServices();
            });
            services.AddSingleton(serviceProvider => {
                return new MapperService();
            });
            services.AddSingleton(serviceProvider => {
                return new Utils();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireModeratorRole", policy => policy.RequireRole("Moderator"));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
                options.AddPolicy("RequireAtLeastUserRole", policy => policy.RequireRole("User", "Moderator", "Admin"));
                options.AddPolicy("RequireAtLeastModeratorRole", policy => policy.RequireRole("Admin", "Moderator"));
                options.AddPolicy("RequireAtLeastAdminRole", policy => policy.RequireRole("Admin"));

            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie();

            services.AddScoped<RoleManager<IdentityRole>>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseNToastNotify();

            // Time to seed the database
            var initializer = new CompositeDatabaseInitializer(new IDatabaseInitializer[]
            {
                 new AddDefaultRolesDatabaseInititalizer(app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>(), app.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>())
            });

            initializer.Seed();
        }
    }
}
