using System;

namespace MusicAndCourses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //1          
            Violin violin = new Violin("Скрипка", "Струнний інструмент");
            Trombone trombone = new Trombone("Тромбон", "Духовий інструмент");
            Ukulele ukulele = new Ukulele("Укулеле", "Струнний щипковий інструмент");
            Cello cello = new Cello("Віолончель", "Струнний смичковий інструмент");
            
            violin.Show();
            violin.Desc();
            violin.Sound();
            violin.History();
            Console.WriteLine();
            
            trombone.Show();
            trombone.Desc();
            trombone.Sound();
            trombone.History();
            Console.WriteLine();
            
            ukulele.Show();
            ukulele.Desc();
            ukulele.Sound();
            ukulele.History();
            Console.WriteLine();
            
            cello.Show();
            cello.Desc();
            cello.Sound();
            cello.History();
            Console.WriteLine();
            
            //2          
            Course course1 = new Course("Основи програмування", 40);
            OnlineCourse course2 = new OnlineCourse("Веб-розробка", 60, "Coursera");
            
            Console.WriteLine(course1.ToString());
            Console.WriteLine(course2.ToString());
            
            Console.ReadLine();
        }
    }
    
    //для завдання 1
    class MusicalInstrument
    {
        public string Name;
        public string Characteristics;
        
        public MusicalInstrument(string name, string characteristics)
        {
            Name = name;
            Characteristics = characteristics;
        }
        
        public virtual void Sound()
        {
            Console.WriteLine("Інструмент видає звук");
        }
        
        public void Show()
        {
            Console.WriteLine("Назва: " + Name);
        }
        
        public void Desc()
        {
            Console.WriteLine("Опис: " + Characteristics);
        }
        
        public virtual void History()
        {
            Console.WriteLine("Історія інструмента");
        }
    }
    
    class Violin : MusicalInstrument
    {
        public Violin(string name, string characteristics) : base(name, characteristics)
        {
        }
        
        public override void Sound()
        {
            Console.WriteLine("Звук: Іі-іі-іі (ніжний звук скрипки)");
        }
        
        public override void History()
        {
            Console.WriteLine("Історія: Скрипка з'явилася в Італії в 16 столітті");
        }
    }
    
    class Trombone : MusicalInstrument
    {
        public Trombone(string name, string characteristics) : base(name, characteristics)
        {
        }
        
        public override void Sound()
        {
            Console.WriteLine("Звук: У-у-у-вааа (потужний звук тромбона)");
        }
        
        public override void History()
        {
            Console.WriteLine("Історія: Тромбон виник у 15 столітті в Європі");
        }
    }
    
    class Ukulele : MusicalInstrument
    {
        public Ukulele(string name, string characteristics) : base(name, characteristics)
        {
        }
        
        public override void Sound()
        {
            Console.WriteLine("Звук: Дзінь-дзінь-дзінь (веселий звук укулеле)");
        }
        
        public override void History()
        {
            Console.WriteLine("Історія: Укулеле створено на Гаваях у 19 столітті");
        }
    }
    
    class Cello : MusicalInstrument
    {
        public Cello(string name, string characteristics) : base(name, characteristics)
        {
        }
        
        public override void Sound()
        {
            Console.WriteLine("Звук: О-о-о-оо (глибокий звук віолончелі)");
        }
        
        public override void History()
        {
            Console.WriteLine("Історія: Віолончель з'явилася в 16 столітті в Італії");
        }
    }
    
    //для завдання 2
    class Course
    {
        public string Name;
        public int Duration;
        
        public Course(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }
        
        public override string ToString()
        {
            return "Курс: " + Name + ", Тривалість: " + Duration + " годин";
        }
    }
    
    class OnlineCourse : Course
    {
        public string Platform;
        
        public OnlineCourse(string name, int duration, string platform) : base(name, duration)
        {
            Platform = platform;
        }
        
        public override string ToString()
        {
            return "Онлайн курс: " + Name + ", Тривалість: " + Duration + " годин, Платформа: " + Platform;
        }
    }
}

