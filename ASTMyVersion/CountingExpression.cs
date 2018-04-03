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
            for (_id = _idEnd; _id < _expression.Length; _id++)
                switch (_expression[_id])
                {
                    case '(':
                        //если сначала идет скобка, проверяем что после скобки идет цифра и после
                        //цифры идет знак и после знака цифра, тогда считаем
                        for (_id = ++_id; _id < _expression.Length; _id++)
                            switch (_expression[_id])
                            {
                                //проверяем что после скобки идет скобка
                                case '(':


                                    break;
                                //проверяем что после скобки идет цифра
                                case char n when n >= '0' && n <= '9':
                                   
                                    //если идет цира проверяем что после циры идет знак
                                    for (_id = ++_id; _id < _expression.Length; _id++)
                                        switch (_expression[_id])
                                        {
                                            case char g when g == '+' || g == '-' || g == '*' || g == '/':
                                                //если идет знак проверяем что после знака идет цифра
                                                for (_id = ++_id; _id < _expression.Length; _id++)
                                                    switch (_expression[_id])
                                                    {
                                                        //если идет цифра тогда выполняем вычесление
                                                        case char u when u >= '0' && n <= '9':
                                                            switch (g)
                                                            {
                                                                case '+':
                                                                    _result += Convert.ToInt32(_expression[_id - 2]
                                                                                   .ToString()) +
                                                                               Convert.ToInt32(_expression[_id]
                                                                                   .ToString());
                                                                    _idEnd = _id + 1;
                                                                    Count();

                                                                    break;
                                                                case '-':
                                                                    _result += Convert.ToInt32(_expression[_id - 2]
                                                                                   .ToString()) -
                                                                               Convert.ToInt32(_expression[_id]
                                                                                   .ToString());
                                                                    _idEnd = _id + 1;
                                                                    Count();
                                                                    break;
                                                                case '/':
                                                                    _result += Convert.ToInt32(_expression[_id - 2]
                                                                                   .ToString()) /
                                                                               Convert.ToInt32(_expression[_id]
                                                                                   .ToString());
                                                                    _idEnd = _id + 1;
                                                                    Count();
                                                                    break;
                                                                case '*':
                                                                    _result += Convert.ToInt32(_expression[_id - 2]
                                                                                   .ToString()) *
                                                                               Convert.ToInt32(_expression[_id]
                                                                                   .ToString());
                                                                    _idEnd = _id + 1;
                                                                    Count();
                                                                    break;
                                                            }

                                                            break;
                                                    }

                                                break;
                                            case '(':
                                                //если идет скобка перевызов функции
                                                Count();
                                                break;
                                        }
                                    break;
                            }


                        break;

                    case ')':
                        _idEnd = _id + 1;
                        Count();
                        break;
                    case char g when g == '+' || g == '-' || g == '*' || g == '/':
                        switch (g)
                        {
                            case '+':
                                _result += _expression[_id + 1];
                                _idEnd = _id + 2;
                                Count();
                                break;
                            case '-':
                                _result -= _expression[_id + 1];
                                _idEnd = _id + 2;
                                Count();
                                break;
                            case '/':
                                _result /= _expression[_id + 1];
                                _idEnd = _id + 2;
                                Count();
                                break;
                            case '*':
                                _result *= _expression[_id + 1];
                                _idEnd = _id + 2;
                                Count();
                                break;
                        }
                        break;
                }
            //(_expression[_id] != '(' && _expression[_id] != ')')
            //        for (; _id < _expression.Length; _id++)
            //        {
            //            _idStart = _id + 1; // прибавляем только цифру
            //            for (_id = ++_id; _id < _expression.Length; _id++)
            //                if (_expression[_id] == '(' || _expression[_id] == ')')
            //                {
            //                    _idEnd = _id;
            //                    CountingInsideBrackets(_callCount++);
            //                }
            //                else
            //                {
            //                    _idEnd = _id;
            //                }
            //        }

            //if (_expression[_id] == '(')
            //    FindBrackets();
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