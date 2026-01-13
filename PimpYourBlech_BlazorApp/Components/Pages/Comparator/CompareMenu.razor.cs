using Microsoft.AspNetCore.Components;

namespace PimpYourBlech_BlazorApp.Components.Pages.Comparator;

public partial class CompareMenu : ComponentBase
{
    [Inject]
    private NavigationManager Nav { get; set; } = default!;

    private void GoToCarCompare()
        => Nav.NavigateTo("/compare/choose");

    private void GoToConfigurationCompare()
        => Nav.NavigateTo("/configurations/compare");
}