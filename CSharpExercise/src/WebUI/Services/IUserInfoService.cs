using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Domain.Entities;

namespace CSharpExercise.src.WebUI.Services
{
    /// <summary>
    /// Interface for UserInfoService
    /// </summary>
    public interface IUserInfoService
    {
       Task<UserInfo> Authenticate(string username, string password);
    }

    /// <summary>
    /// Service for basic Auth using the UserInfoRepository
    /// </summary>
    public class UserInfoService : IUserInfoService
    {
        // locak variable for UserInfoRepository
        private readonly IUserInfoRepository _repository;

        // Consctrutor with UserInfoRepository injeciton
        public UserInfoService(IUserInfoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Authenticate method used by basic auth system to check creds on DB
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="password">user password</param>
        /// <returns></returns>
        public async Task<UserInfo> Authenticate(string username, string password)
        {
            var userInfo = await Task.Run(() => _repository.CheckAuthentication(username, password));
            if (username == null)
                return null;
            return userInfo;
        }
    }
}
