
using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_Contracts.Query;

public class ProductListQuery
{
    public int? ProductId { get; set; }
    public string? SearchTerm { get; set; }
    public string? Brand { get; set; }
    public int? CarId { get; set; }
    public ProductType? Type { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public ProductSort SortBy { get; set; } = ProductSort.NameAsc;
}