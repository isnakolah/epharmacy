using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EPharmacy.Application.Common.Exceptions;

public class ValidateModelException : ModelStateDictionary
{

    public ValidateModelException()
    {
        Errors = new Dictionary<string, List<string>>();
    }


    public ValidateModelException(ModelStateDictionary modelState) : this()
    {
        foreach (var key in modelState.Keys)
        {
            var property = modelState.GetValueOrDefault(key);
            var errors = property.Errors.Select(error => error.ErrorMessage).ToList();

            Errors.Add(key, errors);
        }
    }

    public IDictionary<string, List<string>> Errors { get; }
}
