using System.Diagnostics;

using LibraryApi.DBContext;

namespace LibraryApi.Services;

/// <summary>
/// Service to work with Books Table in Data Base. Uses Result to encapsulate errors
/// </summary>
public interface ILibraryService
{
	public Result<IEnumerable<BookDto>> GetAll();

	public Result<BookDto> GetById(int id);

	public Result<BookDto> DeleteById(int id);

	public Result<BookDto> Update(int id, BookDto book);

	public Result<BookDto> Create(BookDto book);

	public Result<BookDto> GetByISBN(string isbn);
}