using LifeTotalMaui.Pages;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace LifeTotalMaui;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    void RegisterRoutes()
    {
        Routing.RegisterRoute("MainPage", typeof(MainPage));
        Routing.RegisterRoute("Players", typeof(Pages.Players));
        // Register routes for other pages as needed
    }

    async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    async void OnPlayersButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Players");
    }
}
