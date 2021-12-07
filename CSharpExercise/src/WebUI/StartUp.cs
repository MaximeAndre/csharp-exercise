using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Infrastructure.Handlers;
using CSharpExercise.src.Infrastructure.Persistance;
using CSharpExercise.src.Infrastructure.Repositories;
using CSharpExercise.src.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;



namespace CSharpExercise.src.WebUI
{
    public class Startup
    {
        /// <summary>
        /// Constructor Getting appsetting.json configuration
        /// </summary>
        /// <param name="configuration">appsetting.json</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adding controllers
            services.AddControllers();

            // Injecting the ApplicationDbContext to link DB          
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PostgreConnectionString")));

            // Affecting the right implementation of IUserInfoRepository if multiples are avaliable
            services.AddScoped<IUserInfoRepository, PostgreSqlUserInfoRepository>();

            // Settign up basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // Injecting UserInfoService
            services.AddScoped<UserInfoService, UserInfoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Gestion multi-origin 
            // Allow any request
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();

            // Use HTTP instead
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }


}