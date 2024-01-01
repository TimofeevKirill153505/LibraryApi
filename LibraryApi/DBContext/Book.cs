using System.Text.Json.Serialization;

namespace LibraryApi.DBContext;

public enum GenreType
{
	Adventure,
	Science,
	Police,
	Horror,
	Collection,
	Other
}



public class Book
{
	/// <summary>
	/// Id of book. Increments automatically
	/// </summary>
	public int Id { get; private set; }
	
	/// <summary>
	/// ISBN of book. Unique as id, but it's not a primary key
	/// </summary>
	public string ISBN { get; set; }
	
	/// <summary>
	/// Name of a book. Max length is 50 characters 
	/// </summary>
	public string Name { get; set; }
	
	/// <summary>
	/// Genre of book. Is enum, but converts itself to string in db and json serialization
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public  GenreType Genre { get; set; }
	
	/// <summary>
	/// Description of book. Empty string by default
	/// </summary>
	public string Description { get; set; }
	
	/// <summary>
	/// Author of book. Max length is 50 characters
	/// </summary>
	public string Author { get; set; }
	
	/// <summary>
	/// DateTime, when employee gave out the book 
	/// </summary>
	public DateTime? TimeOfDelivery { get; set; }
	
	/// <summary>
	/// DateTime, when book must be returned
	/// </summary>
	public DateTime? TimeOfReturn { get; set; }
	
	public Book(){}
	
	/// <summary>
	/// Copies all data from param to this object, except Id 
	/// </summary>
	/// <param name="book">Object, from which data will be copied</param>
	public void CopyDataFrom(Book book)
	{
		ISBN = book.ISBN;
		Name = book.Name;
		Genre = book.Genre;
		Description = book.Description;
		Author = book.Author;
		TimeOfDelivery = book.TimeOfDelivery;
		TimeOfReturn = book.TimeOfReturn;
	}
}


/// <summary>
/// DTO class for book model.
/// </summary>
public class BookDto
{
	
	public int Id { get; set; }
	
	/// <summary>
	/// ISBN of book. Unique as id, but it's not a primary key
	/// </summary>
	public string ISBN { get; set; }
	
	/// <summary>
	/// Name of a book. Max length is 50 characters 
	/// </summary>
	public string Name { get; set; }
	
	/// <summary>
	/// Genre of book. Is enum, but converts itself to string in db and json serialization
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public  GenreType Genre { get; set; }
	
	/// <summary>
	/// Description of book. Empty string by default
	/// </summary>
	public string Description { get; set; }
	
	/// <summary>
	/// Author of book. Max length is 50 characters
	/// </summary>
	public string Author { get; set; }
	
	/// <summary>
	/// DateTime, when employee gave out the book 
	/// </summary>
	public DateTime? TimeOfDelivery { get; set; }
	
	/// <summary>
	/// DateTime, when book must be returned
	/// </summary>
	public DateTime? TimeOfReturn { get; set; }
}