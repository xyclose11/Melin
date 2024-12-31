using Melin.Server.Wrappers;

namespace Melin.Server.Services;

public interface IFileUploadService
{
    public Task<Result<string>> ValidateFile();
}