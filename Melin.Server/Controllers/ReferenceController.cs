using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Melin.Server.Filter;
using Microsoft.AspNetCore.Authorization;
using Melin.Server.Models;
using Melin.Server.Models.DTO;
using Melin.Server.Models.References;
using Melin.Server.Services;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;

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

    /// <summary>
    /// Retrieves a single Reference from the currently logged-in user
    /// </summary>
    /// <param name="refId">Integer Query Parameter referring to the ID of the Reference</param>
    /// <returns>A Single Reference</returns>
    [HttpGet("get-single-reference")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Reference>> GetSingleReference([FromQuery] int refId)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Retrieve Reference: {ReferenceID}", refId);
                return Unauthorized();
            }
            Log.Information("GET: SingleReference of ID: {refId}", refId);
            var reference = await _referenceService.GetReferenceWithAllDetailsById(User.Identity.Name, refId);
            
            if (reference is { Success: true, Data: not null })
            {
                Log.Information("GET: Reference ID: {refId} Successfully retrieved by User: {userEmail}", refId, User.Identity.Name);
                return Ok(reference.Data);
            }

            Log.Information("GET: Reference Not Found ID: {refId}", refId);
            return NotFound("REFERENCE NOT FOUND");
        }
        catch (Exception e)
        {
            Log.Error("GET: Unable to retrieve single reference of id: {refId}", refId);
            return BadRequest();
        }
    }
    
    
    /// <summary>
    /// Retrieves a list of References from the currently logged-in user
    /// </summary>
    /// <param name="filter">A <see cref="PaginationFilter"/> object. Determines page number and quantity of References</param>
    /// <returns>A 'filtered' <see cref="PagedResponse{T}"/></returns>
    [HttpGet("references")]
    [Authorize]
    [ProducesResponseType(typeof(ICollection<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReferences([FromQuery] PaginationFilter filter)
    {
        try
        {
            if (User.Identity == null)
            {
                Log.Information("Unauthorized Attempt to Retrieve Multiple References");
                return Unauthorized();
            }
            var userEmail = User.Identity.Name;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize)
            {
                PageSize = 1000
            };
        
            var pagedReferences = await _referenceService.GetOwnedReferencesAsync(filter, userEmail);

            var totalRefCount = await _referenceService.GetOwnedReferenceCountAsync(userEmail);

            if (totalRefCount == 0)
            {
                Log.Information("GET: {userEmail}, attempted GetReferences, -> currently has 0",  User.Identity.Name);
                return NotFound("User currently has 0 references");
            }
            Log.Information("{userEmail}, retrieved {referenceAmount} References", User.Identity.Name, totalRefCount);
            
            // TEMP CONVERSION TESTING OUT DTO METHOD
            List<ReferenceToLibraryRequest> output = new List<ReferenceToLibraryRequest>();
            foreach (var reference in pagedReferences)
            {
                var res = new ReferenceToLibraryRequest
                {
                    Id = reference.Id,
                    Type = reference.Type.ToString(),
                    Title = reference.Title,
                    LocationStored = reference.LocationStored,
                    CreatedAt = reference.CreatedAt.ToString(CultureInfo.CurrentCulture),
                    UpdatedAt = reference.UpdatedAt.ToString(CultureInfo.CurrentCulture),
                    Creators = reference.Creators?.ToList(),
                    Tags = reference.Tags?.ToList(),
                    Language = reference.Language.ToString(),
                    DatePublished = reference.DatePublished.ToString(),
                    GroupNames = reference.Groups?.ToList().ConvertAll<string>(g => g.Name)
                };
                output.Add(res);
            }

            return Ok(new PagedResponse<ICollection<ReferenceToLibraryRequest>>(output, validFilter.PageNumber, validFilter.PageSize, totalRefCount));
        }
        catch (Exception e)
        {
            Log.Error("GET: GetReferences Failed. PaginationFilter: {filter}. By User: {userEmail}", filter, User.Identity.Name);
            return BadRequest();
        }

    }

    /// <summary>
    /// Creates a new Reference and sets the initial owner to the current User.
    /// Uses a custom JSON converter to handle the 30+ types of References on a single
    /// endpoint
    /// </summary>
    /// <param name="reference">A <see cref="Reference"/></param>
    /// <returns><see cref="StatusCodeResult"/></returns>
    // POST: Create new reference
    [HttpPost("create-reference")]
    [Authorize]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> PostReference([FromBody] Reference reference) {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            if (User.Identity == null)
            {
                Log.Warning("Unauthorized access attempted");
                return Unauthorized("User not authorized to create References");
            }
            reference.OwnerEmail = User.Identity.Name;

            await _referenceService.AddReferenceAsync(reference);
            Log.Information("{userEmail} Created a Reference with ID: {refId}",  User.Identity.Name, reference.Id);

            return Ok("Reference created successfully");
        } catch (Exception ex) {
            Log.Warning("Unable to create reference");
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Updates a User owned Reference, via HTTP PUT.
    /// </summary>
    /// <param name="id">Integer value depicting the Reference to be updated</param>
    /// <param name="updatedItem">An updated Reference</param>
    /// <returns><see cref="IActionResult"/></returns>
    // UPDATE
    [HttpPut("update/{id}")]
    [Authorize]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] Reference updatedItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (User.Identity?.Name == null)
        {
            Log.Information("Unauthorized attempt to update Reference: {RefID}", id);
            return Unauthorized("User Currently Not Authorized to Update Item");
        }
        
        Log.Information("PUT: {userEmail}, updating reference", User.Identity.Name);

        var result = await _referenceService.UpdateReferenceAsync(User.Identity.Name, id, updatedItem);

        if (!result.Success)
        {
            return BadRequest();
        }

        return new ObjectResult(updatedItem);
    }

    /// <summary>
    /// Deletes a single Reference
    /// </summary>
    /// <param name="refId">Integer Query Parameter for Reference ID</param>
    /// <returns><see cref="ActionResult"/></returns>
    // DELETE: Delete single reference
    [HttpDelete("delete-reference")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> DeleteSpecificReference([FromQuery] int refId)
    {
        try
        {
            if (User.Identity == null)
            {
                Log.Information("Unauthorized Attempt to delete Reference ID: {ReferenceID}", refId);
                return Unauthorized();
            }
            
            if (User.Identity.Name == null)
            {
                Log.Information("Email is Null when attempting to delete Reference ID: {ReferenceID}", refId);
                return Unauthorized();
            }

            var r = await _referenceService.DeleteReferenceAsync(User.Identity.Name, refId);
            
            if (!r)
            {
                return NotFound("Reference with ID: " + refId + " not found.");
            }
            
            
            return Ok("Reference with the ID: " + refId + " has been deleted");
        }
        catch (Exception e)
        {
            Log.Warning("Unable to Delete Reference with ID: {ReferenceID}", refId);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Deletes a List of References
    /// </summary>
    /// <param name="refIdList">A List of Integer's depicting Reference ID's</param>
    /// <returns></returns>
    // DELETE: Delete list of references
    [HttpDelete("delete-multiple-references")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> DeleteListOfReferences(List<int> refIdList)
    {
        try
        {
            if (refIdList.Count < 1)
            {
                Log.Warning("Attempted to Delete from a List that has no References");
                return Problem("ERROR: Ref ID List has < 1 ids");
            }

            if (User.Identity == null)
            {
                Log.Information("Unauthorized Attempt to delete Reference ID: {ReferenceID}", refIdList);
                return Unauthorized();
            }
            
            if (User.Identity.Name == null)
            {
                Log.Information("Email is Null when attempting to delete Reference ID: {ReferenceID}", refIdList);
                return Unauthorized();
            }

            await _referenceService.DeleteReferenceRangeAsync(User.Identity.Name, refIdList);
            
            
            return Ok(true);
            
        }
        catch (Exception e)
        {
            Log.Warning("Attempt to Delete list of References failed. ReferenceIDs: {ReferenceIDList}", refIdList);
            return BadRequest();
        }
    }
}