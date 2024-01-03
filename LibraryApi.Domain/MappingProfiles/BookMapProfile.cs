using AutoMapper;
using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.MappingProfiles;

public class BookMapProfile:Profile
{
	public BookMapProfile()
	{
		CreateMap<BookDto, Book>().ReverseMap();
	}
}