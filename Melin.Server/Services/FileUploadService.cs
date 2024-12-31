using Melin.Server.Wrappers;

namespace Melin.Server.Services;

public class FileUploadService : IFileUploadService
{
    public async Task<Result<string>> ValidateTextFile()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<string>> ValidateCsvFile()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<string>> ValidateBibTexFile()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<string>> ValidateJsonFile()
    {
        return Result<string>.SuccessResult("Valid");
    }
}