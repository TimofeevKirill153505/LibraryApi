using LibraryApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Read.Services.BookReader;

public interface IBookReader
{
	public IActionResult GetAll();

	public IActionResult GetById(int id);

	public IActionResult GetByIsbn(string isbn);
}