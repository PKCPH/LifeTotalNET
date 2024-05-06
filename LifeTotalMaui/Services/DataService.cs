using LifeTotalMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTotalMaui.Services;
public interface IDataService
{
    Task<List<Player>> GetTopPlayersAsync();
}

public class DataService : IDataService
{
    private HttpClient _client;

    public DataService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Player>> GetTopPlayersAsync()
    {
        try
        {
            var response = await _client.GetAsync("http://127.0.0.1:5256/Player/Top20");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Player>>(jsonResponse);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in getting top players: {ex.Message}");
        }
        return new List<Player>();
    }
}