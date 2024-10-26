using Melin.Server.Models;
using Melin.Server.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Services;

public class TagService
{
    private readonly TagContext _tagContext;

    public TagService(TagContext tagContext)
    {
        _tagContext = tagContext;
    }

    public async Task<Tag> CreateTagAsync(Tag tag)
    {
        _tagContext.Tags.Add(tag);
        await _tagContext.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag> GetTagAsync(int id)
    {
        return await _tagContext.Tags.FindAsync(id);
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _tagContext.Tags.ToListAsync();
    }
}
