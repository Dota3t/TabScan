using System.ComponentModel;
using System.Timers;
using SQLite;

namespace TabScan;

public class Record : INotifyPropertyChanged
{
    [PrimaryKey, AutoIncrement]
    public int Id
    {
        get;set;
    }

    public int StudentID { get; set; }
    [Ignore]
    public Student? StudentData { get; set; }
    public string TabletId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string TimeLeft
    {
        get
        {
            TimeSpan ts = EndTime - GetNow();
            string res = "";
            if (ts.TotalDays >= 1)
            {
                return $"{(int)ts.TotalDays}d {(int)ts.TotalHours % 24}h";
            }
            if(ts.TotalHours >= 1)
            {
                return $"{(int)ts.TotalHours}h {(int)ts.TotalMinutes % 60}min";
            }

            return $"{(int)ts.TotalMinutes}min {(int)ts.TotalSeconds % 60}s";
        }
        set
        {
            OnPropertyChanged();
            var _ = value;
        }
    }

    private System.Timers.Timer? timer;
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public static DateTime GetNow()
    {
        return DateTime.Now;
    }

    public Record()
    {
        TabletId = string.Empty;
        StartTime = DateTime.Now;
        EndTime = DateTime.Now;
        TimeLeftTimer();
    }

    public Record(int studentID, string tabletId, DateTime startTime, DateTime endTime)
    {
        StudentID = studentID;
        TabletId = tabletId;
        if(endTime < startTime) throw new Exception("endTime must be greater than startTime");
        StartTime = startTime;
        EndTime = endTime;
        TimeLeftTimer();
    }

    private void TimeLeftTimer()
    {
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += UpdateTimeLeft;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    private void UpdateTimeLeft(object? source, ElapsedEventArgs? e)
    {
        TimeLeft = TimeLeft == "" ? "1" : "";
    }
}