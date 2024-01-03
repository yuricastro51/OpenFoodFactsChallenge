namespace OpenFoodFactsChallenge.Entities;

public class Product(long code, 
    string barcode, 
    string url, 
    string productName, 
    string quantity, 
    string categories, 
    string packaging, 
    string brands, 
    string imageUrl)
{
    public long Code { get; private set; } = code;
    public string Barcode { get; private set; } = barcode;
    public EStatus Status { get; private set; } = EStatus.Draft;
    public DateTime ImportedT { get; private set; }
    public string Url { get; private set; } = url;
    public string ProductName { get; private set; } = productName;
    public string Quantity { get; private set; } = quantity;
    public string Categories { get; private set; } = categories;
    public string Packaging { get; private set; } = packaging;
    public string Brands { get; private set; } = brands;
    public string ImageUrl { get; private set; } = imageUrl;

    public void SetImported()
    {
        ImportedT = DateTime.Now;
        Status = EStatus.Imported;
    }
}

public enum EStatus
{
    Draft,
    Imported
}