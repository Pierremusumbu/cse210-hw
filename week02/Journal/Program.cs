using System;

public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

public class Journal
{
    public List<Entry> Entries { get; set; }

    public Journal()
    {
        Entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        Entries.Add(entry);
    }

    public void DisplayJournal()
    {
        if (Entries.Count == 0)
        {
            Console.WriteLine("No entries to display.");
            return;
        }

        foreach (var entry in Entries)
        {
            Console.WriteLine(entry);
            Console.WriteLine(new string('-', 40));
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            foreach (var entry in Entries)
            {
                sw.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
            }
        }
        Console.WriteLine($"Journal saved to {filename}");
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            Entries.Clear();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(new string[] { "~|~" }, StringSplitOptions.None);
                    if (parts.Length == 3)
                    {
                        var entry = new Entry(parts[1], parts[2], parts[0]);
                        Entries.Add(entry);
                    }
                }
            }
            Console.WriteLine($"Journal loaded from {filename}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
    }
}

public class JournalApp
{
    private Journal journal;
    private List<string> prompts;

    public JournalApp()
    {
        journal = new Journal();
        prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };
    }

    public void DisplayMenu()
    {
        Console.WriteLine("\nJournal Menu:");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save journal to a file");
        Console.WriteLine("4. Load journal from a file");
        Console.WriteLine("5. Exit");
    }

    public void WriteNewEntry()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine($"{prompt}");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Entry entry = new Entry(prompt, response, date);
        journal.AddEntry(entry);
        Console.WriteLine("Entry added!");
    }

    public void SaveJournal()
    {
        Console.Write("Enter the filename to save: ");
        string filename = Console.ReadLine();
        journal.SaveToFile(filename);
    }

    public void LoadJournal()
    {
        Console.Write("Enter the filename to load: ");
        string filename = Console.ReadLine();
        journal.LoadFromFile(filename);
    }

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        JournalApp app = new JournalApp();
        app.Run();
    }
}
