using CSharpExercise.src.Domain.Entities;

namespace CSharpExercise.src.Application.Common.Interface
{
    public interface IUserInfoRepository
    {
        UserInfo CheckAuthentication(string login, string pwd);
    }
}
