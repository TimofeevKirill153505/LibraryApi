using LibraryApi.AuthServer.Authentication;
using LibraryApi.AuthServer.MapperProfiles;
using LibraryApi.AuthServer.Services.AuthService;
using LibraryApi.AuthServer.Services.UsersRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(UserMapProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		
		ValidIssuer = MyAuthOptions.ISSUER,
		
		ValidateAudience = false,
		// ValidateAudience = true,
		// ValidAudience = MyAuthOptions.AUDIENCE,
		
		ValidateLifetime = true,
		
		IssuerSigningKey = MyAuthOptions.GetSymmetricSecurityKey(),

		// validation of security key
		ValidateIssuerSigningKey = true,
	};
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUsersRepository, SimpleUsersRepository>();
builder.Services.AddScoped<IAuthService, SimpleAuthService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	// app.UseSwagger();
	// app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();