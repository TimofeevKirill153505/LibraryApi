namespace LibraryApi.Read.Services;

public interface IReadRepository<out T>
{
	public IEnumerable<T> GetAll();

	public IEnumerable<T> GetAllByPredicate(Func<T, bool> predicate);

	public T GetFirst(Func<T, bool> predicate);
}