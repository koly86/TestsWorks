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
        private readonly char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        public CountingExpression(char[] expression)
        {
            _expression = expression;
            Count();
        }

        private void Count()
        {
            for (_id = _idEnd; _id < _expression.Length; _id++)
                switch (_expression[_id])
                {
                    case '(':
                        _idStart = _id + 2; //прибавляем цифру и скобку
                        foreach (var n in numbers)
                            if (_expression[_id + 1] == n)
                                for (_id = ++_id; _id < _expression.Length; _id++)
                                    if (_expression[_id] == ')')
                                    {
                                        _idEnd = _id;
                                        Console.WriteLine(CountingInsideBrackets(_callCount++));
                                        return;
                                    }


                        break;

                    case ')':
                        _idEnd = _idEnd + 1;
                        Count();
                        break;

                        break;
                    default:
                        foreach (var n in numbers)
                            if (_expression[_id] == n)
                            {
                            }

                        break;
                }
        }


        private int CountingInsideBrackets(byte call)
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
                                return 0;

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

            return _result;
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