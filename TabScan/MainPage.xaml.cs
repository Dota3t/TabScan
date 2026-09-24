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
            //Records = [new Record(new Student(0, "Filip", "Tarza", "5TP", 30), "HUH", new DateTime(2026, 9, 24, 12, 55, 0), new DateTime(2026, 9, 24, 13, 55, 0)),
            //            new Record(new Student(1, "Filip", "Tarzan", "3TUE", 11), "huh", new DateTime(2026, 9, 24, 12, 55, 0), new DateTime(2026, 9, 24, 14, 45, 0))];
            Records = [];
            Students = new Dictionary<int, Student>();
            CVRecords.ItemsSource = Records;

            SetTimer();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var studs = await database.SelectAllStudents();
            foreach(var stud in studs)
            {
                Students.Add(stud.Id, stud);
            }
            var recs = await database.SelectAllRecords();
            foreach(var rec in recs)
            {
                Records.Add(rec);
            }

#if IOS
        UIKit.UIView.AnimationsEnabled = false;
#endif
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
            //List<Record> All = new List<Record>(Records);
            //Records.Clear();
            //foreach (var record in All)
            //{
            //    Records.Add(record);
            //}
            int n = Records.Count;
            for(int i = n - 1; i >= 0; --i)
            {
                Record rec = Records[i];
                Records.RemoveAt(i);
                Records.Insert(i, rec);
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
