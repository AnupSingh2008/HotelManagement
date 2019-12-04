using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Models;
using HotelManagement.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelManagement
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HMDBConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(
                 options =>
                 {
                     options.Password.RequiredLength = 4;
                     options.Password.RequiredUniqueChars = 1;
                     options.Password.RequireNonAlphanumeric = false;

                     options.SignIn.RequireConfirmedEmail = true;
                     options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmationTokenProvider";
                 })
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<CustomEmailConfirmationTokenProvider
            <ApplicationUser>>("CustomEmailConfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(5));
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(o =>o.TokenLifespan = TimeSpan.FromDays(3));

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 3;
            //    options.Password.RequireNonAlphanumeric = false;
            //});

            services.AddControllersWithViews().AddXmlSerializerFormatters();
            //services.AddSingleton<IEmployee, MockEmployeeRepository>();
            services.AddScoped<IEmployee, SqlEmployeeRepository>();
            services.AddScoped<IRoom, SqlRoomRepository>();
            services.AddMvc(config=> {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddAuthentication()
                .AddGoogle(options=> 
            {
                options.ClientId = "398007414664-hu74b89aa48nuqanlggot52e03pfh6l3.apps.googleusercontent.com";
                options.ClientSecret = "r6RWNF-kVPMuZrDpjs2_3uHQ";
            })
             .AddFacebook(options =>
             {
                 options.AppId = "2593981163993406";
                 options.AppSecret = "69840d6a012cd6d24adb1f42b67bce90";
             }
                );


            services.ConfigureApplicationCookie(options=> {
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Administration/AccessDenied");
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditRolePolicy", policy =>
                    policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.InvokeHandlersAfterFailure = false;
            });
            services.AddSingleton<IAuthorizationHandler,
             CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
            services.AddSingleton<DataProtectionPurposeStrings>();
        }
        private bool AuthorizeAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("Admin") &&
                    context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                    context.User.IsInRole("Super Admin");
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseExceptionHandler("/Error");
                
               // app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                this.CreateRoutes(endpoints);
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
        void CreateRoutes(IEndpointRouteBuilder enpoints) // <--
        {
            enpoints.MapControllerRoute( // <--
              "Events",
              string.Concat("{moniker}/{controller=Root}/{action=Index}/{id?}")
              );

            enpoints.MapControllerRoute( // <--
               "Default",
              "{controller=Root}/{action=Index}/{id?}"
            );

        }
    }
}
