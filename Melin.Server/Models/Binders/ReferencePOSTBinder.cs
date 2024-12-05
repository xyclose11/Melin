// using Melin.Server.Models.Binders;
// using Microsoft.AspNetCore.Mvc.ModelBinding;
// using Microsoft.Extensions.DependencyInjection;
//
// public class ReferencePOSTBinder : IModelBinderProvider
// {
//     public IModelBinder GetBinder(ModelBinderProviderContext bindingContext)
//     {
//         var modelType = bindingContext.ModelType;
//
//         // Check if the model is an enum type
//         if (modelType.IsEnum)
//         {
//             var binderType = typeof(EnumModelBinder<>).MakeGenericType(modelType);
//             return (IModelBinder)Activator.CreateInstance(binderType);
//         }
//
//         return null;
//     }
//     
//     
// }