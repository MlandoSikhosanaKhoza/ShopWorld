using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ShopWorld.Api
{
    public static class ModelStateExtentions
    {
        public static string PrintError(this ModelStateDictionary modelState)
        {
            if (modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            foreach (var state in modelState)
            {
                if (state.Value.Errors.Count() > 0)
                {
                    string firstError = state.Value.Errors.First().ErrorMessage;
                    
                    return firstError;
                }
            }
            return "Invalid Data";
        }
    }
}
