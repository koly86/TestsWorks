using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASTNode
{
    class Program
    {
        static void Main(string[] args)
        {
            AstNode astNode = new AstNode(4,"1111",new AstNode(5),new AstNode(123));
            Console.WriteLine(astNode.GetChild(1));
            Console.ReadKey();

        }
    }
}
