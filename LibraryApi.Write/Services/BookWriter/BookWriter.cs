using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using LibraryApi.Domain.Result;
using LibraryApi.Domain.Models;
using LibraryApi.Write.Services.WriteRepository;


namespace LibraryApi.Write.Services.BookWriter;

public class BookWriter: IBookWriter
{
	private readonly IWriteRepository<Book> _books;
	private readonly IMapper _mapper;
	
	public BookWriter(IWriteRepository<Book> books, IMapper mapper)
	{
		_books = books;
		_mapper = mapper;
	}
	
	public IActionResult Create(BookDto bookDto)
	{
		try
		{
			var book = _mapper.Map<Book>(bookDto);

			var bookCreated = _books.Create(book);
			
			var resultObject = new ObjectResult(new Result<BookDto>(true, _mapper.Map<BookDto>(bookCreated)));
			resultObject.StatusCode = 201;
			//return new OkObjectResult(new Result<BookDto>(true, _mapper.Map<BookDto>(bookCreated)));
			return resultObject;
		}
		catch (ArgumentException e)
		{
			var resultObject = new ObjectResult(new Result<BookDto>(false, null, e.Message));
			resultObject.StatusCode = 400;

			return resultObject;
		}
		catch (Exception e)
		{
			var resultObject = new ObjectResult(new Result<BookDto>(false, null, e.Message));
			resultObject.StatusCode = 500;

			return resultObject;
		}
	}

	public IActionResult Update(int id, BookDto bookDto)
	{
		try
		{
			bookDto.Id = id;
			var book = _mapper.Map<Book>(bookDto);

			var bookUpdated = _books.Update(book);
			
			var resultObject = new ObjectResult(new Result<BookDto>(true, _mapper.Map<BookDto>(bookUpdated)));
			resultObject.StatusCode = 201;
			
			return resultObject;
		}
		catch (KeyNotFoundException e)
		{
			var resultObject = new ObjectResult(new Result<BookDto>(false, null, e.Message));
			resultObject.StatusCode = 404;

			return resultObject;
		}
		catch (ArgumentException e)
		{
			var resultObject = new ObjectResult(new Result<BookDto>(false, null, e.Message));
			resultObject.StatusCode = 400;

			return resultObject;
		}
		catch (Exception e)
		{
			var resultObject = new ObjectResult(new Result<BookDto>(false, null, e.Message));
			resultObject.StatusCode = 500;

			return resultObject;
		}
	}

	public IActionResult Delete(int id)
	{
		try
		{
			var bookDeleted = _books.Delete(id);

			return new OkObjectResult(new Result<BookDto>(true, _mapper.Map<BookDto>(bookDeleted)));
		}
		catch (KeyNotFoundException e)
		{
			var resultObject = new ObjectResult(new Result<BookDto>(false, null, e.Message));
			resultObject.StatusCode = 404;

			return resultObject;
		}
		catch (Exception e)
		{
			var resultObject = new ObjectResult(new Result<BookDto>(false, null, e.Message));
			resultObject.StatusCode = 500;

			return resultObject;
		}
	}
}