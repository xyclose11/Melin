using Melin.Server.Models.References;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Melin.Server.Models.Binders;

public class ReferenceModelBinder : IModelBinder
{
    public System.Threading.Tasks.Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue("type").FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        // Determine the correct type based on the "type" property
        Type type = value switch
        {
            "Book" => typeof(Book),
            "Artwork" => typeof(Artwork),
            _ => typeof(Reference)
        };

        var model = Activator.CreateInstance(type);
        bindingContext.Result = ModelBindingResult.Success(model);

        return System.Threading.Tasks.Task.CompletedTask;
    } 
}