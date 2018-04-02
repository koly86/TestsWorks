using System;
using System.Linq;

namespace MyVersion
{
    public class ClassParse
    {
        private readonly char[] _numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        private readonly char[] _operations = {'+', '-', '/', '*', '(', ')'};

        public char[] ArrOfExpression { get; set; }
        private readonly string _expression;
        public char[] _arrOfExpression { get; set; }
        private int c; //считаем количество пустых полей в массиве

        public ClassParse(string expression)
        {
            _expression = expression;
            _arrOfExpression = new char[expression.Length];
            ToFillNewArray();
            _arrOfExpression = null;
            _arrOfExpression = new char[c];
            c = 0;
            ToFillNewArray();

            

            Console.WriteLine();
            foreach (var t in _arrOfExpression)
                if (t == 0)
                    c++;
            

            foreach (var ex in _arrOfExpression)
            {
                Console.Write(ex);
            }

            Console.WriteLine();
            ArrOfExpression = _arrOfExpression;
        }

        


        public ClassParse()
        {
            Console.WriteLine("You Must inkput expression");
        }

        private void ToFillNewArray()
        {
            for (var i = 0; i < _expression.Length; i++)
                if (!_expression[i].Equals(null))
                {
                    foreach (var t in _operations)
                        if (_expression[i] == t)
                        {
                            
                            _arrOfExpression[c] = t;
                            c++;
                        }

                    foreach (var t1 in _numbers)
                        if (_expression[i] == t1)
                        {
                           
                            _arrOfExpression[c] = t1;
                            c++;
                        }
                }
        }
    }
}