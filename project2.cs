using System;

namespace MoneyProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            try
            {
                Money money1 = new Money(100, 50);
                Money money2 = new Money(50, 25);
                
                bool exit = false;
                
                while (!exit)
                {
                    Console.WriteLine("\n=== МЕНЮ ===");
                    Console.WriteLine("1. Показати суми");
                    Console.WriteLine("2. Додати суми (+)");
                    Console.WriteLine("3. Відняти суми (-)");
                    Console.WriteLine("4. Помножити суму на число (*)");
                    Console.WriteLine("5. Поділити суму на число (/)");
                    Console.WriteLine("6. Збільшити суму на 1 копійку (++)");
                    Console.WriteLine("7. Зменшити суму на 1 копійку (--)");
                    Console.WriteLine("8. Порівняти суми (<, >, ==, !=)");
                    Console.WriteLine("9. Змінити суми");
                    Console.WriteLine("0. Вихід");
                    Console.Write("Виберіть опцію: ");
                    
                    string choice = Console.ReadLine();
                    
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("\nПерша сума: " + money1.ToString());
                            Console.WriteLine("Друга сума: " + money2.ToString());
                            break;
                            
                        case "2":
                            Money sum = money1 + money2;
                            Console.WriteLine("\nРезультат додавання: " + sum.ToString());
                            break;
                            
                        case "3":
                            Money diff = money1 - money2;
                            Console.WriteLine("\nРезультат віднімання: " + diff.ToString());
                            break;
                            
                        case "4":
                            Console.Write("\nВведіть число для множення: ");
                            int mult = int.Parse(Console.ReadLine());
                            Money product = money1 * mult;
                            Console.WriteLine("Результат множення: " + product.ToString());
                            break;
                            
                        case "5":
                            Console.Write("\nВведіть число для ділення: ");
                            int div = int.Parse(Console.ReadLine());
                            Money quotient = money1 / div;
                            Console.WriteLine("Результат ділення: " + quotient.ToString());
                            break;
                            
                        case "6":
                            money1++;
                            Console.WriteLine("\nПерша сума після збільшення: " + money1.ToString());
                            break;
                            
                        case "7":
                            money1--;
                            Console.WriteLine("\nПерша сума після зменшення: " + money1.ToString());
                            break;
                            
                        case "8":
                            Console.WriteLine("\nПорівняння сум:");
                            Console.WriteLine(money1.ToString() + " < " + money2.ToString() + " = " + (money1 < money2));
                            Console.WriteLine(money1.ToString() + " > " + money2.ToString() + " = " + (money1 > money2));
                            Console.WriteLine(money1.ToString() + " == " + money2.ToString() + " = " + (money1 == money2));
                            Console.WriteLine(money1.ToString() + " != " + money2.ToString() + " = " + (money1 != money2));
                            break;
                            
                        case "9":
                            Console.Write("\nВведіть гривні для першої суми: ");
                            int hrn1 = int.Parse(Console.ReadLine());
                            Console.Write("Введіть копійки для першої суми: ");
                            int kop1 = int.Parse(Console.ReadLine());
                            money1 = new Money(hrn1, kop1);
                            
                            Console.Write("Введіть гривні для другої суми: ");
                            int hrn2 = int.Parse(Console.ReadLine());
                            Console.Write("Введіть копійки для другої суми: ");
                            int kop2 = int.Parse(Console.ReadLine());
                            money2 = new Money(hrn2, kop2);
                            
                            Console.WriteLine("Суми змінено!");
                            break;
                            
                        case "0":
                            exit = true;
                            break;
                            
                        default:
                            Console.WriteLine("\nНевірний вибір!");
                            break;
                    }
                }
            }
            catch (BankruptException ex)
            {
                Console.WriteLine("\nПомилка: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nПомилка: " + ex.Message);
            }
            
            Console.ReadLine();
        }
    }
    
    class Money
    {
        private int hryvnias;
        private int kopiykas;
        
        public Money(int hryvnias, int kopiykas)
        {
            if (hryvnias < 0 || kopiykas < 0)
            {
                throw new BankruptException("Не можна створити від'ємну суму");
            }
            
            this.hryvnias = hryvnias;
            this.kopiykas = kopiykas;
            
            if (this.kopiykas >= 100)
            {
                this.hryvnias += this.kopiykas / 100;
                this.kopiykas = this.kopiykas % 100;
            }
        }
        
        public int GetHryvnias()
        {
            return hryvnias;
        }
        
        public int GetKopiykas()
        {
            return kopiykas;
        }
        
        public static Money operator +(Money m1, Money m2)
        {
            int totalKopiykas = m1.kopiykas + m2.kopiykas;
            int totalHryvnias = m1.hryvnias + m2.hryvnias;
            
            return new Money(totalHryvnias, totalKopiykas);
        }
        
        public static Money operator -(Money m1, Money m2)
        {
            int total1 = m1.hryvnias * 100 + m1.kopiykas;
            int total2 = m2.hryvnias * 100 + m2.kopiykas;
            int result = total1 - total2;
            
            if (result < 0)
            {
                throw new BankruptException("Банкрут");
            }
            
            return new Money(result / 100, result % 100);
        }
        
        public static Money operator *(Money m, int number)
        {
            int totalKopiykas = (m.hryvnias * 100 + m.kopiykas) * number;
            
            if (totalKopiykas < 0)
            {
                throw new BankruptException("Банкрут");
            }
            
            return new Money(totalKopiykas / 100, totalKopiykas % 100);
        }
        
        public static Money operator /(Money m, int number)
        {
            if (number == 0)
            {
                throw new Exception("Ділення на нуль неможливе");
            }
            
            int totalKopiykas = (m.hryvnias * 100 + m.kopiykas) / number;
            
            return new Money(totalKopiykas / 100, totalKopiykas % 100);
        }
        
        public static Money operator ++(Money m)
        {
            int totalKopiykas = m.hryvnias * 100 + m.kopiykas + 1;
            return new Money(totalKopiykas / 100, totalKopiykas % 100);
        }
        
        public static Money operator --(Money m)
        {
            int totalKopiykas = m.hryvnias * 100 + m.kopiykas - 1;
            
            if (totalKopiykas < 0)
            {
                throw new BankruptException("Банкрут");
            }
            
            return new Money(totalKopiykas / 100, totalKopiykas % 100);
        }
        
        public static bool operator <(Money m1, Money m2)
        {
            int total1 = m1.hryvnias * 100 + m1.kopiykas;
            int total2 = m2.hryvnias * 100 + m2.kopiykas;
            return total1 < total2;
        }
        
        public static bool operator >(Money m1, Money m2)
        {
            int total1 = m1.hryvnias * 100 + m1.kopiykas;
            int total2 = m2.hryvnias * 100 + m2.kopiykas;
            return total1 > total2;
        }
        
        public static bool operator ==(Money m1, Money m2)
        {
            return m1.hryvnias == m2.hryvnias && m1.kopiykas == m2.kopiykas;
        }
        
        public static bool operator !=(Money m1, Money m2)
        {
            return !(m1 == m2);
        }
        
        public override string ToString()
        {
            return hryvnias + " грн " + kopiykas + " коп";
        }
    }
    
    class BankruptException : ApplicationException
    {
        public BankruptException(string message) : base(message)
        {
        }
    }
}
