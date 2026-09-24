using System.Collections.ObjectModel;

namespace TabScan
{
    public partial class MainPage : ContentPage
    {

        bool IsMenuOpen = false;
        public ObservableCollection<Record> Records;

        public MainPage()
        {
            InitializeComponent();
            Records = [new Record([0], "HUH", DateTime.Now, DateTime.Now + TimeSpan.FromHours(1)),
                        new Record([0], "huh", DateTime.Now, DateTime.Now)];
            CVRecords.ItemsSource = Records;
        }

        private void SideMenuOpen(object sender, EventArgs e)
        {
            if (IsMenuOpen)
            {
                Grid.SetColumnSpan(SideBar, 1);
                SideBarContent.IsVisible = false;
                IsMenuOpen = false;
            }
            else {
                Grid.SetColumnSpan(SideBar, 3);
                SideBarContent.IsVisible = true;
                IsMenuOpen = true;
            }
        }

        private async void OpenWpisy(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WpisyPage());
        }

        private async void OpenSkan(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SkanPage());
        }
    }
}
