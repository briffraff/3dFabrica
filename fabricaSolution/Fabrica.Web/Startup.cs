namespace Fabrica.Web
{
    using AutoMapper;
    using Data;
    using Fabrica.Models;
    using Services;
    using Services.Contracts;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Fabrica.Data.Seeds;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<FabricaDBContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString(GlobalConstants.connectionName)));

            services.AddIdentity<FabricaUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = GlobalConstants.PasswordMin;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = GlobalConstants.UniqueChars;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    options.SignIn.RequireConfirmedEmail = false;

                    options.User.AllowedUserNameCharacters = GlobalConstants.AllowedChars;
                    options.User.RequireUniqueEmail = true;

                })
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<FabricaDBContext>();

            //TODO Register services
            //services.AddTransient<FabricaDbSeedData>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IPropsService, PropsService>();
            services.AddTransient<IMarvelousPropsService, MarvelousPropsService>();
            //services.AddTransient<IOrdersService, OrdersService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FabricaDBContext context)
        {
            //seed database,roles,admins,users,props,marvelousprops
            FabricaDbSeedData seeder = new FabricaDbSeedData(context,app,env);
            seeder.SeedAllData().Wait();

            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler(GlobalConstants.exceptionHandlerPath);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: GlobalConstants.mvcMapRouteName,
                    template: GlobalConstants.mvcMapRouteTemplate);
            });
        }
    }
}