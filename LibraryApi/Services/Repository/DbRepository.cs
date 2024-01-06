using LibraryApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Domain.DBContext;
namespace LibraryApi.Services.Repository;

public class DbRepository: IRepository<Book>, IAsyncDisposable
{
	private readonly LibraryDbContext _db;
	
	public DbRepository(string connectionString)
	{
		DbContextOptionsBuilder <LibraryDbContext> optionsBuilder = new();
		optionsBuilder.UseSqlite(connectionString);
		
		var options = optionsBuilder.Options;
		_db = new LibraryDbContext(options);
	}
	
	public Book Create(Book obj)
	{
		var ee = _db.Books.Add(obj);
		_db.SaveChanges();
		
		return ee.Entity;
	}

	public IEnumerable<Book> GetAll()
	{
		return _db.Books;
	}

	public Book GetById(int id)
	{
		return _db.Books.Find(id);
	}
	
	public Book Update(int id, Book obj)
	{
		var ee = _db.Books.Update(obj);
		_db.SaveChanges();
		
		return ee.Entity;
	}

	public Book Delete(int id)
	{
		var book = _db.Books.Find(id);
		_db.Books.Remove(book);
		_db.SaveChanges();

		return book;
	}

	public void Dispose()
	{
		_db.Dispose();
	}

	public async ValueTask DisposeAsync()
	{
		await _db.DisposeAsync();
	}
}