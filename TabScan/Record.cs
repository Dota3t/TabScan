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

    public List<int> StudentIDs { get; set; }
    public string TabletId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Record()
    {
        id = ID++;
        StudentIDs = new List<int>();
        TabletId = string.Empty;
        StartTime = DateTime.Now;
        EndTime = DateTime.Now;
    }

    public Record(List<int> studentIDs, string tabletId, DateTime startTime, DateTime endTime)
    {
        id = ID++;
        StudentIDs = studentIDs;
        TabletId = tabletId;
        if(endTime < startTime) throw new Exception("endTime must be greater than startTime");
        StartTime = startTime;
        EndTime = endTime;
    }
}