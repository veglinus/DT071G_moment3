using System;

namespace moment3
{
    public class Post
    {
        public string Author;
        public string Data;
        public DateTime Time;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is your name?");
            var person = new Post();
            person.Author = Console.ReadLine();

            Console.WriteLine($"\n{person.Author}, what would you like to post?");

            person.Data = Console.ReadLine();
            person.Time = DateTime.Now;

            Console.WriteLine(
                format: "{0}, posted on {1:dd MMM yy} by {2}",
                arg0: person.Data,
                arg1: person.Time,
                arg2: person.Author
            );

            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);
        }
    }
}
