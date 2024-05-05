using LifeTotalMaui.Models;

namespace LifeTotalMaui;

public partial class PlayerProfilePage : ContentPage
{
    private HttpClient _client = new HttpClient();

    public PlayerProfilePage(Player player)
    {
        InitializeComponent();
        BindingContext = player;
        _client.BaseAddress = new Uri(ApiSettings.Base.BaseUrl);
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirm", "Do you really want to delete this player?", "Yes", "No");
        if (confirm)
        {
            bool success = await DeletePlayerAsync(((Player)BindingContext).Id);
            if (success)
            {
                await DisplayAlert("Success", "Player deleted successfully", "OK");
                await Navigation.PopAsync();  // Go back to the previous page
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete the player", "OK");
            }
        }
    }

    private async Task<bool> DeletePlayerAsync(string playerId)
    {
        var url = $"Player/{playerId}";
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting player: {ex.Message}");
            return false;
        }
    }
}