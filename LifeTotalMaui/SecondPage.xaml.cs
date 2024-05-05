using LifeTotalMaui.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace LifeTotalMaui;

public partial class SecondPage : ContentPage
{
    private HttpClient _client = new HttpClient();
    public ObservableCollection<Player> Players { get; private set; } = new ObservableCollection<Player>();
    public ICommand PlayerSelectedCommand { get; private set; }

    public SecondPage()
    {
        InitializeComponent();
        PlayerSelectedCommand = new Command<Player>(OnPlayerSelected);
        BindingContext = this;
        LoadPlayersAsync();
    }

    private async void LoadPlayersAsync()
    {
        try
        {
            var uri = new Uri("http://127.0.0.1:5256/Player");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var players = JsonConvert.DeserializeObject<List<Player>>(jsonResponse);
                if (players != null)
                {
                    foreach (var player in players) { Players.Add(player); }
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

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue?.ToLower() ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            var filteredPlayers = Players.Where(p => p.Name.ToLower().Contains(searchText)).ToList();
            playersListView.ItemsSource = filteredPlayers;
        }
        else
        {
            playersListView.ItemsSource = Players;
        }
    }

    private async void OnCreatePlayerClicked(object sender, EventArgs e)
    {
        // Simulate creating a player
        // You would replace this with logic to actually create the player in your backend
        Players.Add(new Player { Name = playerNameEntry.Text, Elo = 1000 }); // Example data
        playerNameEntry.Text = string.Empty;
        await DisplayAlert("Success", "Player created successfully", "OK");
    }

    private void OnPlayerSelected(Player player)
    {
        if (player != null)
        {
            playerName.Text = player.Name;
            playerElo.Text = $"Elo: {player.Elo}";
            playerMatches.Text = $"Matches: {player.Gamematches}";
            playerDetails.IsVisible = true;  // Ensure the details section is visible
        }
    }
}