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
                            //преобразуем в стринг и потом в инт, что бы сложить 2 числа
                            c += Convert.ToInt32(_expression[i + 1].ToString()) +
                                 Convert.ToInt32(_expression[i + 3].ToString());
                             break;
                        case '-':
                            c += Convert.ToInt32(_expression[i + 1].ToString()) -
                                 Convert.ToInt32(_expression[i + 3].ToString());
                            break;
                        case '/':
                            c += Convert.ToInt32(_expression[i + 1].ToString()) /
                                 Convert.ToInt32(_expression[i + 3].ToString());
                            break;
                        case '*':
                            c += Convert.ToInt32(_expression[i + 1].ToString()) *
                                 Convert.ToInt32(_expression[i + 3].ToString());

                            break;


                        default:
                            break;
                    }

                    if (ToChecksymbolsInsideBrackets())
                    {
                        i += 4;
                        do
                        {
                            switch (_expression[i])
                            {
                                case '+':
                                    c += Convert.ToInt32(_expression[i + 1].ToString());
                                    break;
                                case '-':
                                    c -= Convert.ToInt32(_expression[i + 1].ToString());
                                    break;
                                case '/':
                                    c /= Convert.ToInt32(_expression[i + 1].ToString());
                                    break;
                                case '*':
                                    c *= Convert.ToInt32(_expression[i + 1].ToString());
                                    break;

                                default:
                                    break;
                            }

                            i++;
                        } while (_expression[i] != ')');

                        Console.WriteLine($@"in brackets more than 1 operator {c}");
                    }
                    else
                    {
                        Console.WriteLine($@"in brackets 1 operator = {c}");
                    }
                }
        }

        public bool ToChecksymbolsInsideBrackets()
        {
            var c = 0;
            for (var i = 0; i < _expression.Length; i++)
                if (_expression[i] == '(')
                {
                    
                    Console.WriteLine($@"{_expression[i]} c = {c}");
                    for (var t = i + 1; t < _expression.Length; t++)
                    {
                        c++;
                        Console.WriteLine($@"{_expression[t]} c = {c}");
                        if (_expression[t] == ')')
                        {
                            
                            return c > 5;
                        }
                    }

                }

            return false;
        }
    }
}