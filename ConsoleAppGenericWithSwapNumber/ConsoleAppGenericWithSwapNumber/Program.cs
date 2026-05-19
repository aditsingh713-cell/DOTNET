namespace ConsoleAppGenericWithSwapNumber
{
    internal class Program
    {
        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }



        static void Main(string[] args)
        {
            //Number swap
            int x = 5, y = 10;
            Console.WriteLine($"Before swap: x = {x}, y = {y}");
            Swap(ref x, ref y);
            Console.WriteLine($"After swap: x = {x}, y = {y}");
            //String swap
            string str1 = "Hello", str2 = "World";
            Console.WriteLine($"Before swap: str1 = {str1}, str2 = {str2}");
            Swap(ref str1, ref str2);
            Console.WriteLine($"After swap: str1 = {str1}, str2 = {str2}");
            Console.ReadKey();
        }
    }
}
