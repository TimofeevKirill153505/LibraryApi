using Microsoft.AspNetCore.Mvc;
using LibraryApi.Domain.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Authorization;

using LibraryApi.Authentication;

namespace LibraryApi.Controllers;

/// <summary>
/// Main controller of app. Actions return json object. In case of success filed success of returned json object will be
/// true and value of field data will depend on action. In case of error, field success will be false, data will be null
/// and ErrorMessage field will contain text information about error. Only Admin can create and delete books.
/// Authenticated user can update books. Not authenticated user can only get all books or get books by id.
/// Field Genre of the books is enum that can be converted to string, so in json it can be both string and number,
/// but this value must be numeric value of enum member or its name.
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]/")]
public class BooksController: Controller
{
	private readonly ILibraryService _ls;
	
	public BooksController(ILibraryService ls)
	{
		_ls = ls;
	}

	/// <summary>
	///	Gets all items in db
	/// </summary>
	/// 
	/// <returns> All book objects in db </returns>
	///
	///<remarks>
	///Request example:
	/// GET /api/books/
	/// </remarks>
	/// 
	/// <response code="200">Success. All books in data field</response>;
	///
	/// <response code="500">
	/// Some error occured while processing request.
	/// Success field is false. In error Message text description of an error
	/// </response>
	[HttpGet]
	[AllowAnonymous]
	public IActionResult GetAll()
	{
		return _ls.GetAll();
	}
	
	/// <summary>
	/// Returns item with given id
	/// </summary>
	/// <param name="id">id of element to be found. A route parameter</param>
	/// <returns>
	/// Object with the given id
	/// </returns>
	///
	///<remarks>
	///Request example:
	/// GET /api/books/2
	///</remarks>
	/// 
	/// <response code="200">Success. Book is in data field</response>;
	/// <response code="404">There is no book with given id</response>
	/// <response code="500">
	/// Some error occured while processing request.
	/// Success field is false. In error Message text description of an error
	/// </response>
	[HttpGet("{id:int}")]
	[AllowAnonymous]
	public IActionResult GetById(int id)
	{
		return _ls.GetById(id);
	}

	/// <summary>
	/// Returns item with given isbn
	/// </summary>
	/// <param name="isbn">Isbn number of a book. Locates in a body of request</param>
	/// <returns>
	/// Object with the given isbn
	/// </returns>
	///
	/// <response code="200">Success. Book is in data field</response>
	/// <response code="404">There is no book with given isbn</response>
	/// <response code="500">
	/// Some error occured while processing request.
	/// Success field is false. In error Message text description of an error
	/// </response>
	[HttpGet("isbn/")]
	[AllowAnonymous]
	public IActionResult GetByIsbn(string isbn)
	{
		return _ls.GetByISBN(isbn);
	}
	
	/// <summary>
	/// Update item with give id
	/// </summary>
	/// <param name="id">Id of item, that need to be updated</param>
	/// <param name="book">New book object, that will replace the old one. Field id isn't required.
	/// Locates in body of request
	/// </param>
	///
	///<remarks>
	/// Request example:
	/// PUT /api/books/2
	/// {
	///		"isbn": "978-3-16-148410-0",
	///		"name": "Book Name",
	///		"author": "Book Author",
	///		"genre": "Police",
	///		"description": "Amazing book. The cool one",
	///		"timeOfDelivery": null,
	///		"timeOfReturn": null
	/// }
	/// </remarks>
	/// <returns>Book, that was updated</returns>
	/// <response code="201">Success. Updated book in data field</response>;
	/// <response code="401">
	/// Attempt of unauthenticated access. You need to send a valid jwt in header with your request.
	/// </response>
	/// <response code="400">Bad request. Check the body of request</response>
	/// <response code="404">There is no book with given id</response>
	/// <response code="500">
	/// Some error occured while processing request.
	/// Success field is false. In error Message text description of an error
	/// </response>
	///
	[HttpPut("{id:int}")]
	public IActionResult Update(int id, BookDto book)
	{
		return _ls.Update(id, book);
	}

	/// <summary>
	/// Deletes a book with given id
	/// </summary>
	/// <param name="id">Id of book to delete</param>
	/// <returns>Book, that was deleted</returns>
	/// <remarks>
	/// Request example:
	/// DELETE /api/books/2
	/// </remarks>
	/// <response code="200">Success. Deleted book in data field</response>;
	/// <response code="401">
	/// Attempt of unauthenticated access. You need to send a valid jwt in header with your request.
	/// </response>
	/// <response code="403"> Attempt of unauthorized access. You need to send a valid jwt of admin </response>
	/// <response code="404"> There is no book with given id </response>
	/// <response code="500">
	/// Some error occured while processing request.
	/// Success field is false. In error Message text description of an error
	/// </response>
	[HttpDelete("{id:int}")]
	[Authorize("AdminPolicy")]
	public IActionResult Delete(int id)
	{
		return _ls.DeleteById(id);
		}

	/// <summary>
	/// Creates a new book
	/// </summary>
	/// <param name="book">New book object, that will replace the old one. Field id isn't required.
	/// Locates in body of request
	/// </param>
	/// <returns>Book, that was created</returns>
	/// <response code="200">Success. Created book in data field</response>;
	/// <example>
	/// Request example:
	/// POST /api/books/
	/// {
	///		"isbn": "978-3-16-148410-0",
	///		"name": "Book Name",
	///		"author": "Book Author",
	///		"genre": "Police",
	///		"description": "Amazing book. The cool one",
	///		"timeOfDelivery": null,
	///		"timeOfReturn": null
	/// }
	/// </example>
	/// <response code="201">Success. Created book in data field</response>
	/// <response code="400"> Bad request. Check the body of request.</response>
	/// <response code="401">
	/// Attempt of unauthenticated access. You need to send a valid jwt in header with your request.
	/// </response>
	/// <response code="403">
	/// Attempt of unauthorized access. You need to send a valid jwt of admin
	/// </response>
	/// 
	/// <response code="500">
	/// Some error occured while processing request.
	/// Success field is false. In error Message text description of an error
	/// </response>
	[HttpPost]
	[Authorize("AdminPolicy")]
	public IActionResult Create(BookDto book)
	{
		return _ls.Create(book);
	}
}