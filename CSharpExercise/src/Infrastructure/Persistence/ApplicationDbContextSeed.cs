using CSharpExercise.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSharpExercise.src.Infrastructure.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Seeding 
            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo()
                {
                    Id = 1,
                    Login = "LGilly",
                    Password = "$2a$11$b66P/YxRNxaCad4iB6EsUO7VO2Rykq1.jJFwFhow.yS0NxFEuQnci",
                    FirstName = "Laurent",
                    LastName = "G.",
                    Email = "l.g@oplead.com"
                },
               new UserInfo()
               {
                   Id = 2,
                   Login = "MAndre",
                   Password = "$2a$11$wZOx21wLPR4YuBWVg.soruWxMHo6kbH4g0s3FO6ORaF7upuuZ2Ee6",
                   FirstName = "Maxime",
                   LastName = "Andre",
                   Email = "maxime@andre.com"
               },
               new UserInfo()
               {
                   Id = 3,
                   Login = "ETaume",
                   Password = "$2a$11$OlFzyJ425O67VSd5ibDDrOeXhD1WV2A4P5hrqWdzr/xLImD7zMesG",
                   FirstName = "Emma",
                   LastName = "Taume",
                   Email = "emma@taume.com"
               }
           );
        }
    }
}
