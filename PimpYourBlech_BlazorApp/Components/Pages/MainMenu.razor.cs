using Microsoft.AspNetCore.Components;
using PimpYourBlech_ClassLibrary.Entities;
using PimpYourBlech_ClassLibrary.Services.Admin;
using PimpYourBlech_ClassLibrary.Session.State;

namespace PimpYourBlech_BlazorApp.Components.Pages;

public partial class MainMenu : ComponentBase
{
    private void GoToConfigurator() => Nav.NavigateTo("/configurator/chooseCar");

    private void GoToComparator() => Nav.NavigateTo("/compare");
    
    private void GoToConfigurations() => Nav.NavigateTo("/configurations");

    private void GoToShop() => Nav.NavigateTo("/shop");
    
    private void GoToFAQ() => Nav.NavigateTo("/FAQ");
    
    private void GoAboutUs() => Nav.NavigateTo("/aboutUs");

    private void GoToUserSettings() => Nav.NavigateTo("/user");

    private void Logout()
    {
        UserSession.LogOut();
        LoginState.CurrentUserId = 0;
        Nav.NavigateTo("/");
    }

    private void GoToAdmin() => Nav.NavigateTo("/administrator");
    
    private Customer? c;
    private bool _loading;

    protected override async Task OnInitializedAsync(){

        _loading = true;
        try
        {

            c = await AdminService.GetCustomerByIdAsync(UserSession.CurrentUserId);
        }
        finally
        {
            _loading = false;
        }
    }
}