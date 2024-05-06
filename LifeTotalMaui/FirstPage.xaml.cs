using LifeTotalMaui.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text;

namespace LifeTotalMaui;

public partial class FirstPage : ContentPage
{
    private HttpClient _client = new HttpClient();
    public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();
    private List<string> selectedPlayerIds = new List<string>();
    public FirstPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    //reload playlist
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPlayersAsync();  
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection;
        if (currentSelection != null)
        {
            var selectedPlayerIds = currentSelection.OfType<Player>().Select(p => p.Id).ToList();
        }
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

    private void OnPlayerSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var player = e.SelectedItem as Player;
        if (player != null && !selectedPlayerIds.Contains(player.Id))
        {
            if (selectedPlayerIds.Count < 2)
            {
                selectedPlayerIds.Add(player.Id);
            }
            else
            {
                DisplayAlert("Selection Limit", "You can only select two players for a match.", "OK");
            }

        }
        ((ListView)sender).SelectedItem = null;
    }

    private async void OnCreateMatchClicked(object sender, EventArgs e)
    {
        var selectedPlayers = playersCollectionView.SelectedItems.Cast<Player>().ToList();
        if (selectedPlayers.Count == 2)
        {
            var builder = new UriBuilder("http://localhost:5256/Gamematch");
            builder.Query = $"playerid1={selectedPlayers[0].Id}&playerid2={selectedPlayers[1].Id}";

            try
            {
                var response = await _client.PostAsync(builder.Uri, null);  
                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Match Created", "The match has been successfully created.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", $"Failed to create match: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Selection Error", "Please select exactly two players.", "OK");
        }
    }
}