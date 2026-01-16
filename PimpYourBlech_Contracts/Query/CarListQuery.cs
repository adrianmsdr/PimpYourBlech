using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_Contracts.Query;

public class CarListQuery
{
    public string? NameContains { get; set; }
    public string? Brand { get; set; }
    public int? CarId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public CarSort SortBy { get; set; } = CarSort.NameAsc;
}