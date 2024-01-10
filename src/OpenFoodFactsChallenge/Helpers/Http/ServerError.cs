using Microsoft.AspNetCore.Mvc;

namespace OpenFoodFactsChallenge.Helpers.Http;

public class ServerError : ObjectResult
{
    public ServerError(string value, string message) : base(value)
    {
        Value = new { error = value, message };
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}