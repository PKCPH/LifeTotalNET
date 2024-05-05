namespace LifeTotalMaui;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using LifeTotalMaui.Models;
using Microsoft.Maui.Controls;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly HttpClient _httpClient;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Make GET request to API endpoint
                HttpResponseMessage response = await httpClient.GetAsync("http://127.0.0.1:5256/Player");

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize JSON response to list of players using Newtonsoft.Json
                    string json = await response.Content.ReadAsStringAsync();
                    var players = JsonConvert.DeserializeObject<List<Player>>(json);

                    // Display players (for example, in a list view)
                    // You need to define your UI element for displaying players
                    // For example, if you have a ListView named playerListView:
                    playerListView.ItemsSource = players;
                }
                else
                {
                    // Handle error response
                    // For example, display an error message
                    await DisplayAlert("Error", "Failed to fetch players", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            // For example, display the exception message
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}

