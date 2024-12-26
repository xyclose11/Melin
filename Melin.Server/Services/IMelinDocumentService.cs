using Melin.Server.Models;

namespace Melin.Server.Services;

public interface IMelinDocumentService
{
    public Task<MelinDocument> CreateDocumentAsync(string userEmail, string title, string content);
    public Task<bool> UpdateDocumentAsync(string userEmail, string title, string content);
    public Task<MelinDocument?> GetDocumentAsync(string userEmail, string title);
}