using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParseStringCalculator
{
    public class ParseStringCalculatorController (string expression)
    {
        private string[] _expression = expression.Split();

        public string Calculate()
        {
            var expressionBlocks = expression.Split(' ');

            var num1 = double.Parse(expressionBlocks[0]);
            var num2 = double.Parse(expressionBlocks[2]);
            var oper = expressionBlocks[1];

            switch (oper)
            {
                case "+":
                    return (num1 + num2).ToString();
                default:
                    throw new ArgumentException("Unknown operation");
            }
        }
    }
}


