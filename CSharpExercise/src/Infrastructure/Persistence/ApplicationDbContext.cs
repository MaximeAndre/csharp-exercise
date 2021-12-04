using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.Infrastructure.Persistence;
using CSharpExercise.src.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CSharpExercise.src.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserInfo> UserInfos { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
            modelBuilder.Seed();
           
        }

    }
}
