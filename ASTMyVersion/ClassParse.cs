﻿using System;
using System.Linq;

namespace MyVersion
{
    public class ClassParse
    {
        private readonly char[] _numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        private readonly char[] _operations = {'+', '-', '/', '*', '(', ')'};
        public char[] ArrOfExpression { get; set; }
     
        public ClassParse(string expression)
        {
           
            var arrOfExpression = new char[expression.Length];

            for (var i = 0; i < expression.Length; i++)
                if (!expression[i].Equals(null) && !expression[i].Equals(0) )
                {
                    foreach (var t in _operations)
                        if (expression[i] == t)
                        {
                            arrOfExpression[i] = t;
                        }

                    foreach (var t1 in _numbers)
                        if (expression[i] == t1)
                        {
                            arrOfExpression[i] = t1;
                        }
                }

            Console.WriteLine();
            for (var i = 0; i < arrOfExpression.Length; i++)
              if (arrOfExpression[i] == 0)
                  //c++;

            foreach (var ex in arrOfExpression)
            {
                Console.Write(ex);
            }

           // 
            Console.WriteLine();
            ArrOfExpression = arrOfExpression;
        }

        


        public ClassParse()
        {
            Console.WriteLine("You Must inkput expression");
        }

        
    }
}