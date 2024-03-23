using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using WorkTracker.API.ViewModels;
using WorkTracker.Gateways.Http;

namespace WorkTracker.API.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IHttpHandler _httpHandler;
        private readonly IConfiguration _configuration;

        private readonly IAmazonCognitoIdentityProvider _cognitoService;

        public AuthenticationController(IHttpHandler httpHandler, IConfiguration configuration, IAmazonCognitoIdentityProvider cognitoService)
        {
            _httpHandler = httpHandler;
            _configuration = configuration;
            _cognitoService = cognitoService;
        }

        [HttpPost(Name = "Authenticate user")]
        public async Task<IActionResult> Authenticate(AuthViewModel authViewModel)
        {
            var authParameters = new Dictionary<string, string>
            {
                { "USERNAME", authViewModel.Username },
                { "PASSWORD", authViewModel.Password }
            };

            var authRequest = new InitiateAuthRequest
            {
                ClientId = _configuration["Cognito:ClientId"]!,
                AuthParameters = authParameters,
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
            };
            
            var response = await _cognitoService.InitiateAuthAsync(authRequest);

            return Ok(response.AuthenticationResult.IdToken);
        }

        public class CognitoAuthenticationRequest
        {
            public string AuthFlow { get; set; }
            public string ClientId { get; set; }
            public AuthParameters AuthParameters { get; set; }
        }

        public class AuthParameters
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}