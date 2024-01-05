using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using LibraryApi.Authentication;
using LibraryApi.Services.AuthService;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApi.Controllers;

/// <summary>
/// Simple authentication controller	
/// </summary>
[ApiController]
[Route("/api/token")]
[AllowAnonymous]
public class TokenController : Controller
{
	private IAuthService _authService;
	
	public TokenController(IAuthService authService)
	{
		_authService = authService;
	}

	/// <summary>
	/// Simple authorization action. Use "username":"username", "password":"password" to get valid token of user and
	/// "username":"admin", "password":"admin" to get valid token of admin. All tokens are valid for 5 minutes
	/// </summary>
	/// <param name="username">username </param>
	/// <param name="password">password of user</param>
	/// <returns>Jwt, that is active for two minutes</returns>
	/// <response code="200"> Valid jwt </response>
	/// <response code="401"> username and/or password is/are invalid </response>
	[HttpGet]
	public IActionResult GetToken(string username, string password)
	{
		return _authService.GetToken(username, password);
	}
}