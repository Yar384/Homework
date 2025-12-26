using System;
using System.Collections.Generic;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public override string ToString()
    {
        return $"{Title} ({Author}, {Year})";
    }
}

public class BookList
{
    private List<Book> books = new List<Book>();

    // Індексатор
    public Book this[int index]
    {
        get
        {
            if (index < 0 || index >= books.Count)
                throw new IndexOutOfRangeException();
            return books[index];
        }
        set
        {
            if (index < 0 || index >= books.Count)
                throw new IndexOutOfRangeException();
            books[index] = value;
        }
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    // Видалення книги
    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public bool ContainsBook(Book book)
    {
        return books.Contains(book);
    }

    // Виведення списку
    public void DisplayList()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Список книг порожній");
            return;
        }

        Console.WriteLine("Список книг:");
        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {books[i]}");
        }
    }

    public static BookList operator +(BookList list, Book book)
    {
        list.AddBook(book);
        return list;
    }

    public static BookList operator -(BookList list, Book book)
    {
        list.RemoveBook(book);
        return list;
    }

    public int Count => books.Count;
}

public class Fraction
{
    private int numerator;
    private int denominator;

    // Властивості з перевіркою
    public int Numerator
    {
        get { return numerator; }
        set { numerator = value; }
    }

    public int Denominator
    {
        get { return denominator; }
        set
        {
            if (value == 0)
                throw new ArgumentException("Знаменник не може бути нулем");
            denominator = value;
        }
    }

    public Fraction(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
        Simplify();
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }
    
    public void Simplify()
    {
        int gcd = GCD(numerator, denominator);
        numerator /= gcd;
        denominator /= gcd;

        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }
    }

    public static Fraction operator +(Fraction f1, Fraction f2)
    {
        int newNum = f1.numerator * f2.denominator + f2.numerator * f1.denominator;
        int newDen = f1.denominator * f2.denominator;
        return new Fraction(newNum, newDen);
    }

    public static Fraction operator -(Fraction f1, Fraction f2)
    {
        int newNum = f1.numerator * f2.denominator - f2.numerator * f1.denominator;
        int newDen = f1.denominator * f2.denominator;
        return new Fraction(newNum, newDen);
    }

    public static Fraction operator *(Fraction f1, Fraction f2)
    {
        int newNum = f1.numerator * f2.numerator;
        int newDen = f1.denominator * f2.denominator;
        return new Fraction(newNum, newDen);
    }

    public static Fraction operator /(Fraction f1, Fraction f2)
    {
        if (f2.numerator == 0)
            throw new DivideByZeroException("Ділення на нульовий дріб");

        int newNum = f1.numerator * f2.denominator;
        int newDen = f1.denominator * f2.numerator;
        return new Fraction(newNum, newDen);
    }

    public static bool operator ==(Fraction f1, Fraction f2)
    {
        if (ReferenceEquals(f1, f2)) return true;
        if (f1 is null || f2 is null) return false;

        f1.Simplify();
        f2.Simplify();
        return f1.numerator == f2.numerator && f1.denominator == f2.denominator;
    }

    public static bool operator !=(Fraction f1, Fraction f2)
    {
        return !(f1 == f2);
    }

    public override bool Equals(object obj)
    {
        if (obj is Fraction other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return (numerator, denominator).GetHashCode();
    }

    public override string ToString()
    {
        return $"{numerator}/{denominator}";
    }
}
