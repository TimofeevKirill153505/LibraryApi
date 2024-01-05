using LibraryApi.AuthServer.Models;

namespace LibraryApi.AuthServer.Services.UsersRepository;

public interface IUsersRepository
{
	public UserDto? GetUser(string username, string password);
}