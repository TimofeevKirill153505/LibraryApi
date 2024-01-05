using LibraryApi.Domain.Models;
using LibraryApi.Write.Services.BookWriter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Write.Controllers;

[ApiController]
[Route("/api/books/")]
[Authorize]
public class BookWriteController : Controller
{
	private readonly IBookWriter _bookWriter;

	public BookWriteController(IBookWriter bookWriter)
	{
		_bookWriter = bookWriter;
	}
	
	// GET
	[HttpPost]
	[Authorize("AdminPolicy")]
	public IActionResult Create(BookDto bookDto)
	{
		return _bookWriter.Create(bookDto);
	}

	[HttpPut("{id:int}")]
	public IActionResult Update(int id, BookDto bookDto)
	{
		Console.WriteLine("At update");
		return _bookWriter.Update(id, bookDto);
	}

	[HttpDelete("{id:int}")]
	[Authorize("AdminPolicy")]
	public IActionResult Delete(int id)
	{
		return _bookWriter.Delete(id);
	}
}