using Microsoft.EntityFrameworkCore;
using LibraryApi.Domain.DBContext;
using LibraryApi.Domain.MappingProfiles;
using LibraryApi.Domain.Models;
using LibraryApi.Read.Services;
using LibraryApi.Read.Services.BookReader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Library"));
});

builder.Services.AddScoped<IReadRepository<Book>, ReadDbRepository>();
builder.Services.AddScoped<IBookReader, BookReader>();
builder.Services.AddAutoMapper(typeof(BookMapProfile));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();