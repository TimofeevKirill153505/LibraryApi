using LibraryApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Write.Services.BookWriter;

public interface IBookWriter
{
	public IActionResult Create(BookDto bookDto);
	public IActionResult Update(int id, BookDto bookDto);
	public IActionResult Delete(int id);
}