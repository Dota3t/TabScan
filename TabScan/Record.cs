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