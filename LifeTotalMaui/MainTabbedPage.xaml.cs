using System.Security.Cryptography.X509Certificates;

namespace LifeTotalMaui;

public partial class MainTabbedPage : TabbedPage
{
	public MainTabbedPage()
	{
        InitializeComponent();

        Children.Add(new FirstPage { Title = "First", IconImageSource = "icon_first.png" });
        Children.Add(new SecondPage { Title = "Second", IconImageSource = "icon_second.png" });
        Children.Add(new LeaderboardPage { Title = "Leaderboard", IconImageSource = "icon_third.png" });

    }
}