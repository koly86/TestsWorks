




// Created by Moriakov Nikolai
// insert parameters for ex. 5+5*(6+6)/8


using System;



namespace TreeAST
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(("Waiting for a arifmetic algoritm \nexample 5+5*6+6-(25/5) "));
            var virazenie = Console.ReadLine();
            Console.WriteLine(virazenie);

            var result = SyntaxTree.MathExprIntepreter.Execute(virazenie);
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
