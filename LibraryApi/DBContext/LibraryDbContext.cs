using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryApi.DBContext;

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
		modelBuilder.Entity<Book>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasAlternateKey(book => book.ISBN);

			entity.Property(e => e.Id).ValueGeneratedOnAdd();

			entity.Property(e => e.Description).HasMaxLength(200).IsUnicode().HasDefaultValue("").IsUnicode();

			entity.Property(e => e.ISBN).HasMaxLength(20);

			entity.Property(e => e.Genre).HasConversion(new EnumToStringConverter<GenreType>());

			entity.Property(e => e.Name).HasMaxLength(50).IsUnicode();

			entity.Property(e => e.Author).HasMaxLength(50).IsUnicode();
			
			entity.Property(e => e.TimeOfDelivery).HasDefaultValue(null);
			
			entity.Property(e => e.TimeOfReturn).HasDefaultValue(null);
		});
	}
	
	
}