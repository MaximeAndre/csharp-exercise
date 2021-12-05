using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.Infrastructure.Persistance;
using CSharpExercise.src.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using System.IO;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpExercise.src.Application.UserInfos;

namespace CSharpExercieUnitTest
{
    /// <summary>
    /// Controller Testing Class
    /// Implements  InMemoryDb
    /// </summary>
    [TestClass]
    public class UserInfoControllerUnitTest
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
        public async Task GetUserInfo_ReturnsStatus200()
        {
            //Arrange
            var authenticatedUser  = new UserInfo() {Id = 2,Login = "MAndre",Password = "$2a$11$wZOx21wLPR4YuBWVg.soruWxMHo6kbH4g0s3FO6ORaF7upuuZ2Ee6",FirstName = "Maxime",LastName = "Andre",Email = "maxime@andre.com"};
            //Setting up the httpcontext to be OK 
            var sut = new UserInfoController(_userInfoRepository);

            sut.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, authenticatedUser.Login),
                        new Claim(ClaimTypes.Hash, authenticatedUser.Password)

                    }))
            };
       
            //Act
            IActionResult response  = await sut.GetUserInfo();
            ObjectResult objectResponse = (ObjectResult) response;

            // Assert
            Assert.AreEqual(objectResponse.StatusCode, StatusCodes.Status200OK);
            Assert.AreEqual(objectResponse.Value, JsonSerializer.Serialize(authenticatedUser));
        }


        [TestMethod]
        public async Task GetUserInfo_ReturnsStatus401()
        {
            //Arrange
            var authenticatedUser = new UserInfo() { Id = 2, Login = "MAndr", Password = "$2a$11$wZOx21wLPR4YuBWVg.soruWxMHo6kbH4g0s3FO6ORaF7upuuZ2Ee6", FirstName = "Maxime", LastName = "Andre", Email = "maxime@andre.com" };
            //Setting up the httpcontext to be OK 
            var sut = new UserInfoController(_userInfoRepository);

            sut.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, authenticatedUser.Login),
                        new Claim(ClaimTypes.Hash, authenticatedUser.Password)

                    }))
            };

            //Act
            IActionResult response = await sut.GetUserInfo();

            // Assert
            Assert.AreEqual((response as StatusCodeResult).StatusCode, StatusCodes.Status401Unauthorized);
        }

        //TODO: Need to test Unauthorized 401 ??
    }
}