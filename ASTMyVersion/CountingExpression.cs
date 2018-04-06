using System;

namespace MyVersion
{
    public class CountingExpression
    {
        private readonly char[] _expression;
        private int _bracketsPosition;
        private byte _callCount;
        private int _id;
        private int _idEnd;
        private int _idStart;
        private int _result;

        private char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        public CountingExpression(char[] expression)
        {
            _expression = expression;
            Count();
        }

        private void Count()
        {
            int _temp = 0;
            for (_id = _idEnd; _id < _expression.Length; _id++)
                if (_id + 1 < _expression.Length) 

                    switch (_expression[_id])
                    {
                        case '(':


                            break;

                        case ')':

                            break;

                        case char n when n >= '0' && n <= '9':
                            _temp = Convert.ToInt32(n.ToString());
                            break;

                        case char g when g == '+' || g == '-' || g == '*' || g == '/':
                            if (_id + 1 < _expression.Length)
                                switch (_expression[_id + 1])
                                {
                                    case char n when n >= '0' && n <= '9':
                                        switch (g)
                                        {
                                            case '+':
                                                _result += _temp + Convert.ToInt32(_expression[_id + 1].ToString());
                                                _temp = 0;
                                                _id++;

                                                break;
                                            case '-':
                                                _result += _temp - Convert.ToInt32(_expression[_id + 1].ToString());
                                                _id++;
                                                _temp = 0;
                                                break;
                                            case '/':
                                                _result += _temp / Convert.ToInt32(_expression[_id + 1].ToString());
                                                _id++;
                                                _temp = 0;
                                                break;
                                            case '*':
                                                _result += _temp * Convert.ToInt32(_expression[_id + 1].ToString());
                                                _temp = 0;
                                                _id++;
                                                break;
                                           
                                        }
                                        

                                        break;
                                }

                            break;
                    }

            Console.WriteLine(_result);
        }


        private void FindBrackets()
        {
            for (; _id < _expression.Length; _id++)
                if (_expression[_id] == '(')
                {
                    _bracketsPosition = _id;
                    Console.WriteLine($@"brackets position = {_id} = {_expression[_id]}");
                }
        }

        private void CountingInsideBrackets(byte call)
        {
            for (var i = _idStart; i < _idEnd; i++)
                switch (call)
                {
                    case 0:
                        switch (_expression[i])
                        {
                            case '+':
                                _result += Convert.ToInt32(_expression[i - 1].ToString()) +
                                           Convert.ToInt32(_expression[i + 1].ToString());
                                _idEnd = _idEnd + 1;
                                Console.WriteLine($@"result of count {_result}");
                                break;
                            case '-':
                                _result += Convert.ToInt32(_expression[i - 1].ToString()) -
                                           Convert.ToInt32(_expression[i + 1].ToString());
                                _idEnd = _idEnd + 1;
                                Console.WriteLine($@"result of count {_result}");
                                break;
                            case '/':
                                _result += Convert.ToInt32(_expression[i - 1].ToString()) /
                                           Convert.ToInt32(_expression[i + 1].ToString());
                                _idEnd = _idEnd + 1;
                                Console.WriteLine($@"result of count {_result}");
                                break;
                            case '*':
                                _result += Convert.ToInt32(_expression[i - 1].ToString()) *
                                           Convert.ToInt32(_expression[i + 1].ToString());
                                _idEnd = _idEnd + 1;
                                Console.WriteLine($@"result of count {_result}");
                                break;
                        }

                        call++;
                        break;
                    default:
                        switch (_expression[i])
                        {
                            case '(':
                            case ')':
                                return;

                            case '+':
                                _result += Convert.ToInt32(_expression[i + 1].ToString());
                                _idEnd = _idEnd + 1;
                                Console.WriteLine($@"result of count {_result}");
                                break;
                            case '-':
                                _result -= Convert.ToInt32(_expression[i + 1].ToString());
                                _idEnd = _idEnd + 1;
                                Console.WriteLine($@"result of count {_result}");
                                break;
                            case '*':
                                _result *= Convert.ToInt32(_expression[i + 1].ToString());
                                _idEnd = _idEnd + 1;
                                Console.WriteLine($@"result of count {_result}");
                                break;
                            case '/':
                                _result /= Convert.ToInt32(_expression[i + 1].ToString());
                                _idEnd = _idEnd + 1;
                                Console.WriteLine($@"result of count {_result}");
                                break;
                        }

                        break;
                }


            Count();

            Console.WriteLine($@"result of count {_result}");
        }

        public bool ToChecksymbolsInsideBrackets()
        {
            var c = 0;
            for (var i = 0;
                i < _expression.Length;
                i++)
                if (_expression[i] == '(')
                {
                    Console.WriteLine($@"{_expression[i]} c = {c}");
                    for (var t = i + 1; t < _expression.Length; t++)
                    {
                        c++;
                        Console.WriteLine($@"{_expression[t]} c = {c}");
                        if (_expression[t] == ')') return c > 5;
                    }
                }

            return false;
        }
    }
}