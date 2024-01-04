using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Queries.Response;
using OpenFoodFactsChallenge.Helpers.Http;

namespace OpenFoodFactsChallenge.Controllers
{
    [ApiController]
    [Route("products")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetProductByCodeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByCode([FromQuery] long code, CancellationToken cancellationToken)
        {
            try
            {
                if (code <= 0)
                {
                    return new BadRequest("Invalid code");
                }
                var command = new GetProductByCodeRequest
                {
                    Code = code
                };
                var result = await _mediator.Send<GetProductByCodeResponse?>(command, cancellationToken);

                if (result is null)
                {
                    return new NotFound("Product not found");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return new ServerError("Internal server error");
            }
        }
    }
}
