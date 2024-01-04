using OpenFoodFactsChallenge.Domain.Handlers;

namespace OpenFoodFactsChallenge.Tests.Domain.Handlers;

public class GetProductByCodeHenderTest
{
    private readonly GetProductByCodeHandler _sut;

    public GetProductByCodeHenderTest()
    {
        _sut = new GetProductByCodeHandler();
    }
}