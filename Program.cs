using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace moment3
{
    public class Post
    {
        //public Guid ID {get; set;} // Uncomment for GUID functionability
        public string Author {get; set;}
        public string Data {get; set;}
        public DateTime Time {get; set;}
    }

    public class Guestbook
    {
    public void showPosts() {
        try
        {
            var JSONdata = ReadFile();
            
            if (JSONdata.Count != 0) { // If list is not empy
                int index = 0;
                foreach (var item in JSONdata) // For each post in list, write post ID and Data
                {
                    Console.WriteLine($"[{index}] {item.Author} - {item.Data}");
                    index++;
                }
            } else {
                Console.WriteLine("No entries in guestbook.");
            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("Error in guestbook initialization.");
        }
    }

    public void add()
    {
    var person = new Post();
    enterName();
        void enterName(string err = "") {
            try
            {
                if (err != "") {
                    Console.WriteLine($"Error: {err}");
                }
                Console.WriteLine("What is your name?");
                string author = Console.ReadLine();

                if (author.Length < 3) {
                    enterName("Name too short.");
                } else {
                    person.Author = author;
                    enterMessage();
                }
            }
            catch (System.Exception)
            {
                enterName("Unknown error.");
            }
        }

        void enterMessage() {
            Console.WriteLine($"\n{person.Author}, what would you like to post?");
            person.Data = Console.ReadLine();
            person.Time = DateTime.Now;
            //person.ID = Guid.NewGuid(); // Uncomment for GUID functionability

            var newlist = ReadFile();
            newlist.Add(person);
            SaveFile(newlist);
            refresh();
        }
    }

    public void delete(bool err = false)
    {
        Console.Clear();
        Console.WriteLine("LINUS' GUESTBOOK");
        if (err) {
            Console.WriteLine("Error in deletion, please try again.");
        }
        Console.WriteLine("Which post would you like to delete? (Or type X to go back)");
        showPosts();

        try
        {
            var line = Console.ReadLine();
            if (line != "X") {
                int number = int.Parse(line); // at what index are we removing
                var list = ReadFile();
                list.RemoveAt(number); // Remove object in list at index = number written by user
                SaveFile(list); // save list and refresh
            }
        }
        catch (System.Exception)
        {
            delete(true);
        }

        refresh();
    }

    public List<Post> ReadFile()
    {
        try
        {
            List<Post> newlist = new List<Post> {}; 
            var loadfile = File.ReadAllText("data.json"); // Read file
            newlist = JsonSerializer.Deserialize<List<Post>>(loadfile); // Deserialize file into our empty list
            return newlist;
        }
        catch (System.Exception)
        {
            return new List<Post> {};
        }

    }

    public void SaveFile(List<Post> newlist)
    {
        var newData = JsonSerializer.Serialize(newlist); // Serialize our list into JSON
        File.WriteAllText("data.json", newData); // Write the new list
    }

        public void refresh()
        {
        Console.Clear();
        Console.Write($"\nLINUS' GUESTBOOK");
        Console.Write($"\n 1. Add new post");
        Console.Write($"\n 2. Delete post");
        Console.Write($"\n X. Exit");

        Console.WriteLine("\n");

        showPosts();

        // Wait for input
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

            default: // Any other key simply exits program
            break;
        }
        
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Guestbook gb = new Guestbook();
            gb.refresh();
        }
    }
}
