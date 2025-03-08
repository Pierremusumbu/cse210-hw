using System;
using System.Linq; // To use LINQ for sorting

class Program
{
    static void Main()
    {
        // Step 1: Create a list to hold the numbers
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Collect user inputs until they enter 0
        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());

            if (number == 0)
            {
                break; // Stop when the user enters 0
            }

            numbers.Add(number); // Add the number to the list
        }

        // Step 2: Calculate the sum, average, and maximum
        int sum = numbers.Sum(); // Compute the sum of the numbers
        double average = numbers.Average(); // Compute the average
        int largest = numbers.Max(); // Find the largest number

        // Display sum, average, and largest number
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largest}");

        // Step 3: Stretch Challenge
        // Find the smallest positive number
        int? smallestPositive = numbers.Where(n => n > 0).OrderBy(n => n).FirstOrDefault();
        if (smallestPositive.HasValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }

        // Sort the list in ascending order and display it
        var sortedList = numbers.OrderBy(n => n).ToList();
        Console.WriteLine("The sorted list is:");
        foreach (var num in sortedList)
        {
            Console.WriteLine(num);
        }
    }
}
