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
        private readonly ApplicationDbContext _context;

        //Injection du DbContext pour accéder à la base Postgresql
        public PostgreSqlUserInfoRepository (ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Add User In DB
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserInfo Add(UserInfo userInfo)
        {
            _context.UserInfos.Add(userInfo);
            _context.SaveChanges();
            return userInfo;
        }

        /// <summary>
        /// Get user info from login/password
        /// </summary>
        /// <param name="login">user login</param>
        /// <param name="pwd">user password</param>
        /// <returns></returns>
        public UserInfo? Get(string login, string pwd)
        { 
            return _context.UserInfos.FirstOrDefault(u => u.Login == login && u.Password == pwd);
        }
    }
}
