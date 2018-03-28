using System.Collections.Generic;

namespace ASTNode
{
    public class AstNode
    {
        public virtual int Type { get; set; }

        // текст, связанный с узлом
        public virtual string Text { get; set; }

        // родительский узел для данного узла дерева
        private AstNode parent;

        // потомки (ветви) данного узла дерева
        private readonly IList<AstNode> childs = new List<AstNode>();

        // конструкторы с различными параметрами (для удобства
        public AstNode(int type, string text,
            AstNode child1, AstNode child2)
        {
            Type = type;
            Text = text;
            if (child1 != null)
                AddChild(child1);
            if (child2 != null)
                AddChild(child2);
        }

        public AstNode(int type, AstNode child1, AstNode child2)
            : this(type, null, child1, child2)
        {
        }

        public AstNode(int type, AstNode child1)
            : this(type, child1, null)
        {
        }

        public AstNode(int type, string label)
            : this(type, label, null, null)
        {
        }

        public AstNode(int type)
            : this(type, (string) null)
        {
        }


        // метод добавления дочернего узла
        public void AddChild(AstNode child)
        {
            if (child.Parent != null) child.Parent.childs.Remove(child);

            childs.Remove(child);
            childs.Add(child);
            child.parent = this;
        }

        // метод удаления дочернего узла
        public void RemoveChild(AstNode child)
        {
            childs.Remove(child);
            if (child.parent == this)
                child.parent = null;
        }

        // метод получения дочернего узла по индексу
        public AstNode GetChild(int index)
        {
            return childs[index];
        }

        // метод добавления дочернего узла
        public int ChildCount => childs.Count;


        public AstNode Parent
        {
            get => parent;
            set => value.AddChild(this);
        }

        // индекс данного узла в дочерних узлах родительского узла
        public int Index => Parent == null
            ? -1
            : Parent.childs.IndexOf(this);

        // представление узла в виде строки
        public override string ToString()
        {
            return Text != null
                ? Text
                : AstNodeType.AstNodeTypeToString(Type);
        }
    }

    public class AstNodeType
    {
        public const int UNKNOWN = 0;
        public const int NUMBER = 1;
        public const int IDENT = 5;
        public const int ADD = 11;
        public const int SUB = 12;
        public const int MUL = 13;
        public const int DIV = 14;
        public const int ASSIGN = 51;
        public const int INPUT = 52;
        public const int PRINT = 53;
        public const int BLOCK = 100;
        public const int PROGRAM = 101;

        public static string AstNodeTypeToString(int type)
        {
            switch (type)
            {
                case UNKNOWN: return "?";
                case NUMBER: return "NUM";
                case IDENT: return "ID";
                case ADD: return "+";
                case SUB: return "-";
                case MUL: return "*";
                case DIV: return "/";
                case ASSIGN: return "=";
                case INPUT: return "input";
                case PRINT: return "print";
                case BLOCK: return "..";
                case PROGRAM: return "program";
                default: return "";
            }
        }
    }

    public class MathLangParser : ParserBase
    {
        // конструктор
        public MathLangParser(string source)
            : base(source)
        {
        }

        // далее идет реализация в виде функций правил грамматики
        // NUMBER -> <число>
        public AstNode NUMBER()
        {
            var number = "";
            while (Current == '.' || char.IsDigit(Current)) number += Current;

            Next();

            if (number.Length == 0)
                throw new ParserBaseException(
                    string.Format("Ожидалось число (pos={0})", Pos));

            Skip();
            return new AstNode(AstNodeType.NUMBER, number);
        }

        // IDENT -> <идентификатор>
        public AstNode IDENT()
        {
            var identifier = "";
            if (char.IsLetter(Current))
            {
                identifier += Current;
                Next();
                while (char.IsLetterOrDigit(Current))
                {
                    identifier += Current;
                    Next();
                }
            }
            else
            {
                throw new ParserBaseException(
                    string.Format("Ожидался идентификатор (pos={0})", Pos));
            }

            Skip();
            return new AstNode(AstNodeType.IDENT, identifier);
        }

        // group -> "(" term ")" | IDENT | NUMBER
        public AstNode Group()
        {
            if (IsMatch("("))
            {
                // выбираем альтернативу
                Match("("); // это выражение в скобках
                var result = Term();
                Match(")");
                return result;
            }

            if (char.IsLetter(Current))
            {
                var pos = Pos; // это идентификатор
                return IDENT();
            }

            return NUMBER(); // число
        }

        public AstNode Term()
        {
            return Add();
        }

        public AstNode Add()
        {
            // реализация аналогично правилу mult
            var result = Mult();
            while (IsMatch("+", "-"))
            {
                var oper = Match("+", "-");
                var temp = Mult();
                result =
                    oper == "+"
                        ? new AstNode(AstNodeType.ADD, result, temp)
                        : new AstNode(AstNodeType.SUB, result, temp);
            }

            return result;
        }

        public AstNode Mult()
        {
            var result = Group();
            while (IsMatch("*", "/"))
            {
                // повторяем нужное кол-во раз
            }

            var oper = Match("*", "/"); // здесь выбор альтернативы
            var temp = Group(); // реализован иначе
            result =
                oper == "*"
                    ? new AstNode(AstNodeType.MUL, result, temp)
                    : new AstNode(AstNodeType.DIV, result, temp);
            return result;
        }

        // expr -> "print" term | "input" IDENT | IDENT "=" term
        public AstNode Expr()
        {
            if (IsMatch("print"))
            {
                // выбираем альтернативу
                Match("print"); // это вывод данных
                var value = Term();
                return new AstNode(AstNodeType.PRINT, value);
            }

            if (IsMatch("input"))
            {
                Match("input"); // это ввод данных
                var identifier = IDENT();
                return new AstNode(AstNodeType.INPUT, identifier);
            }
            else
            {
                var identifier = IDENT();
                Match("="); // это операция присвоения значения
                var value = Term();
                return new AstNode(AstNodeType.ASSIGN, identifier, value);
            }
        }

        // program -> ( expr )*
        public AstNode Program()
        {
            var programNode = new AstNode(AstNodeType.PROGRAM);
            while (!End) // повторяем до конца входной строки
                programNode.AddChild(Expr());
            return programNode;
        }

        // result -> program
        public AstNode Result()
        {
            return Program();
        }

        public AstNode Parse()
        {
            Skip();
            var result = Result();
            if (End)
                return result;
            throw new ParserBaseException( // разобрали не всю строку
                string.Format("Лишний символ '{0}' (pos={1})",
                    Current, Pos)
            );
        }

        // статическая реализации предыдузего метода (для удобства)
        public static AstNode Parse(string source)
        {
            var mlp = new MathLangParser(source);
            return mlp.Parse();
        }
    }
}