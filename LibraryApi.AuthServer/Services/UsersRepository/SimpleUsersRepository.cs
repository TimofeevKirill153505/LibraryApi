using System.Runtime.CompilerServices;
using AutoMapper;
using LibraryApi.AuthServer.Models;

namespace LibraryApi.AuthServer.Services.UsersRepository;

public class SimpleUsersRepository: IUsersRepository
{
	private readonly List<User> _users = new List<User>()
	{
		new User { Role = "admin", Username = "admin", Password = "admin" },
		new User { Role = "user", Username = "username", Password = "password" }
	};

	private readonly IMapper _mapper;
	
	public SimpleUsersRepository(IMapper mapper)
	{
		_mapper = mapper;
	}
	
	
	public UserDto? GetUser(string username, string password)
	{
		User? user = _users.FirstOrDefault(usr => usr.Username == username && usr.Password == password);
		if (user == null) return null;

		return _mapper.Map<UserDto>(user);
	}
}