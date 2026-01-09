using System;

namespace DelegatesAndEventsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //1
            Action showTime = () =>
            {
                Console.WriteLine("Поточний час: " + DateTime.Now.ToString("HH:mm:ss"));
            };

            Action showDate = () =>
            {
                Console.WriteLine("Поточна дата: " + DateTime.Now.ToString("dd.MM.yyyy"));
            };
            
            Action showDayOfWeek = () =>
            {
                string day = "";
                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Monday: day = "Понеділок"; break;
                    case DayOfWeek.Tuesday: day = "Вівторок"; break;
                    case DayOfWeek.Wednesday: day = "Середа"; break;
                    case DayOfWeek.Thursday: day = "Четвер"; break;
                    case DayOfWeek.Friday: day = "П'ятниця"; break;
                    case DayOfWeek.Saturday: day = "Субота"; break;
                    case DayOfWeek.Sunday: day = "Неділя"; break;
                }
                Console.WriteLine("День тижня: " + day);
            };

            Func<double, double, double> triangleArea = (baseLength, height) =>
            {
                return (baseLength * height) / 2;
            };
            
            Func<double, double, double> rectangleArea = (width, height) =>
            {
                return width * height;
            };
            
            showTime();
            showDate();
            showDayOfWeek();
            
            Console.WriteLine("\nПлоща трикутника (основа=10, висота=5): " + triangleArea(10, 5));
            Console.WriteLine("Площа прямокутника (ширина=8, висота=6): " + rectangleArea(8, 6));
            
            //2            
            CreditCard card = new CreditCard("1234-5678-9012-3456", "Іванов Іван Іванович", 
                                             new DateTime(2027, 12, 31), "1234", 10000, 5000);
            
            card.MoneyAdded += (amount) =>
            {
                Console.WriteLine($"[Подія] Рахунок поповнено на {amount} грн");
            };

            card.MoneySpent += (amount) =>
            {
                Console.WriteLine($"[Подія] Витрачено {amount} грн");
            };
            
            card.CreditStarted += () =>
            {
                Console.WriteLine("[Подія] Почалося використання кредитних коштів!");
            };
            
            card.TargetAmountReached += (target) =>
            {
                Console.WriteLine($"[Подія] Досягнуто цільової суми {target} грн!");
            };
            
            card.PinChanged += () =>
            {
                Console.WriteLine("[Подія] PIN-код змінено!");
            };
            
            card.ShowInfo();

            Console.WriteLine("\n--- Операції ---");
            card.AddMoney(2000);
            card.ShowBalance();
            
            card.SpendMoney(3000);
            card.ShowBalance();
            
            card.SetTargetAmount(8000);
            card.AddMoney(1500);
            card.ShowBalance();
            
            card.SpendMoney(6000);
            card.ShowBalance();
            
            card.ChangePin("1234", "5678");
            
            Console.ReadLine();
        }
    }
    
    //2
    class CreditCard
    {
        private string cardNumber;
        private string ownerName;
        private DateTime expirationDate;
        private string pin;
        private double creditLimit;
        private double balance;
        private double targetAmount = 0;
        private bool isUsingCredit = false;
        
        public event Action<double> MoneyAdded;
        public event Action<double> MoneySpent;
        public event Action CreditStarted;
        public event Action<double> TargetAmountReached;
        public event Action PinChanged;

        public CreditCard(string cardNumber, string ownerName, DateTime expirationDate, 
                          string pin, double creditLimit, double balance)
        {
            this.cardNumber = cardNumber;
            this.ownerName = ownerName;
            this.expirationDate = expirationDate;
            this.pin = pin;
            this.creditLimit = creditLimit;
            this.balance = balance;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Номер картки: " + cardNumber);
            Console.WriteLine("Власник: " + ownerName);
            Console.WriteLine("Термін дії: " + expirationDate.ToString("MM/yyyy"));
            Console.WriteLine("Кредитний ліміт: " + creditLimit + " грн");
            Console.WriteLine("Баланс: " + balance + " грн");
        }

        public void ShowBalance()
        {
            Console.WriteLine("Поточний баланс: " + balance + " грн");
        }
        
        public void AddMoney(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Сума має бути більше 0");
                return;
            }
            
            balance += amount;
            
            if (MoneyAdded != null)
            {
                MoneyAdded(amount);
            }

            if (targetAmount > 0 && balance >= targetAmount)
            {
                if (TargetAmountReached != null)
                {
                    TargetAmountReached(targetAmount);
                }
                targetAmount = 0;
            }
            
            if (isUsingCredit && balance > 0)
            {
                isUsingCredit = false;
            }
        }
        
        public void SpendMoney(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Сума має бути більше 0");
                return;
            }
            
            if (balance + creditLimit < amount)
            {
                Console.WriteLine("Недостатньо коштів. Ліміт перевищено");
                return;
            }

            bool wasPositive = balance > 0;
            balance -= amount;

            if (MoneySpent != null)
            {
                MoneySpent(amount);
            }
            
            if (wasPositive && balance < 0 && !isUsingCredit)
            {
                isUsingCredit = true;
                if (CreditStarted != null)
                {
                    CreditStarted();
                }
            }
        }
        
        public void SetTargetAmount(double amount)
        {
            targetAmount = amount;
            Console.WriteLine("Встановлено цільову суму: " + amount + " грн");
        }
        
        public void ChangePin(string oldPin, string newPin)
        {
            if (oldPin != pin)
            {
                Console.WriteLine("Неправильний старий PIN");
                return;
            }
            
            if (newPin.Length != 4)
            {
                Console.WriteLine("PIN має складатися з 4 цифр");
                return;
            }
            
            pin = newPin;
            
            if (PinChanged != null)
            {
                PinChanged();
            }
        }
    }
}
