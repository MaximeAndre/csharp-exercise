using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSharpExercieUnitTest
{
    /// <summary>
    /// Controller Testing Class
    /// </summary>
    [TestClass]
    public class UserInfoControllerUnitTest
    {
        [TestMethod]
        public async Task GetUserInfo_ReturnsStatus200()
        {
            //Arrange data
            var userInfo = new UserInfo()
            {
                Id = 2,
                Login = "MAndre",
                Password = BCrypt.Net.BCrypt.HashPassword("MAndrePwd"),
                FirstName = "Maxime",
                LastName = "Andre",
                Email = "maxime@andre.com"
            };
            var mockRepository = new Mock<IUserInfoRepository>();
            mockRepository.Setup(repo => repo.CheckAuthentication(userInfo.Login, userInfo.Password))
                .ReturnsAsync(userInfo);

            //Setting up the httpcontext to be OK 
            var sut = new UserInfoController(mockRepository.Object);

            sut.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                        new Claim(ClaimTypes.Name, userInfo.Login),
                        new Claim(ClaimTypes.Hash, userInfo.Password)

                    }))
            };
       
            //Act
            IActionResult response  = await sut.GetUserInfo();
            ObjectResult objectResponse = (ObjectResult) response;
            // Assert
            Assert.AreEqual(objectResponse.StatusCode, StatusCodes.Status200OK);
            Assert.AreEqual(objectResponse.Value, JsonSerializer.Serialize(userInfo));
        }

        [TestMethod]
        public async Task GetUserInfo_ReturnsStatus500()
        {
            //Arrange data
            var userInfo = new UserInfo()
            {
                Id = 14,
                Login = "test",
                Password = BCrypt.Net.BCrypt.HashPassword("test"),
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com"
            };
            var mockRepository = new Mock<IUserInfoRepository>();
            mockRepository.Setup(repo => repo.CheckAuthentication(userInfo.Login, userInfo.Password))
               .ReturnsAsync(userInfo);

            //Setting up the httpcontext to be NOK 
            var sut = new UserInfoController(mockRepository.Object);

            sut.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                        new Claim(ClaimTypes.Name, userInfo.Login),
                        new Claim(ClaimTypes.Hash, userInfo.Password)

                    }))
            };

            //Act
            IActionResult response = await sut.GetUserInfo();
            
            // Assert
            Assert.AreEqual((response  as StatusCodeResult).StatusCode, StatusCodes.Status500InternalServerError);
        }

        //TODO: Need to test Unauthorized 401 ??



    }
}