using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.Infrastructure;

namespace CSharpExercise.src.Application.UserInfos
{
    public class PostgreSqlUserInfoRepository : IUserInfoRepository
    {
        private ApplicationDbContext context;

        //Injection du DbContext pour accéder à la base Postgresql
        public PostgreSqlUserInfoRepository (ApplicationDbContext context)
        {
            this.context = context;
        }
        public UserInfo CheckAuthentication(string login, string pwd)
        {
            UserInfo result;

            try
            {
                //string passwordHash = BCrypt.Net.BCrypt.HashPassword(pwd);
                //Can be null if not found
                result = context.UserInfos.FirstOrDefault(u => u.Login == login);

                //if login does not exist or password does not match => unauthorized
                if (result == null || !BCrypt.Net.BCrypt.Verify(pwd, result.Password))
                {
                    result = new UserInfo()
                    {
                        Id = -1
                    };
                }

            }
            catch
            {
                // If we have an error on the request we just return null
                return null;
            }

            //don't forget to BCrypt when registering 
            return result;

        }
    }
}
