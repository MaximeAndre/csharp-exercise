using CSharpExercise.src.Domain.Entities;

namespace CSharpExercise.src.Application.Common.Interface
{
    /// <summary>
    /// Interface for User entity repository
    /// </summary>
    public interface IUserInfoRepository
    {
        Task<UserInfo> CheckAuthentication(string login, string pwd);

    }
}
