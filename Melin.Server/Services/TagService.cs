using Melin.Server.Models;
using Melin.Server.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Services;

public class TagService
{
    // private readonly TagContext _tagContext;
    private readonly ReferenceContext _referenceContext;

    public TagService(ReferenceContext referenceContext)
    {
        _referenceContext = referenceContext;
    }
    
    // public TagService(TagContext tagContext)
    // {
    //     _tagContext = tagContext;
    // }

    public async Task<Tag> CreateTagAsync(Tag tag)
    {
        // _tagContext.Tags.Add(tag);
        // await _tagContext.SaveChangesAsync();
        _referenceContext.Tags.Add(tag);
        await _referenceContext.SaveChangesAsync();
        return tag;
    }

    public async Task<ICollection<Tag>> CreateTagsAsync(ICollection<Tag> tags, string createdBy)
    {
        foreach (var tag in tags)
        {
            var existingTag = await GetTagAsync(tag.Id);
            if (existingTag == null && createdBy != null)
            {
                tag.CreatedBy = createdBy;
                _referenceContext.Tags.Add(tag);
            }
        }

        // await _tagContext.SaveChangesAsync();
        await _referenceContext.SaveChangesAsync();
        return tags;
    }
    

    public async Task<Tag> GetTagAsync(int id)
    {
        return await _referenceContext.Tags.FindAsync(id);
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _referenceContext.Tags.ToListAsync();
    }
}
