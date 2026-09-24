using System.Collections.ObjectModel;

namespace TabScan;

public partial class WpisyPage : ContentPage
{

    bool IsMenuOpen = false;
    ObservableCollection<Date> dateCollection = new();

    Date dummyDate = new();

    static Student dummyStudent = new(1, "Filip", "Tarza", "5TP", 31);

    static DateTime dummyNowDate = DateTime.Now;

    Record dummyRecord = new(dummyStudent, "32170983721", dummyNowDate, dummyNowDate);


    public WpisyPage()
	{
		InitializeComponent();

        dummyDate.addToDate(dummyRecord);
        dateCollection.Add(dummyDate);

        wpisyView.ItemsSource = dateCollection;
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

    private async void OpenHome(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void OpenSkan(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SkanPage());
    }

}