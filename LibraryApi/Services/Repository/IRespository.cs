using System.Diagnostics;

namespace LibraryApi.Services.Repository;

public interface IRepository<T>: IDisposable
{
	/// <summary>
	/// Creates new object in repository
	/// </summary>
	/// <param name="obj">Object to create in repository</param>
	/// <returns>Created object from repository</returns>
	/// <exception cref="ArgumentException">Object in params is wrong (id is occupied or some data are invalid)</exception>
	/// <exception cref="Exception">Other error occured. Depends on implementation.
	/// Error message contains information about this error</exception>
	public T Create(T obj);

	/// <summary>
	/// Gets all objects in repository
	/// </summary>
	/// <returns>Collection of objects</returns>
	/// <exception cref="Exception">Some error occured. Depends on implementation Depends on implementation.
	/// Error message contains information about this error</exception>
	public IEnumerable<T> GetAll();
	
	/// <summary>
	/// Gets all objects in repository
	/// </summary>
	/// <returns>Collection of objects</returns>
	/// <exception cref="KeyNotFoundException">There is no object with given id</exception>
	/// <exception cref="Exception">Some error occured. Depends on implementation Depends on implementation.
	/// Error message contains information about this error</exception>
	public T GetById(int id);

	/// <summary>
	/// Updates object with the given id. Replaces it with new obj, but id remains as it was
	/// </summary>
	/// <param name="id">Id of object to update</param>
	/// <param name="obj">Object with new values of fields</param>
	/// <returns>Updated object</returns>
	/// <exception cref="KeyNotFoundException">There is no object with given id</exception>
	/// <exception cref="ArgumentException">Object in params is wrong (id is occupied or some data are invalid)</exception>
	/// <exception cref="Exception">Other error occured. Depends on implementation.</exception>
	public T Update(int id, T obj);
	
	/// <summary>
	/// Deletes object with given id
	/// </summary>
	/// <param name="id">Id of object to delete</param>
	/// <returns>Deleted object</returns>
	/// <exception cref="KeyNotFoundException">There is no object with given id</exception>
	/// <exception cref="Exception">Other error occured. Depends on implementation.</exception>
	public T Delete(int id);
	
}