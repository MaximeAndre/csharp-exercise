using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Application.UserInfos;
using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.Infrastructure;
using CSharpExercise.src.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace CSharpExercise.src.WebUI.Controllers
{
    //authorize = only accessible with valid authorization header
    /// <summary>
    /// Controller for user information
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("user")]
    public class UserInfoController : Controller
    {
        // Repository to acces DB from the class
         private readonly IUserInfoRepository _repository;

        // injection of the repository
         public UserInfoController(IUserInfoRepository repository)
         {
             _repository = repository;
         }

        /// <summary>
        /// Get the current user info
        /// </summary>
        /// <returns>the current user info if authentication is ok else error</returns>
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {

            //Infos from httpcontext about the current authenticated user
            var name = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).SingleOrDefault()?.Value;
            var pwd = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Hash).SingleOrDefault()?.Value;

            //Checking Db info
            var user = await _repository.CheckAuthentication(name,pwd);

            //Checking if nointernal error
            if(user==null) return StatusCode(500);
            //Checking if authorized
            else if (user.Id < 0) return StatusCode(401);
            //else dsiplaying infos
            return StatusCode(200, JsonSerializer.Serialize(user));
        }
    }
}
