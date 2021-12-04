using CSharpExercise.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSharpExercise.src.Infrastructure.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo()
                {
                    Id = 1,
                    Login = "LGilly",
                    Password = BCrypt.Net.BCrypt.HashPassword("LGillyPwd"),
                    FirstName = "Laurent",
                    LastName = "G.",
                    Email = "l.g@oplead.com"
                },
               new UserInfo()
               {
                   Id = 2,
                   Login = "MAndre",
                   Password = BCrypt.Net.BCrypt.HashPassword("MAndrePwd"),
                   FirstName = "Maxime",
                   LastName = "Andre",
                   Email = "maxime@andre.com"
               },
               new UserInfo()
               {
                   Id = 3,
                   Login = "ETaume",
                   Password = BCrypt.Net.BCrypt.HashPassword("ETaumePwd"),
                   FirstName = "Emma",
                   LastName = "Taume",
                   Email = "emma@taume.com"
               }
           );
        }
    }
}
