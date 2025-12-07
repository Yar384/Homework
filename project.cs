using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("========== МЕНЮ ==========");
            Console.WriteLine("1 - Конвертація температури");
            Console.WriteLine("2 - Вивести парні числа у діапазоні");
            Console.WriteLine("3 - Перевірити число Армстронга");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (choice == 0)
            {
                Console.WriteLine("Вихід з програми...");
                break;
            }

            switch (choice)
            {
                case 1:
                    TemperatureTask();
                    break;

                case 2:
                    EvenNumbersTask();
                    break;

                case 3:
                    ArmstrongTask();
                    break;

                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще.");
                    break;
            }

            Console.WriteLine("\nEnter, щоб повернутися в меню...");
            Console.ReadLine();
            Console.Clear();
        }
    }

  //1
  static void TemperatureTask()
    {
        Console.Write("Введіть температуру: ");
        double temp = double.Parse(Console.ReadLine());

        Console.WriteLine("1 - Фаренгейт → Цельсій");
        Console.WriteLine("2 - Цельсій → Фаренгейт");
        Console.Write("Ваш вибір: ");
        int type = int.Parse(Console.ReadLine());

        if (type == 1)
        {
            double c = (temp - 32) * 5 / 9;
            Console.WriteLine($"Результат: {c} °C");
        }
        else if (type == 2)
        {
            double f = temp * 9 / 5 + 32;
            Console.WriteLine($"Результат: {f} °F");
        }
        else
        {
            Console.WriteLine("Невірний вибір.");
        }
    }

    //2
    static void EvenNumbersTask()
    {
        Console.Write("Введіть перше число: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введіть друге число: ");
        int b = int.Parse(Console.ReadLine());

        int start = Math.Min(a, b);
        int end = Math.Max(a, b);

        Console.WriteLine($"Парні числа від {start} до {end}:");

        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 0)
                Console.Write(i + " ");
        }

        Console.WriteLine();
    }

    //3
  static void ArmstrongTask()
    {
        Console.Write("Введіть число: ");
        int number = int.Parse(Console.ReadLine());

        int temp = number;
        int digits = number.ToString().Length;
        int sum = 0;

        while (temp > 0)
        {
            int digit = temp % 10;
            sum += (int)Math.Pow(digit, digits);
            temp /= 10;
        }

        if (sum == number)
            Console.WriteLine("Це число Армстронга!");
        else
            Console.WriteLine("Це НЕ число Армстронга.");
    }
}
