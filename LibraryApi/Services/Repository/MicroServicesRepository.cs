using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibraryApi.Domain.Models;
using LibraryApi.Domain.Result;

namespace LibraryApi.Services.Repository;

public class MicroServicesRepository: IRepository<BookDto>
{
	private readonly HttpClient _client;
	private readonly string _readUri;
	private readonly string _writeUri;
	private readonly HttpContext _context;
	
	private readonly JsonSerializerOptions _jsonOptions;
	
	public MicroServicesRepository(HttpClient client, IConfiguration config, IHttpContextAccessor accessor)
	{
		_client = client;
		_readUri = config["Services:Read"];
		_writeUri = config["Services:Write"];
		
		_jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		_context = accessor.HttpContext;
		if (_context.Request.Headers.Authorization.Any())
		{
			_client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(
				_context.Request.Headers.Authorization.First());
		}
	}
	
	public void Dispose()
	{}

	public BookDto Create(BookDto obj)
	{
		HttpRequestMessage message = new HttpRequestMessage();
		
		message.Method = HttpMethod.Post;
		message.Content = JsonContent.Create(obj);
		message.RequestUri = new Uri(Path.Combine(_writeUri, "api/books"));
		
		var resp = _client.Send(message);
		
		StreamReader streamReader = new StreamReader(resp.Content.ReadAsStream());
		string buff = streamReader.ReadToEnd();
		
		var result = JsonSerializer.Deserialize<Result<BookDto>>(buff, _jsonOptions);

		if (!result.Success)
		{
			throw new Exception(result.ErrorMessage);
		}
		
		return result.Data;
	}

	public IEnumerable<BookDto> GetAll()
	{
		//var res = _client.GetAsync(Path.Combine(_readUri, "api/books"));
		HttpRequestMessage message = new HttpRequestMessage();
		message.Method = HttpMethod.Get;
		message.RequestUri = new Uri(Path.Combine(_readUri, "api/books"));
		var resp = _client.Send(message);
		
		StreamReader streamReader = new StreamReader(resp.Content.ReadAsStream());
		string buff = streamReader.ReadToEnd();
		
		var result = JsonSerializer.Deserialize<Result<IEnumerable<BookDto>>>(buff, _jsonOptions);

		if (!result.Success)
		{

			throw new Exception(result.ErrorMessage);
		}
		
		return result.Data;
	}

	public BookDto Update(int id, BookDto obj)
	{
		HttpRequestMessage message = new HttpRequestMessage();
		
		message.Method = HttpMethod.Put;
		message.Content = JsonContent.Create(obj);
		message.RequestUri = new Uri(Path.Combine(_writeUri, $"api/books/{id}"));
		
		var resp = _client.Send(message);

		StreamReader streamReader = new StreamReader(resp.Content.ReadAsStream());
		
		string buff = streamReader.ReadToEnd();
		Console.WriteLine($"StatusCode {resp.StatusCode}");
		Console.WriteLine($"value in buffer {buff}");
		
		Console.WriteLine("before deser");
		var result = JsonSerializer.Deserialize<Result<BookDto>>(buff, _jsonOptions);
		Console.WriteLine("after deser");
		if (!result.Success)
		{
			throw new Exception(result.ErrorMessage);
		}
		
		return result.Data;
	}

	public BookDto Delete(int id)
	{
		HttpRequestMessage message = new HttpRequestMessage();
		
		message.Method = HttpMethod.Delete;
		message.RequestUri = new Uri(Path.Combine(_writeUri, $"api/books/{id}"));
		
		var resp = _client.Send(message);
		
		StreamReader streamReader = new StreamReader(resp.Content.ReadAsStream());
		string buff = streamReader.ReadToEnd();
		
		var result = JsonSerializer.Deserialize<Result<BookDto>>(buff, _jsonOptions);

		if (!result.Success)
		{
			throw new Exception(result.ErrorMessage);
		}
		
		return result.Data;
	}
}