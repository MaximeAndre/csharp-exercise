using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.Infrastructure.Persistence;
using CSharpExercise.src.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CSharpExercise.src.Infrastructure.Persistance
{
    /// <summary>
    /// Class that does the link between the app and the DB
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// DbSet to handle the UserInfo entity
        /// </summary>
        public DbSet<UserInfo> UserInfos { get; set;}

        /// <summary>
        /// This method ils called when the db model is created
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Calling config for userInfo Entity
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
            //Seeding Db 
            modelBuilder.Seed();         
        }
    }
}
