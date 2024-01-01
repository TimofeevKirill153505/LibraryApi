using System.Reflection;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using LibraryApi.Authentication;
using LibraryApi.DBContext;
using LibraryApi.Services;
using LibraryApi.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(BookMapProfile));

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

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin Policy", policy =>
	{
		policy.RequireRole(new string[] {"admin"});
	});
});

builder.Services.AddScoped<ILibraryService, LibraryService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var path = Path.Combine(AppContext.BaseDirectory, file);
	options.IncludeXmlComments(path, true);
	
	//options.SwaggerDoc("LibraryApi", new OpenApiInfo(){Title = "LibraryApi", Version = "1.0"});
	
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme //
	{
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme.\nEnter" +
					" your token in the text input below. Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	
	
	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{	// initial collection begin
		{	// key-value pair
			new OpenApiSecurityScheme	// key
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}

			},
			new List<string>()		// value
		}
	});
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

app.UseAuthentication();
app.UseAuthorization();


//app.MapControllerRoute("default", "api/{controller=Books}/{action}");
app.MapControllers();

app.Run();