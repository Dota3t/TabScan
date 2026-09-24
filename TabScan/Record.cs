namespace TabScan;

public class Record
{
    private static int ID = 0;

    private int id;
    public int Id
    {
        get
        {
            return id;
        }
    }

    public Student? StudentID { get; set; }
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
    }

    public static DateTime GetNow()
    {
        return DateTime.Now;
    }

    public Record()
    {
        id = ID++;
        TabletId = string.Empty;
        StartTime = DateTime.Now;
        EndTime = DateTime.Now;
    }

    public Record(Student? studentID, string tabletId, DateTime startTime, DateTime endTime)
    {
        id = ID++;
        StudentID = studentID;
        TabletId = tabletId;
        if(endTime < startTime) throw new Exception("endTime must be greater than startTime");
        StartTime = startTime;
        EndTime = endTime;
    }
}