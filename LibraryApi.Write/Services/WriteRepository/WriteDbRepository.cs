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
		if (_db.Books.Any(b => b.ISBN == book.ISBN))
			throw new ArgumentException($"ISBN {book.ISBN} is already occupied");
		
		var ee = _db.Books.Add(book);
		_db.SaveChanges();

		return ee.Entity;
	}

	public Book Update(Book book)
	{
		if (!_db.Books.Any(b=>b.Id == book.Id))
			throw new KeyNotFoundException($"There is no book with id {book.Id}");
		if (_db.Books.Any(b => b.ISBN == book.ISBN && b.Id != book.Id))
			throw new ArgumentException($"ISBN {book.ISBN} is already occupied");
		
		Console.WriteLine($"In updatte. Id {book.Id}, isbn {book.ISBN}");
		var ee = _db.Books.Update(book);
		var res = ee.Entity;
		Console.WriteLine("After update");
		_db.SaveChanges();

		return res;
	}

	public Book Delete(int id)
	{
		if (_db.Books.Find(id) == null)
			throw new KeyNotFoundException($"There is no book with id {id}");
		
		var ee = _db.Books.Remove(_db.Books.Find(id));
		_db.SaveChanges();

		return ee.Entity;
	}
	
}