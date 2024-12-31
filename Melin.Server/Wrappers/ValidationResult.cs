namespace Melin.Server.Wrappers;

public class ValidationResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
}