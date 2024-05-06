using LifeTotalMaui.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;

namespace LifeTotalMaui;

public partial class GamematchPage : ContentPage
{
    private HttpClient _client = new HttpClient();
    public ObservableCollection<GetAllGamematch> Gamematches { get; set; } = new ObservableCollection<GetAllGamematch>();

    public GamematchPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadMatchesAsync();
    }

    private async void LoadMatchesAsync()
    {
        try
        {
            var response = await _client.GetAsync("http://127.0.0.1:5256/Gamematch");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var matches = JsonConvert.DeserializeObject<List<GetAllGamematch>>(jsonResponse);
                Gamematches.Clear();
                foreach (var match in matches)
                {
                    Gamematches.Add(match);
                }
            }
            else
            {
                Console.WriteLine("Failed to load matches");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in loading matches: {ex.Message}");
        }
    }

    private void OnExpandClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var match = button?.BindingContext as GetAllGamematch;

        if (match != null)
        {
            // Collapse all matches
            foreach (var gm in Gamematches)
            {
                if (gm != match)
                    gm.IsExpanded = false;
            }

            // Toggle the selected match
            match.ToggleExpansion();

            // Force the CollectionView to refresh
            matchesCollectionView.ItemsSource = null;
            matchesCollectionView.ItemsSource = Gamematches;
        }
    }

    private async void UpdateLifeTotal(object gamematch1, EventArgs e)
    {
        Gamematch gamematch = gamematch1 as Gamematch;
        try
        {
            var json = JsonConvert.SerializeObject(gamematch);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"http://127.0.0.1:5256/gamematch/{gamematch.Id}", content);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
        }
    }
}