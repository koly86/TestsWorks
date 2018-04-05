using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVersion
{
    
    public class StringVer
    {
        private string _expression { get; set; }
        public StringVer(string expression)
        {
            this._expression = expression;
        }

        private void Count()
        {
            for (int i = 0; i < _expression.Length; i++)
            {
                var f = _expression.IndexOf("+", 0, StringComparison.Ordinal);
                switch (f)
                {
                    case 4: break;
                }
            }
        }
    }
}
