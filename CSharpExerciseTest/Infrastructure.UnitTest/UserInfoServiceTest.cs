using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.Infrastructure.Persistance;
using CSharpExercise.src.Infrastructure.Repositories;
using CSharpExercise.src.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExercieUnitTest.Infrastructure.UnitTest
{
    /// <summary>
    /// Testing Class for userService
    /// </summary>
    [TestClass]
    public class UserInfoServiceTest
    {

        private IUserInfoRepository _userInfoRepository;

        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create InMemoryDb
            var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkInMemoryDatabase()
               .BuildServiceProvider();

            //Specify DbContextOptions
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("CSharp_Exercise")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestInitialize]
        public async Task Init()
        {
            var options = CreateNewContextOptions();
            //using mockRepository 
            _userInfoRepository = new PostgreSqlUserInfoRepository(new ApplicationDbContext(options));

            _userInfoRepository.Add(new UserInfo()
            {
                Id = 1,
                Login = "LGilly",
                Password = "$2a$11$b66P/YxRNxaCad4iB6EsUO7VO2Rykq1.jJFwFhow.yS0NxFEuQnci",
                FirstName = "Laurent",
                LastName = "G.",
                Email = "l.g@oplead.com"
            });
            _userInfoRepository.Add(new UserInfo()
            {
                Id = 2,
                Login = "MAndre",
                Password = "$2a$11$wZOx21wLPR4YuBWVg.soruWxMHo6kbH4g0s3FO6ORaF7upuuZ2Ee6",
                FirstName = "Maxime",
                LastName = "Andre",
                Email = "maxime@andre.com"
            });
            _userInfoRepository.Add(new UserInfo()
            {
                Id = 3,
                Login = "ETaume",
                Password = "$2a$11$OlFzyJ425O67VSd5ibDDrOeXhD1WV2A4P5hrqWdzr/xLImD7zMesG",
                FirstName = "Emma",
                LastName = "Taume",
                Email = "emma@taume.com"
            });
            return;
        }


        [TestMethod]
        public async Task Authenticate_ReturnsUserInfo()
        {
            var userInfoService = new UserInfoService(_userInfoRepository);
            //User to check
            var validUser = new UserInfo() { Id = 2, Login = "MAndre", Password = "$2a$11$wZOx21wLPR4YuBWVg.soruWxMHo6kbH4g0s3FO6ORaF7upuuZ2Ee6", FirstName = "Maxime", LastName = "Andre", Email = "maxime@andre.com" };
            var invalidUser = new UserInfo() { Id = 2, Login = "MAndr", Password = "$2a$11$wZOx21wLPR4YuBWVg.soruWxMHo6kbH4g0s3FO6ORaF7upuuZ2Ee6", FirstName = "Maxime", LastName = "Andre", Email = "maxime@andre.com" };

            //Execute method of SUT (UserInfoService)             
            var validUserInfo = await userInfoService.Authenticate(validUser.Login, validUser.Password);
            var invalidUserInfo = await userInfoService.Authenticate(invalidUser.Login, invalidUser.Password);


            //Assert  
            Assert.IsNull(invalidUserInfo);

            Assert.IsNotNull(validUserInfo);
            Assert.IsInstanceOfType(validUserInfo, typeof(UserInfo));
            Assert.AreEqual(validUser.Id, validUserInfo.Id);
            Assert.AreEqual(validUser.Login, validUserInfo.Login);
            Assert.AreEqual(validUser.Password, validUserInfo.Password);
            Assert.AreEqual(validUser.FirstName, validUserInfo.FirstName);
            Assert.AreEqual(validUser.LastName, validUserInfo.LastName);
            Assert.AreEqual(validUser.Email, validUserInfo.Email);
        }


    }
}
