using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApi.Domain.Auth;

public static class TokenValidation
{
	public const string ValidIssuer = "LibraryApi.Auth";
	
	public static SignatureValidator GetSignatureValidator(string validateTokenUri)
	{
		return (token, parameters) =>
		{
			Console.WriteLine("In signatureValidator");
			
			HttpClient client = new HttpClient();
			HttpRequestMessage message = new HttpRequestMessage();

			message.Headers.Authorization = new AuthenticationHeaderValue("Bearer",$"{token}");
			message.RequestUri = new Uri(validateTokenUri);
			var res = client.Send(message);
			if (res.StatusCode != HttpStatusCode.OK)
			{
				Console.WriteLine("Token is not ok");
				throw new SecurityTokenInvalidSignatureException();
			}
			Console.WriteLine("Token is ok");
			return new JwtSecurityToken(token);
		};
	}
}