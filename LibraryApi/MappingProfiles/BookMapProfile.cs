using AutoMapper;

using LibraryApi.DBContext;

namespace LibraryApi.MappingProfiles;

public class BookMapProfile:Profile
{
	public BookMapProfile()
	{
		CreateMap<BookDto, Book>().ReverseMap();
	}
}