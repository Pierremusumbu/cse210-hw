using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public string Name => _name;
    public string Description => _description;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStringRepresentation();
    public abstract string GetStatus();
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    }

    public override string GetStatus()
    {
        return _isComplete ? "[X]" : "[ ]";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return _points;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_name}|{_description}|{_points}";
    }

    public override string GetStatus()
    {
        return "[∞]";
    }
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _currentCount = 0;
    }

    public override int RecordEvent()
    {
        _currentCount++;
        if (_currentCount == _targetCount)
        {
            return _points + _bonusPoints;
        }
        return _points;
    }

    public override bool IsComplete()
    {
        return _currentCount >= _targetCount;
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_targetCount}|{_bonusPoints}|{_currentCount}";
    }

    public override string GetStatus()
    {
        return $"[{_currentCount}/{_targetCount}]";
    }
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private int _level = 1;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            int pointsEarned = _goals[goalIndex].RecordEvent();
            _score += pointsEarned;
            Console.WriteLine($"You earned {pointsEarned} points!");
            LevelUp();
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
    }

    private void LevelUp()
    {
        int newLevel = (_score / 500) + 1;
        if (newLevel > _level)
        {
            _level = newLevel;
            Console.WriteLine($"Congratulations! You've leveled up to Level {_level}!");
        }
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Goal g = _goals[i];
            Console.WriteLine($"{i + 1}. {g.GetStatus()} {g.Name} — {g.Description}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Current Score: {_score} points | Level: {_level}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            _score = int.Parse(lines[0]);
            _goals.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                string type = parts[0];
                if (type == "SimpleGoal")
                {
                    var goal = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                    if (bool.Parse(parts[4]))
                    {
                        goal.RecordEvent(); // Mark complete if needed
                    }
                    _goals.Add(goal);
                }
                else if (type == "EternalGoal")
                {
                    var goal = new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
                    _goals.Add(goal);
                }
                else if (type == "ChecklistGoal")
                {
                    var goal = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                    for (int j = 0; j < int.Parse(parts[6]); j++)
                    {
                        goal.RecordEvent();
                    }
                    _goals.Add(goal);
                }
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        bool quit = false;

        while (!quit)
        {
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Display Score");
            Console.WriteLine("7. Quit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal(manager);
                    break;
                case "2":
                    manager.DisplayGoals();
                    break;
                case "3":
                    Console.Write("Enter filename to save: ");
                    string saveFile = Console.ReadLine();
                    manager.SaveGoals(saveFile);
                    break;
                case "4":
                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    manager.LoadGoals(loadFile);
                    break;
                case "5":
                    manager.DisplayGoals();
                    Console.Write("Which goal did you accomplish? (Enter number): ");
                    int goalIndex = int.Parse(Console.ReadLine()) - 1;
                    manager.RecordEvent(goalIndex);
                    break;
                case "6":
                    manager.DisplayScore();
                    break;
                case "7":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void CreateGoal(GoalManager manager)
    {
        Console.WriteLine("Select Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choice: ");
        string type = Console.ReadLine();

        Console.Write("Goal Name: ");
        string name = Console.ReadLine();
        Console.Write("Goal Description: ");
        string description = Console.ReadLine();
        Console.Write("Points per completion: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
        {
            manager.AddGoal(new SimpleGoal(name, description, points));
        }
        else if (type == "2")
        {
            manager.AddGoal(new EternalGoal(name, description, points));
        }
        else if (type == "3")
        {
            Console.Write("How many times to complete: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Bonus points on completion: ");
            int bonus = int.Parse(Console.ReadLine());
            manager.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }
    }
}
