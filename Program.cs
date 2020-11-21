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
                foreach (var item in JSONdata) // For each post in list
                {
                    Console.WriteLine($"[{index}] {item.Author} - {item.Data}"); //  display index, author and data
                    index++;
                }
            } else {
                Console.WriteLine("No entries in guestbook.");
            }
        }
        catch (System.Exception)
        {
            Console.WriteLine("Error in guestbook initialization."); // catch errors
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
                string author = Console.ReadLine(); // await input

                if (author.Length < 3) {
                    enterName("Name too short. Minimum is 3 characters.");
                } else {
                    person.Author = author; // set author
                    enterMessage(); // continue to message
                }
            }
            catch (System.Exception)
            {
                enterName("Unknown error.");
            }
        }

        void enterMessage(string err = "") {
            try
                {
                    if (err != "") {
                        Console.WriteLine($"Error: {err}");
                    }
                    Console.WriteLine($"{person.Author}, what would you like to post?");
                    string data = Console.ReadLine(); // await input

                    if (data.Length < 3) {
                        enterMessage("Post too short. Minimum is 3 characters.");
                    } else {
                        person.Data = data; // set input
                        person.Time = DateTime.Now; // also store timestamp
                        //person.ID = Guid.NewGuid(); // Uncomment for GUID functionability

                        var newlist = ReadFile(); // get file
                        newlist.Add(person); // add the new person
                        SaveFile(newlist); // save the new list
                        refresh(); // refresh entire screen
                    }
                }
                catch (System.Exception)
                {
                    enterMessage("Unknown error.");
                }
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
            return new List<Post> {}; // if error, return empty list
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
        Console.WriteLine("LINUS' GUESTBOOK");
        Console.WriteLine("1. Add new post");
        Console.WriteLine("2. Delete post");
        Console.WriteLine("X. Exit (Or any other key)\n");
        showPosts();

        // Wait for input
        var keypressed = Console.ReadKey(true).Key;
        switch (keypressed)
        { // switch case for what key is pressed by user
            case ConsoleKey.X:
                // nothing, exits program
                break;
            case ConsoleKey.D1:
                add();
                break;
            case ConsoleKey.D2:
                delete();
                break;

            default: // Any other key simply exits program
            // exits program
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
