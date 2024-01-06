using AutoMapper;
using LibraryApi.Domain.MappingProfiles;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Result;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Read.Services.BookReader;

public class BookReader: IBookReader
{
	private readonly IReadRepository<Book> _books;
	private readonly IMapper _mapper;
	
	public BookReader(IReadRepository<Book> books, IMapper mapper)
	{
		_books = books;
		_mapper = mapper;
	}
	
	public IActionResult GetAll()
	{
		try
		{
			var res = _books.GetAll();
			
			return new OkObjectResult(new Result<IEnumerable<BookDto>>(true, 
				_mapper.Map<IEnumerable<BookDto>>(res)));
		}
		catch (Exception ex)
		{
			var objResp = new ObjectResult(new Result<IEnumerable<BookDto>>(false, null, ex.Message));
			objResp.StatusCode = StatusCodes.Status500InternalServerError;
			return objResp;
		}
	}

	public IActionResult GetById(int id)
	{
		try
		{
			var res = _books.GetById(id);

			return new OkObjectResult(new Result<BookDto>(true,
				_mapper.Map<BookDto>(res)));
		}
		catch (KeyNotFoundException e)
		{
			var objResp = new ObjectResult(new Result<BookDto>(false, null, e.Message));
			objResp.StatusCode = StatusCodes.Status404NotFound;
			return objResp;
		}
		catch (Exception ex)
		{
			var objResp = new ObjectResult(new Result<BookDto>(false, null, ex.Message));
			objResp.StatusCode = StatusCodes.Status500InternalServerError;
			return objResp;
		}
	}

	public IActionResult GetByIsbn(string isbn)
	{
		try
		{
			var res = _books.GetFirst(book => book.ISBN == isbn);

			return new OkObjectResult(new Result<BookDto>(true,
				_mapper.Map<BookDto>(res)));
		}
		catch (KeyNotFoundException ex)
		{
			var objResp = new ObjectResult(new Result<BookDto>(false, null, ex.Message));
			objResp.StatusCode = StatusCodes.Status404NotFound;
			return objResp;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.GetType().Name);
			var objResp = new ObjectResult(new Result<BookDto>(false, null, ex.Message));
			objResp.StatusCode = StatusCodes.Status500InternalServerError;
			return objResp;
		}
	}
}