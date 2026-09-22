namespace TabScan
{
    public partial class MainPage : ContentPage
    {

        bool IsMenuOpen = false;

        public MainPage()
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
