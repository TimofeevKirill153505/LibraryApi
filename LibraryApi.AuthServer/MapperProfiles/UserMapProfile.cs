using AutoMapper;
using LibraryApi.AuthServer.Models;

namespace LibraryApi.AuthServer.MapperProfiles;

public class UserMapProfile: Profile
{
	public UserMapProfile()
	{
		CreateMap<User, UserDto>();
	}
}