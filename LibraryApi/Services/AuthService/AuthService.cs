using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Services.AuthService;

public class AuthService: IAuthService
{
	private readonly HttpClient _client;
	private readonly string _authUri;
	public AuthService(HttpClient client, IConfiguration config)
	{
		_client = client;
		_authUri = config["Services:Auth"];
	}
	
	public IActionResult GetToken(string username, string password)
	{
		HttpRequestMessage messg = new HttpRequestMessage();
		messg.RequestUri = new Uri($"{_authUri}?username={username}&password={password}");
		messg.Method = HttpMethod.Get;

		var resp = _client.Send(messg);
		if (resp.StatusCode != HttpStatusCode.OK)
		{
			return new StatusCodeResult((int)resp.StatusCode);
		}

		StreamReader sr = new StreamReader(resp.Content.ReadAsStream());
		string buff = sr.ReadToEnd();
		return new OkObjectResult(buff);
	}
}