using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace TreeAST
{
    public class SyntaxTree
    {
        public const string DEFAULT_WHITESPACES = " \n\r\t";


        public SyntaxTree(string source)
        {
            Source = source;
        }

        public string Source { get; }

        public int Pos { get; private set; }


        protected char this[int index] => index < Source.Length ? Source[index] : (char) 0;


        public char Current => this[Pos];


        public bool End => Current == 0;


        public void Next()
        {
            if (!End)
                Pos++;
        }


        public virtual void Skip()
        {
            while (DEFAULT_WHITESPACES.IndexOf(this[Pos]) >= 0)
                Next();
        }


        protected string MatchNoExcept(params string[] terms)
        {
            var pos = Pos;
            foreach (var s in terms)
            {
                var match = true;
                foreach (var c in s)
                    if (Current == c)
                    {
                        Next();
                    }
                    else
                    {
                        Pos = pos;
                        match = false;
                        break;
                    }

                if (!match) continue;
                Skip();
                return s;
            }

            return null;
        }


        public bool IsMatch(params string[] terms)
        {
            var pos = Pos;
            var result = MatchNoExcept(terms);
            Pos = pos;
            return result != null;
        }


        public string Match(params string[] terms)
        {
            var pos = Pos;
            var result = MatchNoExcept(terms);
            if (result == null)
            {
                var message = "String: ";
                var first = true;
                foreach (var s in terms)
                {
                    if (!first)
                        message += ", ";
                    message += $"\"{s}\"";
                    first = false;
                }

                throw new ParserBaseException($"{message} (pos={pos})");
            }

            return result;
        }


        public string Match(string s)
        {
            var pos = Pos;
            try
            {
                return Match(new[] {s});
            }
            catch
            {
                throw new ParserBaseException(
                    string.Format($"{0}: '{1}' (pos={2})", s.Length == 1 ? "Symbol" : "Line", s,
                        pos));
            }
        }


        public class MathExprIntepreter : SyntaxTree
        {
            public static readonly NumberFormatInfo NFI = new NumberFormatInfo();


            public MathExprIntepreter(string source) : base(source)
            {
            }

            public double NUMBER()
            {
                var number = "";
                while (Current == '.' || char.IsDigit(Current))
                {
                    number += Current;
                    Next();
                }

                if (number.Length == 0)
                    throw new ParserBaseException($"Waiting for a number (pos={Pos})");
                Skip();
                return double.Parse(number, NFI);
            }

            public double Group()
            {
                if (!IsMatch("(")) return NUMBER();
                Match("(");
                var result = Add();
                Match(")");
                return result;
            }

            public double Mult()
            {
                var result = Group();
                while (IsMatch("*", "/"))
                {
                    var oper = Match("*", "/");
                    var temp = Group();
                    result = oper == "*"
                        ? result * temp
                        : result / temp;
                }

                return result;
            }


            public double Add()
            {
                var result = Mult();
                while (IsMatch("+", "-"))
                {
                    var oper = Match("+", "-");
                    var temp = Mult();
                    result = oper == "+"
                        ? result + temp
                        : result - temp;
                }

                return result;
            }

            public double Result()
            {
                return Add();
            }


            public double Execute()
            {
                Skip();
                var result = Result();
                if (End)
                    return result;
                throw new ParserBaseException($"Wrong symbol '{Current}' (pos={Pos})");
            }


            public static double Execute(string source)
            {
                var mei = new MathExprIntepreter(source);
                return mei.Execute();
            }
        }
    }

    public class ParserBaseException : Exception
    {
        public ParserBaseException(string format)
        {
            Console.WriteLine(format);
        }
    }
}