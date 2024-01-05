using System.Text;
using LibraryApi.Domain.Auth;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApi.AuthServer.Authentication;

public class MyAuthOptions
{
	
	public const string ISSUER = TokenValidation.ValidIssuer; // token issuer
	// public const string AUDIENCE = "MyAuthClient"; // token audience
	const string KEY = "mysuperunique_secretsecretsecretkey!123";   // key 
	
	// Can be changed to something more secure
	public static SymmetricSecurityKey GetSymmetricSecurityKey()
	{
		return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
	}
}