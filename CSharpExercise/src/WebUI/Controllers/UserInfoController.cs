using CSharpExercise.src.Application.Common.Interface;
using CSharpExercise.src.Application.UserInfos;
using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CSharpExercise.src.WebUI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("user")]
    public class UserInfoController : Controller
    {

        private readonly IUserInfoRepository _repository;

        public UserInfoController(IUserInfoRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public IActionResult GetUserInfo()
        {
            // Calling the repository to get the data
            var model = _repository.CheckAuthentication("mxwind", "mxwindpwd");

            //Checking if nointernal error
            if(model==null) return StatusCode(500);
            //Checking if authorized
            else if (model.Id < 0) return StatusCode(401);
            //else dsiplaying infos
            return StatusCode(200, JsonSerializer.Serialize(model));
        }
    }
}
