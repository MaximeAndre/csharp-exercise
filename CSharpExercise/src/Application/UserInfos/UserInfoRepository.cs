using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Domain.Entities;

namespace CSharpExercise.src.Application.UserInfos
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private List<UserInfo> _userInfoList = new List<UserInfo>()
        {
            new UserInfo()
            {
                Id= 1,
                Login= "mxwind",
                Password= "test",
                FirstName= "Maxime",
                LastName= "Andre",
                Email= "maxime@andre.com"
            },
            new UserInfo()
            {
                Id= 2,
                Login= "mxwind",
                Password= "test",
                FirstName= "Maxime",
                LastName= "Andre",
                Email= "maxime@andre.com"
            },
            new UserInfo()
            {
                Id= 3,
                Login= "mxwind",
                Password= "test",
                FirstName= "Maxime",
                LastName= "Andre",
            }
        };


        public UserInfo CheckAuthentication(string login, string pwd)
        {
            var currentUser = _userInfoList.FirstOrDefault(u => u.Password == pwd && u.Login == login);
            //handle errors and multiple
            return currentUser;
            
        }
    }
}
