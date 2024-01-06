using LibraryApi.Domain.DBContext;
using LibraryApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Read.Services;

public class ReadDbRepository: IReadRepository<LibraryApi.Domain.Models.Book>
{
	private readonly LibraryDbContext _db;
	public ReadDbRepository(LibraryDbContext db)
	{
		// DbContextOptionsBuilder <LibraryDbContext> optionsBuilder = new();
		// optionsBuilder.UseSqlite(connectionString);
		//
		// var options = optionsBuilder.Options;
		_db = db;
	}
	
	public IEnumerable<Book> GetAll()
	{
		return _db.Books;
	}

	public Book GetById(int id)
	{
		var res = _db.Books.Find(id);
		if (res == null)
			throw new KeyNotFoundException($"There is no book with id {id}");

		return res;
	}
	
	public IEnumerable<Book> GetAllByPredicate(Func<Book, bool> predicate)
	{
		return _db.Books.Where(predicate);
	}

	public Book GetFirst(Func<Book, bool> predicate)
	{
		try
		{
			return _db.Books.First(predicate);
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine("InvalidOperationExcepton");
			throw new KeyNotFoundException(ex.Message);
		}
	}
	
}