using LibraryApi.Domain.DBContext;
using LibraryApi.Domain.Models;

namespace LibraryApi.Write.Services.WriteRepository;

public class WriteDbRepository: IWriteRepository<Book>
{
	private readonly LibraryDbContext _db;
	
	public WriteDbRepository(LibraryDbContext db)
	{
		_db = db;
	}
	
	public Book Create(Book book)
	{
		var ee = _db.Books.Add(book);
		_db.SaveChanges();

		return ee.Entity;
	}

	public Book Update(Book book)
	{
		var ee = _db.Books.Update(book);
		_db.SaveChanges();

		return ee.Entity;
	}

	public Book Delete(int id)
	{
		var ee = _db.Books.Remove(_db.Books.Find(id));
		_db.SaveChanges();

		return ee.Entity;
	}
}