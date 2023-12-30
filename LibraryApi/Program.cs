using System.Reflection;
using System.Security.Claims;
using LibraryApi.Authentication;
using LibraryApi.DBContext;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		// указывает, будет ли валидироваться издатель при валидации токена
		ValidateIssuer = true,

		// строка, представляющая издателя
		ValidIssuer = MyAuthOptions.ISSUER,

		// будет ли валидироваться потребитель токена
		ValidateAudience = true,

		// установка потребителя токена
		ValidAudience = MyAuthOptions.AUDIENCE,

		// будет ли валидироваться время существования
		ValidateLifetime = true,

		// установка ключа безопасности
		IssuerSigningKey = MyAuthOptions.GetSymmetricSecurityKey(),

		// валидация ключа безопасности
		ValidateIssuerSigningKey = true,
	};
});

builder.Services.AddScoped<ILibraryService, LibraryService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var path = Path.Combine(AppContext.BaseDirectory, file);
	options.IncludeXmlComments(path);
});
builder.Services.AddDbContext<LibraryDbContext>(options =>
{
	options.UseSqlite(builder.Configuration.GetConnectionString("Library"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


//app.MapControllerRoute("default", "api/{controller=Books}/{action}");
app.MapControllers();

app.Run();