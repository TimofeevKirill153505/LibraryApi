namespace LibraryApi.Domain.Result;
/// <summary>
/// Class, created to react at errors.
/// </summary>
/// <param name="Success">Is action was successful</param>
/// <param name="Data">Data that oy want to give</param>
/// <param name="ErrorMessage">Message, that describe an error occured</param>
/// <typeparam name="T"></typeparam>
public record Result<T> (bool Success, T? Data, string ErrorMessage = "");