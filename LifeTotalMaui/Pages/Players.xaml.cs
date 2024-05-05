using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using LifeTotalMaui.Models;

namespace LifeTotalMaui.Pages;

public partial class Players : ContentPage
{
    private readonly HttpClient _httpClient;

    public Players()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void OnCreatePlayerClicked(object sender, EventArgs e)
    {
        try
        {
            string playerName = playerNameEntry.Text;

            // Create a new player object with the entered name
            var newPlayer = new Player
            {
                Name = playerName
            };

            // Serialize the player object to JSON
            string json = JsonConvert.SerializeObject(newPlayer);

            // Make POST request to create a new player
            HttpResponseMessage response = await _httpClient.PostAsync("http://127.0.0.1:5256/Player", new StringContent(json, Encoding.UTF8, "application/json"));

            // Check if request was successful
            if (response.IsSuccessStatusCode)
            {
                // Player created successfully
                await DisplayAlert("Success", "Player created successfully", "OK");
            }
            else
            {
                // Handle error response
                await DisplayAlert("Error", "Failed to create player", "OK");
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}