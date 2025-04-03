using System;

public class Assignment
{
    private string _studentName;
    private string _topic;

    // Constructor
    public Assignment(string studentName, string topic)
    {
        _studentName = studentName;
        _topic = topic;
    }

    // GetSummary method
    public string GetSummary()
    {
        return $"{_studentName} - {_topic}";
    }
}

public class MathAssignment : Assignment
{
    private string _homeworkList;

    // Constructor that calls the base class constructor
    public MathAssignment(string studentName, string topic, string homeworkList)
        : base(studentName, topic)
    {
        _homeworkList = homeworkList;
    }

    // GetHomeworkList method
    public string GetHomeworkList()
    {
        return _homeworkList;
    }
}

public class WritingAssignment : Assignment
{
    private string _writingInformation;

    // Constructor that calls the base class constructor
    public WritingAssignment(string studentName, string topic, string writingInformation)
        : base(studentName, topic)
    {
        _writingInformation = writingInformation;
    }

    // GetWritingInformation method
    public string GetWritingInformation()
    {
        return $"{_writingInformation} by {GetStudentName()}";
    }

    // Getter for student name
    public string GetStudentName()
    {
        return base.GetSummary().Split(" - ")[0];
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Test MathAssignment
        MathAssignment mathAssignment = new MathAssignment("Roberto Rodriguez", "Fractions", "Section 7.3 Problems 8-19");
        Console.WriteLine(mathAssignment.GetSummary());
        Console.WriteLine(mathAssignment.GetHomeworkList());

        // Test WritingAssignment
        WritingAssignment writingAssignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(writingAssignment.GetSummary());
        Console.WriteLine(writingAssignment.GetWritingInformation());
    }
}