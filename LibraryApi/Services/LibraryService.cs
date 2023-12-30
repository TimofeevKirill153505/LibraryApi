using LibraryApi.DBContext;
using System.ComponentModel;
namespace LibraryApi.Services;


/// <summary>
/// Realisation of ILiabraryService interface. In case of error data field will be null
/// </summary>
class LibraryService : ILibraryService
{
	private readonly LibraryDbContext _db;
	
	public LibraryService(LibraryDbContext db)
	{
		_db = db;
	}
	
	public Result<IEnumerable<Book>> GetAll()
	{
		try
		{
			return new (true, _db.Books);
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<Book> GetById(int id)
	{
		try
		{
			return new(true, _db.Books.Find(id));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<Book> DeleteById(int id)
	{
		try
		{
			var book = _db.Books.Find(id);
			_db.Books.Remove(book);
			_db.SaveChanges();
			return new(true, book);
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<Book> Update(int id, Book book)
	{
		try
		{
			// if (book.Id != id) throw new Exception("Id of item doesn't match with id from route");
			var origin = _db.Books.Find(id);
			if (origin == null)
			{
				throw new Exception($"There is no object with id {id}");
			}
			
			origin.CopyDataFrom(book);
			var ee = _db.Books.Update(origin);
			_db.SaveChanges();
			
			return new(true, ee.Entity);
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<Book> Create(Book book)
	{
		try
		{
			//origin.CopyDataFrom(book);
			var ee = _db.Books.Add(book);
			_db.SaveChanges();
			
			return new(true, ee.Entity);
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}

	public Result<Book> GetByISBN(string isbn)
	{
		try
		{
			//origin.CopyDataFrom(book);
			
			return new(true, _db.Books.First(b=>b.ISBN == isbn));
		}
		catch (Exception e)
		{
			return new(false, null, $"Default generated error message: {e.Message}");
		}
	}
}