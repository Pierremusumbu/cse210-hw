using System;

using System.Collections.Generic;
using System.Threading;

abstract class Activity
{
    protected string Name;
    protected string Description;
    protected int Duration;

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Starting: {Name}");
        Console.WriteLine(Description);
        Console.Write("Enter duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        Spinner(3);
        RunActivity();
        End();
    }

    protected abstract void RunActivity();

    protected void End()
    {
        Console.WriteLine("\nWell done!");
        Spinner(2);
        Console.WriteLine($"You have completed the {Name} activity for {Duration} seconds.");
        Spinner(3);
    }

    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + "... ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    protected void Spinner(int duration)
    {
        string[] symbols = { "|", "/", "-", "\\" };
        int end = Environment.TickCount + duration * 1000;
        int i = 0;

        while (Environment.TickCount < end)
        {
            Console.Write(symbols[i++ % symbols.Length] + "\r");
            Thread.Sleep(200);
        }
        Console.Write(" \r");
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        Name = "Breathing";
        Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    protected override void RunActivity()
    {
        int elapsed = 0;
        while (elapsed < Duration)
        {
            Console.WriteLine("Breathe in...");
            Countdown(4);
            elapsed += 4;

            if (elapsed >= Duration) break;

            Console.WriteLine("Breathe out...");
            Countdown(6);
            elapsed += 6;
        }
    }
}

class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        Name = "Reflection";
        Description = "This activity will help you reflect on times in your life when you have shown strength and resilience.";
    }

    protected override void RunActivity()
    {
        Random rand = new Random();
        Console.WriteLine($"\nPrompt: {prompts[rand.Next(prompts.Count)]}");
        Console.WriteLine("Reflect on the following questions:");

        int elapsed = 0;
        while (elapsed < Duration)
        {
            string question = questions[rand.Next(questions.Count)];
            Console.WriteLine($"\n> {question}");
            Spinner(5);
            elapsed += 5;
        }
    }
}

class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        Name = "Listing";
        Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    protected override void RunActivity()
    {
        Random rand = new Random();
        Console.WriteLine($"\nPrompt: {prompts[rand.Next(prompts.Count)]}");
        Console.WriteLine("You have a few seconds to think...");
        Countdown(5);

        Console.WriteLine("Start listing. Press Enter after each item:");
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        int count = 0;

        while (DateTime.Now < endTime)
        {
            if (!string.IsNullOrEmpty(Console.ReadLine()))
                count++;
        }

        Console.WriteLine($"\nYou listed {count} items!");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            Activity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => null
            };

            if (choice == "4")
            {
                Console.WriteLine("Goodbye!");
                break;
            }

            if (activity != null)
                activity.Start();
            else
            {
                Console.WriteLine("Invalid option. Press Enter to try again.");
                Console.ReadLine();
            }
        }
    }
}