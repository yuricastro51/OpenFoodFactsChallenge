using Microsoft.AspNetCore.Mvc;

namespace OpenFoodFactsChallenge.Helpers.Http;

public class ServerError : ObjectResult
{
    public ServerError(string value) : base(value)
    {
        Value = new { error = value };
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}