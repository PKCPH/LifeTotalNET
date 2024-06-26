using LifeTotalMaui.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace LifeTotalMaui;

public partial class SecondPage : ContentPage
{
    private HttpClient _client = new HttpClient();
    public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
    public ICommand PlayerSelectedCommand { get; set; }
    private List<Player> _selectedPlayers = new List<Player>();


    public SecondPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPlayersAsync(); 
    }


    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _selectedPlayers = e.CurrentSelection.Cast<Player>().ToList();
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
                    Players.Clear();
                    foreach (var player in players) { Players.Add(player); }
                }
            }
            else
            {
                Console.WriteLine($"Failed to fetch data: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
    }

    private async void OnCreatePlayerClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(playerNameEntry.Text))
        {
            await DisplayAlert("Input Error", "Player name cannot be empty.", "OK");
            return;
        }

        var player = new PlayerCreate { Name = playerNameEntry.Text };

        var json = JsonConvert.SerializeObject(player);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var contentString = await content.ReadAsStringAsync();  

        try
        {
            var response = await _client.PostAsync("http://127.0.0.1:5256/Player", content);
            if (response.IsSuccessStatusCode)
            {
                playerNameEntry.Text = string.Empty; 
                LoadPlayersAsync();
                await DisplayAlert("Success", "Player created successfully", "OK");
            }
            else
            {
                await DisplayAlert("Error", $"Failed to create player: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void OnDeletePlayerClicked(object sender, EventArgs e)
    {
        if (_selectedPlayers.Count == 0)
        {
            await DisplayAlert("Delete Player", "No player selected.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Delete Player", "Are you sure you want to delete the selected players?", "Yes", "No");
        if (confirm)
        {
            foreach (var player in _selectedPlayers)
            {
                Players.Remove(player); 
                                        
                await DeletePlayerAsync(player);
            }
            _selectedPlayers.Clear(); 
        }
    }

    private async Task DeletePlayerAsync(Player player)
    {
        var response = await _client.DeleteAsync($"http://127.0.0.1:5256/Player/{player.Id}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Error deleting player: " + response.StatusCode);
        }
    }

}