using System.Runtime.Serialization;

namespace ClassDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Person p1= new Person();
              p1.FirstName = "John";
                p1.LastName = "Smith";
                p1.Age = 30;
            Console.WriteLine($"Person 1: {p1.GetFullName()} is {p1.Age} years old.");
            Person p2 = new Person();
            p2.FirstName = "Jane";
            p2.LastName = "Doe";
            p2.Age = 25;
            Console.WriteLine($"Person 2: {p2.GetFullName()} is {p2.Age} years old.");
            Console.ReadKey();
        }
    }
    class Person
    {
        public string FirstName;
        public string LastName;
        public byte Age;
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
