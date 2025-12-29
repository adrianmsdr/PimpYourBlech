namespace PimpYourBlech_ClassLibrary.Services.Comparator.Models;

using PimpYourBlech_ClassLibrary.Entities;


public class ComparisonResult
{
    public List<Car> Cars { get; set; } = new();
    public List<ComparisonRow> Rows { get; set; } = new();
}