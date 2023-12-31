using System.Reflection;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using LibraryApi.Authentication;
using LibraryApi.Domain.Auth;
using LibraryApi.Domain.Models;
using LibraryApi.Services.Repository;
using LibraryApi.Services;
using LibraryApi.Domain.MappingProfiles;
using LibraryApi.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy =>
	{
		policy.RequireRole("admin");
	});
});


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

// builder.Services.AddDbContext<LibraryDbContext>(options =>
// {
// 	options.UseSqlite(builder.Configuration.GetConnectionString("Library"));
// });

builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddHttpClient<IRepository<BookDto>, MicroServicesRepository>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddAutoMapper(typeof(BookMapProfile));
builder.Services.AddHttpClient<IAuthService, AuthService>();
// builder.Services.AddScoped<IRepository<Book>, DbRepository>(x =>
// 	new DbRepository(builder.Configuration.GetConnectionString("Library"))
// );

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