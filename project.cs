using System;

namespace InterfacesProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //1
            Console.WriteLine("=== Завдання 1 ===\n");
            
            IRemoteControl tvRemote = new TvRemoteControl();
            IRemoteControl radioRemote = new RadioRemoteControl();
            
            Console.WriteLine("--- Керування телевізором ---");
            tvRemote.TurnOn();
            tvRemote.SetChannel(5);
            tvRemote.SetChannel(12);
            tvRemote.TurnOff();

            Console.WriteLine("\n--- Керування радіо ---");
            radioRemote.TurnOn();
            radioRemote.SetChannel(101);
            radioRemote.SetChannel(95);
            radioRemote.TurnOff();

            //2
            Console.WriteLine("\n\n=== Завдання 2 ===\n");

            IValidator emailValidator = new EmailValidator();
            IValidator passwordValidator = new PasswordValidator();

            Console.WriteLine("--- Перевірка email ---");
            string email1 = "test@gmail.com";
            string email2 = "wrongemail";

            Console.WriteLine("Email: " + email1);
            if (emailValidator.Validate(email1))
            {
                Console.WriteLine("Email коректний");
            }
            else
            {
                Console.WriteLine("Email некоректний");
            }
            
            Console.WriteLine("\nEmail: " + email2);
            if (emailValidator.Validate(email2))
            {
                Console.WriteLine("Email коректний");
            }
            else
            {
                Console.WriteLine("Email некоректний");
            }

            Console.WriteLine("\n--- Перевірка паролю ---");
            string password1 = "MyPass123";
            string password2 = "123";

            Console.WriteLine("Пароль: " + password1);
            if (passwordValidator.Validate(password1))
            {
                Console.WriteLine("Пароль коректний");
            }
            else
            {
                Console.WriteLine("Пароль некоректний");
            }
            
            Console.WriteLine("\nПароль: " + password2);
            if (passwordValidator.Validate(password2))
            {
                Console.WriteLine("Пароль коректний");
            }
            else
            {
                Console.WriteLine("Пароль некоректний");
            }
            
            Console.ReadLine();
        }
    }

    // Завдання 1
    interface IRemoteControl
    {
        void TurnOn();
        void TurnOff();
        void SetChannel(int channel);
    }

    class TvRemoteControl : IRemoteControl
    {
        private bool isOn = false;
        private int currentChannel = 1;
        
        public void TurnOn()
        {
            isOn = true;
            Console.WriteLine("Телевізор увімкнено");
        }
        
        public void TurnOff()
        {
            isOn = false;
            Console.WriteLine("Телевізор вимкнено");
        }
        
        public void SetChannel(int channel)
        {
            if (isOn)
            {
                currentChannel = channel;
                Console.WriteLine("Переключено на канал: " + channel);
            }
            else
            {
                Console.WriteLine("Телевізор вимкнений. Спочатку увімкніть його.");
            }
        }
    }
    
    class RadioRemoteControl : IRemoteControl
    {
        private bool isOn = false;
        private int currentChannel = 100;
        
        public void TurnOn()
        {
            isOn = true;
            Console.WriteLine("Радіо увімкнено");
        }
        
        public void TurnOff()
        {
            isOn = false;
            Console.WriteLine("Радіо вимкнено");
        }
        
        public void SetChannel(int channel)
        {
            if (isOn)
            {
                currentChannel = channel;
                Console.WriteLine("Переключено на частоту: " + channel + " FM");
            }
            else
            {
                Console.WriteLine("Радіо вимкнене. Спочатку увімкніть його.");
            }
        }
    }
    
    // Завдання 2
    interface IValidator
    {
        bool Validate(string data);
    }
    
    class EmailValidator : IValidator
    {
        public bool Validate(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            
            if (!data.Contains("@"))
            {
                return false;
            }
            
            if (!data.Contains("."))
            {
                return false;
            }
            
            int atIndex = data.IndexOf("@");
            int dotIndex = data.LastIndexOf(".");
            
            if (atIndex > dotIndex)
            {
                return false;
            }
            
            if (atIndex == 0)
            {
                return false;
            }
            
            if (dotIndex == data.Length - 1)
            {
                return false;
            }
            
            return true;
        }
    }
    
    class PasswordValidator : IValidator
    {
        public bool Validate(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            
            if (data.Length < 6)
            {
                return false;
            }
            
            bool hasDigit = false;
            bool hasLetter = false;
            
            foreach (char c in data)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                if (char.IsLetter(c))
                {
                    hasLetter = true;
                }
            }
            
            if (!hasDigit || !hasLetter)
            {
                return false;
            }
            
            return true;
        }
    }
}
