using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Utils;

public static class ModelStateUtils
{
    public static Dictionary<string, string[]?>? GetModelStateErrors(ModelStateDictionary modelState)
    {
        return modelState
            .Where(ms => ms.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
    }
}
