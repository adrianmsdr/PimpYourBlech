using PimpYourBlech_ClassLibrary.Enums;

namespace PimpYourBlech_ClassLibrary.DTO;

public class ProductListQuery
{
    public int? ProductId { get; set; }
    public string? NameContains { get; set; }
    public string? Brand { get; set; }
    public int? CarId { get; set; }
    public ProductType? Type { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public ProductSort SortBy { get; set; } = ProductSort.NameAsc;
}