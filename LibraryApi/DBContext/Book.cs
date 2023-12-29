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
	/// Genre of book.
	/// </summary>
	public  GenreType Genre { get; set; }
	public string Description { get; set; }
	public string Author { get; set; }
	public DateTime? TimeOfDelivery { get; set; }
	public DateTime? TimeOfReturn { get; set; }
}