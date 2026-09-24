namespace TabScan;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Class { get; set; }
    public int Number { get; set; }
    
    public Student(int id, string firstName, string lastName, string class_, int number)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Class = class_;
        Number = number;
    }
}