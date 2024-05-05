using LifeTotalMaui.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace LifeTotalMaui;

public partial class LeaderboardPage : ContentPage
{
    private HttpClient _client = new HttpClient();

    public LeaderboardPage()
	{
		InitializeComponent();
        Title = "Leaderboard";
        LoadTopPlayersAsync();
    }

    private async void LoadTopPlayersAsync()
    {
        try
        {
            var uri = new Uri("http://127.0.0.1:5256/Player/Top20");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var players = JsonConvert.DeserializeObject<List<Player>>(jsonResponse);
                if (players != null)
                {
                    leaderboardListView.ItemsSource = players;
                }
            }
            else
            {
                Console.WriteLine($"Failed to fetch data: {response.StatusCode}");
                // Handle other status codes and error scenarios
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            // Handle exceptions or display an error message
        }
    }
}