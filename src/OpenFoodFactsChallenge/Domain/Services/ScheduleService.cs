using OpenFoodFactsChallenge.Domain.Repositories;
using Quartz;

namespace OpenFoodFactsChallenge.Domain.Services;

public class ScheduleService : IJob
{
    private readonly IInsertScrapedProductService _insertScrapedProductService;

    public ScheduleService(IInsertScrapedProductService insertScrapedProductService)
    {
        _insertScrapedProductService = insertScrapedProductService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _insertScrapedProductService.Insert(default);
    }
}