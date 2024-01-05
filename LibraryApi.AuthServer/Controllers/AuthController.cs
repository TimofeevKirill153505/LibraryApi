using LibraryApi.AuthServer.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.AuthServer.Controllers;

[ApiController]
[Route("/api/auth/")]
public class AuthController : Controller
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}
	
	// GET
	[HttpGet("token")]
	public IActionResult GetToken(string username, string password)
	{
		return _authService.TryGetToken(username, password);
	}

	[HttpGet("check")]
	[Authorize]
	public IActionResult Check()
	{
		Console.WriteLine("Checked token");
		return Ok();
	}
}