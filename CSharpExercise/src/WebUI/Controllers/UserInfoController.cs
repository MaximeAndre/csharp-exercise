using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
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
        // Service + Logger DI
        private readonly IUserInfoService _userInfoService;
        private readonly ILogger<UserInfoController> _logger;

         public UserInfoController(IUserInfoService userInfoService, ILogger<UserInfoController> logger)
         {
            _userInfoService = userInfoService;
            _logger = logger;
         }

        /// <summary>
        /// Get the current user info
        /// </summary>
        /// <returns>the current user info if authentication is ok else error</returns>
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                //Infos from httpcontext about the current authenticated user
                var name = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var pwd = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Hash).Single().Value;

                //Checking Db info
                var user = await _userInfoService.Authenticate(name, pwd);
                
                if (user == null)
                {
                    _logger.LogInformation("request for user : {name} => Unauthorized at {DateTime.Now}", name, DateTime.Now);
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                _logger.LogInformation("request for user : {name} => Success at {DateTime.Now}", name, DateTime.Now);
                //else dsiplaying infos
                return StatusCode(StatusCodes.Status200OK, JsonSerializer.Serialize(user));

            }
            catch (Exception ex)
            {
                _logger.LogError("The path {ex.Source} threw an exception {ex.Message}",ex.Source,ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }
    }
}
