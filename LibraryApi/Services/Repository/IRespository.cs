using System.Diagnostics;

namespace LibraryApi.Services.Repository;

public interface IRepository<T>: IDisposable
{
	public T Create(T obj);

	public IEnumerable<T> GetAll();

	public T Update(int id, T obj);
		
	public T Delete(int id);
	
}