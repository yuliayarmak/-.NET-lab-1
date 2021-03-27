using System;
using System.Globalization;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            DocDev dev = new LetterDev("Мария Прокофьева Скуратова-Бельская", "Входящее", "письмо");
            dev.Create();

            DocDev dev1 = new MemoDev("Служебная записка");
            dev1.Create();

            DocDev dev2 = new WritDev(44, "1 Jan 2021", "Александр Николаевич Белов", "Приказ");
            dev2.Create();

            DocDev dev3 = new PrescriptDev(187, "27 April 2021", "Распоряжение");
            dev3.Create();

            DocDev dev4 = new ResourceRequestDev("София Александровна Швец", "ресурс1, ресурс2, ресурс3, ресурс4", "Заявка на ресурсы");
            dev4.Create();
        }
    }

    abstract class DocDev
    {
        public string Info { get; set; }

        public DocDev (string info)
        {
            Info = info;
        }

        abstract public Document Create();
    }

    class LetterDev : DocDev
    {
        public string Correspondent { get; set; }
        public string LetterType { get; set; }
        public LetterDev (string correspondent, string lettertype, string info) : base(info)
        {
            Correspondent = correspondent;
            LetterType = lettertype;

        }

        public override Document Create()
        {
            return new Letter(LetterType, Correspondent, Info);
        }
    }

    class MemoDev : DocDev
    {
        public MemoDev(string info) : base(info)
        { }

        public override Document Create()
        {
            return new Memo(Info);
        }
    }

    class WritDev : DocDev
    {
        public int Unit { get; set; }
        public DateTime Deadline { get; }
        public string Executor { get; set; }

        public WritDev (int unit, string deadline, string executor, string info) : base(info)
        {
            Unit = unit;
            var cultureInfo = new CultureInfo("de-DE");
            string dateString = deadline;
            Deadline = DateTime.Parse(dateString, cultureInfo);
            Executor = executor;
        }

        public override Document Create()
        {
            return new Writ(Unit, Deadline, Executor, Info);
        }
    }

    class PrescriptDev : DocDev
    {
        public int Unit { get; set; }
        public DateTime Deadline { get; }

        public PrescriptDev(int unit, string deadline, string info) : base(info)
        {
            Unit = unit;
            var cultureInfo = new CultureInfo("de-DE");
            string dateString = deadline;
            Deadline = DateTime.Parse(dateString, cultureInfo);
        }

        public override Document Create()
        {
            return new Prescript(Unit, Deadline, Info);
        }
    }

    class ResourceRequestDev : DocDev
    {
        public string Name { get; set; }
        public string Resources { get; set; }

        public ResourceRequestDev(string name, string resources, string info) : base(info)
        {
            Name = name;
            Resources = resources;
        }

        public override Document Create()
        {
            return new ResourceRequest(Name, Resources, Info);
        }
    }


    abstract class Document
    { 
        public string Info { get; set; }
        public int Number { get; set; }
        public DateTime DocDate { get; }

        public Document (string info)
        {
            var rnd = new Random();
            Number = rnd.Next(1000000, 9999999);
            DocDate = DateTime.Now;
            Info = info;
        }

    }

    class Letter : Document
    {
        
        public Letter (string LetterType, string Correspondent, string info) : base (info) 
        {
            Console.WriteLine($"\n{LetterType} {Info} №{Number}. \nКорреспондент - {Correspondent}. \nДата и время - {DocDate}.");
        }
  
    }
  
    class Memo : Document
    {
        public Memo(string info) : base (info)
        {
            Console.WriteLine($"\n{Info} №{Number}. \nДата и время - {DocDate}.");
        }
    }

    class Writ : Document
    {
        public Writ (int Unit, DateTime Deadline, string Executor, string info) : base(info)
        {
            Console.WriteLine($"\n{Info} №{Number}. \nПодразделение №{Unit}. \nСрок исполнения: до {Deadline}. \nОтветственный - {Executor}. \nДата и время - {DocDate}.");
        }
    }

    class Prescript : Document
    {
        public Prescript(int Unit, DateTime Deadline, string info) : base(info)
        {
            Console.WriteLine($"\n{Info} №{Number}. \nПодразделение №{Unit}. \nСрок исполнения: до {Deadline}. \nДата и время - {DocDate}.");
        }
    }

    class ResourceRequest : Document
    {
        public ResourceRequest(string Name, string Resources, string info) : base(info)
        {
            Console.WriteLine($" \n{Info} №{Number}. \n{Name} запрашивает доступ к следующим ресурсам: {Resources}. \nДата и время - {DocDate}.");
        }
    }


}
