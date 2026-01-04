using Microsoft.AspNetCore.Components;

namespace PimpYourBlech_BlazorApp.Components.Pages;

public partial class MainMenu : ComponentBase
{
    private void GoToConfigurator() => Nav.NavigateTo("/configurator/chooseCar");

    private void GoToComparator() => Nav.NavigateTo("/compare/choose");
    
    private void GoToConfigurations() => Nav.NavigateTo("/configurations");

    private void GoToShop() => Nav.NavigateTo("/shop");
    
    private void GoToFAQ() => Nav.NavigateTo("/FAQ");
    
    private void GoAboutUs() => Nav.NavigateTo("/aboutUs");

    private void GoToUserSettings() => Nav.NavigateTo("/user");

    private void Logout()
    {
        UserSession.LogOut();
        Nav.NavigateTo("/");
    }

    private void GoToAdmin() => Nav.NavigateTo("/administrator");
}