/*
    Simulate a guestbook.
    The datafile,'guestbook.json', created is in the format of Json.

    Written by Lina Petersson / Mid Sweden University
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
            if (File.Exists(@"guestbook.json") == true)
            { // If stored json data exists then read
                string jsonString = File.ReadAllText(filename);
                posts = JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
        }
        public Post addPost(Post post)
        {
            posts.Add(post);
            marshal();
            return post;
        }

        public int delPost(int index)
        {
            posts.RemoveAt(index);
            marshal();
            return index;
        }

        public List<Post> getPosts()
        {
            return posts;
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
            set { this.author = value; }
            get { return this.author; }
        }
        public string Title
        {
            set { this.title = value; }
            get { return this.title; }
        }
        public string Content
        {
            set { this.content = value; }
            get { return this.content; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Guestbook guestbook = new Guestbook();
            int i = 0;

            while (true)
            {
                Console.Clear(); Console.CursorVisible = false;
                Console.WriteLine("GÄSTBOK\n\n");

                Console.WriteLine("1. Skriv ett nytt gästboksinlägg");
                Console.WriteLine("2. Ta bort ett befintligt inlägg");
                Console.WriteLine("X. Avsluta\n");
            
                i = 0;
                foreach (Post post in guestbook.getPosts())
                {
                    Console.WriteLine("\n[" + i++ + "]");
                    Console.WriteLine("Titel: " + post.Title);
                    Console.WriteLine("Av: " + post.Author);
                    Console.WriteLine("Innehåll: " + post.Content);
                }
               
                int inp = (int)Console.ReadKey(true).Key;
                switch (inp)
                {
                    case '1':
                        Console.CursorVisible = true;
                        Post obj = new Post();
                        Console.Write("\nAnge skribent: ");
                        string author = Console.ReadLine();
                       
                        while (author == "")
                        {
                            Console.Write("\nDu måste ange ett korrekt namn. Försök igen");
                            Console.Write("\nAnge skribent: ");
                            author = Console.ReadLine();
                        }
                        
                        obj.Author = author;
                        Console.Write("Ange titel: ");
                        string title = Console.ReadLine();

                        while (title == "")
                        {
                            Console.Write("\nDu måste ange en korrekt titel. Försök igen");
                            Console.Write("\nAnge titel: ");
                            title = Console.ReadLine();
                        }

                        obj.Title = title;
                      
                        Console.Write("Ange innehåll: ");
                        string content = Console.ReadLine();
                        while (content == "")
                        {
                            Console.Write("\nDu måste ange ett korrekt innehåll. Försök igen");
                            Console.Write("\nAnge innehåll: ");
                            content = Console.ReadLine();
                        }

                        obj.Content = content;
                        if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(title) && !String.IsNullOrEmpty(content)) guestbook.addPost(obj);

                        break;
                    case '2':
                        Console.CursorVisible = true;
                        Console.Write("\nAnge index att radera: ");
                        string index = Console.ReadLine();
                        guestbook.delPost(Convert.ToInt32(index));
                        break;
                    case 88:
                        Environment.Exit(0);
                        break;
                }

            }

        }
    }
}
