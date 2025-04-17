using System;
using System.Collections.Generic;

public abstract class Activity
{
    public DateTime Date { get; set; }
    public int DurationMinutes { get; set; }

    public Activity(DateTime date, int durationMinutes)
    {
        Date = date;
        DurationMinutes = durationMinutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();
    
    public string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} {this.GetType().Name} ({DurationMinutes} min): Distance {GetDistance()} km, Speed: {GetSpeed()} kph, Pace: {GetPace()} min per km";
    }
}

public class Running : Activity
{
    public double Distance { get; set; } // in km

    public Running(DateTime date, int durationMinutes, double distance) : base(date, durationMinutes)
    {
        Distance = distance;
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return (Distance / DurationMinutes) * 60;
    }

    public override double GetPace()
    {
        return DurationMinutes / Distance;
    }
}

public class Cycling : Activity
{
    public double Speed { get; set; } // in kph

    public Cycling(DateTime date, int durationMinutes, double speed) : base(date, durationMinutes)
    {
        Speed = speed;
    }

    public override double GetDistance()
    {
        return (Speed * DurationMinutes) / 60;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }
}

public class Swimming : Activity
{
    public int Laps { get; set; }

    public Swimming(DateTime date, int durationMinutes, int laps) : base(date, durationMinutes)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        return Laps * 50 / 1000.0; // 50 meters per lap, convert to kilometers
    }

    public override double GetSpeed()
    {
        return (GetDistance() / DurationMinutes) * 60;
    }

    public override double GetPace()
    {
        return DurationMinutes / GetDistance();
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 45, 20.0),
            new Swimming(new DateTime(2022, 11, 5), 60, 40)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
