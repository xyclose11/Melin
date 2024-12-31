using Melin.Server.Wrappers;

namespace Melin.Server.Services;

public class FileUploadService : IFileUploadService
{
    public async Task<Result<string>> ValidateFile()
    {
        return Result<string>.SuccessResult("Valid");
    }
}