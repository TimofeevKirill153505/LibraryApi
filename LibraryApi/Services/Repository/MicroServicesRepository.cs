using System.Net;
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

	private T UnpackResponse<T>(HttpResponseMessage resp)
	{
		StreamReader streamReader = new StreamReader(resp.Content.ReadAsStream());
		string buff = streamReader.ReadToEnd();
		if (resp.StatusCode != HttpStatusCode.OK && resp.StatusCode != HttpStatusCode.Created)
		{
			if (resp.StatusCode == HttpStatusCode.Unauthorized) 
				throw new Exception("Attempt of unauthorized access");
			if (resp.StatusCode == HttpStatusCode.Forbidden)
				throw new Exception("You have no rights to access");

			Result<T> res;
			try
			{
				res = JsonSerializer.Deserialize<Result<T>>(buff, _jsonOptions);
			}
			catch (Exception e)
			{
				throw new Exception("Response from service, " +
									$"that can't be parsed by Json serializer. StatusCode: {(int)resp.StatusCode}");
			}

			if (resp.StatusCode == HttpStatusCode.NotFound)
				throw new KeyNotFoundException(res.ErrorMessage);

			if (resp.StatusCode == HttpStatusCode.BadRequest)
				throw new ArgumentException(res.ErrorMessage);

			throw new Exception(res.ErrorMessage);
		}

		
		
		var result = JsonSerializer.Deserialize<Result<T>>(buff, _jsonOptions);

		return result.Data;
		// UnauthorizedAccessException
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

		return UnpackResponse<BookDto>(resp);
	}

	public IEnumerable<BookDto> GetAll()
	{
		//var res = _client.GetAsync(Path.Combine(_readUri, "api/books"));
		HttpRequestMessage message = new HttpRequestMessage();
		message.Method = HttpMethod.Get;
		message.RequestUri = new Uri(Path.Combine(_readUri, "api/books"));
		var resp = _client.Send(message);

		return UnpackResponse<IEnumerable<BookDto>>(resp);
	}
	
	public BookDto GetById(int id)
	{
		//var res = _client.GetAsync(Path.Combine(_readUri, "api/books"));
		HttpRequestMessage message = new HttpRequestMessage();
		message.Method = HttpMethod.Get;
		message.RequestUri = new Uri(Path.Combine(_readUri, $"api/books/{id}"));
		var resp = _client.Send(message);

		return UnpackResponse<BookDto>(resp);
	}
	
	public BookDto Update(int id, BookDto obj)
	{
		HttpRequestMessage message = new HttpRequestMessage();
		
		message.Method = HttpMethod.Put;
		message.Content = JsonContent.Create(obj);
		message.RequestUri = new Uri(Path.Combine(_writeUri, $"api/books/{id}"));
		
		var resp = _client.Send(message);

		return UnpackResponse<BookDto>(resp);
	}

	public BookDto Delete(int id)
	{
		HttpRequestMessage message = new HttpRequestMessage();
		
		message.Method = HttpMethod.Delete;
		message.RequestUri = new Uri(Path.Combine(_writeUri, $"api/books/{id}"));
		
		var resp = _client.Send(message);

		return UnpackResponse<BookDto>(resp);
	}
}