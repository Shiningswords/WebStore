using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Products;

namespace WebStore.ServiceHosting
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDb>(opt =>
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<WebStoreDbInitializer>();

            services.AddControllers();

            services.AddScoped<IEmployeesData, SqlEmployeesData>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDbInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
