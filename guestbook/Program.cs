/*
    Simulate a guestbook.
    The datafile,'guestbook.json', created is in the format of Json.

    Written by Lina Petersson / Student at Mid Sweden University
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

using System.Text.Json;

namespace moment3
{
    public class Guestbook
    {
        private string filename = @"guestbook.json";
        private List<Post> posts = new List<Post>();

        public Guestbook()
        {
            if (File.Exists(filename) == true)
            { // If stored json data exists then read
                string jsonString = File.ReadAllText(filename);
                posts = JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
        }

        public Post addPost(string auth, string cont)
        {
            Post obj = new Post(); // Create object of class Post

            // Add user input to object 
            obj.Author = auth;
            obj.Content = cont;
            posts.Add(obj);
            marshal(); // Call class method marshal
            return obj;
        }

        public int delPost(int index)
        {
            posts.RemoveAt(index); // Remove post at chosen index
            marshal();
            return index;
        }

        public List<Post> getPosts()
        {
            return posts; // Return all posts
        }

        private void marshal()
        {
            // Serialize all the objects and save to file
            var jsonString = JsonSerializer.Serialize(posts);
            File.WriteAllText(filename, jsonString);
        }
    }

    public class Post
    {
        private string author;
        private string content;

        public string Author
        {
            set { this.author = value; } // Set author input to class author
            get { return this.author; }
        }

        public string Content
        {
            set { this.content = value; } // Set input content to class content 
            get { return this.content; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create object of class Guestbook 
            Guestbook guestbook = new Guestbook();
            int i = 0;

            while (true)
            {
                Console.Clear(); // Clear consol 
                Console.CursorVisible = false; // Hide cursor

                // Welcome user
                Console.WriteLine("GÄSTBOK");
                Console.WriteLine("Välkommen till gästboken!");
                Console.WriteLine("\nMedan du är här kan du passa på att skriva ett nytt inlägg, ");
                Console.WriteLine("eller radera ett befintligt som du inte är nöjd med. Mycket nöje!\n\n");

                //Menu options
                Console.WriteLine("1. Skriv ett nytt gästboksinlägg");
                Console.WriteLine("2. Ta bort ett befintligt inlägg");
                Console.WriteLine("X. Avsluta\n");
            
                // Loop through posts and print 
                i = 0;
                foreach (Post post in guestbook.getPosts())
                {
                    Console.WriteLine("\n[" + i++ + "]");
                    Console.WriteLine("Av: " + post.Author);
                    Console.WriteLine("Inlägg: " + post.Content);
                }
               
                int input = (int)Console.ReadKey(true).Key; // Save users menu choice 

                // Depending on users menu choice show case
                switch (input)
                {
                    case '1':
                        Console.CursorVisible = true;
                        //Post obj = new Post();

                        // Ask user for input and save
                        Console.Write("\nAnge skribent: ");
                        string author = Console.ReadLine(); 
                       
                        // If input is empty print error message and ask for new input
                        while (author == "") 
                        {
                            Console.Write("\nDu måste ange ett korrekt namn. Försök igen");
                            Console.Write("\nAnge skribent: ");
                            author = Console.ReadLine();
                        }
                    
                        // Ask user for input and save 
                        Console.Write("Skriv inlägg: ");
                        string content = Console.ReadLine();

                        // If input is empty print error message and ask for new input
                        while (content == "")
                        {
                            Console.Write("\nDitt inlägg kan inte vara tomt. Försök igen");
                            Console.Write("\nSkriv inlägg: ");
                            content = Console.ReadLine();
                        }

                        // Add post to guestbook
                        if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(content)) guestbook.addPost(author, content);
                        break;
                    case '2':
                        Console.CursorVisible = true;
                        Console.Write("\nAnge index att radera: ");
                        string index = Console.ReadLine();

                        // Convert input to int and call method delPost in class Guestbook
                        guestbook.delPost(Convert.ToInt32(index));
                        break;
                    case 88:
                        //Exit application
                        Environment.Exit(0);
                        break;
                }

            }

        }
    }
}
