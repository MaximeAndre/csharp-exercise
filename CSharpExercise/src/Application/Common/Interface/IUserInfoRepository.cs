using CSharpExercise.src.Domain.Entities;

namespace CSharpExercise.src.Application.Common.Interface
{
    /// <summary>
    /// Interface for User entity repository
    /// </summary>
    public interface IUserInfoRepository
    {
        UserInfo Get(string login, string pwd);

        UserInfo? Add(UserInfo userInfo);

    }
}
