using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using LibraryApi.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApi.Controllers;

/// <summary>
/// Simple authentication controller	
/// </summary>
[ApiController]
[Route("/api/token")]
public class TokenController : Controller
{
	public TokenController()
	{
		
	}
	/// <summary>
	/// Simple authorization action. Use {"username":"username", "password":"password"} to get valid token
	/// </summary>
	/// <param name="username">username </param>
	/// <param name="password">password of user</param>
	/// <returns>Jwt, that is active for two minutes</returns>
	/// <response code="200"> Valid jwt </response>
	/// <response code="401"> username and/or password is/are invalid </response>
	[HttpGet]
	public IActionResult GetToken(string username, string password)
	{
		if (username != "username" || password != "password") return Unauthorized();
		var claims = new List<Claim>
		{
			new Claim("username", "username"),
			new Claim("isStuff", "true")
		};
		var jwt = new JwtSecurityToken(
			issuer: MyAuthOptions.ISSUER,
			audience: MyAuthOptions.AUDIENCE,
			claims: claims,
			expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
			signingCredentials: new SigningCredentials(MyAuthOptions.GetSymmetricSecurityKey(),
				SecurityAlgorithms.HmacSha256));

		return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
	}
}