using System.Diagnostics;

using LibraryApi.Domain.Models;
using LibraryApi.Domain.Result;
using LibraryApi.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Services;

/// <summary>
/// Service to work with Books Table in Data Base. Uses Result to encapsulate errors
/// </summary>
public interface ILibraryService
{
	public /* Result<IEnumerable<BookDto>> */ IActionResult GetAll();

	public /* Result<BookDto>*/  IActionResult GetById(int id);

	public /* Result<BookDto> */ IActionResult DeleteById(int id);

	public /* Result<BookDto> */ IActionResult Update(int id, BookDto book);

	public /* Result<BookDto>  */ IActionResult Create(BookDto book);

	public /* Result<BookDto> */ IActionResult GetByISBN(string isbn);
}