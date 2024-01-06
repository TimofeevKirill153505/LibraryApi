using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using AutoMapper;

using LibraryApi.Domain.MappingProfiles;
using LibraryApi.Domain.Models;
using LibraryApi.Services.Repository;
using LibraryApi.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Services;


/// <summary>
/// Realisation of ILibraryService interface. In case of error data field will be null
/// </summary>
class LibraryService : ILibraryService
{
	private readonly IRepository<BookDto> _books;
	private readonly IMapper _mapper;
	
	public LibraryService(IRepository<BookDto> books, IMapper mapper)
	{
		_books = books;
		_mapper = mapper;
	}
	
	public /*Result<IEnumerable<BookDto>>*/ IActionResult GetAll()
	{
		try
		{
			return new OkObjectResult(new Result<IEnumerable<BookDto>>
				(true, _mapper.Map<IEnumerable<BookDto>>(_books.GetAll())));
		}
		catch (Exception e)
		{
			var res = new ObjectResult(new Result<IEnumerable<BookDto>>
				(false, null, $"Default generated error message: {e.Message}"));
			
			res.StatusCode = StatusCodes.Status500InternalServerError;
			
			return res;
		}
	}

	public /*Result<BookDto>*/ IActionResult GetById(int id)
	{
		try
		{
			return new OkObjectResult(new Result<BookDto>(true,
				_mapper.Map<BookDto>(_books.GetAll().First(book => book.Id == id))));
		}
		catch (InvalidOperationException e)
		{

			var resp = new ObjectResult(new Result<BookDto>
				(false, null, $"Key not found message: {e.Message}"));

			resp.StatusCode = StatusCodes.Status404NotFound;

			return resp;
		}
		catch (Exception e)
		{
			var resp = new ObjectResult(new Result<BookDto>
				(false, null, $"Default generated error message: {e.Message}"));

			resp.StatusCode = StatusCodes.Status500InternalServerError;
			return resp;
		}
	}

	public /* Result<BookDto> */ IActionResult DeleteById(int id)
	{
		try
		{
			var book = _books.Delete(id);
			
			return new OkObjectResult( new Result<BookDto>(true, _mapper.Map<BookDto>(book)));
		}
		catch (KeyNotFoundException e)
		{

			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Key not found message: {e.Message}"));

			res.StatusCode = StatusCodes.Status404NotFound;

			return res;
		}
		catch (Exception e)
		{
			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Default generated error message: {e.Message}"));

			res.StatusCode = StatusCodes.Status500InternalServerError;
			return res;
		}
	}

	public /*Result<BookDto>*/ IActionResult Update(int id, BookDto bookDto)
	{
		try
		{
			var res = new ObjectResult(new Result<BookDto>
				(true, _mapper.Map<BookDto>(_books.Update(id, bookDto))));

			res.StatusCode = StatusCodes.Status201Created;

			return res;
		}
		catch (KeyNotFoundException e)
		{

			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Key not found message: {e.Message}"));

			res.StatusCode = StatusCodes.Status404NotFound;

			return res;
		}
		catch (ArgumentException e)
		{
			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Key not found message: {e.Message}"));

			res.StatusCode = StatusCodes.Status400BadRequest;

			return res;
		}
		catch (Exception e)
		{
			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Default generated error message: {e.Message}"));

			res.StatusCode = StatusCodes.Status500InternalServerError;
			return res;
		}
	}

	public /*Result<BookDto>*/ IActionResult Create(BookDto bookDto)
	{
		try
		{
			var res = new ObjectResult(new Result<BookDto>
				(true, _mapper.Map<BookDto>(_books.Create(bookDto))));

			res.StatusCode = StatusCodes.Status201Created;

			return res;
		}
		catch (ArgumentException e)
		{
			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Key not found message: {e.Message}"));

			res.StatusCode = StatusCodes.Status400BadRequest;

			return res;
		}
		catch (Exception e)
		{
			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Default generated error message: {e.Message}"));

			res.StatusCode = StatusCodes.Status500InternalServerError;
			return res;
		}

	}

	public /*Result<BookDto>*/ IActionResult GetByISBN(string isbn)
	{
		try
		{
			return new OkObjectResult(new Result<BookDto>(true, 
				_mapper.Map<BookDto>(_books.GetAll().First(book => book.ISBN == isbn))));
		}
		catch (KeyNotFoundException e)
		{

			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Default generated error message: {e.Message}"));

			res.StatusCode = StatusCodes.Status404NotFound;

			return res;
		}
		catch (Exception e)
		{
			var res = new ObjectResult(new Result<BookDto>
				(false, null, $"Default generated error message: {e.Message}"));

			res.StatusCode = StatusCodes.Status500InternalServerError;
			return res;
		}
		
	}
}