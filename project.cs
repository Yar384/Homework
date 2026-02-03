using System;
using System.Collections.Generic;

namespace GarbageCollectorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //1
            Console.WriteLine("Створення без using");
            Book book1 = new Book("Кобзар", "Тарас Шевченко", 1840, 200);
            book1.ShowInfo();
            book1.Dispose();

            Console.WriteLine("\nСтворення з using");
            using (Book book2 = new Book("Тіні забутих предків", "Михайло Коцюбинський", 1911, 150))
            {
                book2.ShowInfo();
            }

            Console.WriteLine("\nСтворення без виклику Dispose");
            Book book3 = new Book("Захар Беркут", "Іван Франко", 1883, 180);
            book3.ShowInfo();
            book3 = null;
            
            Console.WriteLine("\nВиклик збирача сміття");
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //2
            Console.WriteLine("Бібліотека з using");
            using (Library library1 = new Library())
            {
                library1.AddBook(new Book("Лісова пісня", "Леся Українка", 1911, 100));
                library1.AddBook(new Book("Місто", "Валер'ян Підмогильний", 1928, 250));
                library1.ShowAllBooks();
            }

            Console.WriteLine("\nБібліотека без using");
            Library library2 = new Library();
            library2.AddBook(new Book("Intermezzo", "Михайло Коцюбинський", 1908, 120));
            library2.AddBook(new Book("Марія", "Уляна Кравченко", 1934, 200));
            library2.ShowAllBooks();
            library2.Dispose();
            
            Console.WriteLine("\nБібліотека без Dispose");
            Library library3 = new Library();
            library3.AddBook(new Book("Земля", "Ольга Кобилянська", 1902, 180));
            library3.ShowAllBooks();
            library3 = null;

            Console.WriteLine("\nВиклик збирача сміття");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            Console.ReadLine();
        }
    }

    //1
    class Book : IDisposable
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        
        private bool disposed = false;
        
        public Book(string title, string author, int year, int pages)
        {
            Title = title;
            Author = author;
            Year = year;
            Pages = pages;
            Console.WriteLine($"Книга '{Title}' створена");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"\nНазва: {Title}");
            Console.WriteLine($"Автор: {Author}");
            Console.WriteLine($"Рік видання: {Year}");
            Console.WriteLine($"Кількість сторінок: {Pages}");
        }
        
        public void Dispose()
        {
            if (!disposed)
            {
                Console.WriteLine($"Dispose викликано для книги '{Title}'");
                disposed = true;
                GC.SuppressFinalize(this);
            }
        }
        
        ~Book()
        {
            Console.WriteLine($"Фіналізатор викликано для книги '{Title}'");
        }
    }

    //2
    class Library : IDisposable
    {
        private List<Book> books;
        private bool disposed = false;
        
        public Library()
        {
            books = new List<Book>();
            Console.WriteLine("Бібліотека створена");
        }
        
        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Книга '{book.Title}' додана до бібліотеки");
        }
        
        public void ShowAllBooks()
        {
            Console.WriteLine($"\nКниги в бібліотеці (всього: {books.Count})");
            
            if (books.Count == 0)
            {
                Console.WriteLine("Бібліотека порожня");
                return;
            }
            
            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine($"\nКнига {i + 1}:");
                books[i].ShowInfo();
            }
        }
        
        public void Dispose()
        {
            if (!disposed)
            {
                Console.WriteLine("\nDispose викликано для бібліотеки");
                Console.WriteLine("Очищення ресурсів бібліотеки");
                
                if (books != null)
                {
                    foreach (Book book in books)
                    {
                        book.Dispose();
                    }
                    books.Clear();
                    Console.WriteLine("Список книг очищено");
                }
                
                disposed = true;
                GC.SuppressFinalize(this);
            }
        }
        
        ~Library()
        {
            Console.WriteLine("Фіналізатор викликано для бібліотеки");
        }
    }
}
