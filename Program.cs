using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace moment3
{
    public class Post
    {
        public Guid ID {get; set;}
        public string Author {get; set;}
        public string Data {get; set;}
        public DateTime Time {get; set;}
    }

    class Program
    {
        static void Main(string[] args)
        {

            read();

            void read() {
            
            Console.Write($"\nLINUS' GUESTBOOK");
            Console.Write($"\n 1. Add new post");
            Console.Write($"\n 2. Delete post");
            Console.Write($"\n X. Exit");

            string loadfile;
            
            loadfile = File.ReadAllText("data.json");
            var serialized = JsonSerializer.Deserialize<Post>(loadfile);
            
            Console.WriteLine($"\n{serialized.Data}\n");
            
            //var serialized = "testdata";

            // read data from file
            Console.Write($"\n\n{serialized}\n\n");
            var keypressed = Console.ReadKey(true).Key;


            switch (keypressed)
            {
                case ConsoleKey.X:
                    // nothing
                    break;
                case ConsoleKey.D1:
                    add();
                    break;
                case ConsoleKey.D2:
                    delete();
                    break;

                default:
                break;
            }

            //Console.Write("\nPress any key to exit...");
            
            }

            void add()
            {
            Console.WriteLine("What is your name?");
            var person = new Post();
            person.Author = Console.ReadLine();

            Console.WriteLine($"\n{person.Author}, what would you like to post?");

            person.Data = Console.ReadLine();
            person.Time = DateTime.Now;
            person.ID = Guid.NewGuid();

            Console.WriteLine("\nAdding post:");
            Console.WriteLine(
                format: "[{0}], {1}, posted on {2:dd MMM yy}",
                arg0: person.ID.ToString(),
                arg1: person.Data,
                arg2: person.Time
                //arg3: person.Author
            );

            var jsonString = JsonSerializer.Serialize(person); // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
            File.WriteAllText("data.json", jsonString);

            Console.WriteLine("\n\n\n");
            //Console.Clear();
            read();
            }

            void delete()
            {
                // Guid id
                // get class
            }
        }
    }
}
