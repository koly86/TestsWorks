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
                            Console.WriteLine($@" - 1 {_expression[i + 1]}  + {_expression[i + 3]}");
                            c += _expression[i + 1] + _expression[i + 3];
                            break;
                        case '-':
                            c += _expression[i - 1] - _expression[i + 1];
                            break;
                        case '/':
                            c += _expression[i - 1] / _expression[i + 1];
                            break;
                        case '*':
                            c += _expression[i - 1] * _expression[i + 1];
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
                    }
                    else
                    {
                        Console.WriteLine($@"answer in brackets = {c}");
                    }
                }


            Console.WriteLine($@"answer in brackets = {c}");
        }

        public bool ToChecksymbolsInsideBrackets()
        {
            var c = 0;
            foreach (var t in _expression)
                if (t == '(')
                {

                }

                   


            return false;
        }
    }
}