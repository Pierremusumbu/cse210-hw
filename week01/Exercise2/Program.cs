using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        int grade = int.Parse(Console.ReadLine());

        // Initialize variables to store the letter grade and the sign
        string letterGrade = "";
        string sign = "";

        // Determine the letter grade
        if (grade >= 90)
        {
            letterGrade = "A";
        }
        else if (grade >= 80)
        {
            letterGrade = "B";
        }
        else if (grade >= 70)
        {
            letterGrade = "C";
        }
        else if (grade >= 60)
        {
            letterGrade = "D";
        }
        else
        {
            letterGrade = "F";
        }

        // Determine the sign for grades A, B, C, D
        if (letterGrade != "F") // No sign for F
        {
            int lastDigit = grade % 10;

            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        // Final grade with the sign
        string finalGrade = letterGrade + sign;

        // Output the final grade
        Console.WriteLine($"Your letter grade is: {finalGrade}");

        // Check if the user passed
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations, you passed!");
        }
        else
        {
            Console.WriteLine("Don't worry, keep working hard, and you'll do better next time!");
        }
    }
}