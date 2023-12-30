using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApi.Authentication;

public class MyAuthOptions
{
	
	public const string ISSUER = "MyAuthServer"; // издатель токена
	public const string AUDIENCE = "MyAuthClient"; // потребитель токена
	const string KEY = "mysuperunique_secretsecretsecretkey!123";   // ключ для шифрации
	
	// Can be changed to something more secure
	public static SymmetricSecurityKey GetSymmetricSecurityKey()
	{
		return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
	}
}