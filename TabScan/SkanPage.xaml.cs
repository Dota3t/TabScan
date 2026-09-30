using System.Collections.ObjectModel;
using System.Diagnostics;
using ZXing.Net.Maui;

namespace TabScan;

public partial class SkanPage : ContentPage
{
    private ObservableCollection<string> studentList = new();
    private RecordDatabase database;
    bool IsMenuOpen = false;
    int minutes = 45;
    int hours = 0;
    string ScanValue = "4F3F2D";
    string selectedStudent = "";

    public SkanPage(RecordDatabase database_)
	{
		InitializeComponent();
        database = database_;
        picker.ItemsSource = studentList;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        IsMenuOpen = false;
        studentList.Clear();
        var students = await database.SelectAllStudents();
        foreach (Student student in students)
        {
            studentList.Add(student.FirstName + " " + student.LastName);
        }
/*
        cameraView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional | BarcodeFormats.TwoDimensional,
            AutoRotate = true,
            Multiple = true,
            TryHarder = true
        };*/
    }

    private async void CameraView_BarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var result = e?.Results?.FirstOrDefault();
        if (result is null)
            return;


        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            ScanValue = result.Value;
            serialNumber.Text = "Skan: " + ScanValue;
        });
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
        if (minutes > 0 && minutes < 60)
        {
            minutes += change;
            if (minutes == 0 && hours == 0)
            {
                minutes = 5;
            }
        }
        else if(minutes == 0) { 
            if (hours > 0 && change < 0) { minutes = 55; hours -= 1; }
            else if (change > 0) { minutes += 5; }
        } else { minutes = 0; hours += 1; }

        lenghtDisplay.Text = hours.ToString("D2") + ":" + minutes.ToString("D2");

    }

    private async void OpenWpisy(object sender, EventArgs e)
    {
        SideMenuOpen(sender, e);
        await Navigation.PushAsync(new WpisyPage(database));
    }

    private async void OpenHome(object sender, EventArgs e)
    {
        SideMenuOpen(sender, e);
        await Navigation.PopToRootAsync();
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            selectedStudent = (string)picker.SelectedItem;
        }
    }

    private async void DodajWpis(object sender, EventArgs e)
    {
        if((minutes > 0 || hours > 0) && selectedStudent != "")
        {
            Student selected = (await database.SelectAllStudents((s) => s.FirstName + " " + s.LastName == selectedStudent))[0];
            if (selected is not null)
            {
                Record newRecord = new Record(selected.Id, ScanValue, DateTime.Now, DateTime.Now.AddHours(hours).AddMinutes(minutes));
                await database.InsertRecord(newRecord);
                await Navigation.PopToRootAsync();
            }
        }
    }
}