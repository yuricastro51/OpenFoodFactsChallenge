using OpenFoodFactsChallenge.Domain.Entities;

namespace OpenFoodFactsChallenge.Domain.Queries.Response;

public class GetProductByCodeResponse
{
    public long Code { get; set; }
    public string Barcode { get; set; }
    public EStatus Status { get; set; }
    public DateTime ImportedT { get; set; }
    public string Url { get; set; }
    public string ProductName { get; set; }
    public string Quantity { get; set; }
    public string Categories { get; set; }
    public string Packaging { get; set; }
    public string Brands { get; set; }
    public string ImageUrl { get; set; }
}