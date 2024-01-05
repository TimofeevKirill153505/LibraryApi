using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Services.AuthService;

public interface IAuthService
{
	public IActionResult GetToken(string username, string password);
}