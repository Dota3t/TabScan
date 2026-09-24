using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;

namespace TabScan
{
    public partial class MainPage : ContentPage
    {
        private static System.Timers.Timer timer;

        bool IsMenuOpen = false;
        private ObservableCollection<Record> Records;

        public MainPage()
        {
            InitializeComponent();
            Records = [new Record(new Student(0, "Filip", "Tarza", "5TP", 30), "HUH", new DateTime(2026, 9, 24, 12, 55, 0), new DateTime(2026, 9, 24, 13, 55, 0)),
                        new Record(new Student(1, "Filip", "Tarzan", "3TUE", 11), "huh", new DateTime(2026, 9, 24, 12, 55, 0), new DateTime(2026, 9, 24, 14, 45, 0))];
            CVRecords.ItemsSource = Records;

            SetTimer();
        }

        private void SetTimer()
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += ForceRerender;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void ForceRerender(Object? source, ElapsedEventArgs? e)
        {
            List<Record> All = new List<Record>(Records);
            Records.Clear();
            foreach (var record in All)
            {
                Records.Add(record);
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
            await Navigation.PushAsync(new WpisyPage());
        }

        private async void OpenSkan(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SkanPage());
        }
    }
}
