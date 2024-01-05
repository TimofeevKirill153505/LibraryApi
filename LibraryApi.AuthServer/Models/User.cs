namespace LibraryApi.AuthServer.Models;

public class UserDto
{
	public string Username { get; set; }
	public string Role { get; set; }
}

public class User
{
	public string Username { get; set; }
	public string Role{ get; set; }
	public string Password { get; set; }
}