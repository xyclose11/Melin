using Melin.Server.Models;

namespace Melin.Server.Wrappers;

public class ReferenceResponse<T> where T : Reference, IDisposable
{
    
    public bool Success { get; set; }
    public T Data { get; set; }
    public string ErrorMessage { get; set; }

    public static Result<T> SuccessResult(T data)
    {
        return new Result<T> { Success = true, Data = data };
    }

    public static Result<T> FailureResult(string errorMessage)
    {
        return new Result<T> { Success = false, ErrorMessage = errorMessage };
    }

}