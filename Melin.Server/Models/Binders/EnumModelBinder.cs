namespace Melin.Server.Models.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

public class EnumModelBinder<T> : IModelBinder where T : Enum
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var value = bindingContext.ValueProvider.GetValue(modelName).FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        if (Enum.TryParse(typeof(T), value, true, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.AddModelError(modelName, $"Invalid enum value for {modelName}");
        }

        return Task.CompletedTask;
    }
}