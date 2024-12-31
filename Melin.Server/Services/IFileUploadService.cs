using Melin.Server.Wrappers;

namespace Melin.Server.Services;

public interface IFileUploadService
{
    public Task<Result<string>> ValidateTextFile();
    public Task<Result<string>> ValidateCsvFile();
    public Task<Result<string>> ValidateBibTexFile();
    public Task<Result<string>> ValidateJsonFile();
}