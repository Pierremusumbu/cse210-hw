using System;

    public class Reference
    {
        public string Text { get; set; }
    
        // Constructor for single verse reference
        public Reference(string reference)
        {
            Text = reference;
        }

        // Constructor for verse range
        public Reference(string startReference, string endReference)
        {
            Text = startReference + "-" + endReference;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class Word
    {
        public string Text { get; set; }
        public bool IsHidden { get; set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public void Hide()
        {
            IsHidden = true;
        }

        public string GetDisplayText()
        {
            return IsHidden ? new string('_', Text.Length) : Text;
        }
    }

    public class Scripture
    {
        public Reference ScriptureReference { get; set; }
        public List<Word> Words { get; set; }

        public Scripture(string reference, string text)
        {
            ScriptureReference = new Reference(reference);
            Words = new List<Word>();
            var wordList = text.Split(' ');
            foreach (var word in wordList)
            {
                Words.Add(new Word(word));
            }
        }

        public void HideRandomWord(Random random)
        {
            var visibleWords = Words.FindAll(w => !w.IsHidden);
            if (visibleWords.Count == 0)
                return;

            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine(ScriptureReference);
            foreach (var word in Words)
            {
                Console.Write(word.GetDisplayText() + " ");
            }
            Console.WriteLine();
        }

        public bool AllWordsHidden()
        {
            return Words.TrueForAll(w => w.IsHidden);
        }
    }

    public class Program
    {
        public static void Main()
        {
            var scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
            var random = new Random();

            while (true)
            {
                scripture.Display();
                Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit.");
                var input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWord(random);

                if (scripture.AllWordsHidden())
                {
                    Console.WriteLine("\nAll words have been hidden.");
                    break;
                }
            }

            Console.WriteLine("\nThank you for using the Scripture Memorizer!");
        }
    }