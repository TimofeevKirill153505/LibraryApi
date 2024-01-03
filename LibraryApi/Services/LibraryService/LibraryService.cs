using System.ComponentModel;
using AutoMapper;
using LibraryApi.Domain.MappingProfiles;

using LibraryApi.Domain.Models;
using LibraryApi.Services.Repository;
using LibraryApi.Domain.Result;

namespace LibraryApi.Services;


/// <summary>
/// Realisation of ILibraryService interface. In case of error data field will be null
/// </summary>
class LibraryService : ILibraryService
{
	private readonly IRepository<BookDto> _books;
	private readonly IMapper _mapper;
	
	public LibraryService(IRepository<BookDto> books, IMapper mapper)
	{
		_books = books;
		_mapper = mapper;
	}
	
	public Result<IEnumerable<BookDto>> GetAll()
	{
		try
		{
			return new (true, _mapper.Map<IEnumerable<BookDto>>(_books.GetAll()));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<BookDto> GetById(int id)
	{
		try
		{
			return new(true, 
				_mapper.Map<BookDto>(_books.GetAll().First(book => book.Id == id)));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<BookDto> DeleteById(int id)
	{
		try
		{
			var book = _books.Delete(id);
			
			return new(true, _mapper.Map<BookDto>(book));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<BookDto> Update(int id, BookDto bookDto)
	{
		try
		{
			return new(true, 
				_mapper.Map<BookDto>(_books.Update(id, bookDto)));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<BookDto> Create(BookDto bookDto)
	{
		try
		{
			return new(true, 
				_mapper.Map<BookDto>(_books.Create(bookDto)));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}

	}

	public Result<BookDto> GetByISBN(string isbn)
	{
		try
		{
			return new(true, 
				_mapper.Map<BookDto>(_books.GetAll().First(book => book.ISBN == isbn)));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
		
	}
}