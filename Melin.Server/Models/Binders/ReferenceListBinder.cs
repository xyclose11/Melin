using Melin.Server.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Melin.Server.Models.Binders;
public class RawReferencePayload
{
    public List<ReferenceController.RawReference> RawReferences { get; set; }
}
/// <summary>
/// A custom Reference Binder for specific use in situations where a List of References are being
/// deserialized, since the custom JSON Converter only handles a single Reference.
/// </summary>
public class ReferenceListBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var body = bindingContext.HttpContext.Request.Body;

        // Read the request body into a string
        using var reader = new StreamReader(body);
        var json = await reader.ReadToEndAsync();
            
        try
        {
            var payload = JsonConvert.DeserializeObject<RawReferencePayload>(json);

            var references =
                (from rawReference in payload?.RawReferences ?? []
                    where !string.IsNullOrEmpty(rawReference.Value)
                    select JsonConvert.DeserializeObject<Reference>(rawReference.Value, new ReferenceConverter()))
                .ToList();
            
            bindingContext.Result = ModelBindingResult.Success(references);
        }
        catch (Exception ex)
        {
            Log.Error("Exception Caught when Binding Import Reference Lists");
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Deserialization failed: " + ex.Message);
        }
    }
}