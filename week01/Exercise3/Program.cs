using System;

class Program
{
    static void Main()
    {
        // Start the game loop
        while (true)
        {
            // Step 1: Generate a random magic number between 1 and 100
            Random random = new Random();
            Console.WriteLine("Welcome to the Guess My Number game!");
             // Random number between 1 and 100

            // Initialize a counter for the number of guesses
            int guessCount = 100;
            int guess = 30;

            Console.Write("What is the magic number? ");
            Console.ReadLine();

            int magicNumber = random.Next(1, 20);

            // Step 2: Start a loop to keep guessing until the correct number is guessed
            while (guess != magicNumber)
            {
                // Ask the user for a guess
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());

                // Increment the guess counter
                guessCount++;

                // Check the guess and give feedback
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {guessCount} guesses!");
                }
            }

            // Step 3: Ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");
            string playAgain = Console.ReadLine().ToLower();

            if (playAgain != "yes")
            {
                Console.WriteLine("Thanks for playing! Goodbye!");
                break; // Exit the game loop if the user doesn't want to play again
            }
        }
    }
}