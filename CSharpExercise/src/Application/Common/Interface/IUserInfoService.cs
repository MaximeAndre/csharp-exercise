using CSharpExercise.src.Domain.Entities;

namespace CSharpExercise.src.Application.Common.Interface
{
    public interface IUserInfoService
    {
        /// <summary>
        /// Interface for UserInfoService
        /// </summary>
        public interface IUserInfoService
        {
            Task<UserInfo> Authenticate(string username, string password);
        }
    }
}
