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

	public IEnumerable<Book> GetAllByPredicate(Func<Book, bool> predicate)
	{
		return _db.Books.Where(predicate);
	}

	public Book GetFirst(Func<Book, bool> predicate)
	{
		return _db.Books.First(predicate);
	}
}