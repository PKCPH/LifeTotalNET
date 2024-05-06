using System.Security.Cryptography.X509Certificates;

namespace LifeTotalMaui;

public partial class MainTabbedPage : TabbedPage
{
	public MainTabbedPage()
	{
        InitializeComponent();

        Children.Add(new FirstPage { Title = "Create Match", IconImageSource = "icon_first.png" });
        Children.Add(new SecondPage { Title = "Players", IconImageSource = "icon_second.png" });
        Children.Add(new LeaderboardPage { Title = "Leaderboard", IconImageSource = "icon_third.png" });
        Children.Add(new GamematchPage{ Title = "Schedule", IconImageSource = "icon_fourth.png" });

    }
}