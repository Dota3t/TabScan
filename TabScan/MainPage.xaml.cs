using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;

namespace TabScan
{
    public partial class MainPage : ContentPage
    {
        private static System.Timers.Timer timer;
        private RecordDatabase database;

        bool IsMenuOpen = false;
        private ObservableCollection<Record> Records;
        private Dictionary<int, Student> Students;

        public MainPage(RecordDatabase database_)
        {
            InitializeComponent();
            database = database_;
            Records = [];
            Students = new Dictionary<int, Student>();
            CVRecords.ItemsSource = Records;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            IsMenuOpen = false;
            Records.Clear();
            var recs = await database.SelectAllRecordsFilled((r) => r.EndTime > DateTime.Now);
            foreach (var rec in recs)
            {
                Records.Add(rec);
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
            else {
                Grid.SetColumnSpan(SideBar, 3);
                SideBarContent.IsVisible = true;
                IsMenuOpen = true;
            }
        }

        private async void OpenWpisy(object sender, EventArgs e)
        {
            SideMenuOpen(sender, e);
            await Navigation.PushAsync(new WpisyPage(database));
        }

        private async void OpenSkan(object sender, EventArgs e)
        {
            SideMenuOpen(sender, e);
            await Navigation.PushAsync(new SkanPage(database));
        }
    }
}
