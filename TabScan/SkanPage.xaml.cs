namespace TabScan;

public partial class SkanPage : ContentPage
{

    bool IsMenuOpen = false;
    int minutes = 0;
    int hours = 0;

    public SkanPage()
	{
		InitializeComponent();
	}

    private void SideMenuOpen(object sender, EventArgs e)
    {
        if (IsMenuOpen)
        {
            Grid.SetColumnSpan(SideBar, 1);
            SideBarContent.IsVisible = false;
            IsMenuOpen = false;
        }
        else
        {
            Grid.SetColumnSpan(SideBar, 3);
            SideBarContent.IsVisible = true;
            IsMenuOpen = true;
        }
    }

    private void addToTime(object sender, EventArgs e)
    {
        changeTime(5);
    }

    private void substractFromTime(object sender, EventArgs e)
    {
        changeTime(-5);
    }

    private void changeTime(int change)
    {
        if(minutes > 0 && minutes < 60) { minutes += change; }
        else if(minutes == 0) { 
            if (hours > 0 && change < 0) { minutes = 55; hours -= 1; }
            else if (change > 0) { minutes += 5; }
        } else { minutes = 0; hours += 1; }

        lenghtDisplay.Text = hours.ToString("D2") + ":" + minutes.ToString("D2");

    }

    private async void OpenWpisy(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WpisyPage());
    }

    private async void OpenHome(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}