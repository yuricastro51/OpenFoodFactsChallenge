using HtmlAgilityPack;
using OpenFoodFactsChallenge.Domain.Entities;

namespace OpenFoodFactsChallenge.Domain.Services;

public class WebScrapingService : IWebScrapingService
{
    public List<Product> Scrap()
    {
        var web = new HtmlWeb();
        var doc = web.Load("https://world.openfoodfacts.org/");
        var productsHtml = doc.DocumentNode.SelectNodes("//ul[@class='products']/li/a[@title!='']");

        var products = new List<Product>();

        foreach (var htmlNode in productsHtml)
        {
            var url = htmlNode.GetAttributeValue("href", null);

            doc = web.Load($"https://world.openfoodfacts.org{url}");

            var codeNode = doc.DocumentNode.SelectNodes("//p[@id='barcode_paragraph']/span[@id='barcode']");
            var barcodeNode = doc.DocumentNode.SelectNodes("//p[@id='barcode_paragraph']");
            var quantityNode = doc.DocumentNode.SelectNodes("//p[@id='field_quantity']/span[@class='field_value']");
            var brandsNode = doc.DocumentNode.SelectNodes("//p[@id='field_brands']/span[@class='field_value']");
            var packagingNode = doc.DocumentNode.SelectNodes("//p[@id='field_packaging']/span[@class='field_value']");
            var categoriesNode = doc.DocumentNode.SelectNodes("//p[@id='field_categories']/span[@class='field_value']");
            var imgUrlNode = doc.DocumentNode.SelectNodes("//img[@itemprop='contentUrl']");
            var nameNode = doc.DocumentNode.SelectNodes("//h1[@property='food:name']");

            if (codeNode is null)
            {
                continue;
            }

            var categories = categoriesNode?[0].InnerText.Trim();
            var brands = brandsNode?[0].InnerText.Trim();
            var packaging = packagingNode?[0].InnerText.Trim();
            var barcode = barcodeNode?[0].InnerText.Trim().Substring(10);
            var productName = nameNode[0].InnerText.Split("-")[0].Trim();
            var imgUrl = imgUrlNode?[0].GetAttributeValue("src", null);
            var code = long.Parse(codeNode[0].InnerText);
            var quantity = quantityNode?[0].InnerText.Trim();

            products.Add(new Product(
                code, 
                barcode,
                url,
                productName,
                quantity ?? string.Empty,
                categories ?? string.Empty, 
                packaging ?? string.Empty,
                brands ?? string.Empty,
                imgUrl ?? string.Empty)
            );

        }

        return products;
    }
}