using System.Diagnostics;

using LibraryApi.DBContext;

namespace LibraryApi.Services;

/// <summary>
/// Service to work with Books Table in Data Base. Uses Result to encapsulate errors
/// </summary>
public interface ILibraryService
{
	public Result<IEnumerable<Book>> GetAll();

	public Result<Book> GetById(int id);

	public Result<Book> DeleteById(int id);

	public Result<Book> Update(int id, Book book);

	public Result<Book> Create(Book book);

	public Result<Book> GetByISBN(string isbn);
}