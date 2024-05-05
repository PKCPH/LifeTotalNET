using LifeTotalMaui.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace LifeTotalMaui;

public partial class GamematchPage : ContentPage
{
    public ObservableCollection<Gamematch> GameMatches { get; set; } = new ObservableCollection<Gamematch>();

    public GamematchPage()
    {
        InitializeComponent();
        BindingContext = this;
        LoadGameMatchesAsync();
    }

    private async void LoadGameMatchesAsync()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "http://localhost:5256/Gamematch";
                var jsonResponse = await client.GetStringAsync(url);  // Fetch the JSON string from the API
                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    var matches = JsonConvert.DeserializeObject<List<Gamematch>>(jsonResponse);  // Deserialize the JSON string into a List of Gamematch
                    if (matches != null)
                    {
                        var sortedMatches = matches.OrderBy(m => m.DateTime).ToList();
                        foreach (var match in sortedMatches)
                        {
                            GameMatches.Add(match);  // Add to the observable collection
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load game matches: {ex.Message}");
            // Optionally add UI feedback here to inform the user of the failure
        }
    }
}