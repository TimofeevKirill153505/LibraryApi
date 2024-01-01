using LibraryApi.DBContext;
using System.ComponentModel;
using AutoMapper;
using LibraryApi.MappingProfiles;

namespace LibraryApi.Services;


/// <summary>
/// Realisation of ILibraryService interface. In case of error data field will be null
/// </summary>
class LibraryService : ILibraryService
{
	private readonly LibraryDbContext _db;
	private readonly IMapper _mapper;
	
	public LibraryService(LibraryDbContext db, IMapper mapper)
	{
		_db = db;
		_mapper = mapper;
	}
	
	public Result<IEnumerable<BookDto>> GetAll()
	{
		try
		{
			return new (true, _mapper.Map<IEnumerable<BookDto>>(_db.Books));
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
			return new(true, _mapper.Map<BookDto>(_db.Books.Find(id)));
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
			var book = _db.Books.Find(id);
			_db.Books.Remove(book);
			_db.SaveChanges();
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
			// if (book.Id != id) throw new Exception("Id of item doesn't match with id from route");
			var origin = _db.Books.Find(id);
			if (origin == null)
			{
				throw new Exception($"There is no object with id {id}");
			}

			Book book = _mapper.Map<Book>(bookDto);
			origin.CopyDataFrom(book);
			var ee = _db.Books.Update(origin);
			_db.SaveChanges();
			
			return new(true, _mapper.Map<BookDto>(ee.Entity));
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
			Book book = _mapper.Map<Book>(bookDto);
			var ee = _db.Books.Add(book);
			_db.SaveChanges();
			
			return new(true, _mapper.Map<BookDto>(ee.Entity));
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
			return new(true, _mapper.Map<BookDto>(_db.Books.First(b=>b.ISBN == isbn)));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}
}