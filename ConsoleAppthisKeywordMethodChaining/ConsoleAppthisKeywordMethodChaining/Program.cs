using System.Globalization;

namespace ConsoleAppthisKeywordMethodChaining
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new();
           
            string greeting = p1.SetFirstName("John").SetLastName("Doe").SayHi();

            Console.WriteLine(greeting);
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
            public Person SetFirstName(string firstName)
                {
                    this.FirstName = firstName;
                    return this;
            }
            public Person SetLastName(string lastName)
            {
                this.LastName = lastName;
                return this;
            }
            public string SayHi()
                            {
                return $"Hi, I am {this.GetFullName()}";
            }

        }
    }
}
