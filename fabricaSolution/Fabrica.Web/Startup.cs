namespace Fabrica.Web
{
    using AutoMapper;
    using Data;
    using Fabrica.Data.Seeds;
    using Fabrica.Models;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Contracts;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
                //var b = options.Cookie.SecurePolicy == CookieSecurePolicy.Always;
            });

            services.AddMvc(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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

            //services.AddTransient<FabricaDbSeedData>();
            services.AddTransient<FabricaDBContext>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IPropsService, PropsService>();
            services.AddTransient<IMarvelousPropsService, MarvelousPropsService>();
            services.AddTransient<ICreditAccountsService, CreditAccountsService>();
            services.AddTransient<IOrdersService, OrdersService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiForgery, FabricaDBContext Dbcontext, UserManager<FabricaUser> userManager)
        {
            //seed database,roles,admins,users,props,marvelousprops,accounts
            FabricaDbSeedData seeder = new FabricaDbSeedData(Dbcontext, app, env, userManager);
            seeder.SeedAllData().Wait();

            //Mapper.Initialize(config => config.AddProfile<AutoMapperConfig>());
            AutoMapperConfig.ConfigureMapping();

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

            //middleware for status codes ex:notFoundPage
            app.UseStatusCodePagesWithReExecute(GlobalConstants.statusCodeReExecuteRounteTemplate);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: GlobalConstants.mvcMapRouteName,
                    template: GlobalConstants.mvcMapRouteTemplate);
            });

            app.Use(next => httpContext =>
            {
                string path = httpContext.Request.Path.Value;

                if (string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
                {
                    var tokens = antiForgery.GetAndStoreTokens(httpContext);
                    httpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
                }
                return next(httpContext);
            });
        }
    }
}