using Melin.Server.Data;
using Melin.Server.Models;
using Melin.Server.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Services;

public class MelinDocumentService : IMelinDocumentService
{
    private readonly DataContext _dataContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public MelinDocumentService(DataContext dataContext, UserManager<ApplicationUser> userManager)
    {
        _dataContext = dataContext;
        _userManager = userManager;
    }
    
    public async Task<MelinDocument> CreateDocumentAsync(string userEmail, string title, string content)
    {
        var document = new MelinDocument
        {
            Title = title,
            Content = content,
            LastModifiedByEmail = userEmail,
            OwnerEmail = userEmail,
            CreatedAt = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        };

        _dataContext.MelinDocuments.Add(document);
        await _dataContext.SaveChangesAsync();
        return document;
    }
    

    public async Task<bool> UpdateDocumentAsync(string userEmail, string title, string content)
    {
        var document = await _dataContext.MelinDocuments
            .Where(t => t.Title == title)
            .Where(d => d.OwnerEmail == userEmail)
            .FirstOrDefaultAsync();

        if (document == null) return false;
        document.Content = content;
        document.LastModified = DateTime.UtcNow;
        document.LastModifiedByEmail = userEmail;
        document.Title = title;
        await _dataContext.SaveChangesAsync();

        return true;

    }

    public async Task<MelinDocument?> GetDocumentAsync(string userEmail, string title)
    {
        var document = await _dataContext.MelinDocuments
            .Where(d => d.OwnerEmail == userEmail)
            .Where(d => d.Title == title)
            .FirstOrDefaultAsync();

        return document ?? null;
    }

    public async Task<List<MelinDocument>?> GetDocumentsAsync(string userEmail)
    {
        var documents = await _dataContext.MelinDocuments
            .Where(d => d.OwnerEmail == userEmail)
            .ToListAsync();
        
        return documents ?? null;
    }   
}