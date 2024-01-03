using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OpenFoodFactsChallenge.Controllers;

namespace OpenFoodFactsChallenge.Tests.Controllers;

public class HealthCheckControllerTest
{
    [Fact]
    public void HealthCheck_ReturnsOk()
    {
        var controller = new HealthCheckController();

        var result = (OkObjectResult)controller.HealthCheck();

        result.StatusCode.Should().Be(200);
        result.Value.Should().Be("Fullstack Challenge 20201026");
    }
}