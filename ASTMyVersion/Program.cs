using System;

namespace MyVersion
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Please insert expression");
            var classParse = new ClassParse(Console.ReadLine());
            var countingExpression = new CountingExpression(classParse.ArrOfExpression);
            Console.ReadKey();
        }
    }
}