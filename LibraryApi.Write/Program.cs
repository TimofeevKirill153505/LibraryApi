using LibraryApi.Domain.DBContext;
using LibraryApi.Domain.MappingProfiles;
using LibraryApi.Domain.Models;
using LibraryApi.Write.Services.BookWriter;
using LibraryApi.Write.Services.WriteRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(BookMapProfile));
builder.Services.AddScoped<IWriteRepository<Book>, WriteDbRepository>();
builder.Services.AddScoped<IBookWriter, BookWriter>();

builder.Services.AddDbContext<LibraryDbContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Library"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// app.UseExceptionHandler("/Error");
	// // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	// app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();