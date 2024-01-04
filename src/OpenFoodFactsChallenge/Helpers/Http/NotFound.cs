using Microsoft.AspNetCore.Mvc;

namespace OpenFoodFactsChallenge.Helpers.Http;

public class NotFound : ObjectResult
{
    public NotFound(object? value) : base(value)
    {
        StatusCode = StatusCodes.Status404NotFound;
        Value = new { error = value };
    }
}