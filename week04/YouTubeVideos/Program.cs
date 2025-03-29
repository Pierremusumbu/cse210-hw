using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // Length in seconds
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(string name, string text)
    {
        Comments.Add(new Comment(name, text));
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}\nAuthor: {Author}\nLength: {Length} seconds\nComments ({GetCommentCount()}):");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"- {comment.Name}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>
        {
            new Video("How to Code in C#", "Tech Guru", 600),
            new Video("Best Travel Destinations", "Wanderlust", 900),
            new Video("Top 10 Programming Tips", "Code Master", 750)
        };

        videos[0].AddComment("Alice", "Great tutorial!");
        videos[0].AddComment("Bob", "Very helpful, thanks!");
        videos[0].AddComment("Charlie", "Can you make one on Python?");

        videos[1].AddComment("Dave", "Amazing places! Can't wait to visit.");
        videos[1].AddComment("Eve", "Loved the recommendations.");
        videos[1].AddComment("Frank", "This was so inspiring!");

        videos[2].AddComment("Grace", "Really useful tips!");
        videos[2].AddComment("Hank", "I improved my coding skills a lot.");
        videos[2].AddComment("Ivy", "Awesome insights!");

        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}