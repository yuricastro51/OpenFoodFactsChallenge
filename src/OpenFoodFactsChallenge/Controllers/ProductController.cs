using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenFoodFactsChallenge.Domain.Entities;
using OpenFoodFactsChallenge.Domain.Queries.Requests;
using OpenFoodFactsChallenge.Domain.Queries.Response;
using OpenFoodFactsChallenge.Domain.Repositories;
using OpenFoodFactsChallenge.Domain.Services;
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
        [ProducesResponseType(typeof(GetProductListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts(
            CancellationToken cancellationToken,
            [FromQuery]int skip = 0, 
            [FromQuery]int take = 10)
        {
            try
            {
                if (skip < 0)
                {
                    return new BadRequest("Invalid param: skip");
                }
                if (take <= 0)
                {
                    return new BadRequest("Invalid param: take");
                }
                var command = new GetProductListRequest
                {
                    Skip = skip,
                    Take = take
                };
                var products = await _mediator.Send(command, cancellationToken);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return new ServerError("Internal server error", ex.Message);
            }
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(GetProductByCodeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByCode([FromRoute] long code, CancellationToken cancellationToken)
        {
            try
            {
                if (code <= 0)
                {
                    return new BadRequest("Invalid param: code");
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
            catch (Exception ex)
            {
                return new ServerError("Internal server error", ex.Message);
            }
        }

        [HttpGet("scrap-products-manually")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ScrapProduct([FromServices]IInsertScrapedProductService insertScrapedProductService, CancellationToken cancellationToken)
        {
            try
            {
                await insertScrapedProductService.Insert(cancellationToken);
                return Ok("Products inserted successfully");
            }
            catch (Exception ex)
            {
                return new ServerError("Internal server error: ", ex.Message);
            }
        }
    }
}
