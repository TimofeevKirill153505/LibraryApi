using LibraryApi.Domain.Models;
using LibraryApi.Read.Services;
using LibraryApi.Read.Services.BookReader;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Read.Controllers;

[ApiController]
[Route("/api/books/")]
public class BookGetController : Controller
{
	private readonly IBookReader _br;
	
	public BookGetController(IBookReader br)
	{
		_br = br;
	}
	// GET
	[HttpGet]
	public IActionResult GetAll()
	{
		return _br.GetAll();
	}

	[HttpGet("{id:int}")]
	public IActionResult GetById(int id)
	{
		return _br.GetById(id);
	}

	[HttpGet("isbn")]
	public IActionResult GetByIsbn(string isbn)
	{
		return _br.GetByIsbn(isbn);
	}
}