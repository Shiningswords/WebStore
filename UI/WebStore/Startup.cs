﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.AutoMapperProfiles;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestApi;
using WebStore.Services.Data;
using WebStore.Services.Products;
using WebStore.Clients.Values;
using WebStore.Clients.Employees;
using WebStore.Clients.Products;
using WebStore.Clients.Orders;
using WebStore.Clients.Identity;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ViewModelsMapping>();
            }, typeof(Startup));



            services.AddDbContext<WebStoreDb>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<WebStoreDbInitializer>();

            services.AddIdentity<User, Role>(/*opt => { }*/)
                           //.AddEntityFrameworkStores<WebStoreDb>()
                           .AddDefaultTokenProviders();

            #region WebAPI Identity clients stores

            services
               .AddTransient<IUserStore<User>, UsersClient>()
               .AddTransient<IUserPasswordStore<User>, UsersClient>()
               .AddTransient<IUserEmailStore<User>, UsersClient>()
               .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
               .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
               .AddTransient<IUserClaimStore<User>, UsersClient>()
               .AddTransient<IUserLoginStore<User>, UsersClient>();
            services
               .AddTransient<IRoleStore<Role>, RolesClient>();

            #endregion

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCD1234567890";
                opt.User.RequireUniqueEmail = false;
#endif

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });


            services.AddControllersWithViews(opt =>
            {
                //opt.Filters.Add<>()
                //opt.Conventions
                //opt.Conventions.Add();
            })
                .AddRazorRuntimeCompilation();


            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>(); //временный
            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>(); //постоянный в пределах области
            //services.AddSingleton<IProductData, InMemoryProductData>();
            //services.AddScoped<IEmployeesData, SqlEmployeesData>();
            services.AddScoped<IEmployeesData, EmployeesClient>();

            //services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IProductData, ProductsClient>();
            services.AddScoped<ICartService, CookiesCartService>();
            //services.AddScoped<IOrderService, SqlOrderService>();
            services.AddScoped<IOrderService, OrdersClient>();

            services.AddTransient<IValueService, ValuesClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db/*, IServiceProvider Services*/)
        {
            db.Initialize();

            //var employees = Services.GetService<IEmployeesData>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();


            app.UseWelcomePage("/MVC");

            //app.Use(async (context, next) =>
            //{
            //    Debug.WriteLine($"Request to {context.Request.Path}");
            //    await next(); //Можем прервать конвейер не вызывая await next()
            //    //Постобработка
            //});
            //app.UseMiddleware<>()

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
