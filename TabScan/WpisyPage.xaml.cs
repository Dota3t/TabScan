using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TabScan;

public partial class WpisyPage : ContentPage
{
    private RecordDatabase database;
    private bool IsMenuOpen = false;
    private ObservableCollection<Date> dateCollection = new();
    private string searchText = "";

    public WpisyPage(RecordDatabase database_)
    {
        InitializeComponent();

        wpisyView.ItemsSource = dateCollection;
        database = database_;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        IsMenuOpen = false;
        dateCollection.Clear();
        var dates = await database.SelectAllDates();
        foreach (var date in dates)
        {
            dateCollection.Add(date);
        }
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
        SideMenuOpen(sender, e);
        await Navigation.PopToRootAsync();
    }

    private async void OpenSkan(object sender, EventArgs e)
    {
        SideMenuOpen(sender, e);
        await Navigation.PushAsync(new SkanPage(database));
    }

    private async void SearchBar_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        searchText = searchBar.Text;
        int count = dateCollection.Count;
        List<Date> newDates = await database.SelectAllDates((rec) => (rec.TabletId + rec.StudentData?.FirstName + rec.StudentData?.LastName).Contains(searchText.Replace(" ", "")));
        foreach (Date date in newDates)
        {
            dateCollection.Add(date);
        }

        for (int i = 0; i < count; i++)
        {
            dateCollection.RemoveAt(0);
        }
    }
}