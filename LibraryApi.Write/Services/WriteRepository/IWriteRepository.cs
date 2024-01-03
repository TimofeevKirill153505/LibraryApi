using LibraryApi.Domain.Models;

namespace LibraryApi.Write.Services.WriteRepository;

public interface IWriteRepository<T>
{
	public T Create(T bookDto);
	
	public T Update(T bookDto);

	public T Delete(int id);
}