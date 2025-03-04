using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace ApiService.ValidationResultFactory;

public class ValidationResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        var distinctErrors = validationProblemDetails!.Errors
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Distinct().ToArray());

        return new BadRequestObjectResult(new ValidationResult("Validation errors", distinctErrors));
    }
}