using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1 - Class website");
            Console.WriteLine("2 - Class Magazine");
            Console.WriteLine("3 - Class Store");
            Console.WriteLine("0 - Exite");
            Console.Write("Enter choice: ");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (choice == 0)
            {
                break;
            }

            switch (choice)
            {
                case 1:
                    WebsiteTest();
                    break;
                case 2:
                    JournalTest();
                    break;
                case 3:
                    ShopTest();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            Console.WriteLine("\n Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void WebsiteTest()
    {
        Website site = new Website();
        site.Input();
        site.Print();
    }

    static void JournalTest()
    {
        Journal journal = new Journal();
        journal.Input();
        journal.Print();
    }

    static void ShopTest()
    {
        Shop shop = new Shop();
        shop.Input();
        shop.Print();
    }
}

class Website
{
    private string name;
    private string url;
    private string description;
    private string ip;

    public void Input()
    {
        Console.Write("Name: ");
        name = Console.ReadLine();

        Console.Write("URL: ");
        url = Console.ReadLine();

        Console.Write("Description: ");
        description = Console.ReadLine();

        Console.Write("IP: ");
        ip = Console.ReadLine();
    }

    public void Print()
    {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"URL: {url}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"IP: {ip}");
    }

    public string GetName() { return name; }
    public void SetName(string value) { name = value; }

    public string GetUrl() { return url; }
    public void SetUrl(string value) { url = value; }

    public string GetDescription() { return description; }
    public void SetDescription(string value) { description = value; }

    public string GetIp() { return ip; }
    public void SetIp(string value) { ip = value; }
}

class Journal
{
    private string name;
    private int year;
    private string description;
    private string phone;
    private string email;

    public void Input()
    {
        Console.Write("Name: ");
        name = Console.ReadLine();

        Console.Write("Year: ");
        year = int.Parse(Console.ReadLine());

        Console.Write("Description: ");
        description = Console.ReadLine();

        Console.Write("Phine: ");
        phone = Console.ReadLine();

        Console.Write("Email: ");
        email = Console.ReadLine();
    }

    public void Print()
    {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Year: {year}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Phone: {phone}");
        Console.WriteLine($"Email: {email}");
    }

    public string GetName() { return name; }
    public void SetName(string value) { name = value; }

    public int GetYear() { return year; }
    public void SetYear(int value) { year = value; }

    public string GetDescription() { return description; }
    public void SetDescription(string value) { description = value; }

    public string GetPhone() { return phone; }
    public void SetPhone(string value) { phone = value; }

    public string GetEmail() { return email; }
    public void SetEmail(string value) { email = value; }
}

class Shop
{
    private string name;
    private string address;
    private string description;
    private string phone;
    private string email;

    public void Input()
    {
        Console.Write("Name: ");
        name = Console.ReadLine();

        Console.Write("Address: ");
        address = Console.ReadLine();

        Console.Write("Description: ");
        description = Console.ReadLine();

        Console.Write("Phone: ");
        phone = Console.ReadLine();

        Console.Write("Email: ");
        email = Console.ReadLine();
    }

    public void Print()
    {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Address: {address}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Phone: {phone}");
        Console.WriteLine($"Email: {email}");
    }

    public string GetName() { return name; }
    public void SetName(string value) { name = value; }

    public string GetAddress() { return address; }
    public void SetAddress(string value) { address = value; }

    public string GetDescription() { return description; }
    public void SetDescription(string value) { description = value; }

    public string GetPhone() { return phone; }
    public void SetPhone(string value) { phone = value; }

    public string GetEmail() { return email; }
    public void SetEmail(string value) { email = value; }
}
