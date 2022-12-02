/*
    Simulate a guestbook.
    The datafile,'guestbook.json', created is in the format of Json.

    Written by Lina Petersson / Student at Mid Sweden University
*/
using System;
using System.Collections.Generic;
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

        public Post addPost(Post post)
        {
            posts.Add(post);
            marshal(); // Call class method marshal
            return post;
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
        private string title;
        private string content;
        public string Author
        {
            set { this.author = value; } // Set author input to class author
            get { return this.author; }
        }
        public string Title
        {
            set { this.title = value; } // Set iput title to class title
            get { return this.title; }
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
                    Console.WriteLine("Titel: " + post.Title);
                    Console.WriteLine("Av: " + post.Author);
                    Console.WriteLine("Innehåll: " + post.Content);
                }
               
                int input = (int)Console.ReadKey(true).Key; // Save users menu choice 

                // Depending on users menu choice show case
                switch (input)
                {
                    case '1':
                        Console.CursorVisible = true;
                        Post obj = new Post();

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
                        
                        // When correct save input in Post object
                        obj.Author = author;

                        // Ask user for input and save 
                        Console.Write("Ange titel: ");
                        string title = Console.ReadLine();

                        // If input is empty print error message and ask for new input
                        while (title == "")
                        {
                            Console.Write("\nDu måste ange en korrekt titel. Försök igen");
                            Console.Write("\nAnge titel: ");
                            title = Console.ReadLine();
                        }

                        // When correct save input in Post object
                        obj.Title = title;

                        // Ask user for input and save 
                        Console.Write("Ange innehåll: ");
                        string content = Console.ReadLine();

                        // If input is empty print error message and ask for new input
                        while (content == "")
                        {
                            Console.Write("\nDu måste ange ett korrekt innehåll. Försök igen");
                            Console.Write("\nAnge innehåll: ");
                            content = Console.ReadLine();
                        }

                        // When correct save input in Post object
                        obj.Content = content;

                        // Add post to guestbook
                        if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(title) && !String.IsNullOrEmpty(content)) guestbook.addPost(obj);
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
