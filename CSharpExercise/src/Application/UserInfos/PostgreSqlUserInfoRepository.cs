using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.Infrastructure.Persistance;

namespace CSharpExercise.src.Application.UserInfos
{
    /// <summary>
    /// IUserInforepository implementation for PostgreSql
    /// </summary>
    public class PostgreSqlUserInfoRepository : IUserInfoRepository
    {
        private ApplicationDbContext context;

        //Injection du DbContext pour accéder à la base Postgresql
        public PostgreSqlUserInfoRepository (ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Check wether the auth info are correct and then return user info
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="pwd">user password</param>
        /// <returns></returns>
        public async Task<UserInfo> CheckAuthentication(string login, string pwd)
        {
            UserInfo result;

            try
            {
                //Can be null if not found
                result = context.UserInfos.FirstOrDefault(u => u.Login == login && u.Password == pwd);
            }
            catch
            {
                // If we have an error on the request we just return null
                return null;
            }

            return result;

        }
    }
}
