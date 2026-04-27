namespace ConsoleAppDemothisKey
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            p1.FirstName = "John";
            p1.LastName = "Doe";
            Console.WriteLine(p1.GetFullName());
            Console.WriteLine(p1.SayHi());
            Console.ReadLine();
        }

        class Person
        { 
            public string FirstName;
            public string LastName;

            public string GetFullName()
            {
                return $"{this.FirstName} {this.LastName}"; 
            }
            public string SayHi()
            {
                return $"Hi, I am {this.GetFullName()}";
            }
        }

    }
}
