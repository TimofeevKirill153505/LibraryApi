namespace LibraryApi.Read.Services;

public interface IReadRepository<out T>
{
	/// <summary>
	/// Gets all objects in repository
	/// </summary>
	/// <returns>Collection of objects</returns>
	/// <exception cref="Exception">Some error occured. Depends on implementation Depends on implementation.
	/// Error message contains information about this error</exception>
	public IEnumerable<T> GetAll();

	//public IEnumerable<T> GetAllByPredicate(Func<T, bool> predicate);

	/// <summary>
	/// Gets all objects in repository
	/// </summary>
	/// <returns>Collection of objects</returns>
	/// <exception cref="KeyNotFoundException">There is no object with given id</exception>
	/// <exception cref="Exception">Some error occured. Depends on implementation Depends on implementation.
	/// Error message contains information about this error</exception>
	public T GetById(int id);
	
	/// <summary>
	/// Gets all objects in repository
	/// </summary>
	/// <returns>First object that fits predicate</returns>
	/// <exception cref="KeyNotFoundException">There is no object that fits the predicate</exception>
	/// <exception cref="Exception">Some error occured. Depends on implementation Depends on implementation.
	/// Error message contains information about this error</exception>
	public T GetFirst(Func<T, bool> predicate);
}