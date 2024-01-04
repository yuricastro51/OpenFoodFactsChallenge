using Microsoft.AspNetCore.Mvc;

namespace OpenFoodFactsChallenge.Helpers.Http;

public class BadRequest : ObjectResult
{
    public BadRequest(object? value) : base(value)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        Value = new { error = value };
    }
}