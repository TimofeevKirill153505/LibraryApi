using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using LibraryApi.Domain.Models;

namespace LibraryApi.Domain.DBContext;

public class LibraryDbContext: DbContext
{
	public LibraryDbContext(DbContextOptions<LibraryDbContext> options): base(options)
	{
		
	}
	/// <summary>
	/// Books Collection
	/// </summary>
	public DbSet<Book> Books { get; set; } = null!;
	
	/// <summary>
	/// Configuring properties of Book model.
	/// </summary>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
	}
	
	
}