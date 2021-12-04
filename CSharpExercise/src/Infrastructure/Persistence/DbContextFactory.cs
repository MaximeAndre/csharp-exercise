using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CSharpExercise.src.Infrastructure.Persistance
{
    //unUsed this class is used to handle multiuple instance of applicationDbContext
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public DbContextFactory()
        {
        }

        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public ApplicationDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(Configuration.GetConnectionString("PostgreConnectionString"));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
