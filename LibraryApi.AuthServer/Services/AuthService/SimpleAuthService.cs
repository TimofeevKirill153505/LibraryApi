using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LibraryApi.AuthServer.Authentication;
using LibraryApi.AuthServer.Services.UsersRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApi.AuthServer.Services.AuthService;

public class SimpleAuthService: IAuthService
{
	private readonly IUsersRepository _users;
	
	public SimpleAuthService(IUsersRepository users)
	{
		_users = users;
	}
	
	public IActionResult TryGetToken(string username, string password)
	{
		var res = _users.GetUser(username, password);

		if (res == null)
		{
			return new UnauthorizedResult();
		}
		
		var claims = new List<Claim>
		{
			new Claim("username", res.Username),
			new Claim(ClaimTypes.Role, res.Role)
		};
		var jwt = new JwtSecurityToken(
			issuer: MyAuthOptions.ISSUER,
			claims: claims,
			expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)), // expires after 5 minutes
			signingCredentials: new SigningCredentials(MyAuthOptions.GetSymmetricSecurityKey(),
				SecurityAlgorithms.HmacSha256));

		return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(jwt));
	}
}