using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Melin.Server.Filter;
using Melin.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Melin.Server.Models;
using Melin.Server.Models.Context;
using Melin.Server.Models.References;
using Melin.Server.Services;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation = Melin.Server.Models.Presentation;
using Report = Melin.Server.Models.Report;
using Software = Melin.Server.Models.Software;
using Task = System.Threading.Tasks.Task;

namespace Melin.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ReferenceController : ControllerBase
{
    private readonly IReferenceService _referenceService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly TagService _tagService;

    public ReferenceController(IReferenceService referenceService, UserManager<IdentityUser> userManager, TagService tagService)
    {
        _referenceService = referenceService;
        _userManager = userManager;
        _tagService = tagService;
    }

    [HttpGet("get-single-reference")]
    [Authorize]
    public async Task<IActionResult> GetSingleReference(int refId)
    {
        try
        {
            var reference = await _referenceService.GetReferenceByIdAsync(User.Identity.Name, refId);

            if (reference != null)
            {
                return Ok(reference.Data);
            }
            else
            {
                return NotFound("REFERENCE NOT FOUND");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
    
    [HttpGet("references")]
    [Authorize]
    public async Task<IActionResult> GetReferences([FromQuery] PaginationFilter filter)
    {
        var userEmail = User.Identity.Name;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

        var pagedReferences = await _referenceService.GetOwnedReferencesAsync(filter, userEmail);

        var totalRefCount = await _referenceService.GetOwnedReferenceCountAsync(userEmail);
        
        return Ok(new PagedResponse<ICollection<Reference>>(pagedReferences, validFilter.PageNumber, validFilter.PageSize, totalRefCount));
    }

    // POST: Create new reference
    [HttpPost("create-reference")]
    [Authorize]
    public async Task<ActionResult<Reference>> PostReference(Reference reference) {
        if (reference == null) {
            return BadRequest("Reference cannot be null.");
        }
        
        try {
            await _referenceService.AddReferenceAsync(reference);

            return CreatedAtAction(nameof(PostReference), new { id = reference.Id }, reference);
        } catch (Exception ex) {
            return StatusCode(500, "An error occurred while creating the reference.");
        }
    }
    
    [HttpPost("create-artwork")]
    [Authorize]
    public async Task<ActionResult<Artwork>> PostReferenceArtwork([FromBody] Artwork artwork)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (artwork.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(artwork.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        artwork.OwnerEmail = User.Identity.Name;

        await _referenceService.AddArtworkAsync(artwork);

        return Ok();
    }
    
    [HttpPost("create-audio-recording")]
    [Authorize]
    public async Task<ActionResult<AudioRecording>> PostReferenceAudioRecording([FromBody] AudioRecording audioRecording)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (audioRecording.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(audioRecording.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        audioRecording.OwnerEmail = User.Identity.Name;

        await _referenceService.AddReferenceAsync(audioRecording);

        return Ok();
    }
    
    

    [HttpPost("create-bill")]
    [Authorize]
    public async Task<ActionResult<Artwork>> PostReferenceBill([FromBody] Bill bill)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (bill.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(bill.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        bill.OwnerEmail = User.Identity.Name;

        await _referenceService.AddBillAsync(bill);

        return Ok();
    }

    [HttpPost("create-blog-post")]
    [Authorize]
    public async Task<ActionResult<BlogPost>> PostReferenceBlogPost([FromBody] BlogPost blogPost)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (blogPost.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(blogPost.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        blogPost.OwnerEmail = User.Identity.Name;

        await _referenceService.AddBlogPostAsync(blogPost);

        return Ok();
    }

    [HttpPost("create-book-section")]
    [Authorize]
    public async Task<ActionResult<BookSection>> PostReferenceBookSection([FromBody] BookSection bookSection)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (bookSection.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(bookSection.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        bookSection.OwnerEmail = User.Identity.Name;

        await _referenceService.AddBookSectionAsync(bookSection);

        return Ok();
    }

    [HttpPost("create-case")]
    [Authorize]
    public async Task<ActionResult<LegalCases>> PostReferenceCase([FromBody] LegalCases cCase)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (cCase.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(cCase.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        cCase.OwnerEmail = User.Identity.Name;

        await _referenceService.AddCaseAsync(cCase);

        return Ok();
    }

    [HttpPost("create-conference-paper")]
    [Authorize]
    public async Task<ActionResult<ConferencePaper>> PostReferenceConferencePaper([FromBody] ConferencePaper conferencePaper)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (conferencePaper.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(conferencePaper.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        conferencePaper.OwnerEmail = User.Identity.Name;

        await _referenceService.AddConferencePaperAsync(conferencePaper);

        return Ok();
    }

    [HttpPost("create-dictionary-entry")]
    [Authorize]
    public async Task<ActionResult<DictionaryEntry>> PostReferenceArtwork([FromBody] DictionaryEntry dictionaryEntry)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (dictionaryEntry.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(dictionaryEntry.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        dictionaryEntry.OwnerEmail = User.Identity.Name;

        await _referenceService.AddDictionaryEntryAsync(dictionaryEntry);

        return Ok();
    }

    [HttpPost("create-document")]
    [Authorize]
    public async Task<ActionResult<Document>> PostReferenceArtwork([FromBody] Document document)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (document.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(document.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        document.OwnerEmail = User.Identity.Name;

        await _referenceService.AddDocumentAsync(document);

        return Ok();
    }

    [HttpPost("create-email")]
    [Authorize]
    public async Task<ActionResult<Email>> PostReferenceEmail([FromBody] Email email)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (email.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(email.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        email.OwnerEmail = User.Identity.Name;

        await _referenceService.AddEmailAsync(email);

        return Ok();
    }

    [HttpPost("create-encyclopedia-article")]
    [Authorize]
    public async Task<ActionResult<EncyclopediaArticle>> PostReferenceArtwork([FromBody] EncyclopediaArticle encyclopediaArticle)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (encyclopediaArticle.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(encyclopediaArticle.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        encyclopediaArticle.OwnerEmail = User.Identity.Name;

        await _referenceService.AddEncyclopediaArticleAsync(encyclopediaArticle);

        return Ok();
    }

    [HttpPost("create-film")]
    [Authorize]
    public async Task<ActionResult<Film>> PostReferenceArtwork([FromBody] Film film)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (film.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(film.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        film.OwnerEmail = User.Identity.Name;

        await _referenceService.AddFilmAsync(film);

        return Ok();
    }

    [HttpPost("create-forum-post")]
    [Authorize]
    public async Task<ActionResult<ForumPost>> PostReferenceArtwork([FromBody] ForumPost forumPost)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (forumPost.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(forumPost.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        forumPost.OwnerEmail = User.Identity.Name;

        await _referenceService.AddForumPostAsync(forumPost);

        return Ok();
    }

    [HttpPost("create-hearing")]
    [Authorize]
    public async Task<ActionResult<Hearing>> PostReferenceArtwork([FromBody] Hearing hearing)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (hearing.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(hearing.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        hearing.OwnerEmail = User.Identity.Name;

        await _referenceService.AddHearingAsync(hearing);

        return Ok();
    }

    [HttpPost("create-instant-message")]
    [Authorize]
    public async Task<ActionResult<InstantMessage>> PostReferenceInstantMessage([FromBody] InstantMessage instantMessage)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (instantMessage.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(instantMessage.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        instantMessage.OwnerEmail = User.Identity.Name;

        await _referenceService.AddInstantMessageAsync(instantMessage);

        return Ok();
    }

    [HttpPost("create-interview")]
    [Authorize]
    public async Task<ActionResult<Interview>> PostReferenceArtwork([FromBody] Interview interview)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (interview.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(interview.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        interview.OwnerEmail = User.Identity.Name;

        await _referenceService.AddInterviewAsync(interview);

        return Ok();
    }

    [HttpPost("create-journal-article")]
    [Authorize]
    public async Task<ActionResult<JournalArticle>> PostReferenceArtwork([FromBody] JournalArticle journalArticle)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (journalArticle.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(journalArticle.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        journalArticle.OwnerEmail = User.Identity.Name;

        await _referenceService.AddJournalArticleAsync(journalArticle);

        return Ok();
    }

    [HttpPost("create-letter")]
    [Authorize]
    public async Task<ActionResult<Letter>> PostReferenceArtwork([FromBody] Letter letter)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (letter.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(letter.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        letter.OwnerEmail = User.Identity.Name;

        await _referenceService.AddLetterAsync(letter);

        return Ok();
    }

    [HttpPost("create-magazine-article")]
    [Authorize]
    public async Task<ActionResult<MagazineArticle>> PostReferenceMagazineArticle([FromBody] MagazineArticle magazineArticle)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (magazineArticle.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(magazineArticle.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        magazineArticle.OwnerEmail = User.Identity.Name;

        await _referenceService.AddMagazineArticleAsync(magazineArticle);

        return Ok();
    }

    [HttpPost("create-patent")]
    [Authorize]
    public async Task<ActionResult<Patent>> PostReferencePatent([FromBody] Patent patent)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (patent.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(patent.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        patent.OwnerEmail = User.Identity.Name;

        await _referenceService.AddPatentAsync(patent);

        return Ok();
    }
    
    [HttpPost("create-podcast")]
    [Authorize]
    public async Task<ActionResult<Podcast>> PostReferencePodcast([FromBody] Podcast podcast)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (podcast.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(podcast.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        podcast.OwnerEmail = User.Identity.Name;

        await _referenceService.AddPodcastAsync(podcast);

        return Ok();
    }

    [HttpPost("create-presentation")]
    [Authorize]
    public async Task<ActionResult<Presentation>> PostReferencePresentation([FromBody] Presentation presentation)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (presentation.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(presentation.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        presentation.OwnerEmail = User.Identity.Name;

        await _referenceService.AddPresentationAsync(presentation);

        return Ok();
    }

    [HttpPost("create-radio-broadcast")]
    [Authorize]
    public async Task<ActionResult<RadioBroadcast>> PostReferenceRadioBroadcast([FromBody] RadioBroadcast radioBroadcast)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (radioBroadcast.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(radioBroadcast.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        radioBroadcast.OwnerEmail = User.Identity.Name;

        await _referenceService.AddRadioBroadcastAsync(radioBroadcast);

        return Ok();
    }

    [HttpPost("create-report")]
    [Authorize]
    public async Task<ActionResult<Report>> PostReferencePatent([FromBody] Report report)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (report.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(report.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        report.OwnerEmail = User.Identity.Name;

        await _referenceService.AddReportAsync(report);

        return Ok();
    }

    [HttpPost("create-software")]
    [Authorize]
    public async Task<ActionResult<Software>> PostReferenceSoftware([FromBody] Software software)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (software.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(software.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        software.OwnerEmail = User.Identity.Name;

        await _referenceService.AddSoftwareAsync(software);

        return Ok();
    }

    [HttpPost("create-statute")]
    [Authorize]
    public async Task<ActionResult<Statute>> PostReferenceStatute([FromBody] Statute statute)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (statute.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(statute.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        statute.OwnerEmail = User.Identity.Name;

        await _referenceService.AddStatuteAsync(statute);

        return Ok();
    }

    [HttpPost("create-thesis")]
    [Authorize]
    public async Task<ActionResult<Thesis>> PostReferenceThesis([FromBody] Thesis thesis)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (thesis.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(thesis.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        thesis.OwnerEmail = User.Identity.Name;

        await _referenceService.AddThesisAsync(thesis);

        return Ok();
    }

    [HttpPost("create-tv-broadcast")]
    [Authorize]
    public async Task<ActionResult<TVBroadcast>> PostReferenceTVBroadcast([FromBody] TVBroadcast tvBroadcast)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (tvBroadcast.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(tvBroadcast.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        tvBroadcast.OwnerEmail = User.Identity.Name;

        await _referenceService.AddTVBroadcastAsync(tvBroadcast);

        return Ok();
    }

    [HttpPost("create-video-recording")]
    [Authorize]
    public async Task<ActionResult<VideoRecording>> PostReferenceVideoRecording([FromBody] VideoRecording videoRecording)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (videoRecording.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(videoRecording.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        videoRecording.OwnerEmail = User.Identity.Name;

        await _referenceService.AddVideoRecordingAsync(videoRecording);

        return Ok();
    }

    [HttpPost("create-website")]
    [Authorize]
    public async Task<ActionResult<Webpage>> PostReferenceWebpage([FromBody] Webpage website)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (website.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(website.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        website.OwnerEmail = User.Identity.Name;

        await _referenceService.AddWebpageAsync(website);

        return Ok();
    }


    [HttpPost("create-book")]
    [Authorize]
    public async Task<ActionResult<Book>> PostReferenceBook([FromBody] Book book) {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (book.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(book.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        book.OwnerEmail = User.Identity.Name;


        await _referenceService.AddBookAsync(book);
        
        return Ok();
    }
    


    private async Task<bool> HandleTagsWithReferencePost(ICollection<Tag> tags)
    {
        try
        {
            if (tags.Count > 1)
            {
                string createdBy = User.Identity.Name;
                await _tagService.CreateTagsAsync(tags, createdBy);
            }
            else
            {
                foreach (var tag in tags)
                {
                    tag.CreatedBy = User.Identity.Name;
                    var existingTag = await _tagService.GetTagAsync(tag.Id);
                    if (existingTag == null)
                    {
                        await _tagService.CreateTagAsync(tag);
                    }
                }
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

    }

    
    // UPDATE: update reference
    [HttpPut("update-artwork")]
    [Authorize]
    public async Task<ActionResult<Artwork>> UpdateArtwork(int oldRefId, [FromBody] Artwork artwork)
    {
        try
        {
            var prevArtwork = await _referenceService.UpdateArtworkAsync(User.Identity.Name, oldRefId, artwork);
            if (prevArtwork.Data == false)
            {
                return NotFound("Reference Not Found. Cannot Update");
            }
            
            return Ok("Artwork updated successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    

    // DELETE: Delete single reference
    [HttpDelete("delete-reference")]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteSpecificReference(int refId)
    {
        try
        {

            var r = await _referenceService.DeleteReferenceAsync(User.Identity.Name, refId);
            
            if (r == null)
            {
                return NotFound("Reference with ID: " + refId + " not found.");
            }
            
            
            return Ok("Reference with the ID: " + refId + " has been deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // DELETE: Delete list of references
    [HttpDelete("delete-multiple-references")]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteListOfReferences(List<int> refIdList)
    {
        try
        {
            if (refIdList.Count < 1)
            {
                return Problem("ERROR: Ref ID List has < 1 ids");
            }

            await _referenceService.DeleteReferenceRangeAsync(User.Identity.Name, refIdList);
            
            
            return Ok(true);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}