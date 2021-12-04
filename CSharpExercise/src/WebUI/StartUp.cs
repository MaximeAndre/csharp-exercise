using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Application.UserInfos;
using CSharpExercise.src.Infrastructure;
using CSharpExercise.src.WebUI.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using NSwag;



namespace CSharpExercise.src.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PostgreConnectionString")));

            //Select the IRepository we want to use;
            services.AddScoped<IUserInfoRepository, PostgreSqlUserInfoRepository>();

            // Authentication proces to get infos from the current user.
           /* services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x=>x.LoginPath="/account/login");*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Register the Swagger generator and the Swagger UI middleware
            // app.UseSwaggerUi3();
            // app.UseReDoc(); // serve ReDoc UI

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }


}