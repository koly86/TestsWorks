using System;

namespace MyVersion
{
    public class CountingExpression
    {
        private readonly char[] _expression;
        private int resultinBrackets;

        public CountingExpression(char[] expression)
        {
            _expression = expression;
            Count();
        }

        private void Count()
        {
            var c = 0;
            for (var i = 0; i < _expression.Length; i++)
                if (_expression[i] == '(')
                {
                    switch (_expression[i + 2]) //прибавляем к скобке 2 знака, саму скобку и число
                    {
                        case '+':
                           
                            c += Convert.ToInt32(_expression[i + 1].ToString()) +Convert.ToInt32(_expression[i + 3].ToString());
                            Console.WriteLine($@" - 1 {_expression[i + 1]}  + {_expression[i + 3]}= {c}");
                            break;
                        case '-':
                            c += _expression[i + 1] - _expression[i + 3];
                            break;
                        case '/':
                            c += _expression[i + 1] / _expression[i + 3];
                            break;
                        case '*':
                            c += _expression[i + 1] * _expression[i + 3];
                            break;


                        default:
                            break;
                    }

                    if (ToChecksymbolsInsideBrackets())
                    {
                        do
                        {
                            switch (_expression[i])
                            {
                                case '+':
                                    c += _expression[i + 1];
                                    break;
                                case '-':
                                    c -= _expression[i + 1];
                                    break;
                                case '/':
                                    c /= _expression[i + 1];
                                    break;
                                case '*':
                                    c *= _expression[i + 1];
                                    break;

                                default:
                                    break;
                            }

                            i++;
                        } while (_expression[i] != ')');

                        Console.WriteLine($@"in brackets more than 1 operator {c}");
                    }
                    else
                        Console.WriteLine($@"in brackets 1 operator = {c}");
                }
        }

        public bool ToChecksymbolsInsideBrackets()
        {
            var c = 0;
            for (var i = 0; i < _expression.Length; i++)
                if (_expression[i] == '(')
                {
                    Console.WriteLine($@"{_expression[i]} i = {i}");
                    for (var t = i + 1; t < _expression.Length; t++)
                    {
                        Console.WriteLine($@"{_expression[t]} t = {t}");
                        if (_expression[t] == ')')
                            if (t > 5) // 5 - количество знаков в скобках, если в скобках больше знаков
                                return true;
                            else
                                return false;
                    }
                }

            return false;
        }
    }
}