using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Melin.Server.Models.Binders;

public class CreatorEntityBinder : IModelBinder
{
    public System.Threading.Tasks.Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelName = bindingContext.ModelName;

        // Try to bind the `Creator` properties except `Reference`
        var creator = new Creator();

        // Manually set properties if they exist in the request
        var creatorIdResult = bindingContext.ValueProvider.GetValue($"{modelName}.CreatorId");
        if (creatorIdResult != ValueProviderResult.None && int.TryParse(creatorIdResult.FirstValue, out var creatorId))
        {
            creator.Id = creatorId;
            bindingContext.ModelState.SetModelValue($"{modelName}.CreatorId", creatorIdResult);
        }

        // Additional properties binding as needed...

        // Ignore `Reference` - Don't attempt to set it in the binder
        // Optionally, you could set it to null to avoid binding
        creator.Reference = null;

        bindingContext.Result = ModelBindingResult.Success(creator);
        return System.Threading.Tasks.Task.CompletedTask;
    }
}