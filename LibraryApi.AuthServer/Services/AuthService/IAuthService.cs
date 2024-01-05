using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.AuthServer.Services.AuthService;

public interface IAuthService
{
	public IActionResult TryGetToken(string username, string password);
}