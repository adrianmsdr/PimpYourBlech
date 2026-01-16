using PimpYourBlech_Contracts.Enums;

namespace PimpYourBlech_Contracts.Query;

public class CustomerListQuery
{
    public string? SearchTerm { get; set; }
    public int? CustomerId { get; set; }
    public string? PhoneContains { get; set; }

    public CustomerSort SortBy { get; set; } = CustomerSort.NameAsc;
}