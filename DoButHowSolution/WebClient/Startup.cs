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

            services.AddMvc().AddNToastNotify(new ToastOption()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.BottomCenter
            });;

            services.AddMvc(config =>
            {
                //config.Filters.Add(new RequireHttpsAttribute());
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<ApplicationSignInManager>();
            services.AddScoped<ApplicationUserManager>();

            services.AddScoped<IQuestionServices>(serviceProvider =>
            {
                return new QuestionServices();
            });

            services.AddScoped<IAnswerServices>(serviceProvider =>
            {
                return new AnswerServices();
            });
            services.AddSingleton(serviceProvider =>
            {
                return new MapperService();
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
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie();

            services.AddScoped<RoleManager<IdentityRole>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager)
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

            // Time to seed the database
            var initializer = new CompositeDatabaseInitializer(new IDatabaseInitializer[]
            {
                 new AddDefaultRolesDatabaseInititalizer(app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>())
            });

            initializer.Seed();

            //await CreateRoles(serviceProvider, roleManager);
        }

        private async Task CreateRoles(IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager)
        {
            //adding custom roles
            //var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Moderator", "User" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //creating a super user who could maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = "admin@email.com",
                Email = "admin@email.com"
            };

            string UserPassword = "admin@email.com";
            var _user = await UserManager.FindByEmailAsync(poweruser.Email);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }

        }
    }

    public interface IDatabaseInitializer
    {
        int Order { get; }

        void Seed();
    }

    public class AddDefaultRolesDatabaseInititalizer : IDatabaseInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddDefaultRolesDatabaseInititalizer(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public int Order { get; } = 1;

        public void Seed()
        {
            using (_roleManager)
            {
                if (_roleManager.Roles.Any()) return;

                var roles = new[]
                { "Admin", "Moderator", "User" }; 

                foreach (var role in roles)
                {
                    var result = _roleManager.CreateAsync(new IdentityRole { Name = role }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception("Error creating roles.");
                    }
                }
            }

        }
    }

    public class CompositeDatabaseInitializer : IDatabaseInitializer
    {
        private readonly IEnumerable<IDatabaseInitializer> _databaseInitializers;

        public CompositeDatabaseInitializer(IEnumerable<IDatabaseInitializer> databaseInitializers)
        {
            _databaseInitializers = databaseInitializers;
        }

        public int Order { get; } = 0;

        public void Seed()
        {
            foreach (var databaseInitializer in _databaseInitializers.OrderBy(initializer => initializer.Order))
            {
                databaseInitializer.Seed();
            }
            
        }
    }
}
