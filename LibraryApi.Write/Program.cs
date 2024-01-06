using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using LibraryApi.Domain.DBContext;
using LibraryApi.Domain.MappingProfiles;
using LibraryApi.Domain.Models;
using LibraryApi.Write.Services.BookWriter;
using LibraryApi.Write.Services.WriteRepository;
using LibraryApi.Domain.Auth;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(BookMapProfile));
builder.Services.AddScoped<IWriteRepository<Book>, WriteDbRepository>();
builder.Services.AddScoped<IBookWriter, BookWriter>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	var tvp = new TokenValidationParameters();
	tvp.ValidateAudience = false;
	tvp.ValidateLifetime = true;
	tvp.ValidateIssuer = true;
	tvp.ValidIssuer = TokenValidation.ValidIssuer;
	tvp.SignatureValidator = TokenValidation.GetSignatureValidator(builder.Configuration["ValidateTokenUrl"]);
	
	options.TokenValidationParameters = tvp;
	options.Validate();
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy =>
	{
		policy.RequireRole("admin");
	});
});


builder.Services.AddDbContext<LibraryDbContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Library"));
	options.EnableSensitiveDataLogging();
});


var app = builder.Build();

app.UseMiddleware<MyMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	// app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public class MyMiddleware
{
	private readonly RequestDelegate _next;
 
	public MyMiddleware(RequestDelegate next)
	{
		this._next = next;
	}
 
	public async Task InvokeAsync(HttpContext context)
	{
		// StreamReader sr = new StreamReader(context.Request.Body);
		// Console.WriteLine($"Body of request: {await sr.ReadToEndAsync()}");
		//
		// context.Request.Body.Position = 0;
		Console.WriteLine();
		await _next.Invoke(context);
	}
}