using LifeTotalMaui.Models;
using LifeTotalMaui.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeTotalMaui.ViewModels;

public class LeaderboardViewModel : INotifyPropertyChanged
{
    private IDataService _dataService;

    private ObservableCollection<Player> _players;
    public ObservableCollection<Player> Players
    {
        get => _players;
        set
        {
            _players = value;
            OnPropertyChanged();
        }
    }

    public LeaderboardViewModel(IDataService dataService)
    {
        _dataService = dataService;
        Players = new ObservableCollection<Player>();
        LoadTopPlayersAsync();
    }

    private async void LoadTopPlayersAsync()
    {
        var players = await _dataService.GetTopPlayersAsync();
        if (players != null)
        {
            Players.Clear();
            foreach (var player in players)
            {
                Players.Add(player);
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}