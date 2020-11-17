using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

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
            Console.Clear();
            Console.Write($"\nLINUS' GUESTBOOK");
            Console.Write($"\n 1. Add new post");
            Console.Write($"\n 2. Delete post");
            Console.Write($"\n X. Exit");

            Console.WriteLine("\n");

            try
            {
                var loadfile = File.ReadAllText("data.json"); // Read file
                var JSONdata = JsonSerializer.Deserialize<List<Post>>(loadfile) ?? null; // Deserialize into list of posts
                //Console.WriteLine($"\n\n{JSONdata}");

                if (JSONdata != null) {
                    foreach (var item in JSONdata) // For each post in list, write post ID and Data
                    {
                        Console.WriteLine($"[{item.ID}] {item.Author} - {item.Data}");
                    }
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("No entries in guestbook.");
            }



            var keypressed = Console.ReadKey(true).Key; // Await keypress
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

            /*
            Console.WriteLine("\nAdding post:");
            Console.WriteLine(
                format: "[{0}], {1}, posted on {2:dd MMM yy}",
                arg0: person.ID.ToString(),
                arg1: person.Data,
                arg2: person.Time
            );
            */

            List<Post> newlist = new List<Post> {}; // Initialize empty list of posts

            try
            {
                var loadfile = File.ReadAllText("data.json"); // Read file
                newlist = JsonSerializer.Deserialize<List<Post>>(loadfile); // Deserialize file into our empty list
                newlist.Add(person); // Add our new post
            }
            catch (System.Exception)
            {
                newlist.Add(person); // If file is empty, simply add person to our empty list
            }

            var newData = JsonSerializer.Serialize(newlist); // Serialize our list into JSON
            File.WriteAllText("data.json", newData); // Write the new list
            Console.WriteLine("\n\n\n");

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
