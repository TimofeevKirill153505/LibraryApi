using LibraryApi.Domain.Models;
using LibraryApi.Write.Services.BookWriter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Write.Controllers;

[ApiController]
[Route("/api/books/")]
public class BookWriteController : Controller
{
	private readonly IBookWriter _bookWriter;

	public BookWriteController(IBookWriter bookWriter)
	{
		_bookWriter = bookWriter;
	}
	
	// GET
	[HttpPost]
	public IActionResult Create(BookDto bookDto)
	{
		return _bookWriter.Create(bookDto);
	}

	[HttpPut("{id:int}")]
	public IActionResult Update(int id, BookDto bookDto)
	{
		return _bookWriter.Update(id, bookDto);
	}

	[HttpDelete("{id:int}")]
	public IActionResult Delete(int id)
	{
		return _bookWriter.Delete(id);
	}
}