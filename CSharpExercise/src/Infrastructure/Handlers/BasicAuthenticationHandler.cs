using CSharpExercise.src.Domain.Entities;
using CSharpExercise.src.WebUI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace CSharpExercise.src.Infrastructure.Handlers
{

    /// <summary>
    /// Implement the basic authentication
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        // Acces a la DB via le service
        private readonly IUserInfoService _userInfoService;

        //Injection du Service dans le constructeur
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserInfoService userInfoService) : base(options, logger, encoder, clock)
        {
            _userInfoService = userInfoService;
        }

        /// <summary>
        /// Handling the Authentication process
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            //if Authorization Header is not present in the HttpRequest
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            //Initialize to null
            UserInfo userInfo = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                userInfo = await _userInfoService.Authenticate(username, password);
            }
            catch
            {
                //If error on the header reading return invalid hearder.
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
            //If no user info could be retrieve from header return invalid infos
            if (userInfo == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            //Else prepare the container for the auth data
            //Adding the Hash of the password
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                new Claim(ClaimTypes.Name, userInfo.Login),
                new Claim(ClaimTypes.Hash, userInfo.Password)
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            //return the container
            return AuthenticateResult.Success(ticket);

        }
    }
}
