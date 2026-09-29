using SQLite;

namespace TabScan;

public class Student
{
    static int globalId = 0;
    [PrimaryKey]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Class { get; set; }
    public int Number { get; set; }
    
    public Student(string firstName, string lastName, string class_, int number)
    {
        Id = globalId++;
        FirstName = firstName;
        LastName = lastName;
        Class = class_;
        Number = number;
    }

    public Student()
    {
        Id = globalId++;
    }
}